using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatusData", menuName = "StatusObjects/Players", order = 1)]
public class PlayerCharacterStatus : ScriptableObject
{
    // Stats for player one
    public int remaininghp_one;
    public int maxhp_one;
    public int atk_one;
    public int def_one;
    public int mag_one;
    public int res_one;
    public int spd_one;
    public int node_one;
    public bool isPlayerOneTurn = true;
    public Vector3 player1pos;
    public bool isPlayerOneInCombat = false;
    
    // Flags for character's ability to move and control of the camera object
    public bool isAbleToMovePlayerOne;
    public bool isCameraPlayerOne;
    public bool diceRolledPlayerOne;
    public bool isAbleToRollPlayerOne;
    
    public int[] enemy1Stats = new int[7];

    // Stats for player two
    public int remaininghp_two;
    public int maxhp_two;
    public int atk_two;
    public int def_two;
    public int mag_two;
    public int res_two;
    public int spd_two;
    public int node_two;
    public bool isPlayerTwoTurn = false;
    public bool isPlayerTwoInCombat = false;
    public Vector3 player2pos;

    // Flags for character's ability to move and control of the camera object
    public bool isAbleToMovePlayerTwo;
    public bool isCameraPlayerTwo;
    public bool diceRolledPlayerTwo;
    public bool isAbleToRollPlayerTwo;

    public int[] enemy2Stats = new int[7];
}