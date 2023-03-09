using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Target position is the character's position
    public Transform target;

    // Dot's position is the camera's position
    public Transform dot;

    // Variables for camera movement
    public Vector3 offset;
    public float damping; 

    // Takes in input script utilizing player tag
    public InputScript inputScript;

    
    private Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {  
        // Links the input script using the tag
        inputScript = GameObject.FindGameObjectWithTag("Player").GetComponent<InputScript>();
    }

    // Update is called once per frame
    void Update()
    {
        // Toggles control to give control to player over the camera if they are not moving and hit space
        if (Input.GetKeyDown(KeyCode.Space) && inputScript.isAbleToMove)
        {
            // Toggles Flags to enable camera movement and no more player movement
            inputScript.isCamera = true;
            inputScript.isAbleToMove = false;

            // Initializes camera to be where the player is
            dot.position = target.position;
        }
        // Toggles control to give control to player over the camera if they are in camera movement mode and hit space
        else if (Input.GetKeyDown(KeyCode.Space) && inputScript.isCamera)
        {
            // Toggles Flags to enable character movement and disable camera control
            inputScript.isCamera = false;
            inputScript.isAbleToMove = true;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // If in camera mode has camera follow cursor, otherwise follow player
        if (inputScript.isCamera)
        {
            Vector3 movePosition = dot.position + offset; 
            transform.position = Vector3.SmoothDamp(transform.position, movePosition, ref velocity, damping);
        }
        else
        {
            Vector3 movePosition = target.position + offset; 
            transform.position = Vector3.SmoothDamp(transform.position, movePosition, ref velocity, damping);
        }
        
    }
}
