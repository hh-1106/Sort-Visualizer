
using UnityEngine;
using System.IO;
using UnityEditor.Recorder;
using UnityEditor.Recorder.Input;
using UnityEditor;
using Sirenix.OdinInspector;

public class MovieRecorder : MonoBehaviour
{

    static MovieRecorder instance;

    RecorderController m_RecorderController;

    [Title("Movie Recorder Settings"), HideLabel]

    [InlineEditor(InlineEditorObjectFieldModes.Foldout)]
    [ShowInInspector]
    public MovieRecorderSettings m_Settings = null;


    private void OnEnable()
    {
        Initialize();
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
        var controllerSettings = ScriptableObject.CreateInstance<RecorderControllerSettings>();
        m_RecorderController = new RecorderController(controllerSettings);

        // Setup Recording
        controllerSettings.SetRecordModeToManual();
        controllerSettings.AddRecorderSettings(m_Settings);
        controllerSettings.FrameRate = 60.0f;

        RecorderOptions.VerboseMode = false;
        m_RecorderController.PrepareRecording();
    }

    public static void StartRecording()
    {
        instance.m_RecorderController.StartRecording();
        Debug.Log($"Started recording for file {instance.m_Settings.OutputFile}");
    }

    public static void StopRecording()
    {
        instance.m_RecorderController.StopRecording();
    }
}

