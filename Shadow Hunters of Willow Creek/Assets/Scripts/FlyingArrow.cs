using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingArrow : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    // Update is called once per frame
    void Update()
    {
        // Get the current local position
        Vector3 localPosition = transform.localPosition;

        // Increment the x position by 0.1f
        localPosition.y -= speed;

        // Set the new local position
        transform.localPosition = localPosition;

        if (gameObject.transform.localPosition.y < -30)
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);

        if (collision.collider.tag == "Enemy")
        {
            Debug.Log("Hola");
        }
    }
}
