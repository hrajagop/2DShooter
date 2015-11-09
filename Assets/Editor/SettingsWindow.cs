using UnityEngine;
using UnityEditor;

class SettingsWindow : EditorWindow
{    
    public BuildAssetBundleOptions options = BuildAssetBundleOptions.None;
    public BuildTarget targets = BuildTarget.WebPlayer;

    int index;
    bool force;

    [MenuItem("AssetBundles/Settings")]
    public static void ShowWindow()
    {
        GetWindow(typeof(SettingsWindow));
    }

    void OnGUI()
    {
        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();       
        EditorGUILayout.BeginVertical();
            GUILayout.Label("Build Bundle Settings", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            options = (BuildAssetBundleOptions)EditorGUILayout.EnumMaskField("Asset Bundle Options: ", options);
            targets = (BuildTarget)EditorGUILayout.EnumPopup("Build Target", targets);
            EditorGUI.indentLevel--;
        EditorGUILayout.EndVertical();        
        EditorGUILayout.Space();
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();

        BuildLogic.Option = options;
        BuildLogic.Target = targets;
    }
}
