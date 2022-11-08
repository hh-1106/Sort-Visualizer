using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class PolarArrayVisual : ArrayVisual
{

    [Title("Polar")]
    [OnValueChanged("Refresh")]
    public float innerRadius;
    [OnValueChanged("Refresh")]
    public float outerRadius;

    protected override void UpdateObjs()
    {
        // 生成所有待排序的物体
        CheckChildCount();

        // 修改待排序物体的物理状态
        for (int i = 0; i < n; i++)
        {
            var e = transform.GetChild(i).gameObject;

            float angle = Mathf.Lerp(0, 360, i / (float)n);

            float h = Mathf.Lerp(0, outerRadius / 2f, A[i] / (float)n);

            float x = Mathf.Cos(angle * Mathf.Deg2Rad) * (h + innerRadius) / 2f;
            float y = Mathf.Sin(angle * Mathf.Deg2Rad) * (h + innerRadius) / 2f;

            e.transform.localEulerAngles = new Vector3(0, 0, angle + 90f);
            e.transform.localPosition = new Vector3(x, y, 0);
            e.transform.localScale = new Vector3(strokeWidth, h, 0);

            e.GetComponent<SpriteRenderer>().color = palette[States[i]];
        }
    }
}
