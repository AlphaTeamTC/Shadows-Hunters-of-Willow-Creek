using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityAnim : MonoBehaviour
{
    public Animator animator;
    public Transform player;

    public float rotationAngle;
    private float triggerDistance = 3.0f;
    private bool isNear = false;
    private Quaternion originalRotation;
    
    public string idleAnimation;
    public string actionAnimation;
    void Start()
    {
        originalRotation = transform.rotation; 
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer < triggerDistance && !isNear)
        {
            isNear = true;
            RotateNPC();
            animator.Play(idleAnimation);
            animator.SetTrigger("nearPlayer");
        }
        else if (distanceToPlayer > triggerDistance && isNear)
        {
            isNear = false;
            animator.ResetTrigger("nearPlayer");
            ResetNPCRotation();
            animator.Play(actionAnimation);
            animator.SetTrigger("farPlayer");
        }
    }

    void RotateNPC()
    {
        transform.Rotate(0, rotationAngle, 0);
    }

    void ResetNPCRotation()
    {
        transform.rotation = originalRotation;
    }
}
