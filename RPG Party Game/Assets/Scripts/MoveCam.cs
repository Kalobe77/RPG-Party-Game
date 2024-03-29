/********************************************************************************
 *   Filename:   MoveCam.cs
 *   Date:       2023-05-10
 *   Authors:    Kaleb Gearinger and Adam Stefan
 *   Email:      kgearinger@muhlenberg.edu and astefan@muhlenberg.edu
 *   Description:
 *       This file handles the controls for the camera. It takes in the flag from
 *       the input scripts to make sure the character is able to control the camera
 *       then allows the camera to be controlled using the traditional keys for
 *       movement.
 ********************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCam : MonoBehaviour
{
    // Rigid Body for the cursor
    public Rigidbody2D rb;

    // Camera movement speed
    public float movementSpeed;

    // Takes in input script utilizing player tag
    public InputScript inputScript;
    public InputScript inputScript2;

    // Start is called before the first frame update
    void Start()
    {
        // Links the input script using the tag
        inputScript = GameObject.FindGameObjectWithTag("Player 1").GetComponent<InputScript>();
        inputScript2 = GameObject.FindGameObjectWithTag("Player 2").GetComponent<InputScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        // If in camera mode allow for updating position
        if(inputScript.isCamera)
        {
            rb.MovePosition(rb.position+inputScript.cameraMovement*movementSpeed*Time.fixedDeltaTime);
        }
        else if (inputScript2.isCamera)
        {
            rb.MovePosition(rb.position+inputScript2.cameraMovement*movementSpeed*Time.fixedDeltaTime);
        }
    }
}
