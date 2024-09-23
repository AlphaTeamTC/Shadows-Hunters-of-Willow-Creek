using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MusicIntroController : MonoBehaviour
{
    public AudioClip audioClip;
    public AudioClip audioClip2;

    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource.clip = audioClip;
        audioSource.Play();
        
    }

    // Update is called once per frame
    void Update()
    {
        // If Audio 1 ends, Audio 2 starts
        if (audioClip.length == audioSource.time){
            StartCoroutine(Wait());

            audioSource.clip = audioClip2;
            audioSource.Play();
        }
        // When Audio 2 ends, change scene to Village
        if (audioClip2.length == audioSource.time || Input.GetKeyDown(KeyCode.Space))
        {

            SceneManager.LoadScene("Village");
        }
        
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(10f);
    }


}
