using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove2 : MonoBehaviour
{
    //Variable Names for Unity's Input Manager
    [SerializeField] private string verticalInputName;
    [SerializeField] private string horizontalInputName;

    //Floats for the speed of walking and running, as well as
    //floats for the acceleration and decceleration
    [SerializeField] private float walkSpeed, runSpeed;
    [SerializeField] private float runBuildUp, runSlowDown;

    //Variable for forward movement ('W') key;
    [SerializeField] private KeyCode runKey;
    //Movement Speed as float
    private float movementSpeed;
    //Floats that determine the slope of a plane and
    //the length of the Ray when cast towards the sloped plane
    [SerializeField] private float slopeForce;
    [SerializeField] private float slopeForceRayLength;

    private Vector3 moveDirection;
    public float jumpForce;
    public float gravityScale;

    //Character Controller
    private CharacterController charController;




    // Start is called before the first frame update
    private void Awake()
    {
        charController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        float vertInput = Input.GetAxisRaw(verticalInputName);
        float horizInput = Input.GetAxisRaw(horizontalInputName);

        Vector3 forwardMovement = transform.forward * vertInput;
        Vector3 rightMovement = transform.right * horizInput;

        charController.SimpleMove(Vector3.ClampMagnitude(forwardMovement + rightMovement, 1.0f) * movementSpeed);

        // if ((vertInput != 0 || horizInput != 0) && OnSlope())
        // charController.Move(Vector3.down * charController.height / 2 * slopeForce * Time.deltaTime);
        SetMovementSpeed();

       
    }

    private void SetMovementSpeed()
    {
        if (Input.GetKey(runKey))
            movementSpeed = Mathf.Lerp(movementSpeed, runSpeed, Time.deltaTime * runBuildUp);
        else
            movementSpeed = Mathf.Lerp(movementSpeed, walkSpeed, Time.deltaTime * runSlowDown);
    }

}
