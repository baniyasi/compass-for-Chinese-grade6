using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class dial : MonoBehaviour
{
    [SerializeField] GameObject scalePrefab;
    [SerializeField] GameObject dirNamePrefab;
    float radius = 0.28f;
    
    int numberOfScales = 72;
    string[] directName = { "<color=#e84026ff>北</color>", "东", "南", "西" };
    string[] angleName0 = { "<color=#e84026ff>0</color>", "30", "60", "90", "60", "30", "0", "30", "60", "90", "60", "30" };
    string[] angleName1 = { "90", "60", "30", "0", "30", "60", "90", "60", "30","0","30","60" };
    float dirNameRadius = 0.6f;
    float OuterRadius = 1.19f;
    float InnerRadius = 0.82f;
    // Start is called before the first frame update
    void Start()
    {
        GenerateScales();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void GenerateScales()
    {
        float angleStep = 360f / numberOfScales;
        for (int i = 0; i < numberOfScales; i++)
        {
            // 计算当前刻度的角度（从0度开始，顺时针）
            float angle = i * angleStep;

            // 实例化刻度预制体
            GameObject scaleInstance = Instantiate(scalePrefab, Vector3.zero, Quaternion.identity, transform);
            scaleInstance.transform.localRotation = Quaternion.Euler(0f, 0f, -angle);
            float x = radius * Mathf.Sin(angle * Mathf.Deg2Rad);
            float y = radius * Mathf.Cos(angle * Mathf.Deg2Rad); // 假设表盘在XZ平面
            scaleInstance.transform.localPosition = new Vector3(x, y, 0f); // 如果你的表盘在XY平面，调整y和z

            if (i % 6 == 0)
            {
                scaleInstance.transform.localScale = new Vector3(0.005f, 0.03f, 0f);
                GameObject dirNameInstance0 = Instantiate(dirNamePrefab, Vector3.zero, Quaternion.identity, transform);
                dirNameInstance0.transform.localRotation = Quaternion.Euler(0f, 0f, -angle);
                dirNameInstance0.transform.localPosition = new Vector3(x * InnerRadius, y * InnerRadius, 0f); // 如果你的表盘在XY平面，调整y和z
                Text dirNameText0 = dirNameInstance0.transform.GetChild(0).GetComponent<Text>();
                dirNameText0.text = angleName0[i / 6];
                dirNameText0.fontSize = 15;
                GameObject dirNameInstance1 = Instantiate(dirNamePrefab, Vector3.zero, Quaternion.identity, transform);
                dirNameInstance1.transform.localRotation = Quaternion.Euler(0f, 0f, -angle);
                dirNameInstance1.transform.localPosition = new Vector3(x * OuterRadius, y * OuterRadius, 0f); // 如果你的表盘在XY平面，调整y和z
                Text dirNameText1 = dirNameInstance1.transform.GetChild(0).GetComponent<Text>();
                dirNameText1.text = angleName1[i / 6];
                dirNameText1.fontSize = 15;
            }
            if (i % 18 == 0)
            {
                GameObject dirNameInstance = Instantiate(dirNamePrefab, Vector3.zero, Quaternion.identity, transform);
                dirNameInstance.transform.localRotation = Quaternion.Euler(0f, 0f, -angle);
                dirNameInstance.transform.localPosition = new Vector3(x * dirNameRadius, y * dirNameRadius, 0f); // 如果你的表盘在XY平面，调整y和z

                Text dirNameText = dirNameInstance.transform.GetChild(0).GetComponent<Text>();
                dirNameText.text = directName[i / 18];
            }
            // Debug.Log(scaleInstance.transform.position.x);
            // 计算刻度在圆上的位置 (使用圆参数方程)


            // 让刻度朝向圆心（如果需要）
            // 计算朝向圆心的旋转（旋转180度是因为默认方向可能不对）
            //scaleInstance.transform.LookAt(container.transform.position);
            // 根据你的预制体初始朝向，可能需要在Y轴或其他轴上额外旋转180度或调整
            //scaleInstance.transform.Rotate(0f, 180f, 0f); // 示例调整

            // 或者，如果你的刻度预制体本身方向正确，只需绕Y轴旋转一个角度使其径向排列
            // scaleInstance.transform.localRotation = Quaternion.Euler(0f, angle, 0f);
        }
    }
}
