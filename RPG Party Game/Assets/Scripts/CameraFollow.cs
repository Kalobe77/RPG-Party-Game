using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Transform dot;
    public Vector3 offset;
    public float damping; 
    public InputScript inputScript;

    private Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        inputScript = GameObject.FindGameObjectWithTag("Player").GetComponent<InputScript>();
    }

    // Update is called once per frame
    void Update()
    {
        // Input
        if (Input.GetKeyDown(KeyCode.Space) && inputScript.isMoving == 0)
        {
            inputScript.isCamera = true;
            inputScript.isMoving = 2;
            dot.position = target.position;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && inputScript.isMoving == 2)
        {
            inputScript.isCamera = false;
            inputScript.isMoving = 0;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
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
