using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class PauseScript1 : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject controlsMenu;
    //public static bool isPaused;
    public static bool isPaused;
    public SaveWrapper saveWrapper;

    // Takes in input script utilizing player tag
    public InputScript inputScript;
    public InputScript inputScript2;
    public InventoryScript inventory1;
    public InventoryScript inventory2;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        controlsMenu.SetActive(false);
        saveWrapper = GameObject.FindGameObjectWithTag("SaveWrapper").GetComponent<SaveWrapper>();
        // Links the input script using the tag
        inputScript = GameObject.FindGameObjectWithTag("Player 1").GetComponent<InputScript>();
        inputScript2 = GameObject.FindGameObjectWithTag("Player 2").GetComponent<InputScript>();
        inventory1 = GameObject.FindGameObjectWithTag("Player1Inventory").GetComponent<InventoryScript>();
        inventory2 = GameObject.FindGameObjectWithTag("Player2Inventory").GetComponent<InventoryScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && !inputScript.shopOpen && !inputScript2.shopOpen && !inventory1.isInventory && !inventory2.isInventory)
        {
            if (isPaused)
            {
                ResumeGame();
            }

            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        controlsMenu.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        controlsMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }




    public void Controls()
    {
        pauseMenu.SetActive(false);
        controlsMenu.SetActive(true);
    }

    public void Quit()
    {
        ResumeGame();
        saveWrapper.writeToFile();
        //Time.timeScale = 1f;
        SceneManager.LoadScene("Title Screen");
    }

    
}
