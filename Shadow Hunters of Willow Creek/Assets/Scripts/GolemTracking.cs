using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemTracking : MonoBehaviour
{
    public GameObject player;
    public float velocidad = 1.0f;
    public float distancia = 1.0f;
    public float distanciaAtaque = 1.0f;
    public float tiempoAtaque = 1.0f;
    public float tiempoAtaqueActual = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    void MeleeAttack()
    {
        // WIP: Implement melee attack logic here
        Debug.Log("Melee attack!");

        
    }

    // Update is called once per frame
    void Update()
    {
        float distanciaPlayer = Vector3.Distance(player.transform.position, transform.position);
        if (distanciaPlayer < distancia)
        {
            transform.LookAt(player.transform);
            transform.position += transform.forward * velocidad * Time.deltaTime;
            if (distanciaPlayer < distanciaAtaque)
            {
                tiempoAtaqueActual += Time.deltaTime;
                if (tiempoAtaqueActual > tiempoAtaque)
                {
                    tiempoAtaqueActual = 0.0f;
                    MeleeAttack();
                }
            }
        }
    }
}