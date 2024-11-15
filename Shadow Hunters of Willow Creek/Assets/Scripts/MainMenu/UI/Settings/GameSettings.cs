using System.IO;
using UnityEngine;

namespace MainMenu.UI.Settings
{
    /// <summary>
    /// Class that gets the JSON for the settings
    /// </summary>
    public class GameSettings
    {
        public int resolutionIndex;
        public float volume;

        private static readonly string FILE_PATH = Path.Combine(Application.persistentDataPath, "gameSettings.json");

        public GameSettings(float volume, int resolutionIndex)
        {
            this.volume = volume;
            this.resolutionIndex = resolutionIndex;
        }

        /// <summary>
        /// Save the settings into a JSON file.
        /// </summary>
        /// <param name="gameSettings"></param>
        public static void SaveGameSettings(GameSettings gameSettings)
        {
            string json = JsonUtility.ToJson(gameSettings);
            File.WriteAllText(FILE_PATH, json);
            Debug.Log("Game settings saved successfully to " + FILE_PATH);
        }

        /// <summary>
        /// Load the settings from a JSON file.
        /// </summary>
        /// <returns></returns>
        public static GameSettings LoadGameSettings()
        {
            if (File.Exists(FILE_PATH))
            {
                string json = File.ReadAllText(FILE_PATH);
                GameSettings settings = JsonUtility.FromJson<GameSettings>(json);
                Debug.Log("Game settings loaded from " + FILE_PATH);
                return settings;
            }

            Debug.Log("Settings file not found. Using defaults");
            return new GameSettings(0.5f, 0);
        }
    }
}