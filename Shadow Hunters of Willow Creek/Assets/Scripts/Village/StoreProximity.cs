using UnityEngine;

namespace Village
{
    /// <summary>
    /// Class that manages the proximity of the player to the store. Used for Nur.
    /// </summary>
    public class StoreProximity : MonoBehaviour
    {
        [SerializeField] private GameObject _player;
        public bool isNearStore;

        private void Update()
        {
            // Find the nearest player with the player tag
            _player = GameObject.FindGameObjectWithTag("Player");

            // Get the player's character controller

            // Calculate the distance between the player and the store
            float distance = Vector3.Distance(_player.transform.position, transform.position);

            // If the player is near the store, set the isNearStore flag to true
            if (distance < 2.5f)
            {
                isNearStore = true;
                // Disable the player's character controller
                _player.GetComponent<CharacterController>().enabled = false;
            }
            else
            {
                isNearStore = false;
                // Enable the player's character controller
                _player.GetComponent<CharacterController>().enabled = true;
            }
        }
    }
}