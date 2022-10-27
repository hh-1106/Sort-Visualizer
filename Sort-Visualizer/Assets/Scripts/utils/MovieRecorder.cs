
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


    [Title("Recording Progress"), HideLabel]
    [ProgressBar(0, 16, r: 1, g: 0.6f, b: 0, Segmented = true)]
    public float recordingProgress = 0;
    bool isRecording;


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

    private void Update()
    {
        if (isRecording)
        {
            recordingProgress += 0.2f;
            if (recordingProgress > 17)
            {
                recordingProgress -= 17;
            }
        }
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
        instance.isRecording = true;
        instance.recordingProgress = 0;
        instance.m_RecorderController.StartRecording();
        Debug.Log($"Started recording for file {instance.m_Settings.OutputFile}");
    }

    public static void StopRecording()
    {
        instance.m_RecorderController.StopRecording();
        instance.isRecording = false;
        instance.recordingProgress = 16;
    }
}

