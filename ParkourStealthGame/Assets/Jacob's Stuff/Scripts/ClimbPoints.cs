using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbPoints : MonoBehaviour
{
    public Vector3 location;
    public float disAway;

    // Start is called before the first frame update
    void Start()
    {
        location = this.transform.position;  
    }

    public GameObject CompareDist(GameObject other)
    {
        if(other.tag != "climbingPoint")
        {
            return this.gameObject;
        }

        if (other.GetComponent<ClimbPoints>().disAway > disAway)
        {
            return other.gameObject;
        }
        else
        {
            return this.gameObject;
        }
    }
}
