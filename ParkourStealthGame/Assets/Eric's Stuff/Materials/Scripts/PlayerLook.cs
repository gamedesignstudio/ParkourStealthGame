using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{

    [SerializeField] private string mouseXInputName, mouseYInputName;
    [SerializeField] private float mouseSensitivity;

    [SerializeField] private Transform playerBody;

    private float xAxisClamp;

    private void Awake()
    {
        LockCursor();
        xAxisClamp = 0.0f;
    }

        private void LockCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CameraRotation();
    }

    private void CameraRotation()
    {

        float mouseX = Input.GetAxisRaw(mouseXInputName) * mouseSensitivity;
        float mouseY = Input.GetAxisRaw(mouseYInputName) * mouseSensitivity;

        xAxisClamp += mouseY;

        if (xAxisClamp > 60.0f)
        {
            xAxisClamp = 60.0f;
            mouseY = 0.0f;
            //ClampXAxisRotationToValue(270.0f);
        }
        else if (xAxisClamp < -68.0f)
        {
            xAxisClamp = -68.0f;
            mouseY = 0.0f;
        }
        transform.Rotate(Vector3.left * mouseY);
        playerBody.Rotate(Vector3.up * mouseX);

    }

    private void ClampXAxisRotationToValue(float value)
    {
        Vector3 eulerRoation = transform.eulerAngles;
        eulerRoation.x = value;
        transform.eulerAngles = eulerRoation;

    }

}
