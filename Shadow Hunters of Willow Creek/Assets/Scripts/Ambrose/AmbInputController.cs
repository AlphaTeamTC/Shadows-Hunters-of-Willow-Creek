using UnityEngine;

public class AmbInputController : MonoBehaviour
{

    [Header("Movement")]
    private float xAxisInput;
    private float yAxisInput;
    [SerializeField] Transform orientation;

    [Header("References")]
    [SerializeField] private AmbMoveController pMC;
    [SerializeField] private AmbAnimationController pAC;
    [SerializeField] private AmbStatistics pS;

    
    private CharacterController characterController;
    public Camera playerCam;


    [Header("States")]
    public bool isFalling = false;
    public bool doingAFullAction = false;
    public bool isCrouching = false;
    public bool isJumping = false;
    public bool isDead = false;

    [Header("Statistics")]
    Vector3 forwardDirection;
    Vector3 momentumDirection;
    float momentumSpeed;

    private RaycastHit underThePlayer;
    private bool isGroundNear;



    void Start()
    {
        // Set the cursor to be invisible
        Cursor.visible = false;
        // Lock the cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;

        // Get other scripts
        characterController = GetComponentInParent<CharacterController>();
        pMC = GetComponent<AmbMoveController>();
        pAC = GetComponent<AmbAnimationController>();
        pS = GetComponent<AmbStatistics>();
    }

    void Update()
    {
        // Receives the inputs from the WASD
        InputManagement();

        // Stores a vector in the direction of the looking model
        CalculatePlayerForward();

        // Determines how far from the ground the player is
        CalculateRaycast();

        // Rotate the character in the direction of the inputs
        pMC.RotateCharacter(forwardDirection);

        // Acts as gravity for the player
        pMC.FallVerticalVelocity();


        // If the character is in the ground, it can jump
        if (Input.GetKey(KeyCode.Space) && characterController.isGrounded)
        {
            pAC.Jump();
            doingAFullAction = true;
        }

        // The character switches between crouching and standing with control key
        if(Input.GetKey(KeyCode.LeftControl))
        {
            pAC.Crouching();
            isCrouching = true;
        }
        else if (isCrouching)
        {

            pAC.Standing();
            isCrouching = false;
        }


        // If the character is falling
        if (!characterController.isGrounded && !isGroundNear)
        {
            isFalling = true;
            doingAFullAction = true;
            pAC.Falling();
        }

        // If was falling and reaches the ground
        if (characterController.isGrounded && isFalling)
        {
            isFalling = false;
            doingAFullAction = false;
            pAC.FallToStandAnimation();
        }
        
        // Stores the direction of the last movement
        momentumDirection = forwardDirection;

        // If the is running 
        if (Input.GetKey(KeyCode.LeftShift) && !isCrouching)
        {
            pMC.Movement(2.5f, forwardDirection, underThePlayer);
            pAC.setAnimation(getMovingValue() * 2);
            momentumSpeed = 2f;
        }

        // If the is walking or standing still
        else
        {
            pMC.Movement(1f, forwardDirection, underThePlayer);
            pAC.setAnimation(getMovingValue());
            momentumSpeed = 1f;
        }
    
    }


    // Functions

    // Receives values from the WASD
    private void InputManagement()
    {
        if (!isDead)
        {
            xAxisInput = Input.GetAxis("Horizontal");
            yAxisInput = Input.GetAxis("Vertical");
        }
        else
        {
            xAxisInput = 0;
            yAxisInput = 0;
        }
    }

    // Stores the distance to the ground
    public void CalculateRaycast()
    {
        isGroundNear = Physics.Raycast(transform.position + Vector3.up * 0.05f, Vector3.down, out underThePlayer, characterController.height / 2 * 1.2f);
    }

    // Stores a vector with the directions the model is facing
    private void CalculatePlayerForward()
    {
        Vector3 viewDir = transform.position - new Vector3(playerCam.transform.position.x, transform.position.y, playerCam.transform.position.z);

        orientation.forward = viewDir.normalized;

        forwardDirection = orientation.forward * yAxisInput + orientation.right * xAxisInput;
    }

    // Returns the absolute value of speed, regardless of its direction
    private float getMovingValue()
    {

        if (Mathf.Abs(xAxisInput) > Mathf.Abs(yAxisInput))
        {
            return Mathf.Abs(xAxisInput);
        }
        else
        {
            return Mathf.Abs(yAxisInput);
        }
    }

    // An event called by the animation that sets the speed and direction for a jump
    public void JumpAction()
    {
        isFalling = true;
        pMC.JumpVerticalVelocity();
        pMC.Movement(momentumSpeed, momentumDirection, underThePlayer);
    }

    // If the character takes damage it should update data and play animation
    public void TakeDamage(float damage){
        if(pS.Damage(damage) <= 0)
        {
            pAC.Die();
            isDead = true;
        }
        else
        {
            pAC.TakeDamage();
        }
    }

    // If the character heals the statistics should update
    public void Heal(float health){
        pS.Heal(health);
    }
}




