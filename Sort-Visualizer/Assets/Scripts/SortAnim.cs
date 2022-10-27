using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


public class SortAnim : MonoBehaviour
{
    [Title("渲染算法"), HideLabel]
    [Required]
    [InlineEditor(InlineEditorObjectFieldModes.Foldout)]
    public ArrayVisual av;
    SortAlgorithm sa;

    [Title("排序算法"), HideLabel]
    [EnumToggleButtons]
    [OnValueChanged("Refresh")]
    public SortAlogorithmEnum SAEnum;

    [LabelText("时间步长")]
    [Range(0, 0.1f)]
    public float delay;

    private void Start()
    {
        Application.targetFrameRate = 60;

        Refresh();
    }

    void Refresh()
    {
        if (!Application.isPlaying) return;


        sa = new Shuffle();
        Task shuffleTask = new Task(sa.Sort(av, delay), false);

        sa = SAEnum switch
        {
            SortAlogorithmEnum.Bubble => new BubbleSort(),
            SortAlogorithmEnum.Heap => new HeapSort(),
            SortAlogorithmEnum.Shell => new ShellSort(),
            SortAlogorithmEnum.Counting => new CountingSort(),
            SortAlogorithmEnum.Bucket => new BucketSort(),
            _ => new Shuffle(),
        };
        Task sortTask = new Task(sa.Sort(av, delay), false);

        shuffleTask.Start();

        shuffleTask.Finished += delegate (bool manual)
        {
            sortTask.Start();
        };
    }
}


public enum SortAlogorithmEnum
{
    Shuffle,
    Bubble,
    Heap,
    Shell,
    Counting,
    Bucket,
}
