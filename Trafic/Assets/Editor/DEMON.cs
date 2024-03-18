using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;


public class DEMON : EditorWindow
{
    string task = " task ";

    [MenuItem("AI/DEMON")]
    public static void ShowWindow()
    {
        GetWindow(typeof(DEMON));
    }

    private void OnGUI()
    {
        GUILayout.Label("DEMON Assistent",EditorStyles.boldLabel);

        task = EditorGUILayout.TextField("Command", task);


        if (GUILayout.Button("Execute"))
        {
            EditorSceneManager.OpenScene("Assets/Scenes/trafic game.unity");
        }
    }



}
