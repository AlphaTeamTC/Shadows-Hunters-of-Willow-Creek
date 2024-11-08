using UnityEngine;

namespace MainMenu.UI
{
    /// <summary>
    /// Class in charge of the main menu UI.
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _backgroundImageCanvasGroup;
        [SerializeField, Space] private CanvasGroup _mainMenuCanvasGroup;
        [SerializeField] private CanvasGroup _settingsCanvasGroup;
        [SerializeField] private CanvasGroup _roomOptionsCanvasGroup;
        [SerializeField] private CanvasGroup _joinRoomCanvasGroup;
        [SerializeField, Space] private CanvasGroup _createRoomCanvasGroup;

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

        private void Start()
        {
            CanvasSetup();
        }

        public void CanvasSetup()
        {
            FadeCanvasGroup(_backgroundImageCanvasGroup, true);
            FadeCanvasGroup(_mainMenuCanvasGroup, true);
            FadeCanvasGroup(_settingsCanvasGroup, false);
            FadeCanvasGroup(_roomOptionsCanvasGroup, false);
            FadeCanvasGroup(_joinRoomCanvasGroup, false);
            FadeCanvasGroup(_createRoomCanvasGroup, false);
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
    }
}