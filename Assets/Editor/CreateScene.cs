using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelGenerator))]
public class CreateScene : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        LevelGenerator level = (LevelGenerator)target;

        EditorGUILayout.Space();
        if (GUILayout.Button("Create Keyboard"))
        {
            level.CreateKeyboard();
        }

        if (GUILayout.Button("Create Level"))
        {
            level.CreateGrid();
        }

        if (GUILayout.Button("Get Words"))
        {
            level.GetWords();
        }

        if (GUILayout.Button("Hide/Show Words"))
        {
            level.HideShow();
        }
    }
}