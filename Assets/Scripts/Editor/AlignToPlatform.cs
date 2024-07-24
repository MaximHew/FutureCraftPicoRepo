using UnityEngine;
using UnityEditor;

public class AlignToPlatform : MonoBehaviour
{
    [MenuItem("Tools/Align to Platform")]
    static void AlignObjects()
    {
        GameObject[] selectedObjects = Selection.gameObjects;

        foreach (GameObject obj in selectedObjects)
        {
            RaycastHit hit;
            if (Physics.Raycast(obj.transform.position, Vector3.down, out hit))
            {
                obj.transform.position = hit.point;
                Debug.Log($"{obj.name} aligned to {hit.collider.name}");
            }
            else
            {
                Debug.LogWarning($"{obj.name} did not hit any surface");
            }
        }
    }
}
