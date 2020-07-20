using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    private GameState gs;
    // Start is called before the first frame update
    void Start()
    {
        gs = Camera.main.GetComponent<GameState>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (gs.Paused)
            {
                gs.Paused = false;
                pauseMenu.SetActive(false);
            }
            else
            {
                gs.Paused = true;
                pauseMenu.SetActive(true);
            }
        }
        
    }
    public void Resume()
    {
        gs.Paused = false;
        pauseMenu.SetActive(false);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Upgrades()
    {
        SceneManager.LoadScene(1);
    }
}
