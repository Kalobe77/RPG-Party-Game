/********************************************************************************
 *   Filename:   BattleScreenLogic.cs
 *   Date:       2023-05-10
 *   Authors:    Kaleb Gearinger and Adam Stefan
 *   Email:      kgearinger@muhlenberg.edu and astefan@muhlenberg.edu
 *   Description:
 *       This file handles the logic for manipulating the battle win UI that
 *       so that it displays what the character was able to recieve from winning.
 ********************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleScreenLogic : MonoBehaviour
{
    // Used for health score
    public Text xpTxt;
    public Text itemTxt;
    public Text gemsTxt;

    public GameObject menuStuff;
    public GameObject lostUI;

    // Start is called before the first frame update
    void Start()
    {
        menuStuff.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Manipulate Text
    public void ManipulateText(int exp, string item, int gems)
    {
        bool check = false;
        xpTxt.text = "EXP Gained: " + exp.ToString();
        if (item != null)
        {
            itemTxt.text = item + " Recieved!";
        }
        gemsTxt.text = "Gems Collected: " + gems.ToString();
        ToggleMenu();
    }

    public void ToggleMenu()
    {
        menuStuff.SetActive(true);
    }

    public void TurnOnLost()
    {
        Debug.Log("Turn On Lost UI");
        lostUI.SetActive(true);
    }
}
