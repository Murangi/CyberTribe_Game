using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public void LoadNextcene()
    {
        // Loads the next scene in the build order
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 0);
    }

    public void LoadPreviousScene()
    {
        // Loads the next scene in the build order
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex - 1);
    }

    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadMzanziMarblesScene()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadSettingsScene()
    {
        SceneManager.LoadScene(3);
    }

    public void LoadGamePlayScene()
    {
        SceneManager.LoadScene(2);
    }
    public void LoadMUSICScene()
    {
        SceneManager.LoadScene(4);
    }
    public void LoadGAMESETUPcene()
    {
        SceneManager.LoadScene(5);
    }

    public void LoadMANURULESScene()
    {
        SceneManager.LoadScene(7);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
