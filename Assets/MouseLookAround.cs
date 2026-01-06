using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class MouseLookAround : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    float rotationX = 0f;
    float rotationY = 0f;
    public float sensitivity = 15f;    

    // Update is called once per frame
    void Update()
    {
        rotationY += Input.GetAxis("Mouse X") * sensitivity;
        rotationX += Input.GetAxis("Mouse Y") * sensitivity * -1;
        transform.localEulerAngles = new Vector3 (rotationX, rotationY, 0);
    }
}
