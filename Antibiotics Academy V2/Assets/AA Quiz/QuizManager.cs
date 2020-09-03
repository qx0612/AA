using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    List<Questions> qns = new List<Questions>(); //a list to store the questions

    public string nextScene;                  

    public int sceneID; 

    public TextAsset questionsCSV;

    public Text questionNo;
    public Text question;

    private string ans;

    private int index = 0;
    private int score;
    // Start is called before the first frame update
    void Start()
    {
        string[] data = questionsCSV.text.Split(new char[] { '\n' });     //a string array that stores the split items from the csv

        for (int i = 1; i < data.Length - 1; i++)                         //for loop that stores the data into its respective variables
        {
            string[] row = data[i].Split(new char[] { ',' });             //splits the data on the ,
            Questions question = new Questions();                         //create a new Question instance
            int.TryParse(row[0], out question.questionNo);                //parse through the integer
            question.questions = row[1];                                  //set the questions from the string in row 1  
            question.answer = row[2].Trim();                              //store the answer from the string in row 2

            qns.Add(question);                                            //add the question instance to the questions list
        }

        questionNo.text = "Question: " + qns[index].questionNo;           //set the questionNo text and question text to the first question, since index starts at 0
        question.text = qns[index].questions;
    }

    public void TrueBtn()        //function to set the ans to true
    {
        ans = "TRUE";            
        CheckAnswer();
    }

    public void FalseBtn()       //function to set the ans to false
    {
        ans = "FALSE";
        CheckAnswer();
    }

    void CheckAnswer()           //function to check answer
    {
        if (index < qns.Count && index != qns.Count - 1)    //if the current qns is less than the total question
        {
            if (ans == qns[index].answer)                   //if answer is the same as the answer in the question instance
            {
                score++;                                    //increase score
                Debug.Log("added");
            }
            index++;                                        //increase index
            NextQuestion();                                 
        }

        else
        {
            if (sceneID == 1)
            {
                SceneManager.LoadScene(nextScene);
            }

            else
            {
                Application.Quit();
            }

            Debug.Log("No more Questions.");
            Debug.Log(score);
        }


    }

    void NextQuestion()         //function to display next question
    {
        if (index < qns.Count)  //if index is less than questions count
        {
            questionNo.text = "Question: " + qns[index].questionNo;  //set the questionNo text and question text based on the index
            question.text = qns[index].questions;
        }
    }
}
