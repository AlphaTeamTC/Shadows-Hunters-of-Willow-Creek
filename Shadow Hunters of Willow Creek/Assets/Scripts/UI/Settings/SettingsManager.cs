
using System;
using System.IO;
using UnityEngine;

namespace UI.Settings
{
    /// <summary>
    /// Class that gets the JSON for the settings
    /// </summary>
    public class SettingsManager : MonoBehaviour
    {
        private string filePath;
        //[SerializeField] OptionsMenu optionsMenu;

        void Start()
        {
            filePath = Path.Combine(Application.persistentDataPath, "Game Settings.json");
            Debug.LogFormat($"The path of the file is: {filePath}");
        }

        /// <summary>
        /// Save the settings into a Json 
        /// </summary>
        /// <param name="playerSettings"></param>
        public void saveSettings(PlayerSettings playerSettings)
        {
            try
            {
                string json = JsonUtility.ToJson(playerSettings);
                
                File.WriteAllText(filePath, json);
                Debug.LogFormat($"{playerSettings} saved successfully.");
            }
            catch (IOException ex)
            {
                Debug.LogError($"Failed to save settings: {ex.Message}");
            }
        }

        public PlayerSettings loadSettings()
        {
            if (File.Exists(filePath))
            {
                string settings = File.ReadAllText(filePath);
                Debug.Log($"settings retrieved {settings}");
                PlayerSettings playerSettings = JsonUtility.FromJson<PlayerSettings>(settings);
                return playerSettings;
            }
            else
            {
                Debug.LogWarning("Settings file not found.");
                return null;
            }
            
        }
    }
} 

    


