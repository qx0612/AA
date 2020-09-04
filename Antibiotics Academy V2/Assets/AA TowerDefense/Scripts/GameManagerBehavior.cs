using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManagerBehavior : MonoBehaviour
{

    public GameObject DeathUI;  //DeathUI game object
    public GameObject WinUI;    //WinUI game object

    public Text goldLabel;      //text to show the amount of gold
    private int gold;           //gold value
    public int Gold
    {
        get
        {
            return gold;       //returns gold when getting
        }
        set
        {
            gold = value;     //sets gold to value
            goldLabel.GetComponent<Text>().text = "X " + gold;  //displays the amount of gold in the goldlabel text
        }
    }

    public Text waveLabel;
    public GameObject[] nextWaveLabels;

    public bool gameOver = false;  //bool to see if game is over
    public bool lost = false;      //bool to see if player lost

    private int wave;              //wave counter
    public int Wave
    {
        get { return wave; }
        set
        {
            wave = value;
            if (!gameOver) // if game not over
            {
                if (wave > 10)
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
            if (value < health)             //if the current health value is less than health
            {
                Camera.main.GetComponent<CameraShake>().Shake();  //camera shakes
            }
            // 2
            health = value;
 
            if (health <= 0 && !gameOver)    //if health is less than or equals to 0 and its not game over
            {
                gameOver = true;             //game over is true
                lost = true;                 //lost is true
                DisplayDeathUI();            //display the death ui
            }
            // 3 
            for (int i = 0; i < healthIndicator.Length; i++)
            {
                if (i < Health)                                 //if i is less than health
                {
                    healthIndicator[i].SetActive(false);        //healthIndicator is not active  (healthIndicator is the bacteria that appears in the heart)
                }
                else
                {
                    healthIndicator[i].SetActive(true);         //otherwise, healthindicator is active
                }
            }
        }
    }

    void Start()
    {
        Gold = 300;               //gold starts at 300
        Wave = 0;                 //wave is at 0
        Health = 5;               //health is set to 5
        Time.timeScale = 0f;
    }

    void Update()
    {
        if (gameOver == true && lost == false)  //if game is over and lost is false
        {
            DisplayWinUI();                     //display win UI
        }

    }

    public void DisplayDeathUI()       //function to display the DeathUI
    {
        DeathUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void DisplayWinUI()        //function to display the WinUI
    {
        WinUI.SetActive(true);
        Time.timeScale = 0f;
    }

}