
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
    public MovieRecorderSettings m_Settings = null;

    public FileInfo OutputFile
    {
        get
        {
            var fileName = m_Settings.OutputFile + ".mp4";
            return new FileInfo(fileName);
        }
    }

    void OnEnable()
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
        controllerSettings.AddRecorderSettings(m_Settings);
        controllerSettings.SetRecordModeToManual();
        controllerSettings.FrameRate = 60.0f;

        RecorderOptions.VerboseMode = false;
        m_RecorderController.PrepareRecording();
    }

    public void StartRecording()
    {
        m_RecorderController.StartRecording();
        Debug.Log($"Started recording for file {OutputFile.FullName}");
    }

    public void StopRecording()
    {
        m_RecorderController.StopRecording();
    }
}

