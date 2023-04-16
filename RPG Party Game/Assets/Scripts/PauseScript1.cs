using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class PauseScript1 : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject controlsMenu;
    public static bool isPaused;
    public SaveWrapper saveWrapper;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        controlsMenu.SetActive(false);
        saveWrapper = GameObject.FindGameObjectWithTag("SaveWrapper").GetComponent<SaveWrapper>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
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
