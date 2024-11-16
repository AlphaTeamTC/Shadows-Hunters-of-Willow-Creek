using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicLevels : MonoBehaviour
{
    public List<AudioClip> playlist; // Lista de canciones asignada desde el inspector.
    private AudioSource audioSource;
    private int currentTrackIndex = 0; // Índice de la canción actual.

    void Start()
    {
        // Obtén el componente AudioSource adjunto al GameObject.
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogError("No se encontró un componente AudioSource en este objeto.");
            return;
        }

        // Asegúrate de que haya canciones en la lista.
        if (playlist == null || playlist.Count == 0)
        {
            Debug.LogError("La lista de canciones está vacía.");
            return;
        }

        // Reproduce la primera canción.
        PlayCurrentTrack();
    }

    /// <summary>
    /// Reproduce la canción actual en la lista.
    /// </summary>
    public void PlayCurrentTrack()
    {
        if (playlist.Count == 0) return;

        audioSource.clip = playlist[currentTrackIndex];
        audioSource.Play();
        Debug.Log($"Reproduciendo: {playlist[currentTrackIndex].name}");
    }

    /// <summary>
    /// Reproduce la siguiente canción en la lista.
    /// </summary>
    public void NextTrack()
    {
        if (playlist.Count == 0) return;

        currentTrackIndex = (currentTrackIndex + 1) % playlist.Count;
        PlayCurrentTrack();
    }

    /// <summary>
    /// Reproduce la canción anterior en la lista.
    /// </summary>
    public void PreviousTrack()
    {
        if (playlist.Count == 0) return;

        currentTrackIndex = (currentTrackIndex - 1 + playlist.Count) % playlist.Count;
        PlayCurrentTrack();
    }

    /// <summary>
    /// Pausa la reproducción.
    /// </summary>
    public void PauseMusic()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
            Debug.Log("Música pausada.");
        }
    }

    /// <summary>
    /// Detiene completamente la reproducción.
    /// </summary>
    public void StopMusic()
    {
        audioSource.Stop();
        Debug.Log("Música detenida.");
    }

    /// <summary>
    /// Ajusta el volumen de la música.
    /// </summary>
    /// <param name="volume">Nuevo volumen entre 0.0 y 1.0</param>
    public void SetVolume(float volume)
    {
        audioSource.volume = Mathf.Clamp(volume, 0f, 1f);
        Debug.Log($"Volumen ajustado a {audioSource.volume}");
    }
}
