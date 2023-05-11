/********************************************************************************
 *   Filename:   CameraFollow.cs
 *   Date:       2023-05-10
 *   Authors:     Kaleb Gearinger and Adam Stefan
 *   Email:      kgearinger@muhlenberg.edu and astefan@muhlenberg.edu
 *   Description:
 *       This file contains the CameraFollow Class which when applied to a Unity
 *       Camera object allows it to follow the player characters or be controlled
 *       by following the invisible cursor object.
 ********************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    // Variables for camera movement
    public Vector3 offset;
    public float damping; 

    // Takes in input script utilizing player tag
    public InputScript inputScript;
    public InputScript inputScript2;

    private Vector3 velocity = Vector3.zero;
    public string tag = "Player 1";

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

    // Update is called once per frame
    void FixedUpdate()
    {
        // If in camera mode has camera follow cursor, otherwise follow player
        if (inputScript.isTurn)
        {
            if (inputScript.isCamera)
            {
                Vector3 movePosition = inputScript.dot.position + offset; 
                transform.position = Vector3.SmoothDamp(transform.position, movePosition, ref velocity, damping);
            }
            else if (!inputScript.isCamera)
            {
                Vector3 movePosition = inputScript.target.position + offset; 
                transform.position = Vector3.SmoothDamp(transform.position, movePosition, ref velocity, damping);
            }
        }
        else
        {
            if (inputScript2.isCamera)
            {
                Vector3 movePosition = inputScript2.dot.position + offset; 
                transform.position = Vector3.SmoothDamp(transform.position, movePosition, ref velocity, damping);
            }
            else if (!inputScript2.isCamera)
            {
                Vector3 movePosition = inputScript2.target.position + offset; 
                transform.position = Vector3.SmoothDamp(transform.position, movePosition, ref velocity, damping);
            }
        }
    }
}
