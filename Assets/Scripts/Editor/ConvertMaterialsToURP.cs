using UnityEngine;
using UnityEditor;

public class ConvertMaterialsToURP : Editor
{
    [MenuItem("Tools/Convert All Materials to URP")]
    public static void ConvertAllMaterials()
    {
        string[] guids = AssetDatabase.FindAssets("t:Material");
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Material material = AssetDatabase.LoadAssetAtPath<Material>(path);

            if (material != null)
            {
                Shader shader = Shader.Find("Universal Render Pipeline/Lit");
                if (shader != null)
                {
                    material.shader = shader;
                    Debug.Log($"Converted {material.name} to URP shader.");
                }
                else
                {
                    Debug.LogWarning($"URP shader not found for {material.name}");
                }
            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("Converted all materials to URP");
    }
}
