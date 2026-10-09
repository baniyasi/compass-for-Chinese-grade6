using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class compass : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform dial;
    [SerializeField] Text dirText;
    public float smoothTime = 0.3f;
    public float maxSpeed = 120f; // 度/秒
    public float updateFrequency = 0.1f; // 更新频率(秒)
    
    private float currentHeading = 0f;
    private float targetHeading = 0f;
    private float currentVelocity = 0f;
    private float lastUpdateTime = 0f;

    int currentangle;
    int offsetangle;
    string[] directName = { "北", "东", "南", "西" };
    string text1;
    string text2;
    void Start()
    {
        Input.compass.enabled = true;
        Input.location.Start();
        currentHeading = targetHeading = Input.compass.trueHeading;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastUpdateTime < updateFrequency) return;
        lastUpdateTime = Time.time;
        
        // 获取目标方向
        targetHeading = Input.compass.trueHeading;
        
        // 处理角度环绕
        if (Mathf.Abs(targetHeading - currentHeading) > 180f)
        {
            if (targetHeading > currentHeading)
                currentHeading += 360f;
            else
                targetHeading += 360f;
        }
        
        // 平滑过渡
        currentHeading = Mathf.SmoothDampAngle(
            currentHeading, 
            targetHeading, 
            ref currentVelocity, 
            smoothTime, 
            maxSpeed
        );
        
        // 确保角度在0-360范围内
        currentHeading = (currentHeading % 360 + 360) % 360;

        currentangle = Mathf.RoundToInt(currentHeading);
        offsetangle = currentangle % 90;
        if (offsetangle == 0)
        {
            dirText.text = "正" + directName[currentangle / 90];
        }
        else
        {
            text1 = directName[currentangle / 90] + "偏" + directName[(currentangle / 90 + 1) % 4] + offsetangle.ToString() + "°";
            text2 = directName[(currentangle / 90 + 1) % 4] + "偏" + directName[currentangle / 90] + (90 - offsetangle).ToString() + "°";
            if (offsetangle < 45)
            {
                dirText.text = text1 + " 或 " + text2;
            }
            else
            {
                dirText.text = text2 + " 或 " + text1;
            }
        }


        
        // 应用旋转
        dial.localRotation = Quaternion.Euler(0f, 0f, currentHeading);
    }

}
