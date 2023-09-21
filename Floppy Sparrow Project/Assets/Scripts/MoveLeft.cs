using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private float speed = 10;
    private PlayerController playerControllerScript;
    private GameManager gameManager;
    private float leftBound = -35;
    private bool isScoreAdded = false;



    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();


    }
    
    // Update is called once per frame
    void Update()
    {
        if (!playerControllerScript.gameOver && gameManager.gameStarted)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }

        if (transform.position.x < GameObject.Find("Player").transform.position.x  && gameObject.CompareTag("Obstacle") && !isScoreAdded)
        {
            gameManager.UpdateScore(1);
            isScoreAdded = true;
   
        }
        if (transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
