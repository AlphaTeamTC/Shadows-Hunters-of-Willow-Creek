using System;
using System.IO;
using UnityEngine;

namespace MainMenu.UI.Settings
{
    /// <summary>
    /// Class to store the player data
    /// </summary>
    [Serializable]
    public class PlayerData
    {
        public string playerName;

        private static readonly string FILE_PATH = Path.Combine(Application.persistentDataPath, "playerData.json");

        public PlayerData(string name)
        {
            playerName = name;
        }

        /// <summary>
        /// Saves the player data to a json file
        /// </summary>
        /// <param name="name"></param>
        public static void SavePlayerData(string name)
        {
            PlayerData data = new(name);
            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(FILE_PATH, json);
            Debug.Log("Player data saved to: " + FILE_PATH);
        }

        /// <summary>
        /// Loads the player data from a json file
        /// </summary>
        /// <returns></returns>
        public static string LoadPlayerData()
        {
            if (File.Exists(FILE_PATH))
            {
                string json = File.ReadAllText(FILE_PATH);
                PlayerData data = JsonUtility.FromJson<PlayerData>(json);
                Debug.Log("Player data loaded from: " + FILE_PATH);
                return data.playerName;
            }
            else
            {
                Debug.Log("Player data file not found!");
                return String.Empty;
            }
        }
    }
}