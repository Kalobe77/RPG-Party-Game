using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCam : MonoBehaviour
{
    public Rigidbody2D rb;
    public InputScript inputScript;
    public float movementSpeed;
    Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        inputScript = GameObject.FindGameObjectWithTag("Player").GetComponent<InputScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if(inputScript.isCamera){
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        }
    }

    void FixedUpdate()
    {
        if(inputScript.isCamera)
        {
            rb.MovePosition(rb.position+movement*movementSpeed*Time.fixedDeltaTime);
        }
    }
}
