using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    float spawnBounds = 15f;
    float spawnStartDelay = 2f;
    float fallingSpawnStartDelay = 3f;
    float spawnInterval = 1.5f;
    float powerUpSpawnStartDelay = 10f;
    float powerUpSpawnInterval = 5f;
    public int score = 0;
    public GameObject enemy;
    public GameObject fallingEnemy;
    public GameObject lifeUp;
    public GameObject speedUp;
    TextMeshProUGUI scoreText;
    TextMeshProUGUI healthText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    PlayerControl playerControl;
    Vector3 spawnPos = new Vector3(19, 0.5f, 0);
    Vector3 fallingSpawnPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        score = 0;
        // Get PlayerControl script reference
        playerControl = GameObject.Find("Player").GetComponent<PlayerControl>();
        // Get the text UI elements
        scoreText = GameObject.Find("Score Text").GetComponent<TextMeshProUGUI>();
        healthText = GameObject.Find("Health Text").GetComponent<TextMeshProUGUI>();
        // Invoke spawning methods
        InvokeRepeating("SpawnEnemy", spawnStartDelay, spawnInterval);
        InvokeRepeating("SpawnPowerUp", powerUpSpawnStartDelay, powerUpSpawnInterval);
        InvokeRepeating("SpawnFallingEnemy", fallingSpawnStartDelay, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        IsGameOver();
        // Update text UI elements
        scoreText.text = "Score: " + score;
        healthText.text = "Health: " + playerControl.health;
    }
    void SpawnEnemy()
    {
        if (playerControl.gameOver == false)
        {
            Instantiate(enemy, spawnPos, enemy.transform.rotation);
        }
    }
    void SpawnFallingEnemy()
    {
        fallingSpawnPos = new Vector3(Random.Range(-spawnBounds, spawnBounds), 10.5f, 0);
        if (playerControl.gameOver == false)
        {
            Instantiate(fallingEnemy, fallingSpawnPos, fallingEnemy.transform.rotation);
        }
    }
    void SpawnPowerUp()
    {
        int pickRandomPowerUp = Random.Range(0, 2);
        if (!playerControl.gameOver && pickRandomPowerUp == 0)
        {
            Instantiate(lifeUp, spawnPos, lifeUp.transform.rotation);
        }
        else if (!playerControl.gameOver && pickRandomPowerUp == 1)
        {
            fallingSpawnPos = new Vector3(Random.Range(-spawnBounds, spawnBounds), 10.5f, 0);
            Instantiate(speedUp, fallingSpawnPos, speedUp.transform.rotation);
        }
    }
    void IsGameOver()
    {
        if (playerControl.gameOver)
        {
            gameOverText.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true);
        }
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}