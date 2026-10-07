using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool gameOver;
    private int score;

    public bool IsGameOver => gameOver;
    public int Score => score;

    public void AddScore(int amount)
    {
        if (gameOver)
            return;

        score += amount;

        Debug.Log("Score: " + score);
    }

    public void GameOver()
    {
        if (gameOver)
            return;

        gameOver = true;

        Debug.Log("GAME OVER - Final Score: " + score);

        Time.timeScale = 0f;
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");

        Application.Quit();
    }
}