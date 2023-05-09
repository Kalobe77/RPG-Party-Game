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

    // Access to sprite renderer for right script
    public SpriteRenderer spriteRenderer;

    // Variable to see if combat is between two players
    public bool isAnotherPlayer;

    // Connects the PlayerCharacterStatus Object
    public PlayerCharacterStatus pcs;

    public BattleLogic_Calcs blc;

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

        // Animations for enemy
        animator.SetInteger("enemyAnimation", enemyAnimation);
        
        transform.localScale = new Vector3(5,5,0);
        transform.position = new Vector3(6.25f, -2.75f, 0);
    }

    public void ChangeAnimation(int type)
    {
        animator.SetInteger("enemyAnimation", type);
    }

    public void AnimationComplete()
    {
        if (blc.actionsOfCombatLeft == 0)
        {
            SceneManager.LoadScene("Scenes/Overworld");
        }
        blc.allowedInput = true;
        blc.TurnProperUI();
    }
}
