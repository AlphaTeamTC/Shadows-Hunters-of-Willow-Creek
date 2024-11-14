using Photon.Pun;
using UnityEngine;

namespace CharacterSelector.UI
{
    /// <summary>
    /// Class that manages the UI of the character selector.
    /// </summary>
    public class UIManagerCharacterSelector : MonoBehaviour
    {
        [SerializeField] private GameObject _buttonStartGame;
        [SerializeField] private GameObject _buttonSelectCharacter;

        public GameObject ButtonStartGame => _buttonStartGame;

        private static UIManagerCharacterSelector _instance;
        public static UIManagerCharacterSelector Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<UIManagerCharacterSelector>();
                }
                return _instance;
            }
        }

        private void Start()
        {
            PlayerCreator.Instance.CreatePlayersInRoom(PhotonNetwork.PlayerList);

            if (PhotonNetwork.IsMasterClient)
            {
                _buttonStartGame.SetActive(true);
            }
            else
            {
                _buttonStartGame.SetActive(false);
            }
        }

        public void StartGame()
        {
            PhotonNetwork.LoadLevel("Village Menu");
        }
    }
}