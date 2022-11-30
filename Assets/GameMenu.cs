using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{

    public GameObject DeathScreen;
    public GameObject PauseScreen;
    public GameObject OptionScreen;
    public EnemySpawnSystem spawnSystem;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            TogglePauseScreen();
        }
    }

    public void ToggleDeathScreen() {
        DeathScreen.active = !DeathScreen.active;
        if(DeathScreen.active)
            Time.timeScale = 0f;
        else
            Time.timeScale = 1f;
    }

    public void TogglePauseScreen() {
        PauseScreen.active = !PauseScreen.active;
        if(PauseScreen.active)
            Time.timeScale = 0f;
        else
            Time.timeScale = 1f;
    }

    public void ReloadScene() {
        Time.timeScale = 1f;
        EnemySpawnSystem.startWave = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReviveAtWave() {
        Time.timeScale = 1f;
        EnemySpawnSystem.startWave = spawnSystem.getCurrentWave();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame() {
        Application.Quit();
    }
}
