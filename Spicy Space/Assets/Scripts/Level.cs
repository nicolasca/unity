using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{

    public void LoadStartMenu()
    {
        SceneManager.LoadScene("Start Menu");
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Core Game");
        var gameState = FindObjectOfType<GameState>();
        if (gameState)
        {
            gameState.ResetGame();
        }
    }

    public void LoadGameOver()
    {
        Debug.Log("Load game over");
        SceneManager.LoadScene("Game Over");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
