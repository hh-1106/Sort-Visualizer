using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;


public class SortAnim : MonoBehaviour
{
    [Required]
    [SceneObjectsOnly]
    [Title("渲染方案"), HideLabel]
    [InlineEditor(InlineEditorObjectFieldModes.Foldout)]
    public ArrayVisual av;
    ISortAlgorithm sa;

    [Space(10)]

    [Title("排序算法"), HideLabel]
    [EnumToggleButtons]
    [OnValueChanged("Refresh")]
    [DisableIf("sorting")]
    public SortAlogorithmEnum SAEnum;

    [ShowInInspector]
    [ReadOnly]
    bool sorting;

    [Title("动画"), LabelText("时间步长")]
    [Range(0, 0.1f)]
    public float delay;

    [Title("录制"), LabelText("Enable Record")]
    [DisableInPlayMode]
    public bool enableRecord;

    Task shuffleTask, sortTask;

    [Title("UI")]
    public TextMeshProUGUI SAName;

    private void Start()
    {
        Application.targetFrameRate = 60;
        Refresh();
    }

    void Refresh()
    {
        //? https://docs.unity3d.com/ScriptReference/Application-isPlaying.html 
        if (!Application.isPlaying) return;

        StopCurrentSort();
        av.Refresh();
        StartNewSort();
    }

    void StartNewSort()
    {
        av.sorted = false;

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
        SAName.text = "Shuffle";

        // 开始录制
        if (enableRecord) { MovieRecorder.Instance.StartRecording(); }

        // 打乱结束后自动开始排序
        shuffleTask.Finished += delegate (bool manual)
        {
            sortTask.Start();
            SAName.text = sa.ToString();
        };

        // 排序正常结束后停止录制
        sortTask.Finished += delegate (bool manual)
        {
            if (enableRecord && !manual)
            {
                MovieRecorder.Instance.StopRecording();
            }
        };
    }

    void StopCurrentSort()
    {
        if (shuffleTask != null && shuffleTask.Running)
        {
            shuffleTask.Stop();
            Debug.Log("stop shuffle");
        }
        if (sortTask != null && sortTask.Running)
        {
            sortTask.Stop();
            Debug.Log("stop sort");
        }
    }

    private void Update()
    {
        sorting = sortTask.Running;
        av.sorted = !sorting;
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