using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InputScript : MonoBehaviour
{
    public bool done = true;

    public GameObject shopMenu;

    public bool canShop = false;

    public bool shopOpen = false;

    public int nodeType;

    public int[] spaceTypes = {0, 1, 2, 3};
    //int[] spaceAssign = new int[85];
    //int[] spaceAssign = {2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3};
    public int[] spaceAssign = {2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 3, 3, 3, 3, 3};
    
    // 2D arrays to hold enemy stat information (May use a file in the future)
    public int[,] enemyStatsMatrix = {{0,1,2,3,4,5,6,7,8,9},{10,11,12,13,14,15,16,17,18,19},{20,21,22,23,24,25,26,27,28,29},{30,31,32,33,34,35,36,37,38,39}};
    public int[,] enemyStatsTable = {{10,5,5,5,5,5},{20,7,7,7,7,7},{30,9,9,9,9,9},{40,11,11,11,11,11},{50,13,13,13,13,13},{60,15,15,15,15,15},{70,17,17,17,17,17},{80,19,19,19,19,19},{90,21,21,21,21,21},{100,23,23,23,23,23},{10,7,5,2,5,5},{20,10,7,4,7,7},{30,13,9,6,9,9},{40,16,11,8,11,11},{50,19,13,10,13,13},{60,22,15,12,15,15},{70,25,17,14,17,17},{80,28,19,16,19,19},{90,31,21,18,21,21},{100,34,23,20,23,23},{10,2,5,7,6,5},{20,4,7,10,8,7},{30,6,9,13,10,9},{40,8,11,16,12,11},{50,10,13,19,14,13},{60,12,15,22,16,15},{70,14,17,25,18,17},{80,16,19,28,20,19},{90,18,21,31,22,21},{100,20,23,34,24,23},{15,3,7,3,7,3},{30,5,9,5,9,5},{45,7,11,7,11,7},{60,9,13,9,13,9},{75,11,15,11,15,11},{90,13,17,13,17,13},{105,15,19,15,19,15},{120,17,21,17,21,17},{135,19,23,19,23,19},{150,21,25,21,25,21}};
    
    // Used to change velocity and position
    public Rigidbody2D rb;

    // Target position is the character's position
    public Transform target;

    // Dot's position is the camera's position
    public Transform dot;

    // Adds animator to object to add in walking animations
    public Animator animator;

    // Creats a vector to store input direction
    Vector2 movement;

    // Creats a vector to store input direction for camera movement
    public Vector2 cameraMovement;

    // Stores what node the character is currently on
    public int node = 0;

    // Flags for character's ability to move and control of the camera object
    public bool isAbleToMove = true;
    public bool isCamera = false;
    public bool diceRolled = false;
    public bool isTurn = true;
    public bool isAbleToRoll = true;
    public bool isInCombat = false;

    // Character offset to adjust character to center of tile
    public float characterOffset = .7f;

    // Link to routeScript to access adjacency matrix and childNodeList
    public Route routeScript;

    // Amount of movement for character
    public int spacesRemaining;

    // Grab dice object and location
    public GameObject dice;

    // List to store what nodes were visited
    public List<int> nodesVisited;

    // Grabs Camera Follow Script
    public CameraFollow cameraFollow;

    // Connect the Turn Handler Script
    public TurnHandlerScript turnHandler;

    // Stats for character
    public int remaininghp;
    public int maxhp;
    public int atk;
    public int def;
    public int mag;
    public int res;
    public int spd;

    // To Allow Data to be loaded from scriptable object
    public PlayerCharacterStatus pcs;

    // Start is called before the first frame update
    void Start()
    {  
        shopMenu.SetActive(false);
        // Links the input script using the tag
        cameraFollow = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
        turnHandler = GameObject.FindGameObjectWithTag("TurnLogic").GetComponent<TurnHandlerScript>();
        if (gameObject.tag == "Player 1")
        {
            remaininghp = pcs.remaininghp_one;
            maxhp = pcs.maxhp_one;
            atk = pcs.atk_one;
            def = pcs.def_one;
            mag = pcs.mag_one;
            res = pcs.res_one;
            spd = pcs.spd_one;
            node = pcs.node_one;
            target.position = pcs.player1pos;
            isTurn = pcs.isPlayerOneTurn;
            turnHandler.isGameHappening = true;
            isAbleToMove = pcs.isAbleToMovePlayerOne;
            isCamera = pcs.isCameraPlayerOne;
            diceRolled = pcs.diceRolledPlayerOne;
            isAbleToRoll = pcs.isAbleToRollPlayerOne;
            isInCombat = pcs.isPlayerOneInCombat;
        }
        else if (gameObject.tag == "Player 2")
        {
            remaininghp = pcs.remaininghp_two;
            maxhp = pcs.maxhp_two;
            atk = pcs.atk_two;
            def = pcs.def_two;
            mag = pcs.mag_two;
            res = pcs.res_two;
            spd = pcs.spd_two;
            node = pcs.node_two;
            target.position = pcs.player2pos;
            isTurn = pcs.isPlayerTwoTurn;
            turnHandler.isGameHappening = true;
            isAbleToMove = pcs.isAbleToMovePlayerTwo;
            isCamera = pcs.isCameraPlayerTwo;
            diceRolled = pcs.diceRolledPlayerTwo;
            isAbleToRoll = pcs.isAbleToRollPlayerTwo;
            isInCombat = pcs.isPlayerTwoInCombat;
        }
        dice.transform.position = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);
        Debug.Log(dice);
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseScript1.isPaused != true)
        {
            if (isTurn)
            {
                if (isInCombat)
                {
                    movement.x = 0;
                    movement.y = 0;
                    diceRolled = false;
                    dice.transform.position = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);
                    nodesVisited.Clear();
                    isAbleToMove = false;
                    turnHandler.UpdateStatus();
                    turnHandler.ProgressTurn();
                    SceneManager.LoadScene("Scenes/Battle");
                }
                if (spacesRemaining == 0 && !diceRolled)
                {
                    dice.SetActive(true);
                }
                if (spacesRemaining == 0 && diceRolled)
                {
                    // Resets Everything
                    movement.x = 0;
                    movement.y = 0;
                    diceRolled = false;
                    dice.transform.position = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);
                    nodesVisited.Clear();
                    isAbleToMove = false;
                    // combat spot
                    turnHandler.UpdateStatus(); // stores info for new scene

                    nodeType = spaceAssign[node];
                    if (nodeType == 1)
                    {
                        canShop = true;
                        spacesRemaining = -1;
                        shopMenu.SetActive(true);
                    }

                    else if (nodeType == 2)
                    {
                        RandomizeEnemyStats(node);
                        isInCombat = !isInCombat;
                    }

                    else if (nodeType == 3)
                    {
                        nodeType = 5;

                    }
                }
                if (canShop)
                {
                    if (Input.GetKeyDown(KeyCode.E) && !isCamera)
                    {
                        spacesRemaining = 0;
                        canShop = false;
                        diceRolled = false;
                        shopMenu.SetActive(false);
                        turnHandler.ProgressTurn();
                        turnHandler.NextTurn();
                        turnHandler.UpdateStatus();
                    }
                }


                // Movement only enabled if character is able to move and is not controlling the camera
                if (isAbleToMove && !isCamera && diceRolled)
                {
                    // Grabs X and Y of the inputs
                    movement.x = Input.GetAxisRaw("Horizontal");
                    movement.y = Input.GetAxisRaw("Vertical");

                    // Starts Move Function
                    StartCoroutine(Move());
                }
                // Toggles control to give control of the camera if they are not moving and hit space
                //if (Input.GetKeyDown(KeyCode.Space) && (isAbleToMove || !diceRolled))
                if (Input.GetKeyDown(KeyCode.Space) && (isAbleToMove || !diceRolled) && !isCamera)
                {
                    // Toggles Flags to enable camera movement and no more player movement
                    isCamera = true;
                    dice.SetActive(false);
                    if (canShop)
                    {
                        shopMenu.SetActive(false);
                    }
                    // Initializes camera to be where the player is
                    dot.position = target.position;
                }
                // Toggles control to give control of the player over the camera if they are in camera movement mode and hit space
                else if (Input.GetKeyDown(KeyCode.Space) && isCamera)
                {
                    // Toggles Flags to enable character movement and disable camera control
                    isCamera = false;
                    if (canShop)
                    {
                        shopMenu.SetActive(true);
                    }
                    if (spacesRemaining > 0 || spacesRemaining == -1)
                    {
                        dice.SetActive(false);
                    }
                    else 
                    {
                        dice.SetActive(true);
                    }
                    
                }
                // Allows control of camera if in camera mode
                if(isCamera){
                    cameraMovement.x = Input.GetAxisRaw("Horizontal");
                    cameraMovement.y = Input.GetAxisRaw("Vertical");
                }
            
                // Animations for the character when moving
                animator.SetFloat("Horizontal", movement.x);
                animator.SetFloat("Vertical", movement.y);
                animator.SetFloat("Speed", movement.sqrMagnitude);
            }
        }
    }

    public void ShopEnd()
    {
        shopMenu.SetActive(false);
        shopOpen = false;
        canShop = false;
    }

    // Function to handle moving
    IEnumerator Move()
    {
        // Checks that character is not unable to move
        if (!isAbleToMove)
        {
            yield break;
        }
        // Toggles ability to move to disable more input
        isAbleToMove = false;
        
        // Checks if player tried to move right
        if (movement.x > 0)
        {
            // Iterates through all nodes to check for connections
            for (int i = 0; i < routeScript.childNodeList.Count; i++)
            {
                // Sees if a connection exists
                if (routeScript.routeMatrix[node,i] == 1)
                {
                    // If a connection exists sees if the node is located to the right
                    if (routeScript.childNodeList[i].position.x > routeScript.childNodeList[node].position.x)
                    {
                        // If so makes sure right animation plays by adjusting x and y of movement and moves to square
                        movement.x = 1;
                        movement.y = 0;
                        while(MoveToSquare(routeScript.childNodeList[i].position)){yield return null;}
                        yield return new WaitForSeconds(0.1f);
                        // Sets characters current node to node it moved to
                        node = i;
                        CheckPath();
                        break;
                    }
                    // If not makes sure no animation plays
                    else
                    {
                        movement.x = 0;
                        movement.y = 0;
                    }
                }
            }
        }
        // Checks if player tried to move left
        else if (movement.x < 0)
        {
            // Iterates through all nodes to check for connections
            for (int i = 0; i < routeScript.childNodeList.Count; i++)
            {
                // Sees if a connection exists
                if (routeScript.routeMatrix[node,i] == 1)
                {
                    // If a connection exists sees if the node is located to the left
                    if (routeScript.childNodeList[i].position.x < routeScript.childNodeList[node].position.x)
                    {
                        // If so makes sure right animation plays by adjusting x and y of movement and moves to square
                        movement.x = -1;
                        movement.y = 0;
                        while(MoveToSquare(routeScript.childNodeList[i].position)){yield return null;}
                        yield return new WaitForSeconds(0.1f);
                        // Sets characters current node to node it moved to
                        node = i;
                        CheckPath();
                        break;
                    }
                    // If not makes sure no animation plays
                    else
                    {
                        movement.x = 0;
                        movement.y = 0;
                    }
                }
            }
        }
        // Checks if player tried to move up
        else if (movement.y > 0)
        {
            // Iterates through all nodes to check for connections
            for (int i = 0; i < routeScript.childNodeList.Count; i++)
            {
                // Sees if a connection exists
                if (routeScript.routeMatrix[node,i] == 1)
                {
                    // If a connection exists sees if the node is located to above
                    if (routeScript.childNodeList[i].position.y > routeScript.childNodeList[node].position.y)
                    {
                        // If so makes sure right animation plays by adjusting x and y of movement and moves to square
                        movement.x = 0;
                        movement.y = 1;
                        while(MoveToSquare(routeScript.childNodeList[i].position)){yield return null;}
                        yield return new WaitForSeconds(0.1f);
                        // Sets characters current node to node it moved to
                        node = i;
                        CheckPath();
                        break;
                    }
                    // If not makes sure no animation plays
                    else
                    {
                        movement.x = 0;
                        movement.y = 0;
                    }
                }
            }
        }
        // Checks if player tried to move down
        else if (movement.y < 0)
        {
            // Iterates through all nodes to check for connections
            for (int i = 0; i < routeScript.childNodeList.Count; i++)
            {
                // Sees if a connection exists
                if (routeScript.routeMatrix[node,i] == 1)
                {
                    // If a connection exists sees if the node is located below
                    if (routeScript.childNodeList[i].position.y < routeScript.childNodeList[node].position.y)
                    {
                        // If so makes sure right animation plays by adjusting x and y of movement and moves to square
                        movement.x = 0;
                        movement.y = -1;
                        while(MoveToSquare(routeScript.childNodeList[i].position)){yield return null;}
                        yield return new WaitForSeconds(0.1f);
                        // Sets characters current node to node it moved to
                        node = i;
                        CheckPath();
                        break;
                    }
                    // If not makes sure no animation plays
                    else
                    {
                        movement.x = 0;
                        movement.y = 0;
                    }
                }
            }
        }
        // Toggles ability to move to enable more input
        isAbleToMove = true;
    }

    // Function to move character and notify when it is completed
    bool MoveToSquare(Vector3 goal)
    {
        return new Vector3(goal.x, goal.y+characterOffset, goal.z) != (this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(goal.x, goal.y+characterOffset, goal.z), 5f*Time.deltaTime));
    }

    // Checks Path Currently Moved to Make Sure Character Moves to Square that they were not previously on
    void CheckPath()
    {
        if (nodesVisited.Count == 1)
        {
            nodesVisited.Add(node);
            spacesRemaining = spacesRemaining - 1;
        }
        else
        {
            if (node != nodesVisited[nodesVisited.Count-2])
            {
                nodesVisited.Add(node);
                spacesRemaining = spacesRemaining - 1;
            }
            else
            {
                nodesVisited.RemoveAt(nodesVisited.Count - 1);
                spacesRemaining = spacesRemaining + 1;
            }
        }
    }

    // Function to randomize a enemies stats and type using the space they are on
    void RandomizeEnemyStats(int onNode)
    {
        // Variables to Hold the Enemy's Level and Type
        int eLevel;
        int eType;
        
        // Different Level Ranges for Spawns in Different Regions
        if(onNode < 20 || onNode > 50)
        {
            // Randomize between level 1-5 enemy
            eLevel = Random.Range(1,6);
            // Randomize Type of Slime
            eType = Random.Range(1,3);
        }
        else
        {
            // Randomize between level 1-5 enemy
            eLevel = Random.Range(6,11);
            // Randomize Type of Slime
            eType = Random.Range(1,5);
        }   
        Debug.Log(eLevel);
        Debug.Log(eType);
    
        // Utilizing the Level and Type, Retrieve the Rest of the stats
        int index = enemyStatsMatrix[eType-1, eLevel-1];
        Debug.Log(index);
        Debug.Log(enemyStatsTable[index,5]);
        // Set the stats for the appropriate enemy save
        if (pcs.isPlayerOneTurn)
        {
            pcs.enemy1Stats[0] = enemyStatsTable[index,0];
            pcs.enemy1Stats[1] = enemyStatsTable[index,0];
            pcs.enemy1Stats[2] = enemyStatsTable[index,1];
            pcs.enemy1Stats[3] = enemyStatsTable[index,2];
            pcs.enemy1Stats[4] = enemyStatsTable[index,3];
            pcs.enemy1Stats[5] = enemyStatsTable[index,4];
            pcs.enemy1Stats[6] = enemyStatsTable[index,5];
            pcs.enemy1Stats[7] = eLevel;
            pcs.enemy1Stats[8] = eType;
        }
        else if (pcs.isPlayerTwoTurn)
        {
            pcs.enemy2Stats[0] = enemyStatsTable[index,0];
            pcs.enemy2Stats[1] = enemyStatsTable[index,0];
            pcs.enemy2Stats[2] = enemyStatsTable[index,1];
            pcs.enemy2Stats[3] = enemyStatsTable[index,2];
            pcs.enemy2Stats[4] = enemyStatsTable[index,3];
            pcs.enemy2Stats[5] = enemyStatsTable[index,4];
            pcs.enemy2Stats[6] = enemyStatsTable[index,5];
            pcs.enemy2Stats[7] = eLevel;
            pcs.enemy2Stats[8] = eType;
        }
    }
}