using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Logic_Script : MonoBehaviour
{
    public int player_score;
    public Text score_text;
    public Text high_score_text;
    public GameObject GameOverScreen;
    public GameObject StartScreen;
    public GameObject PauseScreen;
    public Text final_score_text;
    public Text final_high_score_text;

    [Header("Screen Shake")]
    public Camera mainCamera;
    public float shakeDuration = 0.3f;
    public float shakeMagnitude = 0.2f;

    [Header("Difficulty (score thresholds)")]
    public int mediumAt = 10;
    public int hardAt = 25;
    public int insaneAt = 50;

    private bool is_game_over = false;
    private bool is_paused = false;
    private bool game_started = false;
    private Vector3 originalCamPos;
    private int high_score;
    private PipeSpawnScript spawner;

    void Start()
    {
        high_score = PlayerPrefs.GetInt("HighScore", 0);

        if (high_score_text != null)
            high_score_text.text = "Best: " + high_score.ToString();

        if (mainCamera == null)
            mainCamera = Camera.main;
        originalCamPos = mainCamera.transform.position;

        if (StartScreen != null)
            StartScreen.SetActive(true);
        if (GameOverScreen != null)
            GameOverScreen.SetActive(false);
        if (PauseScreen != null)
            PauseScreen.SetActive(false);

        spawner = FindObjectOfType<PipeSpawnScript>();

        Time.timeScale = 1f;
    }

    void Update()
    {
        if (game_started && !is_game_over)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TogglePause();
            }
            else if (is_paused && Input.GetKeyDown(KeyCode.Space))
            {
                ResumeGame();
            }
        }
    }

    public void StartGame()
    {
        game_started = true;
        if (StartScreen != null)
            StartScreen.SetActive(false);

        if (spawner != null)
            spawner.BeginSpawning();
    }

    [ContextMenu("Add 1 to score")]
    public void addScore()
    {
        if (is_game_over) return;

        player_score++;
        score_text.text = player_score.ToString();

        // Update high score live during gameplay
        if (player_score > high_score)
        {
            high_score = player_score;
            if (high_score_text != null)
                high_score_text.text = "Best: " + high_score.ToString();
        }

        // Difficulty jumps at score milestones
        if (spawner != null)
        {
            if (player_score == mediumAt)
            {
                spawner.SetDifficulty(pipeSpeed: 7f, spawnRate: 1.6f);
            }
            else if (player_score == hardAt)
            {
                spawner.SetDifficulty(pipeSpeed: 9f, spawnRate: 1.3f);
            }
            else if (player_score == insaneAt)
            {
                spawner.SetDifficulty(pipeSpeed: 11f, spawnRate: 1.0f);
            }
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Game_Over()
    {
        if (is_game_over) return;
        is_game_over = true;

        // Save high score
        if (player_score > high_score)
        {
            high_score = player_score;
        }
        PlayerPrefs.SetInt("HighScore", high_score);
        PlayerPrefs.Save();

        // Show final scores
        if (final_score_text != null)
            final_score_text.text = player_score.ToString();
        if (final_high_score_text != null)
            final_high_score_text.text = "Best: " + high_score.ToString();

        GameOverScreen.SetActive(true);

        // Stop all pipes
        Pipe_Script[] pipes = FindObjectsOfType<Pipe_Script>();
        foreach (Pipe_Script pipe in pipes)
        {
            pipe.StopMoving();
        }

        // Stop spawner
        if (spawner != null)
            spawner.StopSpawning();

        // Screen shake
        StartCoroutine(ScreenShake());
    }

    public void TogglePause()
    {
        is_paused = !is_paused;
        if (is_paused)
        {
            Time.timeScale = 0f;
            if (PauseScreen != null)
                PauseScreen.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            if (PauseScreen != null)
                PauseScreen.SetActive(false);
        }
    }

    public void ResumeGame()
    {
        is_paused = false;
        Time.timeScale = 1f;
        if (PauseScreen != null)
            PauseScreen.SetActive(false);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private IEnumerator ScreenShake()
    {
        float elapsed = 0f;
        while (elapsed < shakeDuration)
        {
            float x = originalCamPos.x + Random.Range(-1f, 1f) * shakeMagnitude;
            float y = originalCamPos.y + Random.Range(-1f, 1f) * shakeMagnitude;
            mainCamera.transform.position = new Vector3(x, y, originalCamPos.z);

            elapsed += Time.deltaTime;
            yield return null;
        }
        mainCamera.transform.position = originalCamPos;
    }
}
