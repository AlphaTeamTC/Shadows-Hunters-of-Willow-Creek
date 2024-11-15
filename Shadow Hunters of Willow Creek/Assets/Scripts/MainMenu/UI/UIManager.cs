using System;
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
        [SerializeField, Space] private CanvasGroup _createRoomCanvasGroup;
        [SerializeField] private TMP_InputField _createdRoomNameInputField;
        [SerializeField, Space] private CanvasGroup _playerNameCanvasGroup;
        [SerializeField] private TMP_InputField _playerNameInputField;
        [SerializeField, Space] private CanvasGroup _roomListCanvasGroup;
        [SerializeField, Space] private CanvasGroup _loadingCanvasGroup;
        [SerializeField] private TMP_Text _loadingText;
        [SerializeField, Space] private CanvasGroup _errorCanvasGroup;
        [SerializeField] private TMP_Text _errorText;
        [SerializeField, Space] private GameObject _buttonSubmitNickname;

        public GameObject ButtonSubmitNickname => _buttonSubmitNickname;

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

        public delegate void OnSubmitNickname(string playerName);
        public OnSubmitNickname OnSubmitNicknameEvent;

        public void CanvasSetup()
        {
            FadeCanvasGroup(_gameTitleCanvasGroup, true);
            FadeCanvasGroup(_mainMenuCanvasGroup, false);
            FadeCanvasGroup(_settingsCanvasGroup, false);
            FadeCanvasGroup(_roomOptionsCanvasGroup, false);
            FadeCanvasGroup(_createRoomCanvasGroup, false);
            FadeCanvasGroup(_roomListCanvasGroup, false);
            FadeCanvasGroup(_playerNameCanvasGroup, false);
            FadeCanvasGroup(_loadingCanvasGroup, false);
            FadeCanvasGroup(_errorCanvasGroup, false);
        }

        /// <summary>
        /// Fades a CanvasGroup in or out.
        /// </summary>
        /// <param name="canvasGroup"></param>
        /// <param name="active"></param>
        /// <param name="duration"></param>
        public void FadeCanvasGroup(CanvasGroup canvasGroup, bool active, float duration = 0.3f)
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
        /// Sets the create room canvas group.
        /// </summary>
        /// <param name="active"></param>
        public void SetCreateRoomCanvasGroup(bool active)
        {
            FadeCanvasGroup(_createRoomCanvasGroup, active);
        }

        /// <summary>
        /// Sets the player name canvas group.
        /// </summary>
        /// <param name="active"></param>
        public void SetPlayerNameCanvasGroup(bool active)
        {
            FadeCanvasGroup(_playerNameCanvasGroup, active);
        }

        /// <summary>
        /// Sets the room list canvas group.
        /// </summary>
        /// <param name="active"></param>
        public void SetRoomListCanvasGroup(bool active)
        {
            FadeCanvasGroup(_roomListCanvasGroup, active);
        }

        /// <summary>
        /// Sets the loading canvas group.
        /// </summary>
        /// <param name="active"></param>
        /// <param name="loadingText"></param>
        public void SetLoadingCanvasGroup(bool active, string loadingText = "Loading...")
        {
            _loadingText.text = loadingText;
            FadeCanvasGroup(_loadingCanvasGroup, active);
        }

        /// <summary>
        /// Sets the error canvas group.
        /// </summary>
        /// <param name="active"></param>
        /// <param name="errorText"></param>
        public void SetErrorCanvasGroup(bool active, string errorText = "Error : ")
        {
            _errorText.text = errorText;
            FadeCanvasGroup(_errorCanvasGroup, active);
        }

        /// <summary>
        /// Action when the create room button is clicked
        /// </summary>
        public void ButtonOnCreateRoomClicked()
        {
            if (String.IsNullOrEmpty(_createdRoomNameInputField.text))
            {
                Debug.LogErrorFormat($"*** UIManager: Room Name is empty!");
                return;
            }

            OnCreateRoomEvent?.Invoke(_createdRoomNameInputField.text);
        }

        /// <summary>
        /// Action when the leave room button is clicked
        /// </summary>
        public void ButtonLeaveRoomClicked()
        {
            OnLeaveRoomEvent?.Invoke();
        }

        /// <summary>
        /// Action when the close error on the Error canvas button is clicked
        /// </summary>
        public void ButtonCloseErrorCanvasGroup()
        {
            FadeCanvasGroup(_errorCanvasGroup, false);
        }

        /// <summary>
        /// Action when the submit nickname button is clicked
        /// </summary>
        public void ButtonSubmitNicknameClicked()
        {
            if (String.IsNullOrEmpty(_playerNameInputField.text))
            {
                Debug.LogErrorFormat($"*** Player name is empty!");
                return;
            }

            OnSubmitNicknameEvent?.Invoke(_playerNameInputField.text);
        }

        /// <summary>
        /// Clears the room name input field
        /// </summary>
        public void ClearRoomNameInputField()
        {
            _createdRoomNameInputField.text = String.Empty;
        }
    }
}