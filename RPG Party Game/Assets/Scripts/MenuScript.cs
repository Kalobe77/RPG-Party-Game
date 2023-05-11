/********************************************************************************
 *   Filename:   MenuScript.cs
 *   Date:       2023-05-10
 *   Authors:    Kaleb Gearinger and Adam Stefan
 *   Email:      kgearinger@muhlenberg.edu and astefan@muhlenberg.edu
 *   Description:
 *       This file handles the different buttons on the title screen and menus.
 *       The functions make sure the proper file is called utilizing SaveWrapper
 *       to manipulate the PlayerCharacterStatus object variables, which then allows
 *       the proper variables to be paseed to the next scene.
 ********************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public SaveWrapper saveWrapper;
    void Start()
    {
        saveWrapper = GameObject.FindGameObjectWithTag("SaveWrapper").GetComponent<SaveWrapper>();
    }
    // Title Screen
    public void PlayGame()
    {
        SceneManager.LoadScene("NewLoad Game");
    }

    public void Tutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
///////////////
    // New/Load Game Screen
    public void Players()
    {
        SceneManager.LoadScene("Players");
    }

    public void LoadGame()
    {
        saveWrapper.readFromFile("/Saves/saveFile.txt");
        SceneManager.LoadScene("OverWorld");
    }

    public void BackTitle()
    {
        SceneManager.LoadScene("Title Screen");
    }
///////////////
    // Amount of Players Screen
    public void PlayNew()
    {
        saveWrapper.readFromFile("/Saves/Player2Default.txt");
        SceneManager.LoadScene("OverWorld");
    }

    public void BackNewLoad()
    {
        SceneManager.LoadScene("NewLoad Game");
    }

    // Exit to Main Menu
    public void QuitToMenu()
    {
        SceneManager.LoadScene("Title Screen");
    }
}
