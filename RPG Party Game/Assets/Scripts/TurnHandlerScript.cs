using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TurnHandlerScript : MonoBehaviour
{
    // Used for health score
    public Text healthText;

    // Handles if game is going on
    public bool isGameHappening = false;

    // Takes in input script utilizing player tag
    public InputScript inputScript;
    public InputScript inputScript2;
    
    public CameraFollow cameraFollow;

    // To Allow Data to be loaded from scriptable object
    public PlayerCharacterStatus pcs;

    // Start is called before the first frame update
    void Start()
    {
        // Links the input script using the tag
        inputScript = GameObject.FindGameObjectWithTag("Player 1").GetComponent<InputScript>();
        inputScript2 = GameObject.FindGameObjectWithTag("Player 2").GetComponent<InputScript>();
        // Links the input script using the tag
        cameraFollow = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
        healthUpdate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void healthUpdate()
    {
        if (inputScript.isTurn){

            healthText.text = "HP: " + inputScript.remaininghp.ToString() + " / " + inputScript.maxhp.ToString();
        }

        else if (inputScript2.isTurn){

            healthText.text = "HP: " + inputScript2.remaininghp.ToString() + " / " + inputScript2.maxhp.ToString();
        }
    }

    // Rotates Turn Order
    public void ProgressTurn()
    {
        if (inputScript.isTurn){
            cameraFollow.tag = "Player 2";
        }
        else if (inputScript2.isTurn){
            cameraFollow.tag = "Player 1";
        }
        
    }

    public void NextTurn()
    {
        if (inputScript.isTurn)
        {
            inputScript.isTurn = !inputScript.isTurn;
            inputScript2.isTurn = !inputScript2.isTurn;
        }
        else if (inputScript2.isTurn)
        {
            inputScript.isTurn = !inputScript.isTurn;
            inputScript2.isTurn = !inputScript2.isTurn;
            pcs.turn = pcs.turn + 1;
        }
    }

    public void UpdateStatus()
    {
        // Sets Variables for players to the correct values
        pcs.remaininghp_one = inputScript.remaininghp;
        pcs.maxhp_one = inputScript.maxhp;
        pcs.atk_one = inputScript.atk;
        pcs.def_one = inputScript.def;
        pcs.mag_one = inputScript.mag;
        pcs.res_one = inputScript.res;
        pcs.spd_one = inputScript.spd;
        pcs.node_one = inputScript.node;
        pcs.isPlayerOneTurn = inputScript.isTurn;
        pcs.player1pos = inputScript.target.position;
        pcs.isAbleToMovePlayerOne = inputScript.isAbleToMove;
        pcs.isCameraPlayerOne = inputScript.isCamera;
        pcs.diceRolledPlayerOne = inputScript.diceRolled;
        pcs.isAbleToRollPlayerOne = inputScript.isAbleToRoll;
        pcs.isPlayerOneInCombat = inputScript.isInCombat;
        

        pcs.remaininghp_two = inputScript2.remaininghp;
        pcs.maxhp_two = inputScript2.maxhp;
        pcs.atk_two = inputScript2.atk;
        pcs.def_two = inputScript2.def;
        pcs.mag_two = inputScript2.mag;
        pcs.res_two = inputScript2.res;
        pcs.spd_two = inputScript2.spd;
        pcs.node_two = inputScript2.node;
        pcs.isPlayerTwoTurn = inputScript2.isTurn;
        pcs.player2pos = inputScript2.target.position;
        pcs.isAbleToMovePlayerTwo = inputScript2.isAbleToMove;
        pcs.isCameraPlayerTwo = inputScript2.isCamera;
        pcs.diceRolledPlayerTwo = inputScript2.diceRolled;
        pcs.isAbleToRollPlayerTwo = inputScript2.isAbleToRoll;
        pcs.isPlayerTwoInCombat = inputScript2.isInCombat;
    }
}
