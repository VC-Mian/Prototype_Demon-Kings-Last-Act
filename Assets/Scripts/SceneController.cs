using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;

    [SerializeField] private Animator transitionAnim;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void NextLevel()
    {
        StartCoroutine(LoadLevel());
    }

    public void LoadScene(string sceneName)
    {
        Debug.Log("Request to load scene: " + sceneName);
        SceneManager.LoadSceneAsync(sceneName);
    }

    private IEnumerator LoadLevel()
    {
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1); // Ensure this duration matches your animation length

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        Debug.Log("Current Scene Index: " + currentSceneIndex);
        Debug.Log("Next Scene Index: " + nextSceneIndex);

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            Debug.Log("Loading next scene with index: " + nextSceneIndex);
            SceneManager.LoadSceneAsync(nextSceneIndex);
        }
        else
        {
            Debug.LogWarning("Next level index is out of range. No more levels to load.");
        }

        transitionAnim.SetTrigger("Start");
    }

    public void RestartCurrentScene()
    {
        StartCoroutine(RestartScene());
    }

    private IEnumerator RestartScene()
    {
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1); // Ensure this duration matches your animation length
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("Restarting current scene with index: " + currentSceneIndex);
        SceneManager.LoadScene(currentSceneIndex);
        transitionAnim.SetTrigger("Start");
    }
}


