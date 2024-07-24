using UnityEditor;
using UnityEngine;

public class CameraPreviewWindow : EditorWindow
{
    private Camera cameraToPreview;
    private RenderTexture renderTexture;

    [MenuItem("Window/Camera Preview")]
    public static void ShowWindow()
    {
        GetWindow<CameraPreviewWindow>("Camera Preview");
    }

    private void OnEnable()
    {
        // Initialize Render Texture
        renderTexture = new RenderTexture(256, 144, 16, RenderTextureFormat.ARGB32);
    }

    private void OnDisable()
    {
        // Clean up Render Texture
        if (renderTexture)
        {
            renderTexture.Release();
            DestroyImmediate(renderTexture);
        }
    }

    private void OnGUI()
    {
        GUILayout.Label("Select a Camera to Preview", EditorStyles.boldLabel);

        cameraToPreview = (Camera)EditorGUILayout.ObjectField("Camera", cameraToPreview, typeof(Camera), true);

        if (cameraToPreview)
        {
            if (renderTexture == null)
            {
                renderTexture = new RenderTexture(256, 144, 16, RenderTextureFormat.ARGB32);
            }

            cameraToPreview.targetTexture = renderTexture;

            GUILayout.Box("", GUILayout.Width(position.width), GUILayout.Height(position.width * (9.0f / 16.0f)));
            Rect previewRect = GUILayoutUtility.GetLastRect();
            EditorGUI.DrawPreviewTexture(previewRect, renderTexture, null, ScaleMode.ScaleToFit);

            // Force the camera to render into the Render Texture
            cameraToPreview.Render();

            // Reset the camera's target texture to avoid affecting its normal rendering
            cameraToPreview.targetTexture = null;
        }
    }
}