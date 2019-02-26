using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimTest : MonoBehaviour
{

    public Animator anim;
    [SerializeField] private KeyCode runKey;
    [SerializeField] private KeyCode backKey;
    [SerializeField] private KeyCode jumpKey;



    // Start is called before the first frame update
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
    }
}
