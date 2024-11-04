using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SubtitlesIntro : MonoBehaviour
{
    public TextMeshProUGUI subtitleText;
    public float defaultDisplayTime = 20f; // Adjust the default display time as needed

    private string currentSubtitle;
    private float startTime;

    public void ShowSubtitle(string[] textArray, float[] durationArray)
    {
        // Ensure the text array and duration array have the same length
        if (textArray.Length != durationArray.Length)
        {
            Debug.LogError("Text array and duration array must have the same length.");
            return;
        }

        // Set the subtitle text to the first string in the array
        subtitleText.text = textArray[0];
        currentSubtitle = textArray[0];

        // Set the start time to the current time
        startTime = Time.time;

        // Start the coroutine to display the subtitles with individual durations
        StartCoroutine(DisplaySubtitle(textArray, durationArray));
    }

    private IEnumerator DisplaySubtitle(string[] textArray, float[] durationArray)
    {
        // Loop through the text array and duration array
        for (int i = 0; i < textArray.Length; i++)
        {
            // Set the subtitle text to the current string in the array
            subtitleText.text = textArray[i];
            currentSubtitle = textArray[i];

            // Set the start time to the current time
            startTime = Time.time;

            // Wait for the specified duration
            yield return new WaitForSeconds(durationArray[i]);
        }

        // Clear the subtitle text
        subtitleText.text = "";
        currentSubtitle = "";
    }

    void Start()
    {
        // Add the text array and corresponding duration array to the script
        string[] initialTextArray = new string[]
        {
            "Willow Creek, once a kingdom of peace and light, where life thrived under the watchful eyes of a benevolent ruler.",
            "But ambition, born from love and fear, turned its fate.",
            "A young king, desperate to save his queen, unleashed an ancient force—one that should have remained sealed.",
            "Now, the castle stands as a tomb of his own making, a prison of corrupted magic and haunting shadows.",
            "What was once hope has become a nightmare, and the king, a prisoner of his own curse.",
            "Today, the fate of Willow Creek rests in the hands of those brave enough to face the darkness and reclaim the light."
        };

        // Create a duration array with individual durations for each subtitle
        float[] initialDurationArray = new float[]
        {
            5f, 6.5f, 8f, 9.5f, 9f, 6.5f
        };

        ShowSubtitle(initialTextArray, initialDurationArray);
    }
}
