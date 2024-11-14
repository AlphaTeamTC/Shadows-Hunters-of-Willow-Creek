using System;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine;

namespace UI.Settings
{
    /// <summary>
    /// Class that does the methods for the buttons. 
    /// </summary>
    public class OptionsMenu : MonoBehaviour
    {
        [Header("Volume")] [SerializeField] Slider volumeSlider;
        [SerializeField] private AudioMixer masterMixer;

        [Header("Name")] [SerializeField] TMP_InputField nameField;

        [Header("Resolution")] [SerializeField]
        TMP_Dropdown resolution;


        //Class 
        public PlayerSettings settings = new PlayerSettings();

        //JSON STRING
        public string jsonSettingsString;

        //Settings
        [Header("Settings (JSON)")] [SerializeField]
        SettingsManager settingsManager;

        /// <summary>
        /// Initial settings for game 
        /// </summary>
        void Start()
        {
            //Volume
            SetVolume(25);
        }



        // Methods for Volume ------

        /// <summary>
        /// Method to set the volume of the audio mixer with code/direct float value
        /// </summary>
        /// <param name="_value"></param>
        public void SetVolume(float _value)
        {
            if (_value < 1)
            {
                _value = 0.001f;
            }

            RefreshSlider(_value);
            masterMixer.SetFloat("MasterVolume", Mathf.Log10(_value / 100) * 20f);
        }

        /// <summary>
        /// Method to set the volume value from the slider itself
        /// </summary>
        public void SetVolumeFromSlider()
        {
            SetVolume(volumeSlider.value);
        }

        /// <summary>
        /// Refresh the slider
        /// </summary>
        /// <param name="_value"></param>
        public void RefreshSlider(float _value)
        {
            volumeSlider.value = _value;
        }

        /// <summary>
        /// Get the slider value for the json 
        /// </summary>
        /// <returns></returns>
        public float getSliderValue()
        {
            return volumeSlider.value;
        }


        //Methods for Resolution --------
        public void SetResolution(int resolutionIndex)
        {
            switch (resolutionIndex)
            {
                case 0:
                    Screen.SetResolution(640, 480, FullScreenMode.Windowed);
                    break;
                case 1:
                    Screen.SetResolution(1200, 720, FullScreenMode.Windowed);
                    break;
                case 2:
                    Screen.SetResolution(1920, 1080, FullScreenMode.Windowed);
                    break;
            }
        }

        //Methods for Name ------
        public void setPlayerName(string name)
        {
            if (nameField.text == "")
            {
                Debug.LogError("Name can't be empty");
            }

            //Warn player that the name has been saved
            //Debug.LogWarning($"Player name set to: {name}."); - For when multiplayer is implemented
            Debug.LogWarning($"Player name set to: {nameField.text}.");


            //Save the player name
            //Debug.Log($"Name saved: {name}."); - For when multiplayer is implemented
            Debug.Log($"Name saved: {nameField.text}.");
        }


        //Methods for setting buttons ------
        /// <summary>
        /// MEthod to reset the settings
        /// Volume: 20/100
        /// Resolution: 1200 x 720
        /// Name: Empty
        /// </summary>
        public void onResetSettings()
        {
            //Volume
            SetVolume(20);
            //Resolution
            SetResolution(2);
            //Name
            setPlayerName("User");
        }

        /// <summary>
        /// Save the settings into the JSON 
        /// </summary>
        public void onSaveSettings()
        {
            settings.volumeSettings = volumeSlider.value;
            settings.playerName = nameField.text;
            settings.resolutionIndex = resolution.value;
            settingsManager.saveSettings(settings);
        }

        /// <summary>
        /// Discard settings u put into the most recent ones, if not resets it.
        /// </summary>
        public void onDiscardSettings()
        {
            if (jsonSettingsString != null)
            {
                settings = settingsManager.loadSettings();

                SetVolume(settings.volumeSettings);
                SetResolution(settings.resolutionIndex);
                setPlayerName(settings.playerName);

            }

            else
            {

                //Volume
                SetVolume(20);
                //Resolution
                SetResolution(2);
                //Name
                setPlayerName("");
            }
        }

        public void onExit()
        {
            Debug.Log("Exiting  options menu");
        }
}
}


