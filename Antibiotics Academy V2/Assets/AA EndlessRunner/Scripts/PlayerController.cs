using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rgb; // get rigidbody component of player game object
    public float jumpForce = 500f; // set the jump force of player

    public Animator anim; // get the animator of player game object

    public Text scoreText; 
    public int yourScore; // score text which increases
    float nextScoreIncrease = 0f; // time between each score increment

    private float startTime; // get the starting time

    public bool doubleJumpAllowed = false; // bool to check if player can double jump
    public bool onTheGround = false; // bool to check if player is on the ground

    public GameObject retryPanel; // pop-up that shows when player loses the endless runner game

    // Start is called before the first frame update
    void Start()
    {
        retryPanel.SetActive(false); // set pop-up to false at start

        Time.timeScale = 1; // set time scale of game to 1
        rgb = GetComponent<Rigidbody2D>(); // get the rigidbody component of the player game object
        anim = GetComponent<Animator>(); // get the animator component of the player game object

        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        IncreaseScore(); // function to increase the score

        if (rgb.velocity.y == 0) // if no change in the y axis value of the rigidbody
        {
            onTheGround = true; // player is on the ground
        }
        else // if there is a change in the y axis value of the rigidbody
        {
            onTheGround = false; // player is not on the ground
        }
        if (onTheGround) // if player is on the ground
        {
            doubleJumpAllowed = true; // player is allowed to double
        }
        if (onTheGround && Input.GetButtonDown("Jump")) //Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began //Input.GetButtonDown("Jump")
        {
            Jump();
        }
        else if (doubleJumpAllowed && Input.GetButtonDown("Jump")) //Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began //Input.GetButtonDown("Jump")
        {
            Jump();
            doubleJumpAllowed = false;
        }
        scoreText.text = "SCORE: " + yourScore;
    }

    void FixedUpdate()
    {
        rgb.velocity = new Vector2(0, rgb.velocity.y);
    }

    void Jump()
    {
        rgb.velocity = new Vector2(rgb.velocity.x, 0f);
        rgb.AddForce(Vector2.up * jumpForce);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            retryPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    void IncreaseScore()
    {
        if (Time.unscaledTime > nextScoreIncrease)
        {
            yourScore += 1;
            nextScoreIncrease = Time.unscaledTime + 0.5f;
        }
    }
    
    public void retryGame()
    {
        SceneManager.LoadScene(11); // retry endless runner
    }

    public void backToMain()
    {
        SceneManager.LoadScene(7); // back to community
    }
}
