using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbAnimationController : MonoBehaviour
{
    [Header("References")]
    private Animator animator;
    [SerializeField] private GameObject ambrose;

    // Start is called before the first frame update
    void Start()
    {
        animator = ambrose.GetComponent<Animator>();
        animator.SetFloat("xSpeed", 0);
        animator.SetFloat("ySpeed", 0);
    }

    public void setAnimation(float speed)
    {
        animator.SetFloat("ySpeed", speed);
    }

    public void Falling()
    {
        animator.SetBool("isFalling", true);
    }

    public void FallToStandAnimation()
    {
        animator.SetBool("isLanding", true);
    }

    public void Crouching()
    {
        animator.SetBool("isCrouching", true);

    }

    public void Standing()
    {
        animator.SetBool("isCrouching", false);

    }
    public void Jump()
    {
        animator.SetBool("isJumping", true);

    }
}


