using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Prefab del enemigo
    public int poolSize = 10; // Tamaño del pool de objetos
    public float spawnInterval = 3f; // Intervalo de tiempo entre spawns
    public Transform[] spawnPoints; // Puntos de spawn disponibles

    private List<GameObject> enemyPool;
    private float nextSpawnTime;
    private int currentWave = 0;
    public int enemiesPerWave = 5; // Cantidad de enemigos por oleada
    public float waveInterval = 10f; // Intervalo entre oleadas
    private bool waveInProgress = false;

    void Start()
    {
        // Inicializa el pool de enemigos
        enemyPool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab);
            enemy.SetActive(false); // Desactiva el objeto para reutilización
            enemyPool.Add(enemy);
        }
        StartCoroutine(StartWave());
    }

    void Update()
    {
        if (waveInProgress && Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    private IEnumerator StartWave()
    {
        while (true)
        {
            currentWave++;
            Debug.Log("Iniciando oleada: " + currentWave);
            int spawnedEnemies = 0;
            waveInProgress = true;

            while (spawnedEnemies < enemiesPerWave)
            {
                SpawnEnemy();
                spawnedEnemies++;
                yield return new WaitForSeconds(spawnInterval);
            }

            waveInProgress = false;
            Debug.Log("Oleada " + currentWave + " completada.");
            yield return new WaitForSeconds(waveInterval); // Espera antes de la siguiente oleada
        }
    }

    void SpawnEnemy()
    {
        foreach (GameObject enemy in enemyPool)
        {
            if (!enemy.activeInHierarchy) // Encuentra un enemigo inactivo en el pool
            {
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                enemy.transform.position = spawnPoint.position;
                enemy.transform.rotation = spawnPoint.rotation;
                enemy.SetActive(true); // Activa el enemigo
                return;
            }
        }

        Debug.Log("No hay enemigos disponibles en el pool."); // Mensaje de depuración si no hay enemigos inactivos
    }
}

