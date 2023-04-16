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
}
