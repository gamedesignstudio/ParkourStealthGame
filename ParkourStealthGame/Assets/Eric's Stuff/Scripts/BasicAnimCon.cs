using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAnimCon : MonoBehaviour
{

    public Animator anim;
    [SerializeField] private KeyCode runKey;
    [SerializeField] private KeyCode backKey;
    [SerializeField] private KeyCode jumpKey;
    [SerializeField] private KeyCode sprintKey;
    [SerializeField] private KeyCode crouchKey;

    // [SerializeField] private GameObject charController;

    // Start is called before the first frame update

    private void Awake()
    {
        //charController = GetComponent<GameObject>();
    }
    void Start()
    {
        anim = this.GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(runKey))
            anim.SetBool("isJogging", true);
        else
            anim.SetBool("isJogging", false);

        if (Input.GetKey(backKey))
            anim.SetBool("isReversing", true);
        else
            anim.SetBool("isReversing", false);

        if (Input.GetKey(jumpKey))
            anim.SetBool("isJumping", true);
        else
            anim.SetBool("isJumping", false);

        if (Input.GetKey(crouchKey))
        {
            anim.SetBool("isCrouching", true);
            
        }
        else
            anim.SetBool("isCrouching", false);

        if (Input.GetKey(sprintKey))
            anim.SetBool("isSprinting", true);
        else
            anim.SetBool("isSprinting", false);
    }
}
