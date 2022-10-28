
using UnityEngine;
using System.IO;
using UnityEditor.Recorder;
using UnityEditor.Recorder.Input;
using UnityEditor;
using Sirenix.OdinInspector;

public class MovieRecorder : MonoBehaviour
{

    RecorderController m_RecorderController;

    [Title("Recorder Settings")]
    [ShowInInspector]
    [InlineEditor(InlineEditorObjectFieldModes.Foldout)]
    public RecorderControllerSettings controllerSettings = null;


    [InlineEditor(InlineEditorObjectFieldModes.Foldout)]
    [ShowInInspector]
    public MovieRecorderSettings m_Settings = null;

    void Start()
    {
        Initialize();
    }

    internal void Initialize()
    {
        m_RecorderController = new RecorderController(controllerSettings);

        // Setup Recording
        controllerSettings.AddRecorderSettings(m_Settings);
        controllerSettings.SetRecordModeToManual();

        RecorderOptions.VerboseMode = true;
    }

    public void StartRecording()
    {
        if (m_RecorderController.IsRecording())
        {
            StopRecording();
        }

        Initialize();
        m_RecorderController.PrepareRecording();
        m_RecorderController.StartRecording();
    }

    public void StopRecording()
    {
        if (m_RecorderController.IsRecording())
        {
            m_RecorderController.StopRecording();
        }
    }
}

