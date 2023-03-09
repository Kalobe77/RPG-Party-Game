using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCam : MonoBehaviour
{
    // Rigid Body for the cursor
    public Rigidbody2D rb;

    // Takes in input script utilizing player tag
    public InputScript inputScript;

    // Camera movement speed
    public float movementSpeed;

    // Creats a vector to store input direction
    Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        // Links the input script using the tag
        inputScript = GameObject.FindGameObjectWithTag("Player").GetComponent<InputScript>();
    }

    // Update is called once per frame
    void Update()
    {
        // Allows control of camera if in camera mode
        if(inputScript.isCamera){
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        }
    }

    void FixedUpdate()
    {
        // If in camera mode allow for updating position
        if(inputScript.isCamera)
        {
            rb.MovePosition(rb.position+movement*movementSpeed*Time.fixedDeltaTime);
        }
    }
}
