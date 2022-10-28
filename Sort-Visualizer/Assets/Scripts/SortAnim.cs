using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


public class SortAnim : MonoBehaviour
{
    [Required]
    [SceneObjectsOnly]
    [Title("渲染方案"), HideLabel]
    [InlineEditor(InlineEditorObjectFieldModes.Foldout)]
    public BaseArrayVisual av;
    ISortAlgorithm sa;

    [Space(10)]

    [Title("排序算法"), HideLabel]
    [EnumToggleButtons]
    [OnValueChanged("Refresh")]
    public SortAlogorithmEnum SAEnum;

    [Title("动画"), LabelText("时间步长")]
    [Range(0, 0.1f)]
    public float delay;

    [LabelText("开启录制")]
    // [DisableInPlayMode]
    public bool enableRecord;

    Task shuffleTask, sortTask;

    private void Start()
    {
        Application.targetFrameRate = 60;
        Refresh();
    }

    void Refresh()
    {
        if (!Application.isPlaying) return;

        StopCurrentSort();
        av.Refresh();
        StartNewSort();
    }

    void StartNewSort()
    {
        // 创建打乱任务
        sa = new Shuffle();
        shuffleTask = new Task(sa.Sort(av, -1));

        // 创建排序任务
        sa = SAEnum switch
        {
            SortAlogorithmEnum.Bubble => new BubbleSort(),
            SortAlogorithmEnum.Heap => new HeapSort(),
            SortAlogorithmEnum.Shell => new ShellSort(),
            SortAlogorithmEnum.Counting => new CountingSort(),
            SortAlogorithmEnum.Bucket => new BucketSort(),
            SortAlogorithmEnum.Merge => new MergeSort(),
            SortAlogorithmEnum.Quick => new QuickSort(),
            SortAlogorithmEnum.Insertion => new InsertionSort(),
            SortAlogorithmEnum.Radix => new RadixSort(),
            _ => new Shuffle(),
        };
        sortTask = new Task(sa.Sort(av, delay), false);


        if (enableRecord)
        {
            // MovieRecorder.StartRecording();
        }
        // 打乱结束后自动开始排序
        shuffleTask.Finished += delegate (bool manual)
        {
            Debug.Log("shuffle finished");
            sortTask.Start();
            Debug.Log("sort start");
        };

        sortTask.Finished += delegate (bool manual)
        {
            Debug.Log("sort finished");
            // if (enableRecord) { MovieRecorder.StopRecording(); }
        };
    }

    void StopCurrentSort()
    {
        if (shuffleTask != null && shuffleTask.Running)
        {
            shuffleTask.Stop();
        }
        if (sortTask != null && sortTask.Running)
        {
            sortTask.Stop();
        }
    }
}


public enum SortAlogorithmEnum
{
    Insertion,
    Bubble,
    Merge,
    Quick,
    Heap,
    Bucket,
    Shell,
    Radix,
    Counting,
}
