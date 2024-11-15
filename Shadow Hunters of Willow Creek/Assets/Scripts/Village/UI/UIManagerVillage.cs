using UnityEngine;

namespace Village.UI
{
    /// <summary>
    /// Class that manages the UI of the village.
    /// </summary>
    public class UIManagerVillage : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _hudCanvasGroup;
        [SerializeField, Space] private CanvasGroup _storeCanvasGroup;
        [SerializeField] private GameObject _storeCamera;
        [SerializeField, Space] private CanvasGroup _inventoryCanvasGroup;

        private GameObject _camera;
        private GameObject _player;

        private StoreProximity _storeProximity;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            // Find the main camera
            _camera = GameObject.FindGameObjectWithTag("MainCamera");

            // Disable store camera game object at the start of the game
            _storeCamera.SetActive(false);

            _storeProximity = FindObjectOfType<StoreProximity>();

            // Show the HUD and inventory UI at the start of the game
            CanvasVisibilityManager(_hudCanvasGroup, true);
            CanvasVisibilityManager(_inventoryCanvasGroup, true);

            // Hide the store UI at the start of the game
            CanvasVisibilityManager(_storeCanvasGroup, false);
        }

        private void Update()
        {
            // Find the nearest player with the player tag
            _player = _storeProximity.player;

            if (_storeProximity.isNearStore)
            {
                // If the player is near the store, show the store UI by pressing the E key
                if (Input.GetKeyDown(KeyCode.E))
                {
                    // Hide the HUD
                    CanvasVisibilityManager(_hudCanvasGroup, false);

                    // Hide the inventory UI
                    CanvasVisibilityManager(_inventoryCanvasGroup, false);

                    // Show the store UI
                    CanvasVisibilityManager(_storeCanvasGroup, true);

                    // Disable main camera
                    _camera.SetActive(false);

                    // Enable store camera
                    _storeCamera.SetActive(true);

                    // Free the cursor
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;

                    // Get the player's character controller
                    _player.GetComponent<CharacterController>().enabled = false;
                }
            }
        }

        /// <summary>
        /// When the exit store button is clicked, hide the store UI and show the HUD and inventory UI.
        /// </summary>
        public void OnExitStoreButtonClicked()
        {
            CanvasVisibilityManager(_hudCanvasGroup, true);
            CanvasVisibilityManager(_inventoryCanvasGroup, true);
            CanvasVisibilityManager(_storeCanvasGroup, false);

            _storeCamera.SetActive(false);

            _camera.SetActive(true);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            _player.GetComponent<CharacterController>().enabled = true;
        }

        /// <summary>
        /// Manages the visibility of the canvas.
        /// </summary>
        /// <param name="canvasGroup"></param>
        /// <param name="visible"></param>
        private void CanvasVisibilityManager(CanvasGroup canvasGroup, bool visible)
        {
            canvasGroup.alpha = visible ? 1 : 0;
            canvasGroup.blocksRaycasts = visible;
            canvasGroup.interactable = visible;
        }
    }
}