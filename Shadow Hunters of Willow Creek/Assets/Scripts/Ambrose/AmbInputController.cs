using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbInputController : MonoBehaviour
{

    [Header("Movement")]
    private float xAxisInput;
    private float yAxisInput;
    public Transform orientation;

    [Header("References")]
    [SerializeField] private AmbMoveController pMC;
    [SerializeField] private AmbAnimationController pAC;

    private CharacterController characterController;
    public Camera playerCam;


    [Header("States")]
    public bool isFalling;
    public bool doingAFullAction;
    public bool isCrouching;
    public bool isJumping;

    [Header("Statistics")]
    Vector3 forwardDirection;

    private RaycastHit underThePlayer;
    private bool isGroundNear;

    Vector3 momentumDirection;
    float momentumSpeed;


    void Start()
    {
        // Set the cursor to be invisible
        Cursor.visible = false;
        // Optionally, lock the cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;


        characterController = GetComponentInParent<CharacterController>();
        pMC = GetComponent<AmbMoveController>();
        pAC = GetComponent<AmbAnimationController>();

        doingAFullAction = false;
        isFalling = false;
        isCrouching = false;
    }

    // Update is called once per frame
    void Update()
    {
        InputManagement();

        CalculatePlayerForward();

        CalculateRaycast();

        pMC.RotateCharacter(forwardDirection);

        pMC.FallVerticalVelocity();


        if (Input.GetKey(KeyCode.Space) && characterController.isGrounded)
        {
            pAC.Jump();
            doingAFullAction = true;
        }


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


        // If is falling

        if (!characterController.isGrounded && !isGroundNear)
        {
            isFalling = true;
            doingAFullAction = true;
            pAC.Falling();

        }

        // If was falling and reach the ground

        if (characterController.isGrounded && isFalling)
        {
            isFalling = false;
            doingAFullAction = false;
            pAC.FallToStandAnimation();
        }

        
        momentumDirection = forwardDirection;
        // RUN
        if (Input.GetKey(KeyCode.LeftShift) && !isCrouching)
        {
            pMC.Movement(2.5f, forwardDirection, underThePlayer);
            pAC.setAnimation(getMovingValue() * 2);
            momentumSpeed = 2f;
        }

        // WALK OR STAND
        else
        {
            pMC.Movement(1f, forwardDirection, underThePlayer);
            pAC.setAnimation(getMovingValue());
            momentumSpeed = 1f;
        }
        





    }

    // Functions
    private void InputManagement()
    {
        xAxisInput = Input.GetAxis("Horizontal");
        yAxisInput = Input.GetAxis("Vertical");
    }

    public void CalculateRaycast()
    {
        isGroundNear = Physics.Raycast(transform.position + Vector3.up * 0.05f, Vector3.down, out underThePlayer, characterController.height / 2 * 1.2f);
    }
    private void CalculatePlayerForward()
    {
        Vector3 viewDir = transform.position - new Vector3(playerCam.transform.position.x, transform.position.y, playerCam.transform.position.z);

        orientation.forward = viewDir.normalized;

        forwardDirection = orientation.forward * yAxisInput + orientation.right * xAxisInput;
    }
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

    public void JumpAction()
    {
        isFalling = true;
        pMC.JumpVerticalVelocity();
        pMC.Movement(momentumSpeed, momentumDirection, underThePlayer);
    }
}
