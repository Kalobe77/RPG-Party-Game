using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputScript : MonoBehaviour
{

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

    // Start is called before the first frame update
    void Start()
    {  
        // Links the input script using the tag
        cameraFollow = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
        turnHandler = GameObject.FindGameObjectWithTag("TurnLogic").GetComponent<TurnHandlerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isTurn)
        {
            if (spacesRemaining == 0 && !diceRolled)
            {
                dice.SetActive(true);
            }
            if (spacesRemaining == 0 && diceRolled){
                // Resets Everything
                movement.x = 0;
                movement.y = 0;
                diceRolled = false;
                dice.transform.position = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);
                nodesVisited.Clear();
                turnHandler.ProgressTurn();
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

            // Toggles control to give control to player over the camera if they are not moving and hit space
            if (Input.GetKeyDown(KeyCode.Space) && isAbleToMove)
            {
                // Toggles Flags to enable camera movement and no more player movement
                isCamera = true;
                isAbleToMove = false;
                isAbleToRoll = false;
                dice.SetActive(false);

                // Initializes camera to be where the player is
                dot.position = target.position;
            }
            // Toggles control to give control to player over the camera if they are in camera movement mode and hit space
            else if (Input.GetKeyDown(KeyCode.Space) && isCamera && !isAbleToRoll)
            {
                // Toggles Flags to enable character movement and disable camera control
                isCamera = false;
                isAbleToMove = true;
                isAbleToRoll = true;
                dice.SetActive(true);
            }
            // Allows control of camera if in camera mode
            if(isCamera){
                cameraMovement.x = Input.GetAxisRaw("Horizontal");
                cameraMovement.y = Input.GetAxisRaw("Vertical");
            }
        }
        // Animations for the character when moving
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
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
}
