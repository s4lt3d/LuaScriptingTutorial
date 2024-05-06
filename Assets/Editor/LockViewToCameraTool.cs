using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public class LockViewToCameraTool
{
    private static bool isCameraViewEnabled;
    
    private const string MenuPath = "Tools/Lock Scene View to Camera";

    private const string CameraViewEnabledPrefKey = "LockViewToCameraTool_IsEnabled";

    static LockViewToCameraTool()
    {
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
            Menu.SetChecked(MenuPath, isCameraViewEnabled);
        }
    }

    [MenuItem(MenuPath, priority = 1)]
    private static void ToggleCameraView()
    {
        isCameraViewEnabled = !isCameraViewEnabled;

        EditorPrefs.SetBool(CameraViewEnabledPrefKey, isCameraViewEnabled);

        Menu.SetChecked(MenuPath, isCameraViewEnabled);

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