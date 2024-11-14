using CharacterSelector.UI;
using Gameplay.Spawn;
using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

namespace LocalPhoton
{
    public class PUNPlayerSpawner : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _playerPrefabs;
        private GameObject _localPlayer;
        public GameObject LocalPlayer => _localPlayer;

        private static PUNPlayerSpawner _instance;
        public static PUNPlayerSpawner Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<PUNPlayerSpawner>();
                }
                return _instance;
            }
        }

        private void Start()
        {
            // Check if the player is connected to the server.
            if (PhotonNetwork.IsConnected) SpawnPlayer();
        }

        public void SpawnPlayer()
        {
            if (_localPlayer != null)
            {
                Debug.LogFormat($"*** PUNPlayerSpawner: Player already spawned");
                return;
            }

            Transform spawnPoint = SpawnManager.Instance.GetRandomSpawnPoint();
            // Spawn the player in the scene using Photon.
            _localPlayer = PhotonNetwork.Instantiate(_playerPrefabs[0].name, spawnPoint.position, spawnPoint.rotation);

            Debug.LogFormat($"*** PUNPlayerSpawn: Spawning the player...");
        }
    }
}