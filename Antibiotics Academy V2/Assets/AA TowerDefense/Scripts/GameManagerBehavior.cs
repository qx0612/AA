using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManagerBehavior : MonoBehaviour
{
    public GameObject spawner;
    private SpawnEnemy spawn;

    public GameObject DeathUI;
    public GameObject WinUI;

    public Text goldLabel;
    private int gold;
    public int Gold
    {
        get
        {
            return gold;
        }
        set
        {
            gold = value;
            goldLabel.GetComponent<Text>().text = "X " + gold;
        }
    }

    public Text waveLabel;
    public GameObject[] nextWaveLabels;

    public bool gameOver = false;
    public bool lost = false;

    private int wave;
    public int Wave
    {
        get { return wave; }
        set
        {
            wave = value;
            if (!gameOver) // if game not over
            {
                if (wave > 1)
                {
                    for (int i = 0; i < nextWaveLabels.Length; i++)
                    {
                        Debug.Log(wave);
                        //nextWaveLabels[i].GetComponent<Animator>().SetTrigger("nextWave");
                    }
                }
                else if (wave > spawn.waves.Length)
                {
                    gameOver = true;
                    lost = false;
                }
                waveLabel.text = "WAVE: " + (wave + 1);               
            }        
        }
    }

    public GameObject[] healthIndicator;

    private int health;
    public int Health
    {
        get { return health; }
        set
        {
            // 1
            if (value < health)
            {
                Camera.main.GetComponent<CameraShake>().Shake();
            }
            // 2
            health = value;
            // 2
            if (health <= 0 && !gameOver)
            {
                gameOver = true;
                lost = true;
                //GameObject gameOverText = GameObject.FindGameObjectWithTag("GameOver");
                //gameOverText.GetComponent<Animator>().SetBool("gameOver", true);
                DisplayDeathUI();
            }
            // 3 
            for (int i = 0; i < healthIndicator.Length; i++)
            {
                if (i < Health)
                {
                    healthIndicator[i].SetActive(false);
                }
                else
                {
                    healthIndicator[i].SetActive(true);
                }
            }
        }
    }

    // Use this for initialization
    void Start()
    {
        Gold = 300;
        Wave = 0;
        Health = 5;
        Time.timeScale = 0f;
        spawn = spawner.GetComponent<SpawnEnemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver == true && lost == false)
        {
            DisplayWinUI();
        }

    }

    public void DisplayDeathUI()
    {
        DeathUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void DisplayWinUI()
    {
        WinUI.SetActive(true);
        Time.timeScale = 0f;
    }

}