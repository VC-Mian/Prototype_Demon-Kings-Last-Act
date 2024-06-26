using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("----------- Audio Source ----------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("----------- Audio Clip ----------")]
    public AudioClip walk;
    public AudioClip death;
    public AudioClip jump;
    public AudioClip hurt;
    public AudioClip crouch;
    public AudioClip attack;

    // Public fields for background music clips for each scene
    public AudioClip scene0BackgroundMusic;
    public AudioClip scene1BackgroundMusic;
    public AudioClip scene2BackgroundMusic;

    // Dictionary to map scene names to background music clips
    private Dictionary<string, AudioClip> sceneBackgroundMusic;

    private void Awake()
    {
        // Implement Singleton pattern
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Make this object persistent

            // Initialize the dictionary
            sceneBackgroundMusic = new Dictionary<string, AudioClip>
            {
                { "scene0", scene0BackgroundMusic },
                { "scene1", scene1BackgroundMusic },
                { "scene2", scene2BackgroundMusic }
            };
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
            return;
        }
    }

    private void Start()
    {
        PlayBackgroundMusicForCurrentScene();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayBackgroundMusicForCurrentScene();
    }

    private void PlayBackgroundMusicForCurrentScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        if (sceneBackgroundMusic.TryGetValue(currentSceneName, out AudioClip newBackgroundClip))
        {
            PlayBackgroundMusic(newBackgroundClip);
        }
    }

    public void PlayLoopingSFX(AudioClip clip)
    {
        if (SFXSource.clip != clip)
        {
            SFXSource.clip = clip;
            SFXSource.loop = true;
            SFXSource.Play();
        }
    }

    public void StopLoopingSFX()
    {
        SFXSource.loop = false;
        SFXSource.Stop();
        SFXSource.clip = null;
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void PlayBackgroundMusic(AudioClip clip)
    {
        if (musicSource.clip != clip)
        {
            musicSource.clip = clip;
            musicSource.loop = true;
            musicSource.Play();
        }
    }
}

