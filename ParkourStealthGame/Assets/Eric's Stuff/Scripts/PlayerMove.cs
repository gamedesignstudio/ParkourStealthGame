using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //controls the speed in which you move
    private float speed;
    [SerializeField] private float normalSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float crouchSpeed;
    [SerializeField] private float jumpHeight;

    //controls for moving
    [SerializeField] private KeyCode forward;
    [SerializeField] private KeyCode backwards;
    [SerializeField] private KeyCode left;
    [SerializeField] private KeyCode right;
    [SerializeField] private KeyCode jump;
    [SerializeField] private KeyCode sprint;
    [SerializeField] private KeyCode climb;
    [SerializeField] private KeyCode crouch;
    [SerializeField] private KeyCode slide;

    //references to player's components
    private Transform playerTrans;
    private Rigidbody playerRigi;
    private MeshCollider playerCol;
    private GameObject playerObj;
    
    //checks to see state of player
    private bool isJumping;
    private bool isClimbing;
    private bool isSliding;

    //variables used to store checkpoint locations
    public Vector3 respawnLoc;

    private void Awake()
    {
        
    }


    // Start is called before the first frame update
    void Start()
    {
        playerTrans = GetComponent<Transform>();
        playerRigi = GetComponent<Rigidbody>();
        playerCol = GetComponent<MeshCollider>();

        //set the speed to normal (jogging) speed
        speed = normalSpeed;

        playerRigi.useGravity = true;
        //store player's current location as the respawn location
        respawnLoc = playerTrans.position;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeSpeed();
        //call the player move function
        PlayerMovement();

    }

    void FixedUpdate()
    {
        /*
        if (Input.GetKey(forward))
        {

            //move rigidbody forward
            this.transform.position = transform.position + transform.forward * speed * Time.deltaTime;
            // playerRigi.velocity = new Vector3(speed, 0, 0);
        }*/
    }

    private void ChangeSpeed()
    {
        //if crouch set speed to crouchSpeed; if sprint set speet to runSpeed; else speed = normalSpeed
        if (Input.GetKey(crouch))
        {
            speed = crouchSpeed;
        }
        else if (Input.GetKey(sprint))
        {
            speed = runSpeed;
        }
        else
        {
            speed = normalSpeed;
        }


    }

    private void PlayerMovement()
    {
        //float horizInput = Input.GetAxisRaw("Horizontal");
        //float vertInput = Input.GetAxis("Vertical");
        //transform.Translate(horizInput * speed * Time.deltaTime, 0, vertical * speed * Time.deltaTime);
        if (Input.GetKey(forward))
        {
            //move rigidbody forward
            this.transform.position = transform.position + transform.forward * speed * Time.deltaTime;
            // playerRigi.velocity = new Vector3(speed, 0, 0);
        }
        if (Input.GetKey(backwards))
        {
            //move rigidbody forward
            this.transform.position = transform.position + -transform.forward * speed * Time.deltaTime;
        }
        if (Input.GetKey(left))
        {
            //move rigidbody forward
            this.transform.position = transform.position + -transform.right * speed * Time.deltaTime;
        }
        if (Input.GetKey(right))
        {
            //move rigidbody forward
            this.transform.position = transform.position + transform.right * speed * Time.deltaTime;
        }
        

        //create a vector 3 that tells the object where you want to go
        //Vector3 movement = new Vector3(horizInput * speed * Time.deltaTime, 0, vertInput * speed * Time.deltaTime);

        // playerRigi.MovePosition(transform.position + movement);


        JumpInput();

    }

    private bool OnSlope()
    {
        //return Physics.CheckCapsule(playerCol.bounds.center, new Vector3(playerCol.bounds.center.x, playerCol.bounds.min.y, playerCol.bounds.center.z), playerCol.radius);
        return false;
    }

    private void JumpInput()
    {
        if (OnSlope() && Input.GetKeyDown(jump))
        {
            isJumping = true;

            //playerRigi.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);

            //move rigidbody forward
            this.transform.position = transform.position + transform.up * jumpHeight * Time.deltaTime;

            isJumping = false;
        }

    }

    private IEnumerator JumpEvent()
    {
        /* float timeInAir = 0.0f;
         charController.slopeLimit = 90.0f;

         do
         {
             float jumpForce = jumpFallOff.Evaluate(timeInAir);
             charController.Move(Vector3.up * jumpForce * jumpMultiplier * Time.deltaTime);
             timeInAir += Time.deltaTime;

             yield return null;

         } while (!charController.isGrounded && charController.collisionFlags != CollisionFlags.Above);


         charController.slopeLimit = 45.0f;
         isJumping = false;
         */
        return null;
    }


}
