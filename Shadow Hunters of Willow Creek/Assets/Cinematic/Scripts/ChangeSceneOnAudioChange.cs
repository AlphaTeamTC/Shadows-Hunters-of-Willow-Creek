using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneOnAudioChange : MonoBehaviour
{
    public AudioSource audioSource;
    public string nextSceneName;

    void Start()
    {
        if (audioSource == null)
        {
            Debug.LogError("AudioSource is not assigned.");
            return;
        }

        // Start coroutine to monitor when the audio finishes
        StartCoroutine(CheckAudioCompletion());
    }

    void Update()
    {
        // Check for ESC key press to skip
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LoadNextScene();
        }
    }

    private IEnumerator CheckAudioCompletion()
    {
        // Wait until the audio is no longer playing
        while (audioSource.isPlaying)
        {
            yield return null;
        }

        // Load the next scene when audio finishes
        LoadNextScene();
    }

    private void LoadNextScene()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogError("Next scene name is not set.");
        }
    }
}
