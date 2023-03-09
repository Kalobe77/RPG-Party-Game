using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnHandlerScript : MonoBehaviour
{
    // Takes in input script utilizing player tag
    public InputScript inputScript;
    public InputScript inputScript2;
    
    public CameraFollow cameraFollow;

    // Start is called before the first frame update
    void Start()
    {
        // Links the input script using the tag
        inputScript = GameObject.FindGameObjectWithTag("Player 1").GetComponent<InputScript>();
        inputScript2 = GameObject.FindGameObjectWithTag("Player 2").GetComponent<InputScript>();
        // Links the input script using the tag
        cameraFollow = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Rotates Turn Order
    public void ProgressTurn()
    {
        if (inputScript.isTurn){
            inputScript.isTurn = !inputScript.isTurn;
            inputScript2.isTurn = !inputScript2.isTurn;
            cameraFollow.tag = "Player 2";
        }
        else if (inputScript2.isTurn){
            inputScript.isTurn = !inputScript.isTurn;
            inputScript2.isTurn = !inputScript2.isTurn;
            cameraFollow.tag = "Player 1";
        }
        
    }
}
