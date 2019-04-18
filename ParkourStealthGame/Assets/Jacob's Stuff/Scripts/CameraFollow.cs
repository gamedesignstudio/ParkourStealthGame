using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraFollow : MonoBehaviour
{
    public float distanceAway;
    public float distanceUp;
    public float smooth;
    public Transform objFollowing;
    public Vector3 targetPosition;
    public Quaternion targetRotation;
    public GameObject target;
    //private Vector3 rotationLimitStart;
    //private Vector3 rotationLimitEnd;

    // Start is called before the first frame update
    void Start()
    {
        objFollowing = target.transform;
        //rotationLimitStart.set(90, 0, 0);
        //rotationLimitEnd.set(270, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(0, 0, 0);
    }

    void LateUpdate()
    {
        targetPosition = objFollowing.position + objFollowing.up * distanceUp - objFollowing.forward * distanceAway;
        targetRotation = objFollowing.rotation;

        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smooth);
        //transform.Rotate(0, 0, 0);

        //transform.LookAt(objFollowing);
    }
}
