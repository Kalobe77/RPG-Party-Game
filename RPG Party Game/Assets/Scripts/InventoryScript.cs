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
    public bool canUse;

    public PlayerCharacterStatus pcs;
    public InputScript inputScript;
    public InputScript inputScript2;
    public PauseScript1 pause;
    public int gems;

    // Start is called before the first frame update
    void Start()
    {
        canUse = true;

        inventoryMenu.SetActive(false);
        chooseMenu.SetActive(false);

        inputScript = GameObject.FindGameObjectWithTag("Player 1").GetComponent<InputScript>();
        inputScript2 = GameObject.FindGameObjectWithTag("Player 2").GetComponent<InputScript>();
        pause = GameObject.FindGameObjectWithTag("TurnLogic").GetComponent<PauseScript1>();

        SetFlag();
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
                Debug.Log("In Inventory");
                Debug.Log(isInventory);
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
        
        if (canUse == false && isTurn)
        {
            MenuDown();
        }
    }
 
    public void MenuUp()
    {
        inventoryMenu.SetActive(true);
        Time.timeScale = 0f;
        isInventory = true;
        inputScript.isAbleToRoll = false;
        inputScript2.isAbleToRoll = false;
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
        inputScript.isAbleToRoll = true;
        inputScript2.isAbleToRoll = true;
        Time.timeScale = 1f;
        isInventory = false;
    }

    public void LessHeal()
    {
        if (isTurn && canUse)
        {
            if (itemStorage[0] > 0)
            {
                itemStorage[0] = itemStorage[0] - 1;
                hPotionText.text = "Health Potion: " + itemStorage[0].ToString();
                canUse = false;
                SaveItems();
            }
        }
    }
 /////////
    public void MoreHeal()
    {
        SetFlag();
        if (isTurn)
        {
            Debug.Log(gems);
            if (gems >= 2)
            {
                Debug.Log("Had Enough Gems");
                itemStorage[0]++;
                hPotionText.text = "Health Potion: " + itemStorage[0].ToString();
                gems = gems - 2;
                SetGems();
                SaveItems();
            }
        }
    }
 
    public void LessTrap()
    {
        if (isTurn && canUse)
        {
            if (itemStorage[1] > 0)
            {
                itemStorage[1] = itemStorage[1] - 1;
                trapsText.text = "Traps: " + itemStorage[1].ToString();
                canUse = false;
                SaveItems();
            }
        }
    }
 
    public void MoreTrap()
    {
        SetFlag();
        if (isTurn)
        {
            if (gems >= 2)
            {
                itemStorage[1]++;
                trapsText.text = "Traps: " + itemStorage[1].ToString();
                gems = gems - 2;
                SetGems();
                SaveItems();
            }
        }
    }
 
    public void LessChoose()
    {
        if (isTurn && canUse)
        {
            if (itemStorage[2] > 0)
            {
                itemStorage[2] = itemStorage[2] - 1;
                choiceText.text = "Movement Choice: " + itemStorage[2].ToString();
                canUse = false;
                SaveItems();
            }
            else
            {
                MenuDown();
                ChoicesDown();
            }
        }
    }
 
    public void MoreChoose()
    {
        SetFlag();
        if (isTurn)
        {
            if (gems >= 3)
            {
                itemStorage[2]++;
                choiceText.text = "Movement Choice: " + itemStorage[2].ToString();
                gems = gems - 3;
                SetGems();
                SaveItems();
            }
        }
    }
 
    public void Choices()
    {
        if (isTurn && canUse)
        {
            inventoryMenu.SetActive(false);
            chooseMenu.SetActive(true);
        }
    }

    public void ChoicesDown()
    {
        chooseMenu.SetActive(false);
        MenuDown();
    }

    public void LessBoost()
    {
        if (isTurn && canUse)
        {
            if (itemStorage[3] > 0)
            {
                itemStorage[3] = itemStorage[3] - 1;
                ePotionText.text = "Energy Potion: " + itemStorage[3].ToString();
                canUse = false;
                SaveItems();
            }
        }
    }
 
    public void MoreBoost()
    {
        SetFlag();
        if (isTurn)
        {
            if (gems >= 4)
            {
                itemStorage[3]++;
                ePotionText.text = "Energy Potion: " + itemStorage[3].ToString();
                gems = gems - 4;
                SetGems();
                SaveItems();
            }
        }
    }
 
    // Sets Gems for PlayerCharacterStatus
    public void SetGems()
    {
        if (gameObject.tag == "Player1Inventory")
        {
            pcs.gems_one = gems;
        }
        else if (gameObject.tag == "Player2Inventory")
        {
            pcs.gems_two = gems;
        }
    }

    public void SetFlag()
    {
        if (gameObject.tag == "Player1Inventory")
        {
            isTurn = inputScript.isTurn;
            gems = pcs.gems_one;
            itemStorage = pcs.inventory_one;
        }
        else if (gameObject.tag == "Player2Inventory")
        {
            isTurn = inputScript2.isTurn;
            gems = pcs.gems_two;
            itemStorage = pcs.inventory_two;
        }
    }

    // Sets Items for PlayerCharacterStatus
    public void SaveItems()
    {
        if (gameObject.tag == "Player1Inventory")
        {
            pcs.inventory_one = itemStorage;
        }
        else if (gameObject.tag == "Player2Inventory")
        {
            pcs.inventory_two = itemStorage;
        }
    }
}