/********************************************************************************
 *   Filename:   HealthBarScript.cs
 *   Date:       2023-05-10
 *   Authors:    Kaleb Gearinger and Adam Stefan
 *   Email:      kgearinger@muhlenberg.edu and astefan@muhlenberg.edu
 *   Description:
 *       Provides the ability manipulate the health bar. Called by 
 *       BattleLogic_Calcs.cs.
 ********************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{

    public Slider slider;

    public void SetHealth(int health)
    {
        slider.value = health;
    }

    public void SetMaxHealth(int maxHealth, int remainingHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = remainingHealth;
    }
}
