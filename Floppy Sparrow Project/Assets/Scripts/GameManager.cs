using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private Button startButton;
    public Button resumeButton;

    public TextMeshProUGUI scoreText;

    public GameObject columnPrefab;
    public GameObject titleScreen;
    public GameObject gameOverScreen;
    private PlayerController playerControllerScript;

    public bool isPaused = false;
    public bool gameStarted = false;

    public float spawnRate = 2.0f;
    private int score;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();

        startButton = GameObject.Find("Start Button").GetComponent<Button>();
        startButton.onClick.AddListener(StartGame);
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnTarget()
    {
        while (!playerControllerScript.gameOver && !isPaused)
        {
            yield return new WaitForSeconds(spawnRate);
            Vector3 spawnPos = new Vector3(25, Random.Range(-0.75f, 4.25f), 0);
            Instantiate(columnPrefab, spawnPos, columnPrefab.transform.rotation);

        }

    }

    public void StartGame()
    {
        //isGameActive = true;
        score = 0;
        gameStarted = true;

        StartCoroutine(SpawnTarget());
        UpdateScore(0);

        titleScreen.gameObject.SetActive(false);
    }

    public void GameOver()
    {
        gameOverScreen.gameObject.SetActive(true);

    }


    public void RestartGame()
    {
        Physics.gravity = new Vector3(0, -9.8f, 0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }
}
