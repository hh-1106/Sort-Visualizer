using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class TriangleArrayVisual : ArrayVisual
{

    [Title("显示区域")]
    public float pannelWidth;
    public float pannelHeight;

    public override void UpdateObjs()
    {
        // 生成所有待排序的物体
        if (transform.childCount != n)
        {
            for (int i = 0; i < n; i++)
            {
                GameObject e = Instantiate(arrayElementPrefab) as GameObject;
                e.transform.parent = transform;
            }
        }

        // 修改待排序物体的物理状态
        for (int i = 0; i < n; i++)
        {
            var e = transform.GetChild(i).gameObject;

            float x = Mathf.Lerp(-pannelWidth / 2f, pannelWidth / 2f, (i + .5f) / (float)n);
            float h = Mathf.Lerp(0, pannelHeight, A[i] / (float)n);

            e.transform.position = new Vector3(x, 0, 0);
            e.transform.localScale = new Vector3(strokeWidth, h, 0);
            e.GetComponent<SpriteRenderer>().color = palette[States[i]];
        }
    }

}
