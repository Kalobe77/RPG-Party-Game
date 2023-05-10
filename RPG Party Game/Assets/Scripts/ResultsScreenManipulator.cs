using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultsScreenManipulator : MonoBehaviour
{
    // Grabs the Text to Allow For Manipulation
    public Text firstText;
    public Text secondText;
    public Text firstGems;
    public Text secondGems;

    // Access to Player Character Status File
    public PlayerCharacterStatus pcs;

    // To store the variables needed
    public int p1Gems;
    public int p2Gems;

    // Start is called before the first frame update
    void Start()
    {
        // Obtains how many gems each player has
        p1Gems = pcs.gems_one;
        p2Gems = pcs.gems_two;

        // Checks to see what place each player got
        if (p1Gems > p2Gems)
        {
            firstText.text = "1st: Player 1";
            secondText.text = "2nd: Player 2";
            firstGems.text = "# of Gems: " + p1Gems;
            secondGems.text = "# of Gems: " + p2Gems;
        }
        else if (p2Gems > p1Gems)
        {
            firstText.text = "1st: Player 2";
            secondText.text = "2nd: Player 1";
            firstGems.text = "# of Gems: " + p2Gems;
            secondGems.text = "# of Gems: " + p1Gems;
        }
        else
        {
            firstText.text = "Tied";
            secondText.text = "Tied";
            firstGems.text = "# of Gems: " + p1Gems;
            secondGems.text = "# of Gems: " + p2Gems;
        }
    }
}
