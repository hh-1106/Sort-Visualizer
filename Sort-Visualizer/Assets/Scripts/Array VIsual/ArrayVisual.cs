using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class ArrayVisual : MonoBehaviour
{

    [Title("显示区域")]
    public float pannelWidth;
    public float pannelHeight;

    [Title("数组元素")]
    public GameObject arrayElementPrefab;

    [Range(0, 1)]
    public float strokeWidth;

    [Title("数组长度")]
    [OnValueChanged("Refresh")]
    public int n;
    [HideInInspector]
    public int[] A; // 数组
    int[] states;   // 数组每个元素的状态

    [Title("配色版")]
    public Color[] palette;

    public int this[int i]
    {
        get
        {
            return A[i];
        }
        set
        {
            A[i] = value;
        }
    }

    public int[] States
    {
        get
        {
            return states;
        }
        set
        {
            states = value;
        }
    }

    private void Awake()
    {
        Refresh();
    }

    private void Update()
    {
        UpdateObjs();
    }

    public void Swap(int i, int j)
    {
        int temp = A[i];
        A[i] = A[j];
        A[j] = temp;
    }


    void UpdateObjs()
    {
        bool needReset = transform.childCount == 0;

        for (int i = 0; i < n; i++)
        {
            float x = Mathf.Lerp(-pannelWidth / 2f, pannelWidth / 2f, (i + .5f) / (float)n);
            float h = Mathf.Lerp(0, pannelHeight, A[i] / (float)n);

            GameObject e;

            if (needReset)
            {
                e = Instantiate(arrayElementPrefab);
                e.transform.parent = transform;
                e.transform.position = new Vector3(x, 0, 0);
            }
            else
            {
                e = transform.GetChild(i).gameObject;
            }

            e.transform.localScale = new Vector3(strokeWidth, h, 0);
            e.GetComponent<SpriteRenderer>().color = palette[States[i]];
        }
    }

    void Refresh()
    {
        if (!Application.isPlaying) return;

        A = new int[n];
        states = new int[n];

        for (int i = 0; i < n; i++)
        {
            A[i] = n - i;
            states[i] = 0;
        }

        UpdateObjs();
    }

}
