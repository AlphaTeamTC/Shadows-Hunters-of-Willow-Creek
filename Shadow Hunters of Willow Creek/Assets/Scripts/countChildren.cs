using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class countChildren : EditorWindow
{
    [MenuItem("Tools/Count Children")]
    public static void ShowWindow()
    {
        GetWindow<countChildren>("Count Children");
    }

    private void OnGUI()
    {
        GUILayout.Label("Count Children of Selected Object", EditorStyles.boldLabel);

        if (GUILayout.Button("Count Children"))
        {
            CountChildren();
        }
    }

    private void CountChildren()
    {
        GameObject selectedObject = Selection.activeGameObject;

        if (selectedObject != null)
        {
            int childCount = selectedObject.transform.childCount;
            Debug.Log($"El objeto '{selectedObject.name}' tiene {childCount} hijos.");
        }
        else
        {
            Debug.LogWarning("No has seleccionado ningún objeto.");
        }
    }
}