using System.Collections;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    float speed = 5f;
    float bounds = 15;
    float horizontalInput;
    float jumpForce = 7f;
    public float health = 3;
    Rigidbody playerRb;
    bool isOnGround = true;
    public bool gameOver = false;
    public GameObject speedUpIndicator;
    Vector3 speedUpIndicatorOffset = new Vector3(0, 1.25f, 0);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        IsAlive();
        PlayerMovement();
        speedUpIndicator.transform.position = transform.position + speedUpIndicatorOffset;
    }
    private void OnCollisionEnter(Collision collision)
    {
        // Check if player is on the ground and when player is hit by an enemy
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
        else if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Falling Enemy"))
        {
            // Reduce health if hit by an enemy
            health -= 1;
        }
        else if (collision.gameObject.CompareTag("LifeUp"))
        {
            health += 1;
        }
        else if (collision.gameObject.CompareTag("SpeedUp"))
        {
            StartCoroutine(SpeedUpCountdown());
        }
    }
    IEnumerator SpeedUpCountdown()
    {
        speedUpIndicator.SetActive(true);
        speed = 10f;
        yield return new WaitForSeconds(10);
        speed = 5f;
        speedUpIndicator.SetActive(false);
    }
    void IsAlive()
    {
        if (health <= 0)
        {
            gameOver = true;
        }
    }
    void PlayerMovement()
    {
        // Move the player left or right depending on user input if the game is not over
        if (!gameOver)
        {
            horizontalInput = Input.GetAxis("Horizontal");
            playerRb.MovePosition(playerRb.position + Vector3.right * horizontalInput * speed * Time.deltaTime);
        }
        // Make the player jump when "Space" is pressed
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
        }
        // Keep the player inbounds
        if (transform.position.x < -bounds)
        {
            transform.position = new Vector3(-bounds, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > bounds)
        {
            transform.position = new Vector3(bounds, transform.position.y, transform.position.z);
        }

    }
}