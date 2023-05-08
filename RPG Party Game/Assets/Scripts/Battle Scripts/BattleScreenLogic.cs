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
}
