using UnityEngine;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    void Start()
    {
        string mbtiResult = PlayerPrefs.GetString("MBTI_Result"); // Retrieve the MBTI result
        Debug.Log("Retrieved MBTI result: " + mbtiResult); // Log the retrieved result for debugging

        // Use Resources.FindObjectsOfTypeAll to include inactive GameObjects
        GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();
        
        GameObject resultObject = null;
        
        // Find the GameObject with the same name as the MBTI result
        foreach (GameObject obj in allObjects)
        {
            // Ensure the object is part of the scene and not an asset
            if (obj.name == mbtiResult && obj.hideFlags == HideFlags.None)
            {
                resultObject = obj;
                break;
            }
        }

        if (resultObject != null)
        {
            resultObject.SetActive(true); // Show the result GameObject

            // Ensure the Canvas is active
            Canvas parentCanvas = resultObject.GetComponentInParent<Canvas>();
            if (parentCanvas != null)
            {
                parentCanvas.gameObject.SetActive(true); // Ensure the Canvas is active
            }

            // Activate the Image component
            Image imageComponent = resultObject.GetComponentInChildren<Image>();
            if (imageComponent != null)
            {
                imageComponent.gameObject.SetActive(true);
            }
        }
    }
}