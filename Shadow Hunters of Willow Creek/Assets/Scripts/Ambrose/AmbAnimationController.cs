using Photon.Pun;
using UnityEngine;

public class AmbAnimationController : MonoBehaviourPunCallbacks
{
    [Header("References")]
    private Animator animator;
    [SerializeField] private GameObject ambrose;

    // Initialize the animator and initial values
    void Start()
    {
        enabled = photonView.IsMine;

        animator = ambrose.GetComponent<Animator>();
        animator.SetFloat("xSpeed", 0);
        animator.SetFloat("ySpeed", 0);
    }

    // Set the walking/running animations
    public void setAnimation(float speed)
    {
        animator.SetFloat("ySpeed", speed);
    }

    // Set the falling animation
    public void Falling()
    {
        animator.SetBool("isFalling", true);
    }

    // Set the landing animation when ends falling
    public void FallToStandAnimation()
    {
        animator.SetBool("isLanding", true);
    }

    // Set the crouching animation
    public void Crouching()
    {
        animator.SetBool("isCrouching", true);

    }

    // Set the crouching animation false
    public void Standing()
    {
        animator.SetBool("isCrouching", false);

    }

    // Set the jumping animation
    public void Jump()
    {
        animator.SetBool("isJumping", true);

    }

    // Set the damage reaction animation
    public void TakeDamage(){
        animator.Play("TakeDamage");
    }

    // Set the dying animation
    public void Die(){
        animator.SetBool("isDead", true);
    }
}


