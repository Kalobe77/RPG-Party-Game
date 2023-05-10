using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RightScript : MonoBehaviour
{
    // Animations for Enemys
    public Animator animator;

    // Different Controllers for Different Enemies
    public RuntimeAnimatorController slime;
    public RuntimeAnimatorController slime_fire;
    public RuntimeAnimatorController slime_ice;
    public RuntimeAnimatorController slime_toxic;
    public RuntimeAnimatorController slime_boss;
    public RuntimeAnimatorController slime_boss_transformed;

    // Access to sprite renderer for right script
    public SpriteRenderer spriteRenderer;

    // Variable to see if combat is between two players
    public bool isAnotherPlayer;

    // Connects the PlayerCharacterStatus Object
    public PlayerCharacterStatus pcs;

    // Connects to the Battle Logic Calcs Script
    public BattleLogic_Calcs blc;
    
    // Connects to Battle Screen Logic Script
    public BattleScreenLogic bsl;

    // Variables to Control Animation
    public int enemyType;
    public int enemyAnimation;

    // Start is called before the first frame update
    void Start()
    {
        if(pcs.isPlayerOneTurn)
        {
            enemyType = pcs.enemy1Stats[8];
            enemyAnimation = 0;
        }
        else if(pcs.isPlayerTwoTurn)
        {
            enemyType = pcs.enemy2Stats[8];
            enemyAnimation = 0;
        }

        if(enemyType == 1)
        {
            animator.runtimeAnimatorController = slime;
        }
        else if(enemyType == 2)
        {
            animator.runtimeAnimatorController = slime_fire;
        }
        else if(enemyType == 3)
        {
            animator.runtimeAnimatorController = slime_ice;
        }
        else if(enemyType == 4)
        {
            animator.runtimeAnimatorController = slime_toxic;
        }

        if (enemyType == 5)
        {
            animator.runtimeAnimatorController = slime_boss;

            // Animations for enemy
            animator.SetInteger("enemyAnimation", enemyAnimation);
            transform.localScale = new Vector3(5,5,0);
            transform.position = new Vector3(6.25f, 0, 0);
        }
        else if (enemyType == 6)
        {
            animator.runtimeAnimatorController = slime_boss_transformed;

            // Animations for enemy
            animator.SetInteger("enemyAnimation", enemyAnimation);
            transform.localScale = new Vector3(5,5,0);
            transform.position = new Vector3(6.25f, 0, 0);
        }
        else
        {
            // Animations for enemy
            animator.SetInteger("enemyAnimation", enemyAnimation);
            
            transform.localScale = new Vector3(5,5,0);
            transform.position = new Vector3(6.25f, -2.75f, 0);
        }
        
    }

    public void ChangeAnimation(int type)
    {
        blc.CheckBattleStatus();
        if (blc.battleStatus == 1)
        {
            animator.SetInteger("enemyAnimation", 1);
        }
        else
        {
            animator.SetInteger("enemyAnimation", type);
        }
    }

    public void GiveAtkOrientation(int atkType)
    {
        animator.SetInteger("enemyAtk", atkType);
    }

    public void AnimationComplete()
    {
        blc.CheckBattleStatus();
        if (blc.actionsOfCombatLeft == 0 && blc.battleStatus == 0)
        {
            blc.SaveStats();
            blc.NextTurn();
            SceneManager.LoadScene("Scenes/Overworld");
        }
        else if (blc.battleStatus == 1)
        {
            blc.allowedInput = false;
            blc.endScreenInput = true;
            blc.TurnOffUIs();
            if (!blc.alreadyHappened)
            {
                blc.ExperienceGain();
                blc.RandomGems();
                blc.itemEarned = blc.RandomItem();
                bsl.ManipulateText(blc.experienceGained, blc.itemEarned, blc.gems);
                blc.alreadyHappened = true;
            }
        }
        else if (blc.battleStatus == 2)
        {
            blc.allowedInput = false;
            blc.endScreenInput = true;
            blc.TurnOffUIs();
            bsl.TurnOnLost();
        }
        else 
        {
            blc.allowedInput = true;
            blc.TurnProperUI();
        }
    }

}
