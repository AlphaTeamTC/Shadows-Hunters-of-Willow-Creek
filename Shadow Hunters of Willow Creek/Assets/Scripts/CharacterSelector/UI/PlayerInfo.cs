using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CharacterSelector.UI
{
    /// <summary>
    /// Class that manages the player info UI in the character selector.
    /// </summary>
    public class PlayerInfo : MonoBehaviour
    {
        [SerializeField] private TMP_Text _playerName;
        [SerializeField] private TMP_Text _playerCharacterName;

        public string PlayerName { get; set; }
        public string PlayerCharacterName { get; set; }

        /// <summary>
        /// Sets the player info in the UI.
        /// </summary>
        /// <param name="playerName"></param>
        /// <param name="playerCharacterName"></param>
        public void SetPlayerInfo(string playerName, string playerCharacterName)
        {
            _playerName.text = playerName;
            _playerCharacterName.text = playerCharacterName;
                
            PlayerName = playerName;
            PlayerCharacterName = playerCharacterName;
        }
    }
}