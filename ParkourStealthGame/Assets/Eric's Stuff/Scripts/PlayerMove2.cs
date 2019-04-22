using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove2 : MonoBehaviour {
    //Variable Names for Unity's Input Manager
    [SerializeField] private string verticalInputName;
    [SerializeField] private string horizontalInputName;

    //Floats for the speed of walking and running, as well as
    //floats for the acceleration and decceleration
    [SerializeField] private float crouchSpeed, walkSpeed, runSpeed;
    [SerializeField] private float runBuildUp, runSlowDown;

    //Variable for forward movement ('W') key;
    [SerializeField] private KeyCode runKey;
    [SerializeField] private KeyCode crouchKey;
    [SerializeField] private KeyCode jumpKey;
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

    //Variables for climbing
    [SerializeField] private float climbSpeed;
    [SerializeField] private KeyCode ascend;
    [SerializeField] private KeyCode decend;
    [SerializeField] private KeyCode climbL;
    [SerializeField] private KeyCode climbR;
    [SerializeField] private float maxReach;
    private Vector3 pointLocation;
    List<GameObject> clmbPtList = new List<GameObject>();
    [SerializeField] Transform player;
    private bool isJumping;
    [SerializeField] private AnimationCurve jumpFallOff;
    [SerializeField] private float jumpMultiplier;




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
        ClimbingCheck();

        if(Input.GetKeyDown(crouchKey)) {
            movementSpeed = Mathf.Lerp(movementSpeed, crouchSpeed, Time.deltaTime * runSlowDown);
            charController.height /= 2f;
        }
        else if(Input.GetKeyUp(crouchKey)) {
            movementSpeed = Mathf.Lerp(movementSpeed, crouchSpeed, Time.deltaTime * runSlowDown);
            charController.height *= 2f;
        }
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
        JumpInput();

       
    }

    private void SetMovementSpeed()
    {
        if(Input.GetKey(runKey)) {
            movementSpeed = Mathf.Lerp(movementSpeed, runSpeed, Time.deltaTime * runBuildUp);

        }
        else if(Input.GetKey(crouchKey)) {
            movementSpeed = Mathf.Lerp(movementSpeed, crouchSpeed, Time.deltaTime * runSlowDown);
        }
        else {
            movementSpeed = Mathf.Lerp(movementSpeed, walkSpeed, Time.deltaTime * runSlowDown);
        }
    }

    private void JumpInput(){
        if(Input.GetKeyDown(jumpKey) && !isJumping) {
            isJumping = true;
            StartCoroutine(JumpEvent());
        }
    }

    private IEnumerator JumpEvent() {

        float timeInAir = 0.0f;
        charController.slopeLimit = 90.0f;

        do {

            float jumpForce = jumpFallOff.Evaluate(timeInAir);
            charController.Move(Vector3.up * jumpForce * jumpMultiplier * Time.deltaTime);
            timeInAir += Time.deltaTime;

            yield return null;

        } while(!charController.isGrounded && charController.collisionFlags != CollisionFlags.Above);

        charController.slopeLimit = 45.0f;
        isJumping = false;
    
    }

    //check to see if a climbing movement key is being pressed
    private void ClimbingCheck()
    {
        if (Input.GetKeyDown(ascend))
        {
            Debug.Log("Climbing up");
            ClimbUp();
        }
        if (Input.GetKeyDown(decend))
        {
            Debug.Log("Climbing down");
            ClimbDown();
        }
        if (Input.GetKeyDown(climbL))
        {
            Debug.Log("Climbing Left");
            ClimbLeft();
        }
        if (Input.GetKeyDown(climbR))
        {
            Debug.Log("Climbing Right");
            ClimbRight();
        }
    }

    private void ClimbUp()
    {
        /*Doesn't Work
        //move up 
        if (Input.GetKey(ascend))
        {
            moveDirection.y = climbSpeed;
        }*/

        clmbPtList = FindAllReachablePoints(this.gameObject.transform.position, maxReach, 1);
        pointLocation = FurthestReachablePoint(clmbPtList);
        Debug.Log("pointLocation found at: " + pointLocation);
        Debug.Log("Player location: " + this.transform.position);
        //player.position = pointLocation;
        charController.Move(Vector3.up * climbSpeed);
        Debug.Log("Player location: " + this.transform.position);
        Debug.Log("Player location: " + this.transform.position);
        
    }

    private void ClimbDown()
    {
        clmbPtList = FindAllReachablePoints(this.gameObject.transform.position, maxReach, 3);
        pointLocation = FurthestReachablePoint(clmbPtList);
        Debug.Log("pointLocation found at: " + pointLocation);
        this.transform.Translate(pointLocation.y, 0, 0); 

    }

    private void ClimbLeft()
    {
        clmbPtList = FindAllReachablePoints(this.gameObject.transform.position, maxReach, 4);
        pointLocation = FurthestReachablePoint(clmbPtList);
        Debug.Log("pointLocation found at: " + pointLocation);
        this.transform.Translate(pointLocation.x, 0, 0); //may need to chang if orientation is different
    }

    private void ClimbRight()
    {
        clmbPtList = FindAllReachablePoints(this.gameObject.transform.position, maxReach, 2);
        pointLocation = FurthestReachablePoint(clmbPtList);
        Debug.Log("pointLocation found at: " + pointLocation);
        this.transform.Translate(pointLocation.x, 0, 0);  //may need to chang if orientation is different
    }

    /*
     * for int dir:
     * up = 1; right = 2; down = 3; left = 4
     */
    private List<GameObject> FindAllReachablePoints(Vector3 playerLoc, float maxDis, int dir)
    {
        List<GameObject> tempList = new List<GameObject>();
        GameObject[] tempArray;
        tempArray = GameObject.FindGameObjectsWithTag("climbingPoint");

        foreach(GameObject o in tempArray)
        {
            tempList.Add(o);
        }

        foreach(GameObject cP in tempList)
        {
            if (maxDis < Vector3.Distance(cP.transform.position, this.transform.position))
            {
                tempList.Remove(cP);
            }
            else if(dir == 1)
            {
                if(this.transform.position.y >= cP.transform.position.y)
                {
                    tempList.Remove(cP);
                }
            }
            else if(dir == 2)
            {
                if (this.transform.position.y >= cP.transform.position.y)
                {
                    tempList.Remove(cP);
                }
            }
            else if(dir == 3)
            {
                if (this.transform.position.y <= cP.transform.position.y)
                {
                    tempList.Remove(cP);
                }
            }
            else if(dir == 4)
            {
                if (this.transform.position.x <= cP.transform.position.x)
                {
                    tempList.Remove(cP);
                }
            }
            else
            {
                Debug.Log("Passed wrong direction to FindAllReachablePoints()");
            }

        }
        return tempList;
    }

    //find the furthest point that is in reach; return its position 
    private Vector3 FurthestReachablePoint(List<GameObject> list)
    {
        GameObject fP = null;
        if(list.Count > 0)
        {
            foreach (GameObject cP in list)
            {
                //if furthest point is null, set it to current point
                if (fP == null)
                {
                    fP = cP;
                }

                fP = fP.GetComponent<ClimbPoints>().CompareDist(cP);
            }
            return fP.transform.position;
        }
        else
        {
            //player stays where they are
            return this.transform.position;
        }
    }


}
