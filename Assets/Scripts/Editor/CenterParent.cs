using UnityEngine;
using UnityEditor;

public class CenterParentGizmoTool : MonoBehaviour
{
    [MenuItem("Tools/Center Parent Gizmo")]
    public static void CenterGizmo()
    {
        if (Selection.activeTransform == null || Selection.activeTransform.childCount == 0)
        {
            Debug.LogWarning("Please select a parent GameObject with child objects.");
            return;
        }

        Transform parentTransform = Selection.activeTransform;
        Vector3 centroid = Vector3.zero;
        foreach (Transform child in parentTransform)
        {
            centroid += child.position;
        }
        centroid /= parentTransform.childCount;

        Vector3 offset = parentTransform.position - centroid;

        Undo.RecordObject(parentTransform, "Center Parent Gizmo");
        parentTransform.position = centroid;

        foreach (Transform child in parentTransform)
        {
            Undo.RecordObject(child, "Center Parent Gizmo");
            child.position += offset;
        }

        Debug.Log("Parent gizmo centered to child objects.");
    }
}
