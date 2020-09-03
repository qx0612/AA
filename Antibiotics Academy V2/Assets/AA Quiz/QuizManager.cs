using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    List<Questions> qns = new List<Questions>();

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
        string[] data = questionsCSV.text.Split(new char[] { '\n' });

        for (int i = 1; i < data.Length - 1; i++)
        {
            string[] row = data[i].Split(new char[] { ',' });
            Questions question = new Questions();
            int.TryParse(row[0], out question.questionNo);
            question.questions = row[1];
            question.answer = row[2].Trim();

            qns.Add(question);
        }

        questionNo.text = "Question: " + qns[index].questionNo;
        question.text = qns[index].questions;
    }

    public void TrueBtn()
    {
        ans = "TRUE";
        CheckAnswer();
    }

    public void FalseBtn()
    {
        ans = "FALSE";
        CheckAnswer();
    }

    void CheckAnswer()
    {
        if (index < qns.Count && index != qns.Count - 1)
        {
            if (ans == qns[index].answer)
            {
                score++;
                Debug.Log("added");
            }
            index++;
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

    void NextQuestion()
    {
        if (index < qns.Count)
        {
            questionNo.text = "Question: " + qns[index].questionNo;
            question.text = qns[index].questions;
        }
    }
}
