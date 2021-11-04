using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float rotationSpeed;
    public GameObject player;

    void Update()
    {  
        if (player != null)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime);
            transform.position = player.transform.position;
        }
    }
}
