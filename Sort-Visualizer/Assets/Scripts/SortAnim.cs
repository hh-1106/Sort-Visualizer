using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


public class SortAnim : MonoBehaviour
{
    public ArrayVisual av;
    SortAlgorithm sa;

    [Title("排序算法"), EnumToggleButtons, HideLabel]
    public SortAlogorithmEnum SAEnum;

    [LabelText("时间步长"), Range(0, 0.2f)]
    public float delay;

    private void Start()
    {
        Application.targetFrameRate = 60;


        sa = SAEnum switch
        {
            SortAlogorithmEnum.Shuffle => new Shuffle(),
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
}
