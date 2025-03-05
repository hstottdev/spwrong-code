using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public static int nextScene = 2;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MainMenu()
    {
        nextScene = 2;
        if (GameManager.paused)
        {
            ResumeGame();
        }
        SceneManager.LoadScene("Static Noise");

    }

    public void TransitionToScene(string scene)
    {
        nextScene = SceneManager.GetSceneByName(scene).buildIndex;

        if (GameManager.paused)
        {
            ResumeGame();
        }

        SceneManager.LoadScene("Static Noise");
    }

    public void StartNewGame()
    {
        nextScene = 3;

        if (GameManager.paused)
        {
            ResumeGame();
        }

        SceneManager.LoadScene("Static Noise");
    }

    public void NextLevel()
    {
        nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        Debug.Log(nextScene);

        if (GameManager.paused)
        {
            ResumeGame();
        }
        SceneManager.LoadScene("Static Noise");
    }

    public void TryAgain()
    {
        nextScene = SceneManager.GetActiveScene().buildIndex;
        if (GameManager.paused)
        {
            ResumeGame();
        }
        SceneManager.LoadScene("Static Noise");
    }

    public void ResumeGame()
    {
        GameManager.paused = false;
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
