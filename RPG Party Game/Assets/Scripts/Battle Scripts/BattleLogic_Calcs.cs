using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleLogic_Calcs : MonoBehaviour
{
    // Who is Attacker First
    public bool leftIsAttacker = true;

    // Stats for character on left
    public int remaininghp_left;
    public int maxhp_left;
    public int atk_left;
    public int def_left;
    public int mag_left;
    public int res_left;
    public int spd_left;
    public int inputLeft;
    public bool leftTurn;

    // Stats for character on right
    public int remaininghp_right;
    public int maxhp_right;
    public int atk_right;
    public int def_right;
    public int mag_right;
    public int res_right;
    public int spd_right;
    public int inputRight;
    public bool rightTurn;
    public bool rightPlayerIsComputer = true;
    public int enemy_level;

    public bool state = true;

    // UI and allows us to control whether they appear
    public GameObject leftUI;
    public GameObject rightUI;

    // Used to manage if a user can put another input in and where in combat
    public bool allowedInput = true;
    public int actionsOfCombatLeft = 4;

    // Input holder
    Vector2 input;
    
    // Variable to add delay length in seconds between inputs
    public float delayBetweenInputs;

    // Gives access to the health bar scripts
    public HealthBarScript leftHealthScript;
    public HealthBarScript rightHealthScript;

    // To Allow Data to be loaded from scriptable object
    public PlayerCharacterStatus pcs;

    // Start is called before the first frame update
    void Start()
    {
        if(pcs.isPlayerOneTurn)
        {
            remaininghp_left = pcs.remaininghp_one;
            maxhp_left = pcs.maxhp_one;
            atk_left = pcs.atk_one;
            def_left = pcs.def_one;
            mag_left = pcs.mag_one;
            res_left = pcs.res_one;
            spd_left = pcs.spd_one;

            remaininghp_right = pcs.enemy1Stats[0];
            maxhp_right = pcs.enemy1Stats[1];
            atk_right = pcs.enemy1Stats[2];
            def_right = pcs.enemy1Stats[3];
            mag_right = pcs.enemy1Stats[4];
            res_right = pcs.enemy1Stats[5];
            spd_right = pcs.enemy1Stats[6];
            enemy_level = pcs.enemy1Stats[7];
        }
        else
        {
            remaininghp_left = pcs.remaininghp_two;
            maxhp_left = pcs.maxhp_two;
            atk_left = pcs.atk_two;
            def_left = pcs.def_two;
            mag_left = pcs.mag_two;
            res_left = pcs.res_two;
            spd_left = pcs.spd_two;

            remaininghp_right = pcs.enemy2Stats[0];
            maxhp_right = pcs.enemy2Stats[1];
            atk_right = pcs.enemy2Stats[2];
            def_right = pcs.enemy2Stats[3];
            mag_right = pcs.enemy2Stats[4];
            res_right = pcs.enemy2Stats[5];
            spd_right = pcs.enemy2Stats[6];
            enemy_level = pcs.enemy2Stats[7];
        }
        leftHealthScript.SetMaxHealth(maxhp_left, remaininghp_left);
        rightHealthScript.SetMaxHealth(maxhp_right, remaininghp_right);
        leftUI.SetActive(false);
        rightUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(state)
        {
            Debug.Log("Did Run");
            state = false;
            allowedInput = false;
            // Starts Move Function
            StartCoroutine(DelayStart());
        }
        if (allowedInput)
        {
            if (rightTurn && rightPlayerIsComputer)
            {
                AI();
                StartCoroutine(BattleCommand());
            }
            else
            {
                // Grabs X and Y of the inputs
                input.x = Input.GetAxisRaw("Horizontal");
                input.y = Input.GetAxisRaw("Vertical");

                if (input.sqrMagnitude > 0)
                {
                    // Starts Battle Function
                    StartCoroutine(BattleCommand());
                }
            }
            
            
        }
        if (remaininghp_right <= 0)
        {
            if (pcs.isPlayerOneTurn)
            {
                pcs.remaininghp_one = remaininghp_left;
                pcs.maxhp_one = maxhp_left;
                pcs.atk_one = atk_left;
                pcs.def_one = def_left;
                pcs.mag_one = mag_left;
                pcs.res_one = res_left;
                pcs.spd_one = spd_left;
                ExperienceGain();
                pcs.isPlayerOneInCombat = !pcs.isPlayerOneInCombat;
                NextTurn();
            }
            else if (pcs.isPlayerTwoTurn)
            {    
                pcs.remaininghp_two = remaininghp_left;
                pcs.maxhp_two = maxhp_left;
                pcs.atk_two = atk_left;
                pcs.def_two = def_left;
                pcs.mag_two = mag_left;
                pcs.res_two = res_left;
                pcs.spd_two = spd_left;
                ExperienceGain();
                pcs.isPlayerTwoInCombat = !pcs.isPlayerTwoInCombat;
                NextTurn();
            }
            SceneManager.LoadScene("Scenes/Overworld");
        }
        if (remaininghp_left <= 0)
        {
            if (pcs.isPlayerOneTurn)
            {
                pcs.remaininghp_one = maxhp_left;
                pcs.maxhp_one = maxhp_left;
                pcs.atk_one = atk_left;
                pcs.def_one = def_left;
                pcs.mag_one = mag_left;
                pcs.res_one = res_left;
                pcs.spd_one = spd_left;
                pcs.isPlayerOneInCombat = !pcs.isPlayerOneInCombat;
                NextTurn();
            }
            else if (pcs.isPlayerTwoTurn)
            {    
                pcs.remaininghp_two = maxhp_left;
                pcs.maxhp_two = maxhp_left;
                pcs.atk_two = atk_left;
                pcs.def_two = def_left;
                pcs.mag_two = mag_left;
                pcs.res_two = res_left;
                pcs.spd_two = spd_left;
                pcs.isPlayerTwoInCombat = !pcs.isPlayerTwoInCombat;
                NextTurn();
            }
            SceneManager.LoadScene("Scenes/Overworld");
        }
        
    }

    IEnumerator BattleCommand()
    {
        // Checks that character is not unable to move
        if (!allowedInput)
        {
            yield break;
        }
        // Toggles ability to move to disable more input
        allowedInput = false;

        if(input.x > 0){
            input.x = 1;
            input.y = 0;
            if(leftTurn){
                inputLeft = 1;
                CombatManager();
                yield return new WaitForSeconds(delayBetweenInputs);
            }
            else{
                inputRight = 1;
                CombatManager();
                yield return new WaitForSeconds(delayBetweenInputs);
            }
        }
        else if (input.x < 0)
        {
            input.x = -1;
            input.y = 0;
            if(leftTurn){
                inputLeft = 2;
                CombatManager();
                yield return new WaitForSeconds(delayBetweenInputs);
            }
            else{
                inputRight = 2;
                CombatManager();
                yield return new WaitForSeconds(delayBetweenInputs);
            }
        }
        else if (input.y > 0)
        {
            input.x = 0;
            input.y = 1;
            if(leftTurn){
                inputLeft = 3;
                CombatManager();
                yield return new WaitForSeconds(delayBetweenInputs);
            }
            else{
                inputRight = 3;
                CombatManager();
                yield return new WaitForSeconds(delayBetweenInputs);
            }
        }
        else if (input.y < 0)
        {
            input.x = 0;
            input.y = -1;
            if(leftTurn){
                inputLeft = 4;
                CombatManager();
                yield return new WaitForSeconds(delayBetweenInputs);
            }
            else{
                inputRight = 4;
                CombatManager();
                yield return new WaitForSeconds(delayBetweenInputs);
            }
        }
        input.x = 0;
        input.y = 0;
        allowedInput = true;
    }

    public void CombatManager()
    {
        actionsOfCombatLeft = actionsOfCombatLeft - 1;
        if (actionsOfCombatLeft == 0)
        {
            if (leftIsAttacker)
            {
                ModifierCalculation(inputLeft, inputRight);
            }
            else
            {
                ModifierCalculation(inputRight, inputLeft);
            }
            // To Allow Data to be loaded from scriptable object
            if (pcs.isPlayerOneTurn)
            {
                pcs.remaininghp_one = remaininghp_left;
                pcs.maxhp_one = maxhp_left;
                pcs.atk_one = atk_left;
                pcs.def_one = def_left;
                pcs.mag_one = mag_left;
                pcs.res_one = res_left;
                pcs.spd_one = spd_left;
                pcs.enemy1Stats[0] = remaininghp_right;
                pcs.enemy1Stats[1] = maxhp_right;
                pcs.enemy1Stats[2] = atk_right;
                pcs.enemy1Stats[3] = def_right;
                pcs.enemy1Stats[4] = mag_right;
                pcs.enemy1Stats[5] = res_right;
                pcs.enemy1Stats[6] = spd_right;
                NextTurn();
            }
            else if (pcs.isPlayerTwoTurn)
            {    
                pcs.remaininghp_two= remaininghp_left;
                pcs.maxhp_two = maxhp_left;
                pcs.atk_two = atk_left;
                pcs.def_two = def_left;
                pcs.mag_two = mag_left;
                pcs.res_two = res_left;
                pcs.spd_two = spd_left;
                pcs.enemy2Stats[0] = remaininghp_right;
                pcs.enemy2Stats[1] = maxhp_right;
                pcs.enemy2Stats[2] = atk_right;
                pcs.enemy2Stats[3] = def_right;
                pcs.enemy2Stats[4] = mag_right;
                pcs.enemy2Stats[5] = res_right;
                pcs.enemy2Stats[6] = spd_right;
                NextTurn();
            }
            SceneManager.LoadScene("Scenes/Overworld");
        }
        else if (actionsOfCombatLeft == 2)
        {
            rightUI.SetActive(false);
            leftUI.SetActive(false);
            if (leftIsAttacker)
            {
                ModifierCalculation(inputLeft, inputRight);
            }
            else
            {
                ModifierCalculation(inputRight, inputLeft);
            }
            leftIsAttacker = !leftIsAttacker;
            if (leftTurn)
            {
                leftUI.SetActive(true);
            }
            else
            {
                leftUI.SetActive(false);
            }
            if (rightTurn)
            {
                rightUI.SetActive(true);
            }
            else
            {
                rightUI.SetActive(false);
            }
        }
        else
        {
            leftTurn = !leftTurn;
            rightTurn = !rightTurn;
            if (leftTurn)
            {
                leftUI.SetActive(true);
            }
            else
            {
                leftUI.SetActive(false);
            }
            if (rightTurn)
            {
                rightUI.SetActive(true);
            }
            else
            {
                rightUI.SetActive(false);
            }
        }
        return;
    }

    public void ModifierCalculation(int attackerInput, int defenderInput)
    {
        float modifier;
        if (attackerInput == 1)
        {
            if (defenderInput == 1)
            {
                modifier = 1f;
                if (leftIsAttacker)
                {
                    DamageCalculation(modifier, atk_left, def_right, leftIsAttacker);
                }
                else
                {
                    DamageCalculation(modifier, atk_right, def_left, leftIsAttacker);
                }
            }
            else if (defenderInput == 2)
            {
                modifier = 1.5f;
                if (leftIsAttacker)
                {
                    DamageCalculation(modifier, atk_left, def_right, leftIsAttacker);
                }
                else
                {
                    DamageCalculation(modifier, atk_right, def_left, leftIsAttacker);
                }
            }
            else if (defenderInput == 3)
            {
                modifier = 1.2f;
                if (leftIsAttacker)
                {
                    DamageCalculation(modifier, atk_left, def_right, leftIsAttacker);
                }
                else
                {
                    DamageCalculation(modifier, atk_right, def_left, leftIsAttacker);
                }
            }
            else if (defenderInput == 4)
            {
                modifier = 1f;
                if (leftIsAttacker)
                {
                    DamageCalculation(modifier, atk_left, def_right, leftIsAttacker);
                }
                else
                {
                    DamageCalculation(modifier, atk_right, def_left, leftIsAttacker);
                }
            }
        }
        else if (attackerInput == 2)
        {
            if (defenderInput == 1)
            {
                modifier = 1.8f;
                if (leftIsAttacker)
                {
                    DamageCalculation(modifier, atk_left, def_right, leftIsAttacker);
                }
                else
                {
                    DamageCalculation(modifier, atk_right, def_left, leftIsAttacker);
                }
            }
            else if (defenderInput == 2)
            {
                modifier = 2f;
                if (!leftIsAttacker)
                {
                    DamageCalculation(modifier, atk_left, def_right, !leftIsAttacker);
                }
                else
                {
                    DamageCalculation(modifier, atk_right, def_left, !leftIsAttacker);
                }
            }
            else if (defenderInput == 3)
            {
                modifier = 1.7f;
                if (leftIsAttacker)
                {
                    DamageCalculation(modifier, atk_left, def_right, leftIsAttacker);
                }
                else
                {
                    DamageCalculation(modifier, atk_right, def_left, leftIsAttacker);
                }
            }
            else if (defenderInput == 4)
            {
                modifier = 1f;
                if (leftIsAttacker)
                {
                    DamageCalculation(modifier, atk_left, def_right, leftIsAttacker);
                }
                else
                {
                    DamageCalculation(modifier, atk_right, def_left, leftIsAttacker);
                }
            }
        }
        else if (attackerInput == 3)
        {
            if (defenderInput == 1)
            {
                modifier = 1.2f;
                if (leftIsAttacker)
                {
                    DamageCalculation(modifier, mag_left, res_right, leftIsAttacker);
                }
                else
                {
                    DamageCalculation(modifier, mag_right, res_left, leftIsAttacker);
                }
            }
            else if (defenderInput == 2)
            {
                modifier = 1.5f;
                if (leftIsAttacker)
                {
                    DamageCalculation(modifier, mag_left, res_right, leftIsAttacker);
                }
                else
                {
                    DamageCalculation(modifier, mag_right, res_left, leftIsAttacker);
                }
            }
            else if (defenderInput == 3)
            {
                modifier = 1f;
                if (leftIsAttacker)
                {
                    DamageCalculation(modifier, mag_left, res_right, leftIsAttacker);
                }
                else
                {
                    DamageCalculation(modifier, mag_right, res_left, leftIsAttacker);
                }
            }
            else if (defenderInput == 4)
            {
                modifier = 1f;
                if (leftIsAttacker)
                {
                    DamageCalculation(modifier, mag_left, res_right, leftIsAttacker);
                }
                else
                {
                    DamageCalculation(modifier, mag_right, res_left, leftIsAttacker);
                }
            }
        }
        else if (attackerInput == 4)
        {
            if (defenderInput == 1)
            {
                modifier = 1.2f;
                if (leftIsAttacker)
                {
                    DamageCalculation(modifier, mag_left, res_right, leftIsAttacker);
                }
                else
                {
                    DamageCalculation(modifier, mag_right, res_left, leftIsAttacker);
                }
            }
            else if (defenderInput == 2)
            {
                modifier = 1.2f;
                if (leftIsAttacker)
                {
                    DamageCalculation(modifier, mag_left, res_right, leftIsAttacker);
                }
                else
                {
                    DamageCalculation(modifier, mag_right, res_left, leftIsAttacker);
                }
            }
            else if (defenderInput == 3)
            {
                modifier = 1.2f;
                if (leftIsAttacker)
                {
                    DamageCalculation(modifier, mag_left, res_right, leftIsAttacker);
                }
                else
                {
                    DamageCalculation(modifier, mag_right, res_left, leftIsAttacker);
                }
            }
            else if (defenderInput == 4)
            {
                modifier = 1.2f;
                if (leftIsAttacker)
                {
                    DamageCalculation(modifier, mag_left, res_right, leftIsAttacker);
                }
                else
                {
                    DamageCalculation(modifier, mag_right, res_left, leftIsAttacker);
                }
            }
        }
        return;
    }

    public void DamageCalculation(float modifier, int offenseStat, int defenseStat, bool isDamageToLeft)
    {
        int damage = Mathf.RoundToInt((offenseStat - defenseStat/2)*modifier);
        if (damage > 0)
        {
            if (!isDamageToLeft)
            {
                remaininghp_left = remaininghp_left - damage;
                if (remaininghp_left < 0)
                {
                    remaininghp_left = 0;
                }
                leftHealthScript.SetHealth(remaininghp_left);
            }
            else
            {
                remaininghp_right = remaininghp_right - damage;
                if (remaininghp_right < 0)
                {
                    remaininghp_right = 0;
                }
                rightHealthScript.SetHealth(remaininghp_right);
            }
        }
    }

    public void NextTurn()
    {
        if (pcs.isPlayerOneTurn)
        {
            pcs.isPlayerOneTurn = !pcs.isPlayerOneTurn;
            pcs.isPlayerTwoTurn = !pcs.isPlayerTwoTurn;
        }
        else if (pcs.isPlayerTwoTurn)
        {
            pcs.isPlayerOneTurn = !pcs.isPlayerOneTurn;
            pcs.isPlayerTwoTurn = !pcs.isPlayerTwoTurn;
        }
    }

    public void ExperienceGain()
    {
        if (pcs.isPlayerOneTurn)
        {
            int experience = 70;
            if (pcs.level_one >= pcs.enemy1Stats[7])
            {
                experience = Mathf.RoundToInt(70*Mathf.Exp(-.3f*(pcs.level_one-pcs.enemy1Stats[7])));
            }
            pcs.exp_one = pcs.exp_one + experience;
            if (pcs.exp_one >= 100)
            {
                LevelUp();
            }
        }
        else if (pcs.isPlayerTwoTurn)
        {
            int experience = 70;
            if (pcs.level_two >= pcs.enemy2Stats[7])
            {
                experience = Mathf.RoundToInt(70*Mathf.Exp(-.3f*(pcs.level_one-pcs.enemy1Stats[7])));
            }
            if (pcs.exp_two >= 100)
            {
                LevelUp();
            }
        }
    }

    public void LevelUp()
    {
        if (pcs.isPlayerOneTurn)
        {
            pcs.exp_one = pcs.exp_one - 100;
            pcs.level_one = pcs.level_one + 1;
            pcs.maxhp_one = pcs.maxhp_one + 10;
            pcs.remaininghp_one = pcs.maxhp_one;
            pcs.atk_one = pcs.atk_one + 1;
            pcs.def_one = pcs.def_one + 1;
            pcs.mag_one = pcs.mag_one + 1;
            pcs.res_one = pcs.res_one + 1;
            pcs.spd_one = pcs.spd_one + 1;
        }
        else if (pcs.isPlayerTwoTurn)
        {
            pcs.exp_two = pcs.exp_two - 100;
            pcs.level_two = pcs.level_two + 1;
            pcs.maxhp_two = pcs.maxhp_two + 10;
            pcs.remaininghp_two = pcs.maxhp_two;
            pcs.atk_two = pcs.atk_two + 1;
            pcs.def_two = pcs.def_two + 1;
            pcs.mag_two = pcs.mag_two + 1;
            pcs.res_two = pcs.res_two + 1;
            pcs.spd_two = pcs.spd_two + 1;
        }
    }

    public void AI()
    {
        float chanceToGuessCorrectly = (float).7*Mathf.Log(enemy_level)/Mathf.Log(59);
        float number = Random.Range(0.0f, 1.0f);
        if (leftIsAttacker)
        {
            if (chanceToGuessCorrectly >= number)
            {
                if (inputLeft == 1)
                {
                    input.x = 1;
                    input.y = 0;
                }
                else if (inputLeft == 2)
                {
                    input.x = -1;
                    input.y = 0;
                }
                else if (inputLeft == 3)
                {
                    input.x = 0;
                    input.y = 1;
                }
                else if (inputLeft == 4)
                {
                    input.x = 0;
                    input.y = -1;
                }
            }
            else
            {
                int aiInput = Random.Range(1,5);
                if (aiInput == 1)
                {
                    input.x = 1;
                    input.y = 0;
                }
                else if (aiInput == 2)
                {
                    input.x = -1;
                    input.y = 0;
                }
                else if (aiInput == 3)
                {
                    input.x = 0;
                    input.y = 1;
                }
                else if (aiInput == 4)
                {
                    input.x = 0;
                    input.y = -1;
                }
            }
        }
        else
        {
            if (chanceToGuessCorrectly >= number)
            {
                if (atk_left - def_left/2 > mag_right - res_left/2)
                {
                    input.x = 1;
                    input.y = 0;

                }
                else if(atk_left - def_left/2 < mag_right - res_left/2)
                {
                    input.x = -1;
                    input.y = 0;
                }
                else
                {
                    int variable = Random.Range(0,2);
                    if (variable == 1)
                    {
                        input.x = variable;
                    }
                    else
                    {
                        input.x = variable - 1;
                    }
                    input.y = 0;
                }
            }
            else
            {
                int aiInput = Random.Range(1,5);
                if (aiInput == 1)
                {
                    input.x = 1;
                    input.y = 0;
                }
                else if (aiInput == 2)
                {
                    input.x = -1;
                    input.y = 0;
                }
                else if (aiInput == 3)
                {
                    input.x = 0;
                    input.y = 1;
                }
                else if (aiInput == 4)
                {
                    input.x = 0;
                    input.y = -1;
                }
            }
        }
        Debug.Log(input);
    }

    IEnumerator DelayStart()
    {
        yield return new WaitForSeconds(10f);
        if (leftIsAttacker)
        {
            leftTurn = true;
            rightTurn = false;
            leftUI.SetActive(true);
        }
        else
        {
            leftTurn = false;
            rightTurn = true;
            rightUI.SetActive(true);
            leftUI.SetActive(false);
        }
        allowedInput = true;
    }

}

