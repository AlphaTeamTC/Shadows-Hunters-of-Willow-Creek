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



    [Header("Statistics")]
    Vector3 forwardDirection;


    void Start()
    {
        characterController = GetComponent<CharacterController>();
        pMC = GetComponent<AmbMoveController>();
        pAC = GetComponent<AmbAnimationController>();
    }

    // Update is called once per frame
    void Update()
    {
        InputManagement();

        CalculatePlayerForward();

        pMC.RotateCharacter(forwardDirection);


        // JUMP
        if (Input.GetKeyDown(KeyCode.Space))
        {
            pMC.JumpVerticalVelocity();
            pAC.Jump();
        }
        // FALL
        else
        {
            pMC.FallVerticalVelocity();
        }

        /////////////////////////////////////////////

        // RUN
        if (Input.GetKey(KeyCode.LeftShift) && characterController.isGrounded)
        {
            pMC.Movement(2.5f, forwardDirection);
            pAC.setAnimation(getMovingValue() * 2);
        }

        // WALK OR STAND
        else
        {
            pMC.Movement(1f, forwardDirection);
            pAC.setAnimation(getMovingValue());
        }




    }

    // Functions
    private void InputManagement()
    {
        xAxisInput = Input.GetAxis("Horizontal");
        yAxisInput = Input.GetAxis("Vertical");
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
}
