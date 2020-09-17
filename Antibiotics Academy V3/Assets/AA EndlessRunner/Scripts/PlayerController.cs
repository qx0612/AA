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
            doubleJumpAllowed = true; // player is allowed to double jump
        }
        if (onTheGround && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) // if player is on the ground and taps on the screen to jump
        //Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began //Input.GetButtonDown("Jump")
        {
            Jump(); // function to jump
        }
        else if (doubleJumpAllowed && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) // if double jump is allowed and player taps on the screen to jump a second time
        //Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began //Input.GetButtonDown("Jump")
        {
            Jump(); // function to jump
            doubleJumpAllowed = false; // set double jump to false since aready double jumpedd
        }
        scoreText.text = "SCORE: " + yourScore; // update the score text according to the current score
    }

    void FixedUpdate()
    {
        rgb.velocity = new Vector2(0, rgb.velocity.y); // set the velocity of rigidbody to be the change in y axis value when jumping
    }

    void Jump() // function to jump
    {
        rgb.velocity = new Vector2(rgb.velocity.x, 0f); // set the velocity of rigidbody to be the change in x axis vaue when running
        rgb.AddForce(Vector2.up * jumpForce); // add a upward force to the rigidbody to jump
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Enemy")) // whe the player collides with any obstacles with the layermask "Enemy"
        {
            retryPanel.SetActive(true); // pop-up a retry screen
            Time.timeScale = 0; // pause the game
        }
    }

    void IncreaseScore() // function to increase the score
    {
        if (Time.unscaledTime > nextScoreIncrease) // if 0.5 seconds passed
        {
            yourScore += 1; // score increment by 1
            nextScoreIncrease = Time.unscaledTime + 0.5f; // set score to increase next after 0.5 seconds pass
        }
    }
    
    public void retryGame() // function to retry the endless runner
    {
        SceneManager.LoadScene(11); // retry endless runner
    }

    public void backToMain() // function to go back to the community
    {
        SceneManager.LoadScene(7); // back to community
    }
}
