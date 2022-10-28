
using UnityEngine;
using System.IO;
using UnityEditor.Recorder;
using UnityEditor.Recorder.Input;
using UnityEditor;
using Sirenix.OdinInspector;

public class MovieRecorder : MonoBehaviour
{
    public static MovieRecorder instance;

    RecorderController m_RecorderController;

    [Title("Recorder Settings")]
    [ShowInInspector]
    [InlineEditor(InlineEditorObjectFieldModes.Foldout)]
    public RecorderControllerSettings controllerSettings = null;


    [InlineEditor(InlineEditorObjectFieldModes.Foldout)]
    [ShowInInspector]
    public MovieRecorderSettings m_Settings = null;

    void OnEnable()
    {
        Initialize();
    }

    void OnDisable()
    {
        instance = null;
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        DontDestroyOnLoad(this);
    }

    internal void Initialize()
    {
        m_RecorderController = new RecorderController(controllerSettings);

        // Setup Recording
        controllerSettings.SetRecordModeToManual();
        controllerSettings.AddRecorderSettings(m_Settings);

        RecorderOptions.VerboseMode = false;
    }

    public static void StartRecording()
    {
        instance.m_RecorderController.PrepareRecording();
        instance.m_RecorderController.StartRecording();
        Debug.Log($"Started recording for file {instance.m_Settings.OutputFile}");
    }

    public static void StopRecording()
    {
        instance.m_RecorderController.StopRecording();
    }
}

