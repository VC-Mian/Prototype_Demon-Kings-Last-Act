using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;  // Add this line to include the TextMeshPro namespace

public class CutsceneController : MonoBehaviour
{
    public TextMeshProUGUI cutsceneText;  // Change Text to TextMeshProUGUI
    public float displayTime = 5f;  // Duration the cutscene text is displayed
    public string nextSceneName = "scene0";  // Name of the next scene to load

    private void Start()
    {
        StartCoroutine(PlayCutscene());
    }

    private IEnumerator PlayCutscene()
    {
        // Ensure the text is visible
        cutsceneText.gameObject.SetActive(true);

        // Wait for the specified duration
        yield return new WaitForSeconds(displayTime);

        // Load the next scene
        SceneManager.LoadScene(nextSceneName);
    }
}
