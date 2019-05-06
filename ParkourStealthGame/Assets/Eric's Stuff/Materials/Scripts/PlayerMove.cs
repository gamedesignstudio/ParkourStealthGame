using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    //controls the speed in which you move
    private float speed;
    [SerializeField] private float normalSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float crouchSpeed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float climbingSpeed;

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
    private bool isGrounded;
    private bool isClimbing;
    private bool isCrouching;
    private bool isFalling;
    private int choice;

    //variables used to store checkpoint locations
    private Vector3 respawnLoc;

    //Variables for climbing
    [SerializeField] private float climbSpeed;
    [SerializeField] private KeyCode ascend;
    [SerializeField] private KeyCode decend;
    [SerializeField] private float maxReach;
    private Vector3 pointLocation;

    //Variables for Finishing level
    [SerializeField] private Scene SceneToLoad;

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

        Random.seed = System.DateTime.Now.Millisecond;

        isGrounded = false;
        isClimbing = false;
        isCrouching = false;
        isFalling = false;

        //store player's current location as the respawn location
        respawnLoc = playerTrans.position;
    }

    // Update is called once per frame
    void Update()
    {
        //alter speed if crouching or sprinting
        ChangeSpeed();

        //call the player move function
        PlayerMovement();

        //check to see if you're climbing
        ClimbingCheck();

    }

    void FixedUpdate()
    {
        if(isClimbing && pointLocation != transform.position) {
            playerRigi.position = Vector3.MoveTowards(transform.position, pointLocation, climbingSpeed);
        }
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
        if (Input.GetKey(crouch) && isGrounded)
        {
            speed = crouchSpeed;
            isCrouching = true;
        }
        else if(Input.GetKey(crouch) && !isGrounded) {
            speed = normalSpeed;
            isCrouching = true;
        }
        else if (Input.GetKey(sprint))
        {
            speed = runSpeed;
            isCrouching = false;
        }
        else
        {
            speed = normalSpeed;
            isCrouching = false;
        }


    }

    private void PlayerMovement()
    {
        //make sure player is not climbing
        if (!isClimbing) {
            if (Input.GetKey(forward))
            {
                //move rigidbody forward
                playerRigi.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);
            }
            if (Input.GetKey(backwards))
            {
                //move rigidbody backwards
                playerRigi.MovePosition(transform.position + -transform.forward * speed * Time.deltaTime);
            }
            if (Input.GetKey(left))
            {
                //move rigidbody left
                playerRigi.MovePosition(transform.position + -transform.right * speed * Time.deltaTime);
            }
            if (Input.GetKey(right))
            {
                //move rigidbody right
                playerRigi.MovePosition(transform.position + transform.right * speed * Time.deltaTime);
            }
            if (Input.GetKeyDown(jump) && isGrounded)
            {
                //move rigidbody up
                playerRigi.AddForce(new Vector3(0, jumpHeight, 0), ForceMode.Impulse);

                isGrounded = false;
            }
        }
    }

    private bool OnSlope()
    {
        //return Physics.CheckCapsule(playerCol.bounds.center, new Vector3(playerCol.bounds.center.x, playerCol.bounds.min.y, playerCol.bounds.center.z), playerCol.radius);
        return false;
    }

    private void OnCollisionEnter(Collision other)
    {
        if((other.gameObject.tag == "building" || other.gameObject.tag == "plank") && isGrounded == false) {
            isGrounded = true;
        }

        if(isFalling) {
            Death();
        }
    }

    private void ClimbingCheck()
    {
        //check what direction you're climbing. 1 is up; 2 is right; 3 is down; 4 is left
        if (Input.GetKeyDown(ascend))
        {
            Debug.Log("Climbing up");

            //find furthest reachable point out of the list
            pointLocation = FindReachablePoint(playerTrans.position, maxReach, 1);

            //if there was a point found
            if(pointLocation != playerTrans.position)
            {
                Debug.Log("Point Location: " + pointLocation);


                playerRigi.velocity = new Vector3(0f, 0f, 0f);

                //set gravity off
                playerRigi.useGravity = false;

                //set isClimbing to true
                isClimbing = true;

                //move player to the point's location
                //playerRigi.MovePosition(pointLocation);
            }

        }

        //do same thing as above except down
        if (Input.GetKeyDown(decend))
        {
            Debug.Log("Climbing down");

            //find furthest reachable point out of the list
            pointLocation = FindReachablePoint(playerTrans.position, maxReach, 3);

            //if there was a point found
            if (pointLocation != null)
            {
                //set gravity off
                playerRigi.useGravity = false;

                //move player to the point's location
                this.transform.position = pointLocation;
            }
        }
    }

    private Vector3 FindReachablePoint(Vector3 playerLoc, float maxDis, int dir)
    {
        //make a temp GameObject variable
        GameObject tempObj;

        //temp variable to nill
        tempObj = null;

        //make an array of GameObjects
        GameObject[] tempArray;

        //Store all climbing points into the array of gameObjects
        tempArray = GameObject.FindGameObjectsWithTag("climbingPoint");

        //for every gameObject in
        foreach (GameObject o in tempArray)
        {
            //if the max distance is greater than or equal to than the distance between the player and climb point
            if (maxDis >= Vector3.Distance(o.transform.position, transform.position))
            {
                //if the direction is up
                if (dir == 1)
                {
                    //check to make sure the position has a higher y position
                    if(o.transform.position.y > transform.position.y)
                    {
                        //if tempObj is null set it to o
                        if (tempObj == null)
                        {
                            tempObj = o;
                        }
                        //else if the distance from player to o is greater than the distance from player to tempObj
                        else if (Vector3.Distance(o.transform.position, transform.position) > Vector3.Distance(tempObj.transform.position, transform.position))
                        {
                            tempObj = o;
                        }
                    }
                   
                }

                //if the direction is down
                if (dir == 3)
                {
                    //if tempObj is null set it to o
                    if (tempObj == null)
                    {
                        tempObj = o;
                    }
                    //else if the distance from player to o is greater than the distance from player to tempObj
                    else if(Vector3.Distance(o.transform.position, transform.position) > Vector3.Distance(tempObj.transform.position, transform.position))
                    {
                        tempObj = o;
                    }
                }

            }
        }

        //if tempObj is null let user know, else return the point's vector3 position
        if(tempObj == null)
        {
            Debug.Log("Did not find a climbable point");
            return playerTrans.position;
        }
        else
        {
            return tempObj.transform.position;
        }

    }

    void OnTriggerEnter(Collider collider)
    {
        //if player colliders with climbing point
        if (collider.tag == "climbingPoint")
        {
            //set gravity true 
            playerRigi.useGravity = true;

            //if climbing up
            if (isClimbing)
            {

                //move rigidbody forward
                playerRigi.MovePosition(transform.position + transform.forward * speed * 2 * Time.deltaTime);

                isClimbing = false;
                
            }
        }

        if(collider.tag == "falling") {
            isFalling = true;
        }

        if(collider.tag == "landing") {
            isFalling = false;
        }

        if(collider.tag == "checkpoint") {
            respawnLoc = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }

        if(collider.tag == "death") {
            Death();
        }

        if(collider.tag == "finish") {
            Debug.Log("FINISHED!");
            SceneManager.LoadScene(SceneToLoad.handle);
        }

        if(collider.tag == "plank") {
             choice = Random.Range(1, 3);
            Debug.Log("Random value: " + choice);
        }
    }

    private void OnTriggerStay(Collider collider) {

        //if player colliders with climbing point
        if(collider.tag == "climbingPoint") {
            //set gravity true 
            playerRigi.useGravity = true;

            //if climbing up
            if(isClimbing) {

                //move rigidbody forward
                playerRigi.MovePosition(transform.position + transform.forward * speed * 2 * Time.deltaTime);

                isClimbing = false;

            }
        }

        //Plank colliders
        if(collider.tag == "plank" && choice == 1) {
            if(!isCrouching) {
                playerRigi.AddForce(transform.right * 150f);
            }
            else {
                playerRigi.AddForce(Vector3.zero);
            }
        }

        if(collider.tag == "plank" && choice == 2) {
            if(!isCrouching) {
                playerRigi.AddForce(-transform.right * 150f);
            }
            else {
                playerRigi.AddForce(Vector3.zero);
            }
        }
    }

    void Death() {
        transform.position = respawnLoc;

        //need to make sure is climbing is false and gravity is on
        isClimbing = false;
        isFalling = false;
        playerRigi.useGravity = true;
    }



}
