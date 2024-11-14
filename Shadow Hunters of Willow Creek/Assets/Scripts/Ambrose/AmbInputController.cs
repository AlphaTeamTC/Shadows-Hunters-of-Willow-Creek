using Photon.Pun;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Inventory;
using Items;

public class AmbInputController : MonoBehaviourPunCallbacks
{

    [Header("Movement")]
    private float xAxisInput;
    private float yAxisInput;
    [SerializeField] Transform orientation;

    [Header("References")]
    [SerializeField] private AmbMoveController pMC;
    [SerializeField] private AmbAnimationController pAC;
    [SerializeField] private AmbStatistics pS;
    [SerializeField] private PlayerInventory inv; 

    
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

    [Header("Interactions")]
    private Vector3 interactSource;
    private float interactRadius = 1.7f;

    void Start()
    {
        enabled = photonView.IsMine;

        // Set the cursor to be invisible
        Cursor.visible = false;
        // Lock the cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;

        // Get other scripts
        characterController = GetComponentInParent<CharacterController>();
        pMC = GetComponent<AmbMoveController>();
        pAC = GetComponent<AmbAnimationController>();
        pS = GetComponent<AmbStatistics>();

        // Get the camera
        playerCam = Camera.main;

        // Find the cinemachine free look camera
        GameObject cinemachinePrefab = GameObject.Find("3D Camera");
        // Set the camera to follow local player
        if (photonView.IsMine)
        {
            cinemachinePrefab.GetComponentInChildren<Cinemachine.CinemachineFreeLook>().Follow = transform;
            cinemachinePrefab.GetComponentInChildren<Cinemachine.CinemachineFreeLook>().LookAt = transform;
        }
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

        // When pressing Intro, will check if there is an interactable and activate it
        if (Input.GetKeyDown(KeyCode.C)) {
            checkForInteractables();
        }
        
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
            pMC.Movement(pS.Speed,2.5f, forwardDirection, underThePlayer);
            pAC.setAnimation(getMovingValue() * 2);
            momentumSpeed = 2f;
        }

        // If the is walking or standing still
        else
        {
            pMC.Movement(pS.Speed,1f, forwardDirection, underThePlayer);
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
    private void CalculateRaycast()
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
    private void JumpAction()
    {
        isFalling = true;
        pMC.JumpVerticalVelocity();
        pMC.Movement(pS.Speed, momentumSpeed, momentumDirection, underThePlayer);
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

    private void checkForInteractables(){
        // Get the player's position
        interactSource = transform.position + new Vector3(0,1,0);

        // Use Physics.OverlapSphere to find colliders within the specified radius
        Collider[] hitColliders = Physics.OverlapSphere(interactSource, interactRadius);

        // Search for the first object which is an Interactable
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.TryGetComponent(out IInteractable interactObj)) {
                // Call its interaction method
                interactObj.Interact();
                // End searching after the first one
                return;
            }
        }
    }
}




