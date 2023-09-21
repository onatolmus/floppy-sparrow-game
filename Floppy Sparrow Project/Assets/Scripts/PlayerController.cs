using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private GameManager gameManager;

    public float jumpForce = 10f;
    public float gravityModifier = 1.5f;

    public bool gameOver = false;

    private Animator playerAnim;

    private AudioSource playerAudio;
    public AudioClip jumpSound;
    public AudioClip crashSound;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        DisableRb();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        playerAnim = GetComponentInChildren<Animator>();
        playerAudio = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.gameStarted)
        {
            EnableRb();
            playerAnim.SetBool("Game_Started_b", true);
        }
        if (Input.GetKeyDown(KeyCode.Space) && !gameOver && gameManager.gameStarted)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            playerAnim.SetTrigger("Jump_trig");
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // game over when the player collides with the columns or the ground
        if ((collision.gameObject.CompareTag("Obstacle")) || (collision.gameObject.CompareTag("Ground")))
        {
            gameOver = true;
            gameManager.GameOver();
            playerAnim.SetBool("Death_b", true);
            playerAudio.PlayOneShot(crashSound, 1.0f);
        }

    }

    void EnableRb()
    {
        playerRb.isKinematic = false;
        playerRb.detectCollisions = true;
    }
    void DisableRb()
    {
        playerRb.isKinematic = true;
        playerRb.detectCollisions = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
}
