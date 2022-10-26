using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


public class SortAnim : MonoBehaviour
{
    [LabelText("渲染算法"), Required]
    public ArrayVisual av;
    SortAlgorithm sa;

    [Title("排序算法"), EnumToggleButtons, HideLabel]
    public SortAlogorithmEnum SAEnum;

    [LabelText("时间步长"), Range(0, 0.1f)]
    public float delay;

    private void Start()
    {
        Application.targetFrameRate = 100;


        sa = SAEnum switch
        {
            SortAlogorithmEnum.Bubble => new BubbleSort(),
            SortAlogorithmEnum.Heap => new HeapSort(),
            SortAlogorithmEnum.Shell => new HeapSort(),
            _ => new Shuffle(),
        };

        StartCoroutine(sa.Sort(av, delay));
    }

    private void Update()
    {
    }
}


public enum SortAlogorithmEnum
{
    Shuffle,
    Bubble,
    Heap,
    Shell,
}
