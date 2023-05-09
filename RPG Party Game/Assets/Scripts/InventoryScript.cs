using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InventoryScript : MonoBehaviour
{
    // 0: Health, 1: Trap, 2: Choose, 3: Boost
    public int[] itemStorage = {3, 4, 5, 6};

    public Text hPotionText;
    public Text trapsText;
    public Text choiceText;
    public Text ePotionText;

    public GameObject inventoryMenu;
    public GameObject chooseMenu;
    public bool isInventory;
    public bool isTurn;

    public InputScript inputScript;
    public InputScript inputScript2;
    public PauseScript1 pause;

    // Start is called before the first frame update
    void Start()
    {
        inventoryMenu.SetActive(false);
        chooseMenu.SetActive(false);

        inputScript = GameObject.FindGameObjectWithTag("Player 1").GetComponent<InputScript>();
        inputScript2 = GameObject.FindGameObjectWithTag("Player 2").GetComponent<InputScript>();
        pause = GameObject.FindGameObjectWithTag("TurnLogic").GetComponent<PauseScript1>();

        if (gameObject.tag == "Player1Inventory")
        {
            isTurn = inputScript.isTurn;
        }
        else if (gameObject.tag == "Player2Inventory")
        {
            isTurn = inputScript2.isTurn;
        }
    }

    // Update is called once per frame
    void Update()
    { 
        if (Input.GetKeyDown(KeyCode.I) && !inputScript.shopOpen && !inputScript2.shopOpen && !(PauseScript1.isPaused)) // && !inputScript.isAbleToRoll && !inputScript2.isAbleToRoll)
        {
            if (gameObject.tag == "Player1Inventory")
            {
                isTurn = inputScript.isTurn;
            }
            else if (gameObject.tag == "Player2Inventory")
            {
                isTurn = inputScript2.isTurn;
            }
            if (isTurn && (!inputScript.diceRolled && !inputScript2.diceRolled))
            {
                if (isInventory)
                {
                    MenuDown();
                }

                else
                {
                    MenuUp();
                }
            }
        }
    }

    public void MenuUp()
    {
        inventoryMenu.SetActive(true);
        Time.timeScale = 0f;
        isInventory = true;
        ///
        //Debug.Log("Health Potion: " + item.ToString());
        hPotionText.text = "Health Potion: " + itemStorage[0].ToString();
        trapsText.text = "Traps: " + itemStorage[1].ToString();
        choiceText.text = "Movement Choice: " + itemStorage[2].ToString();
        ePotionText.text = "Energy Potion: " + itemStorage[3].ToString();
    }

    public void MenuDown()
    {
        inventoryMenu.SetActive(false);
        Time.timeScale = 1f;
        isInventory = false;
    }

    public void LessHeal()
    {
        if (isTurn)
        {
            if (itemStorage[0] > 0)
            {
                itemStorage[0] = itemStorage[0] - 1;
                hPotionText.text = "Health Potion: " + itemStorage[0].ToString();
            }
        }
    }

    public void LessTrap()
    {
        if (isTurn)
        {
            if (itemStorage[1] > 0)
            {
                itemStorage[1] = itemStorage[1] - 1;
                trapsText.text = "Traps: " + itemStorage[1].ToString();
            }
        }
    }

    public void LessChoose()
    {
        if (isTurn)
        {
            if (itemStorage[2] > 0)
            {
                itemStorage[2] = itemStorage[2] - 1;
                choiceText.text = "Movement Choice: " + itemStorage[2].ToString();
            }
        }
    }

    public void Choices()
    {
        inventoryMenu.SetActive(false);
        chooseMenu.SetActive(true);
    }

    public void ChoicesDown()
    {
        chooseMenu.SetActive(false);
        MenuDown();
    }

    public void LessBoost()
    {
        if (isTurn)
        {
            if (itemStorage[3] > 0)
            {
                itemStorage[3] = itemStorage[3] - 1;
                ePotionText.text = "Energy Potion: " + itemStorage[3].ToString();
            }
        }
    }

   // public void UpdateInventory()
   // {
        
   // }
}
