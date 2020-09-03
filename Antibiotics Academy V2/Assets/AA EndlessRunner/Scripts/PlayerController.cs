using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rgb;
    public float jumpForce = 500f;

    public Animator anim;

    public Text scoreText;
    public int yourScore;
    float nextScoreIncrease = 0f;

    private float startTime;

    public bool doubleJumpAllowed = false;
    public bool onTheGround = false;

    public GameObject retryPanel;

    // Start is called before the first frame update
    void Start()
    {
        retryPanel.SetActive(false);

        Time.timeScale = 1;
        rgb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        IncreaseScore();

        if (rgb.velocity.y == 0)
        {
            onTheGround = true;
        }
        else
        {
            onTheGround = false;
        }
        if (onTheGround)
        {
            doubleJumpAllowed = true;
        }
        if (onTheGround && Input.GetButtonDown("Jump")) //Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began //Input.GetButtonDown("Jump")
        {
            Jump();
        }
        else if (doubleJumpAllowed && Input.GetButtonDown("Jump")) //Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began
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
