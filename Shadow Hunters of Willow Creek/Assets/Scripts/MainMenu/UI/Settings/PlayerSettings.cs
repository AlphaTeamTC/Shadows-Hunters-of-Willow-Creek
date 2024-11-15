using LocalPhoton;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace MainMenu.UI.Settings
{
    /// <summary>
    /// Class that does the methods for the buttons. 
    /// </summary>
    public class PlayerSettings : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _nameInputField;
        [SerializeField] private PUNConnector _punConnector;

        [SerializeField] private Slider _volumeSlider;
        [SerializeField] private TMP_Dropdown resolutionDropdown;

        private readonly Resolution[] customResolutions = new Resolution[]
        {
            new() { width = 1920, height = 1080 },
            new() { width = 1280, height = 720 },
            new() { width = 640, height = 480 }
        };

        private GameSettings gameSettings;

        private void Start()
        {
            _nameInputField.text = PlayerData.LoadPlayerData();

            gameSettings = GameSettings.LoadGameSettings();
            _volumeSlider.value = gameSettings.volume;

            resolutionDropdown.ClearOptions();
            List<string> options = new()
            {
                "1920 x 1080",
                "1280 x 720",
                "640 x 480"
            };
            resolutionDropdown.AddOptions(options);

            resolutionDropdown.value = gameSettings.resolutionIndex;
            resolutionDropdown.RefreshShownValue();
        }

        private void Update()
        {
            // Update the volume slider
            AudioListener.volume = _volumeSlider.value;
        }

        /// <summary>
        /// Save the player settings
        /// </summary>
        public void OnSaveSettingsButtonClicked()
        {
            string newName = _nameInputField.text;

            if (string.IsNullOrEmpty(newName))
            {
                Debug.Log("Name field is empty!");
                return;
            }

            PlayerData.SavePlayerData(newName);
            _punConnector.SubmitNickname(newName);

            gameSettings.volume = _volumeSlider.value;
            gameSettings.resolutionIndex = resolutionDropdown.value;

            GameSettings.SaveGameSettings(gameSettings);
            ApplyGameSettings();

            Debug.Log("Game settings saved and applied!");
        }

        /// <summary>
        /// Reset the player settings
        /// </summary>
        public void OnResetSettings()
        {
            _nameInputField.text = PlayerData.LoadPlayerData();
            gameSettings = new GameSettings(0.5f, 0);
            UpdateUI();
        }

        /// <summary>
        /// Discard the player settings
        /// </summary>
        public void OnDiscardSettings()
        {
            _nameInputField.text = PlayerData.LoadPlayerData();
            gameSettings = GameSettings.LoadGameSettings();
            UpdateUI();
        }

        /// <summary>
        /// Apply the game settings
        /// </summary>
        public void ApplyGameSettings()
        {
            Resolution selectedResolution = customResolutions[gameSettings.resolutionIndex];
            Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen);

            AudioListener.volume = gameSettings.volume;
        }

        /// <summary>
        /// Update the UI
        /// </summary>
        public void UpdateUI()
        {
            _volumeSlider.value = gameSettings.volume;
            resolutionDropdown.value = gameSettings.resolutionIndex;
            resolutionDropdown.RefreshShownValue();
        }
    }
}