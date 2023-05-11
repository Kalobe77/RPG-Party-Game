/********************************************************************************
 *   Filename:   LeftScript.cs
 *   Date:       2023-05-10
 *   Authors:    Kaleb Gearinger and Adam Stefan
 *   Email:      kgearinger@muhlenberg.edu and astefan@muhlenberg.edu
 *   Description:
 *       This file primarily handles the animations that happen regarding the
 *       character that is on the left of the screen. It's functions work to
 *       manipulate the animator based on what outcome has occured from the inputs
 *       given in the BattleLogic_Calcs.cs.
 ********************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftScript : MonoBehaviour
{
    // Renders Character Sprite
    public Sprite character;

    // Animations for Player
    public Animator animator;

    // Access to sprite renderer for right script
    public SpriteRenderer spriteRenderer;

    // Connects to the Battle Logic Calcs Script
    public BattleLogic_Calcs blc;
    
    // Start is called before the first frame update
    void Start()
    {
        // Renders Sprite Input
        spriteRenderer.sprite = character;

        // Sets Sprite so it is lined up at y = 0
        float offSetSprite = spriteRenderer.bounds.size.y/2;
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + offSetSprite, this.transform.position.z);
    }
    
    // Changes the animation of the player
    public void ChangeAnimation(int type)
    {
        blc.CheckBattleStatus();
        if (blc.battleStatus == 2)
        {
            animator.SetInteger("playerAnimation", 1);
        }
        else
        {
            animator.SetInteger("playerAnimation", type);
        }   
    }
}
