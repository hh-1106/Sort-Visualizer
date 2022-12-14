using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class ArrayVisual : MonoBehaviour
{

    [Title("数组"), LabelText("元素")]
    [OnValueChanged("Refresh")]
    public GameObject arrayElementPrefab;

    [LabelText("宽度")]
    [Range(0, 1)]
    [OnValueChanged("Refresh")]
    public float strokeWidth;

    [LabelText("数量")]
    [Range(0, 128)]
    [DisableInPlayMode]
    [OnValueChanged("Refresh")]
    public int n;

    [ReadOnly]
    public bool sorted = false;


    [HideInInspector]
    public int[] A; // 数组
    protected int[] states;   // 数组每个元素的状态

    [Title("配色板")]
    [OnValueChanged("Refresh")]
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
        if (!sorted)
        {
            UpdateObjs();
        }
    }

    public void Swap(int i, int j)
    {
        int temp = A[i];
        A[i] = A[j];
        A[j] = temp;
    }

    protected virtual void InitArray()
    {
        A = new int[n];
        states = new int[n];

        for (int i = 0; i < n; i++)
        {
            // A[i] = n - i;
            A[i] = i + 1;
            states[i] = 0;
        }
    }

    public void CheckChildCount()
    {
        if (transform.childCount != n)
        {
            DestoryChildren();
            GenerateChildren();
        }
    }

    protected virtual void UpdateObjs()
    {
    }

    public void Refresh()
    {
        Clear();
        InitArray();
        UpdateObjs();
    }

    void Clear()
    {
        A = null;
        states = null;
        ClearVisual();
    }

    protected virtual void ClearVisual()
    {
        DestoryChildren();
    }

    protected void DestoryChildren()
    {
        while (transform.childCount > 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
    }

    protected void GenerateChildren()
    {
        for (int i = 0; i < n; i++)
        {
            GameObject e = Instantiate(arrayElementPrefab) as GameObject;
            e.transform.parent = transform;
        }
    }
}
