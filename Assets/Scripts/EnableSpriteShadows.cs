using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableSpriteShadows : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.receiveShadows = true; // 允许接收其他物体投射的阴影
        renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On; // 允许投射阴影
    }
}
