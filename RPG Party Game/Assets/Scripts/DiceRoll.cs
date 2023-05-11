/********************************************************************************
 *   Filename:   DiceRoll.cs
 *   Date:       2023-05-10
 *   Authors:    Kaleb Gearinger and Adam Stefan
 *   Email:      kgearinger@muhlenberg.edu and astefan@muhlenberg.edu
 *   Description:
 *       This file handles each character's dice object. It provides the ability
 *       to roll the dice to figure out how many spaces the character can move. It
 *       also handles the animation of the dice object.
 ********************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRoll : MonoBehaviour
{
    // Takes in input script utilizing player tag
    public InputScript inputScript;
    public InventoryScript inventoryScript;

    // Grab dice object
    public GameObject dice;

    // Stores Sprites of all numbers 1-6
    public Sprite one;
    public Sprite two;
    public Sprite three;
    public Sprite four;
    public Sprite five;
    public Sprite six;

    // Access to the Sprite Renderer of the Dice
    public SpriteRenderer spriteRenderer;

    // Grabs animator from the dice
    public Animator diceAnimator;

    // Despawn time for the dice
    public int despawnTime;

    // Tag for connection to correct player
    public string tag;

    

    // Start is called before the first frame update
    void Start()
    {
        // Links the input script using the tag
        if (tag == "Player 1"){
            inputScript = GameObject.FindGameObjectWithTag("Player 1").GetComponent<InputScript>();
            inventoryScript = GameObject.FindGameObjectWithTag("Player1Inventory").GetComponent<InventoryScript>();
        }
        else if (tag == "Player 2"){
            inputScript = GameObject.FindGameObjectWithTag("Player 2").GetComponent<InputScript>();
            inventoryScript = GameObject.FindGameObjectWithTag("Player2Inventory").GetComponent<InventoryScript>();
        }
        
        diceAnimator = GetComponent<Animator>();
        dice.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Checks if T is pressed and if dice has already been rolled
        if (Input.GetKeyDown(KeyCode.T) && !inputScript.isCamera && !inputScript.diceRolled && inputScript.isAbleToRoll &&!PauseScript1.isPaused)
        {
            inputScript.diceRolled = true;
            inventoryScript.canUse = false;
            int randomNumber = Random.Range(1,7);
            inputScript.spacesRemaining = randomNumber;
            StartCoroutine(ChangeSprite(randomNumber));
        }
    }

    // Function to modify dice after rolling
    IEnumerator ChangeSprite(int numberRolled)
    {
        // Based on what number is rolled, a different number is displayed on the dice
        if (numberRolled == 1)
        {
            spriteRenderer.sprite = one;
        }
        else if (numberRolled == 2)
        {
            spriteRenderer.sprite = two;
        }
        else if (numberRolled == 3)
        {
            spriteRenderer.sprite = three;
        }
        else if (numberRolled == 4)
        {
            spriteRenderer.sprite = four;
        }
        else if (numberRolled == 5)
        {
            spriteRenderer.sprite = five;
        }
        else if (numberRolled == 6)
        {
            spriteRenderer.sprite = six;
        }

        // Disables Animation for dice
        diceAnimator.GetComponent<Animator>().enabled = false;

        // Waits so many seconds before despawning dice and allowing movement control
        yield return new WaitForSeconds(despawnTime);
        dice.SetActive(false);
        diceAnimator.GetComponent<Animator>().enabled = true;
        inputScript.isAbleToMove = true;
        inputScript.nodesVisited.Add(inputScript.node);
    }
}
