using TMPro;
using UnityEngine;

namespace MainMenu.UI
{
    /// <summary>
    /// Class in charge of the main menu UI.
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _gameTitleCanvasGroup;
        [SerializeField, Space] private CanvasGroup _mainMenuCanvasGroup;
        [SerializeField, Space] private CanvasGroup _settingsCanvasGroup;
        [SerializeField, Space] private CanvasGroup _roomOptionsCanvasGroup;
        [SerializeField, Space] private CanvasGroup _joinedRoomCanvasGroup;
        [SerializeField] private TMP_Text _joinedRoomName;
        [SerializeField, Space] private CanvasGroup _createRoomCanvasGroup;
        [SerializeField, Space] private CanvasGroup _loadingCanvasGroup;
        [SerializeField] private TMP_Text _loadingText;
        [SerializeField, Space] private CanvasGroup _errorCanvasGroup;
        [SerializeField] private TMP_Text _errorText;

        // Singletoning the UIManager
        private static UIManager _instance;
        public static UIManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<UIManager>();
                }

                return _instance;
            }
        }

        public delegate void OnCreateRoom(string roomName);
        public OnCreateRoom OnCreateRoomEvent;

        public delegate void OnLeaveRoom();
        public OnLeaveRoom OnLeaveRoomEvent;

        public void CanvasSetup()
        {
            FadeCanvasGroup(_gameTitleCanvasGroup, true);
            FadeCanvasGroup(_mainMenuCanvasGroup, false);
            FadeCanvasGroup(_settingsCanvasGroup, false);
            FadeCanvasGroup(_roomOptionsCanvasGroup, false);
            FadeCanvasGroup(_joinedRoomCanvasGroup, false);
            FadeCanvasGroup(_createRoomCanvasGroup, false);
            FadeCanvasGroup(_loadingCanvasGroup, false);
            FadeCanvasGroup(_errorCanvasGroup, false);
        }

        /// <summary>
        /// Fades a CanvasGroup in or out.
        /// </summary>
        /// <param name="canvasGroup"></param>
        /// <param name="active"></param>
        /// <param name="duration"></param>
        public void FadeCanvasGroup(CanvasGroup canvasGroup, bool active, float duration = 0.4f)
        {
            float targetAlpha = active ? 1 : 0;

            // If fading in, enable the canvas group
            if (active)
            {
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
            }

            // Use LeanTween to fade the canvas group
            LeanTween.alphaCanvas(canvasGroup, targetAlpha, duration).setOnComplete(() =>
            {
                // If fading out, disable the canvas group
                if (!active)
                {
                    canvasGroup.interactable = false;
                    canvasGroup.blocksRaycasts = false;
                }
            });
        }

        /// <summary>
        /// Sets the game title canvas group.
        /// </summary>
        /// <param name="active"></param>
        public void SetGameTitleCanvasGroup(bool active)
        {
            FadeCanvasGroup(_gameTitleCanvasGroup, active);
        }

        /// <summary>
        /// Sets the main menu canvas group.
        /// </summary>
        /// <param name="active"></param>
        public void SetMainMenuCanvasGroup(bool active)
        {
            FadeCanvasGroup(_mainMenuCanvasGroup, active);
        }

        /// <summary>
        /// Sets the settings canvas group.
        /// </summary>
        /// <param name="active"></param>
        public void SetSettingsCanvasGroup(bool active)
        {
            FadeCanvasGroup(_settingsCanvasGroup, active);
        }

        /// <summary>
        /// Sets the room options canvas group.
        /// </summary>
        /// <param name="active"></param>
        public void SetRoomOptionsCanvasGroup(bool active)
        {
            FadeCanvasGroup(_roomOptionsCanvasGroup, active);
        }

        /// <summary>
        /// Sets the join room canvas group.
        /// </summary>
        /// <param name="active"></param>
        public void SetJoinedRoomCanvasGroup(bool active, string roomName = "")
        {
            _joinedRoomName.text = roomName;
            FadeCanvasGroup(_joinedRoomCanvasGroup, active);
        }

        /// <summary>
        /// Sets the create room canvas group.
        /// </summary>
        /// <param name="active"></param>
        public void SetCreateRoomCanvasGroup(bool active)
        {
            FadeCanvasGroup(_createRoomCanvasGroup, active);
        }

        public void SetLoadingCanvasGroup(bool active, string loadingText = "Loading...")
        {
            _loadingText.text = loadingText;
            FadeCanvasGroup(_loadingCanvasGroup, active);
        }

        public void SetErrorCanvasGroup(bool active, string errorText = "Error : ")
        {
            _errorText.text = errorText;
            FadeCanvasGroup(_errorCanvasGroup, active);
        }
    }
}