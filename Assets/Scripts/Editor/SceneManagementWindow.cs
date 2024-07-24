using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.IO;

public class SceneManagementWindow : EditorWindow
{
    private Vector2 scrollPos;

    [MenuItem("Window/Scene Management")]
    public static void ShowWindow()
    {
        GetWindow<SceneManagementWindow>("Scene Management");
    }

    void OnGUI()
    {
        GUILayout.Label("Scenes in Project", EditorStyles.boldLabel);
        scrollPos = GUILayout.BeginScrollView(scrollPos);

        string[] sceneGuids = AssetDatabase.FindAssets("t:Scene");
        foreach (string guid in sceneGuids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            string sceneName = Path.GetFileNameWithoutExtension(path);

            GUILayout.BeginHorizontal();
            if (GUILayout.Button(sceneName, GUILayout.ExpandWidth(false)))
            {
                if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                {
                    EditorSceneManager.OpenScene(path);
                }
            }

            if (GUILayout.Button("Add to Build", GUILayout.ExpandWidth(false)))
            {
                AddSceneToBuild(path);
            }

            if (GUILayout.Button("Delete Scene", GUILayout.ExpandWidth(false)))
            {
                DeleteScene(path);
            }

            GUILayout.EndHorizontal();
        }

        GUILayout.EndScrollView();
    }

    void AddSceneToBuild(string scenePath)
    {
        EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;
        foreach (EditorBuildSettingsScene scene in scenes)
        {
            if (scene.path == scenePath)
            {
                Debug.Log("Scene already in build settings: " + scenePath);
                return;
            }
        }

        EditorBuildSettingsScene newScene = new EditorBuildSettingsScene(scenePath, true);
        ArrayUtility.Add(ref scenes, newScene);
        EditorBuildSettings.scenes = scenes;

        Debug.Log("Added scene to build settings: " + scenePath);
    }

    void RemoveSceneFromBuild(string scenePath)
    {
        EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;
        int indexToRemove = -1;
        for (int i = 0; i < scenes.Length; i++)
        {
            if (scenes[i].path == scenePath)
            {
                indexToRemove = i;
                break;
            }
        }

        if (indexToRemove != -1)
        {
            ArrayUtility.RemoveAt(ref scenes, indexToRemove);
            EditorBuildSettings.scenes = scenes;
            Debug.Log("Removed scene from build settings: " + scenePath);
        }
        else
        {
            Debug.Log("Scene not found in build settings: " + scenePath);
        }
    }

    void DeleteScene(string scenePath)
    {
        if (EditorUtility.DisplayDialog("Delete Scene", "Are you sure you want to delete the scene: " + scenePath + "?", "Delete", "Cancel"))
        {
            // Ensure the scene is not in build settings
            RemoveSceneFromBuild(scenePath);

            // Delete the scene file
            File.Delete(scenePath);
            File.Delete(scenePath + ".meta");
            AssetDatabase.Refresh();

            Debug.Log("Deleted scene: " + scenePath);
        }
    }
}