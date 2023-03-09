using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputScript : MonoBehaviour
{

    // Used to change velocity and position
    public Rigidbody2D rb;
    public Animator animator;

    public float movementSpeed;
    Vector2 movement;
    int node = 0;
    bool isMoving = false;
    public float characterOffset = .7f;

    public Route routeScript;

    // Update is called once per frame
    void Update()
    {
        // Input
        if (!isMoving)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            StartCoroutine(Move());
        }
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    IEnumerator Move()
    {
        if (isMoving)
        {
            yield break;
        }
        isMoving = true;
        if (movement.x > 0)
        {
            for (int i = 0; i < routeScript.childNodeList.Count; i++)
            {
                if (routeScript.routeMatrix[node,i] == 1)
                {
                    if (routeScript.childNodeList[i].position.x > routeScript.childNodeList[node].position.x)
                    {
                        movement.x = 1;
                        movement.y = 0;
                        while(MoveToSquare(routeScript.childNodeList[i].position)){yield return null;}
                        yield return new WaitForSeconds(0.1f);
                        node = i;
                        break;
                    }
                    else
                    {
                        movement.x = 0;
                        movement.y = 0;
                    }
                }
            }
        }
        else if (movement.x < 0)
        {
            for (int i = 0; i < routeScript.childNodeList.Count; i++)
            {
                if (routeScript.routeMatrix[node,i] == 1)
                {
                    if (routeScript.childNodeList[i].position.x < routeScript.childNodeList[node].position.x)
                    {
                        movement.x = -1;
                        movement.y = 0;
                        while(MoveToSquare(routeScript.childNodeList[i].position)){yield return null;}
                        yield return new WaitForSeconds(0.1f);
                        node = i;
                        break;
                    }
                    else
                    {
                        movement.x = 0;
                        movement.y = 0;
                    }
                }
            }
        }
        else if (movement.y > 0)
        {
            for (int i = 0; i < routeScript.childNodeList.Count; i++)
            {
                if (routeScript.routeMatrix[node,i] == 1)
                {
                    if (routeScript.childNodeList[i].position.y > routeScript.childNodeList[node].position.y)
                    {
                        movement.x = 0;
                        movement.y = 1;
                        while(MoveToSquare(routeScript.childNodeList[i].position)){yield return null;}
                        yield return new WaitForSeconds(0.1f);
                        node = i;
                        break;
                    }
                    else
                    {
                        movement.x = 0;
                        movement.y = 0;
                    }
                }
            }
        }
        else if (movement.y < 0)
        {
            for (int i = 0; i < routeScript.childNodeList.Count; i++)
            {
                if (routeScript.routeMatrix[node,i] == 1)
                {
                    if (routeScript.childNodeList[i].position.y < routeScript.childNodeList[node].position.y)
                    {
                        movement.x = 0;
                        movement.y = -1;
                        while(MoveToSquare(routeScript.childNodeList[i].position)){yield return null;}
                        yield return new WaitForSeconds(0.1f);
                        node = i;
                        break;
                    }
                    else
                    {
                        movement.x = 0;
                        movement.y = 0;
                    }
                }
            }
        }
        isMoving = false;
    }

    bool MoveToSquare(Vector3 goal)
    {
        return new Vector3(goal.x, goal.y+characterOffset, goal.z) != (this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(goal.x, goal.y+characterOffset, goal.z), 5f*Time.deltaTime));
    }
}
