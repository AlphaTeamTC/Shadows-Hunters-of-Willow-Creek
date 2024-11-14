using UnityEngine;

namespace Gameplay.Spawn
{
    public class SpawnManager : MonoBehaviour
    {
        [SerializeField] private Transform[] _spawnPoints;

        // Singletoning the SpawnManager
        private static SpawnManager _instance;
        public static SpawnManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<SpawnManager>();
                }
                if (_instance == null)
                {
                    _instance = new GameObject("SpawnManager").AddComponent<SpawnManager>();
                }
                return _instance;
            }
        }

        /// <summary>
        /// Get a random spawn point
        /// </summary>
        /// <returns></returns>
        public Transform GetRandomSpawnPoint()
        {
            return _spawnPoints[Random.Range(0, _spawnPoints.Length)];
        }

        private void Start()
        {
            // Deactivate all spawn points just in case
            foreach (Transform spawnPoint in _spawnPoints)
            {
                spawnPoint.gameObject.SetActive(false);
            }
        }
    }
}
