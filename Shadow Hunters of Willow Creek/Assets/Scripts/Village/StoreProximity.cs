using UnityEngine;

namespace Village
{
    /// <summary>
    /// Class that manages the proximity of the player to the store. Used for Nur.
    /// </summary>
    public class StoreProximity : MonoBehaviour
    {
        public GameObject player;
        public bool isNearStore;

        private void Update()
        {
            // Find the nearest player with the player tag
            player = GameObject.FindGameObjectWithTag("Player");

            // Get the player's character controller

            // Calculate the distance between the player and the store
            float distance = Vector3.Distance(player.transform.position, transform.position);

            // If the player is near the store, set the isNearStore flag to true
            if (distance < 2.5f)
            {
                isNearStore = true;
            }
            else
            {
                isNearStore = false;
            }
        }
    }
}