using UnityEngine;
using UnityEditor;

[InitializeOnLoad] // Ensure the class is initialized on load
public class CameraViewTool
{
    private static bool isCameraViewEnabled;

    // Key for saving the state in EditorPrefs
    private const string CameraViewEnabledPrefKey = "CameraViewTool_IsEnabled";

    static CameraViewTool()
    {
        // Load the saved state when the class is initialized
        isCameraViewEnabled = EditorPrefs.GetBool(CameraViewEnabledPrefKey, false);
        if (isCameraViewEnabled)
        {
            SceneView.duringSceneGui += OnSceneGUI;
        }

        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }

    private static void OnPlayModeStateChanged(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.EnteredPlayMode || state == PlayModeStateChange.ExitingPlayMode)
        {
            // Ensure the menu item checkbox reflects the current state
            Menu.SetChecked("Tools/Camera View Toggle", isCameraViewEnabled);
        }
    }

    [MenuItem("Tools/Camera View Toggle", priority = 1)]
    private static void ToggleCameraView()
    {
        isCameraViewEnabled = !isCameraViewEnabled;

        // Save the state to EditorPrefs
        EditorPrefs.SetBool(CameraViewEnabledPrefKey, isCameraViewEnabled);

        Menu.SetChecked("Tools/Camera View Toggle", isCameraViewEnabled);

        if (isCameraViewEnabled)
        {
            SceneView.duringSceneGui += OnSceneGUI;
        }
        else
        {
            SceneView.duringSceneGui -= OnSceneGUI;
        }
    }

    private static void OnSceneGUI(SceneView sceneView)
    {
        if (isCameraViewEnabled)
        {
            Camera gameCam = Camera.main;
            if (gameCam != null)
            {
                sceneView.LookAtDirect(gameCam.transform.position, gameCam.transform.rotation);
            }
        }
    }
}