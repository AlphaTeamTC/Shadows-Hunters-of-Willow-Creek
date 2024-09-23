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
            "In the heart of the ancient mountains lies the forgotten town of Willow Creek.",
            "Its villagers, once a vibrant community, have been plagued by strange occurrences and haunting whispers.",
            "Their once-grand castle, a symbol of their prosperity, now stands as a chilling reminder of a dark secret.",
            "Desperate for relief, they've offered a hefty reward to any brave soul willing to enter the castle and exorcise the evil that lurks within.",
            "Rumors of the castle's haunting have spread far and wide, attracting adventurers and seekers of fortune from across the land.",
            "Some say the castle is cursed by a powerful sorcerer who was buried alive within its walls.",
            "Others whisper of a demonic entity that has taken up residence, feeding on the fear and despair of those who dare to enter.",
            "Despite the terrifying tales, the promise of riches and the chance to rid the town of its torment has lured many into the castle's clutches.",
            "But none have ever returned, leaving their fate shrouded in mystery.",
            "A ha ha ha ha Oh, new fools seeking to enter my domain...",
            "How naive you are. You think you can conquer the darkness that dwells within these walls?",
            "You have no idea what you're truly facing. The castle is my prison, but it is also my fortress.",
            "Those who have entered before you have met their doom, and you will be no different."
        };

        // Create a duration array with individual durations for each subtitle
        float[] initialDurationArray = new float[]
        {
            5f, 6.5f, 8f, 9.5f, 9f, 6.5f, 9f, 9.5f, 4.25f, 4.25f, 5f, 6.5f, 5.5f
        };

        ShowSubtitle(initialTextArray, initialDurationArray);
    }
}