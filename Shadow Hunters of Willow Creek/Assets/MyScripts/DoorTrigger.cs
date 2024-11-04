using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public GameObject[] doors; // Array de puertas que deben desaparecer
    public int requiredPlayerCount = 2; // Número total de jugadores necesarios (sumados en ambas entradas)
    public Collider entry1; // Primer área de entrada
    public Collider entry2; // Segunda área de entrada

    private HashSet<GameObject> playersInEntry1 = new HashSet<GameObject>();
    private HashSet<GameObject> playersInEntry2 = new HashSet<GameObject>();

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (entry1.bounds.Contains(other.transform.position))
            {
                playersInEntry1.Add(other.gameObject);
                Debug.Log("Jugadores en entrada 1: " + playersInEntry1.Count);
            }
            else if (entry2.bounds.Contains(other.transform.position))
            {
                playersInEntry2.Add(other.gameObject);
                Debug.Log("Jugadores en entrada 2: " + playersInEntry2.Count);
            }
            CheckPlayerCount();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (playersInEntry1.Remove(other.gameObject))
            {
                Debug.Log("Jugadores en entrada 1: " + playersInEntry1.Count);
            }
            else if (playersInEntry2.Remove(other.gameObject))
            {
                Debug.Log("Jugadores en entrada 2: " + playersInEntry2.Count);
            }
        }
    }

    private void CheckPlayerCount()
    {
        int totalPlayers = playersInEntry1.Count + playersInEntry2.Count;
        Debug.Log("Total de jugadores en ambas entradas: " + totalPlayers);
        
        if (totalPlayers >= requiredPlayerCount)
        {
            foreach (GameObject door in doors)
            {
                Destroy(door);
            }
        }
    }
}
