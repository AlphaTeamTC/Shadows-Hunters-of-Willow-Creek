using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemStoneAttack : MonoBehaviour
{
    public GameObject player;
    public float distanceThreshold = 10f; // Distance at which rocks are launched
    public float launchInterval = 5f; // Time between rock launches
    public GameObject rockPrefab;
    public GameObject Launcher;
    public float launchForce = 10f; // Force applied to launched rocks
    public float maxRockSpeed = 5f; // Maximum speed of launched rocks
    public float rockLifetime = 10f; // Lifetime of launched rocks

    private float timeSinceLastLaunch = 0f;

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

        if (distanceToPlayer > distanceThreshold)
        {
            // Face the player
            Launcher.transform.LookAt(player.transform);

            // Update the timer
            timeSinceLastLaunch += Time.deltaTime;

            // Check if it's time to launch a rock
            if (timeSinceLastLaunch >= launchInterval)
            {
                // Launch a rock
                GameObject rock = Instantiate(rockPrefab, Launcher.transform.position, Launcher.transform.rotation);
                Rigidbody rockRb = rock.GetComponent<Rigidbody>();

                // Apply force in the direction the Launcher is facing
                rockRb.AddForce(Launcher.transform.forward * launchForce, ForceMode.Impulse);

                // Clamp the rock's velocity to the maximum speed
                rockRb.velocity = Vector3.ClampMagnitude(rockRb.velocity, maxRockSpeed);

                // Destroy the rock after its lifetime
                Destroy(rock, rockLifetime);

                // Reset the timer
                timeSinceLastLaunch = 0f;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == player)
        {
            // WIP: Implement player damage logic here
            Debug.Log("Player hit by rock!");
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}