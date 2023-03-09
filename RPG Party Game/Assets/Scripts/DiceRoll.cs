using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRoll : MonoBehaviour
{
    // Takes in input script utilizing player tag
    public InputScript inputScript;

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

    // Start is called before the first frame update
    void Start()
    {
        // Links the input script using the tag
        inputScript = GameObject.FindGameObjectWithTag("Player").GetComponent<InputScript>();
        diceAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Checks if T is pressed and if dice has already been rolled
        if (Input.GetKeyDown(KeyCode.T) && !inputScript.isCamera && !inputScript.diceRolled)
        {
            int randomNumber = Random.Range(1,6);
            inputScript.spacesRemaining = randomNumber;
            StartCoroutine(ChangeSprite(randomNumber));
        }
    }

    // Function to modify dice after rolling
    IEnumerator ChangeSprite(int numberRolled)
    {
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
        inputScript.diceRolled = true;
        dice.SetActive(false);
        diceAnimator.GetComponent<Animator>().enabled = true;
        inputScript.isAbleToMove = true;
        inputScript.nodesVisited.Add(inputScript.node);
    }
}
