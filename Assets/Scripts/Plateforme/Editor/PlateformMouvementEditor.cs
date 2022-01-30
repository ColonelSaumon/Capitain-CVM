using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlateformMovement))]
public class PlateformMouvementEditor : Editor
{
    /// <summary>
    /// Désigne la propriété (du component) _vitesse
    /// </summary>
    private SerializedProperty _vitesse;
    /// <summary>
    /// Désigne la propriété (du component) _points
    /// </summary>
    private SerializedProperty _points;

    private void OnEnable()
    {
        _vitesse = this.serializedObject.FindProperty("_vitesse");
        _points = this.serializedObject.FindProperty("_points");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(_vitesse);
        if (GUILayout.Button("Ajouter un point"))
        {
            PlateformMovement transformPM = this.target as PlateformMovement;
            GameObject go = new GameObject($"Position{_points.arraySize + 1}");

            go.transform.position = transformPM.transform.position;
            go.transform.parent = transformPM.transform.parent;

            _points.InsertArrayElementAtIndex(_points.arraySize - 1);
            _points.GetArrayElementAtIndex(_points.arraySize - 1).objectReferenceValue = go.transform;
        }
        EditorGUILayout.PropertyField(_points);
        serializedObject.ApplyModifiedProperties();

        if (GUILayout.Button("Revenir à la position initiale"))
        {
            PlateformMovement transformPM = this.target as PlateformMovement;
            Transform t = _points.GetArrayElementAtIndex(0).objectReferenceValue as Transform;
            transformPM.transform.position = t.position;
        }
    }
}
