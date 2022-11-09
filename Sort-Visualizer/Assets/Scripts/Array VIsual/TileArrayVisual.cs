using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


public class TileArrayVisual : ArrayVisual
{
    [Title("显示区域")]
    [OnValueChanged("Refresh")]
    public float pannelWidth;
    [OnValueChanged("Refresh")]
    public float pannelHeight;

    [Title("迭代")]
    [ShowInInspector]
    [ReadOnly]
    int iterations = 1;
    public float iterationDelay = 0.2f;
    float lastIterationTime = 0;

    protected override void ClearVisual()
    {
        DestoryChildren();
        iterations = 1;
        lastIterationTime = 0;
    }

    protected override void UpdateObjs()
    {
        if (!Application.isPlaying)
        {
            CheckChildCount();
            iterations = 1;
        }
        else
        {
            if (lastIterationTime + iterationDelay < Time.time)
            {
                GenerateChildren();
                iterations++;
                lastIterationTime = Time.time;
            }
        }

        float w = pannelWidth / (float)n;
        float h = pannelHeight / (float)iterations;
        // h = Mathf.Min(h, 1);
        int tn = transform.childCount;

        for (int i = 0; i < tn; i++)
        {
            int k = (int)Mathf.Floor(i / n);
            int j = i % n;
            var e = transform.GetChild(i).gameObject;

            float x = Mathf.Lerp(-pannelWidth / 2f, pannelWidth / 2f, (j + .5f) / (float)n);
            float y = Mathf.Lerp(pannelHeight / 2f, -pannelHeight / 2f, (k + 0.5f) / (float)iterations);
            float a = A[j] / (float)n;

            e.transform.position = new Vector3(x, y, 0);
            e.transform.localScale = new Vector3(w, h, 1);

            if (k == iterations - 2 || !Application.isPlaying)
            {
                Color col = palette[States[j]];
                col.a = a;
                e.GetComponent<SpriteRenderer>().color = col;
            }
        }
    }
}
