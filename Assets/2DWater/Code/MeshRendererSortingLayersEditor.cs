#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MeshRenderer))]
public class MeshRendererSortingLayersEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Renderer renderer = (Renderer)target;

        if (renderer == null)
            return;

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Sorting Settings", EditorStyles.boldLabel);

        EditorGUI.BeginChangeCheck();

        string sortingLayer = EditorGUILayout.TextField("Sorting Layer", renderer.sortingLayerName);
        int sortingOrder = EditorGUILayout.IntField("Sorting Order", renderer.sortingOrder);

        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(renderer, "Change Sorting Settings");

            renderer.sortingLayerName = sortingLayer;
            renderer.sortingOrder = sortingOrder;

            EditorUtility.SetDirty(renderer);
        }
    }
}
#endif