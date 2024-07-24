using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;  // Add this line for IEnumerator

public class VRButtonInteraction : MonoBehaviour
{
    public string sceneName;
    public float fadeOutDuration = 1f;
    public float fadeInDuration = 1f;

    private Button button;
    private XRGrabInteractable interactable;

    private void Awake()
    {
        button = GetComponent<Button>();
        interactable = GetComponent<XRGrabInteractable>();

        if (button != null)
        {
            button.onClick.AddListener(OnButtonClick);
        }

        if (interactable != null)
        {
            interactable.onSelectEntered.AddListener(OnButtonSelect);
        }
    }

    private void OnButtonClick()
    {
        StartCoroutine(FadeAndSwitchScene(sceneName));
    }

    private void OnButtonSelect(XRBaseInteractor interactor)
    {
        StartCoroutine(FadeAndSwitchScene(sceneName));
    }

    private IEnumerator FadeAndSwitchScene(string scene)
    {
        FadeController.Instance.StartFadeOut(fadeOutDuration);
        yield return new WaitForSeconds(fadeOutDuration);
        SceneManager.LoadScene(scene);
        FadeController.Instance.StartFadeIn(fadeInDuration);
    }
}