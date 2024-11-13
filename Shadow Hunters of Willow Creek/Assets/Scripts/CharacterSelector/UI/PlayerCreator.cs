using UnityEngine;

namespace CharacterSelector.UI
{
    public class PlayerCreator : MonoBehaviour
    {
        [SerializeField] private PlayerInfo _playerPrefab;
        [SerializeField] private Transform _playerParent;

        // Singletoning the PlayerCreator
        private static PlayerCreator _instance;
        public static PlayerCreator Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<PlayerCreator>();
                }

                if (_instance == null)
                {
                    // Create a new GameObject with the PlayerCreator component
                    GameObject go = new("PlayerCreator");
                    go.AddComponent<PlayerCreator>();
                    _instance = go.GetComponent<PlayerCreator>();
                }

                return _instance;
            }
        }

        /// <summary>
        /// Creates the players in the room.
        /// </summary>
        /// <param name="playerList"></param>
        public void CreatePlayersInRoom(Photon.Realtime.Player[] playerList)
        {
            // Clean the player parent from previous players
            foreach (Transform child in _playerParent)
            {
                Destroy(child.gameObject);
            }

            foreach (var player in playerList)
            {
                /// Instantiate the player prefab and set the player info
                PlayerInfo p = Instantiate(_playerPrefab, _playerParent);

                p.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
                p.transform.localScale = Vector3.one;

                p.SetPlayerInfo(player.NickName, "Character name");
            }
        }

        /// <summary>
        /// Adds a new player to the room.
        /// </summary>
        /// <param name="player"></param>
        public void AddPlayer(Photon.Realtime.Player player)
        {
            PlayerInfo p = Instantiate(_playerPrefab, _playerParent);

            p.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            p.transform.localScale = Vector3.one;

            p.SetPlayerInfo(player.NickName, "Ambrose");
        }

        /// <summary>
        /// Removes a player from the room.
        /// </summary>
        /// <param name="player"></param>
        public void RemovePlayer(Photon.Realtime.Player player)
        {
            foreach (Transform child in _playerParent)
            {
                if (child.GetComponent<PlayerInfo>().PlayerName == player.NickName)
                {
                    Destroy(child.gameObject);
                    break;
                }
            }
        }
    }
}