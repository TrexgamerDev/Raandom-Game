using UnityEngine;

public class MoveObstacles : MonoBehaviour
{
    float speed = 20f;
    float fallingSpeed = 15f;
    float speedUpFallingSpeed = 5f;
    float leftBound = -24f;
    float bottomBound = -1f;
    PlayerControl playerControl;
    GameManager gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerControl = GameObject.Find("Player").GetComponent<PlayerControl>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveEnemies();
        MoveSpeedUps();
        MoveLifeUps();
        DestroyOutOfBounds();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
    void MoveEnemies()
    {
        // Move the obstacle depending on its type
        if (playerControl.gameOver == false && gameObject.CompareTag("Enemy"))
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        else if (playerControl.gameOver == false && gameObject.CompareTag("Falling Enemy"))
        {
            transform.Translate(Vector3.down * fallingSpeed * Time.deltaTime);
        }
    }
    void MoveLifeUps()
    {
        if (playerControl.gameOver == false && gameObject.CompareTag("LifeUp"))
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
    }
    void MoveSpeedUps()
    {
        if (gameObject.CompareTag("SpeedUp") && !playerControl.gameOver)
        {
            transform.Translate(Vector3.down * speedUpFallingSpeed * Time.deltaTime);
        }
    }
    void DestroyOutOfBounds()
    {
        // Destroy obstacle when it exits the camera
        if (transform.position.x < leftBound && gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            gameManager.score++;
        }
        else if (transform.position.y < bottomBound && gameObject.CompareTag("Falling Enemy"))
        {
            Destroy(gameObject);
            gameManager.score++;
        }
        else if (transform.position.y < bottomBound && gameObject.CompareTag("SpeedUp"))
        {
            Destroy(gameObject);
        }
    }
}