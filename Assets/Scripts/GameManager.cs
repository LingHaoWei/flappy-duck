using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Player player;
    public Text title;
    public Text scoreText;
    public Text clickToPlay;
    public GameObject playButton;
    public GameObject homeButton;
    public GameObject replayButton;
    public GameObject getReady;
    public GameObject gameOver;
    bool isReady = false;
    private int score;

    private void Awake()
    {
        gameOver.SetActive(false);
        homeButton.SetActive(false);
        replayButton.SetActive(false);
        scoreText.enabled = false;
        clickToPlay.enabled = false;

        Application.targetFrameRate = 60;

        Pause();
    }

    public void Ready()
    {
        player.enabled = true;
        isReady = true;
        title.enabled = false;
        clickToPlay.enabled = true;
        scoreText.enabled = true;
        getReady.SetActive(true);
        homeButton.SetActive(false);
        playButton.SetActive(false);
        replayButton.SetActive(false);
        gameOver.SetActive(false);

        Pipes[] pipes = FindObjectsOfType<Pipes>();

        for (int i = 0; i < pipes.Length; i++)
        {
            Destroy(pipes[i].gameObject);
        }

        Pause();
    }

    public void Play()
    {
        isReady = false;
        scoreText.enabled = true;
        score = 0;
        scoreText.text = score.ToString();
        clickToPlay.enabled = false;
        playButton.SetActive(false);
        replayButton.SetActive(false);
        getReady.SetActive(false);
        gameOver.SetActive(false);
        homeButton.SetActive(false);
        player.enabled = true;
        Time.timeScale = 1f;
        
        //Pipes[] pipes = FindObjectsOfType<Pipes>();

        //for (int i = 0; i < pipes.Length; i++) {
        //    Destroy(pipes[i].gameObject);
        //}
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        player.enabled = false;
    }

    public void Home()
    {
        Awake();
    }

    public void GameOver()
    {
        isReady = false;
        gameOver.SetActive(true);
        playButton.SetActive(false);
        replayButton.SetActive(true);
        homeButton.SetActive(true);
        Pause();
    }

    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isReady == true)
        {
            Play();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Reset();
        }
    }

    public void Reset()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
