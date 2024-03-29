/********************************************************************************
 *   Filename:   ItemScript.cs
 *   Date:       2023-05-10
 *   Authors:    Kaleb Gearinger and Adam Stefan
 *   Email:      kgearinger@muhlenberg.edu and astefan@muhlenberg.edu
 *   Description:
 *       This file handles the different item effects. Currently there are four
 *       types of items.
 *          1. Health Potion - Heals the character 5hp
 *          2. Trap - Sets a trap on the characters current space
 *          3. Movement Choice - Allows character to select the amount of spaces
 *                               they can move on there next turn
 *          4. Energy Potion - Boosts Characters stats by 1 for rest of game
 *                             (Will be probably adjusted to decay or end after
 *                              so many turns)
 ********************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class ItemScript : MonoBehaviour
{
    //public bool canUse;

    // Takes in input script utilizing player tag
    public InputScript inputScript;
    public InputScript inputScript2;
    public InventoryScript inventory1;
    public InventoryScript inventory2;

    public PlayerCharacterStatus pcs;

    // Start is called before the first frame update
    void Start() 
    {
        //canUse = true;
        
        // Links the input script using the tag
        inputScript = GameObject.FindGameObjectWithTag("Player 1").GetComponent<InputScript>();
        inputScript2 = GameObject.FindGameObjectWithTag("Player 2").GetComponent<InputScript>();

        inventory1 = GameObject.FindGameObjectWithTag("Player1Inventory").GetComponent<InventoryScript>();
        inventory2 = GameObject.FindGameObjectWithTag("Player2Inventory").GetComponent<InventoryScript>();
    }

    // Update is called once per frame
    void Update() 
    {
        
    }

    public void Heal()
    {
        //turn = inputScript.isTurn;

        if(inputScript.isTurn && (inventory1.itemStorage[0] != 0) && inventory1.canUse)
        {
            if (inputScript.remaininghp + 5 > inputScript.maxhp)
            {
                inputScript.remaininghp = inputScript.maxhp;
            }

            else
            {
                inputScript.remaininghp = inputScript.remaininghp + 5;
            }

            //inventory1.canUse = false;
        }

        else if(inputScript2.isTurn && (inventory2.itemStorage[0] != 0) && inventory2.canUse)
        {
            if (inputScript2.remaininghp + 5 > inputScript2.maxhp)
            {
                inputScript2.remaininghp = inputScript2.maxhp;
            }

            else
            {
                inputScript2.remaininghp = inputScript2.remaininghp + 5;
            }

            //inventory2.canUse = false;
        }
    }

    public void Trap()
    {
        if(inputScript.isTurn && (inventory1.itemStorage[1] != 0) && inventory1.canUse)
        {
            if (inputScript.node == 0)
            {
                pcs.spaceAssign[inputScript.node] = 4;
            }
            else if (inputScript.node == 1)
            {
                pcs.spaceAssign[inputScript.node] = 5;
            }
            else if (inputScript.node == 2)
            {
                pcs.spaceAssign[inputScript.node] = 6;
            }
            else if (inputScript.node == 3)
            {
                pcs.spaceAssign[inputScript.node] = 7;
            }

            //inventory1.canUse = false;
        }

        else if(inputScript2.isTurn && (inventory2.itemStorage[1] != 0) && inventory2.canUse)
        {
            if (inputScript.node == 0)
            {
                pcs.spaceAssign[inputScript2.node] = 4;
            }
            else if (inputScript.node == 1)
            {
                pcs.spaceAssign[inputScript2.node] = 5;
            }
            else if (inputScript.node == 2)
            {
                pcs.spaceAssign[inputScript2.node] = 6;
            }
            else if (inputScript.node == 3)
            {
                pcs.spaceAssign[inputScript2.node] = 7;
            }

            //inventory2.canUse = false;
        }
    }
////////////
    public void Choose(int num)
    {
        if(inputScript.isTurn) //&& inventory1.canUse)
        {
            inputScript.GetNum(num);

            //inventory1.canUse = false;
        }

        else if(inputScript2.isTurn) //&& inventory2.canUse)
        {
            inputScript2.GetNum(num);

            //inventory2.canUse = false;
        }
    }
////////////
    public void Boost()
    {
        if(inputScript.isTurn && (inventory1.itemStorage[3] != 0) && inventory1.canUse)
        {
            inputScript.atk = inputScript.atk + 1;
            inputScript.def = inputScript.def + 1;
            inputScript.mag = inputScript.mag + 1;
            inputScript.res = inputScript.res + 1;
            inputScript.spd = inputScript.spd + 1;

            //inventory1.canUse = false;
        }

        else if(inputScript2.isTurn && (inventory2.itemStorage[3] != 0) && inventory2.canUse)
        {
            inputScript2.atk = inputScript2.atk + 1;
            inputScript2.def = inputScript2.def + 1;
            inputScript2.mag = inputScript2.mag + 1;
            inputScript2.res = inputScript2.res + 1;
            inputScript2.spd = inputScript2.spd + 1;

            //inventory2.canUse = false;
        }
    }

    // gives player ablity to choose number on dice
    // heals //
    // boosts stats by one //
    // maybe a landmine //
    // teleportation spaces
}
