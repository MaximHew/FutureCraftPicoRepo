using UnityEditor;
using UnityEngine;

namespace CustomTools
{
    public class AlignToTopCenterEditor : MonoBehaviour
    {
        [MenuItem("Tools/Align Selected Objects to Top Center")]
        static void AlignSelectedObjects()
        {
            foreach (GameObject obj in Selection.gameObjects)
            {
                AlignToTopCenter alignScript = obj.GetComponent<AlignToTopCenter>();
                if (alignScript != null)
                {
                    alignScript.AlignToSurface();
                }
                else
                {
                    Debug.LogWarning($"Object {obj.name} does not have an AlignToTopCenter component");
                }
            }
        }
    }
}
