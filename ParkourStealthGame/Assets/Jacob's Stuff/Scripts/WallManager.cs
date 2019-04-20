using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallManager : MonoBehaviour
{
    [SerializeField] Transform player;
    bool isClimbing;
    // Start is called before the first frame update
    void Start()
    {
        isClimbing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isClimbing)
        {
            player.Translate(Vector3.up * Time.deltaTime);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            if (Input.GetKeyDown("e"))
            {
                Debug.Log("Player is climbing vent");
                isClimbing = true; 
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isClimbing = false;
        }
    }
}
