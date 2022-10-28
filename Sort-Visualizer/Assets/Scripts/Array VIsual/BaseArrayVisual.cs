using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class BaseArrayVisual : MonoBehaviour
{

    [Title("显示区域")]
    public float pannelWidth;
    public float pannelHeight;

    [Title("数组"), LabelText("元素")]
    public GameObject arrayElementPrefab;

    [LabelText("宽度")]
    [Range(0, 1)]
    public float strokeWidth;

    [LabelText("数量")]
    [DisableInPlayMode]
    public int n;
    [HideInInspector]
    public int[] A; // 数组
    int[] states;   // 数组每个元素的状态

    [Title("配色板")]
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

    public void InitArray()
    {
        Debug.Log("initArray");

        A = new int[n];
        states = new int[n];

        for (int i = 0; i < n; i++)
        {
            A[i] = n - i;
            states[i] = 0;
        }
    }

    public void UpdateObjs()
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

    public void Refresh()
    {
        //? https://docs.unity3d.com/ScriptReference/Application-isPlaying.html 
        if (!Application.isPlaying) return;

        InitArray();
        UpdateObjs();
    }
}
