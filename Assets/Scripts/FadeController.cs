using UnityEngine;
using UnityEngine.UI; // Add this line for RawImage
using System.Collections;

public class FadeController : MonoBehaviour
{
    public static FadeController Instance { get; private set; }
    private CanvasGroup fadeCanvasGroup;
    public float fadeDuration = 1f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            fadeCanvasGroup = GetOrCreateFadeCanvas();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartFadeOut(float duration)
    {
        fadeDuration = duration;
        StartCoroutine(FadeOut());
    }

    public void StartFadeIn(float duration)
    {
        fadeDuration = duration;
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeOut()
    {
        fadeCanvasGroup.gameObject.SetActive(true);

        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            fadeCanvasGroup.alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            yield return null;
        }
        fadeCanvasGroup.alpha = 1f; // Ensure it's fully opaque at the end
    }

    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            fadeCanvasGroup.alpha = Mathf.Clamp01(1f - (elapsedTime / fadeDuration));
            yield return null;
        }
        fadeCanvasGroup.alpha = 0f; // Ensure it's fully transparent at the end
        fadeCanvasGroup.gameObject.SetActive(false);
    }

    private CanvasGroup GetOrCreateFadeCanvas()
    {
        if (fadeCanvasGroup == null)
        {
            GameObject fadeCanvas = new GameObject("FadeCanvas");
            DontDestroyOnLoad(fadeCanvas);  // Ensure this object is not destroyed on load
            fadeCanvas.transform.SetParent(transform); // Make it a child of the FadeController

            Canvas canvas = fadeCanvas.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay; // Ensure this is set to Overlay
            fadeCanvasGroup = fadeCanvas.AddComponent<CanvasGroup>();
            canvas.sortingOrder = 999;

            // Adding a RawImage to visualize the fade effect
            RawImage rawImage = fadeCanvas.AddComponent<RawImage>();
            rawImage.color = new Color(0, 0, 0, 1); // Black color with full alpha

            fadeCanvasGroup.alpha = 0f; // Start with transparent
            fadeCanvas.SetActive(false); // Initially inactive
            Debug.Log("Created FadeCanvas"); // Debug log
        }

        return fadeCanvasGroup;
    }
}