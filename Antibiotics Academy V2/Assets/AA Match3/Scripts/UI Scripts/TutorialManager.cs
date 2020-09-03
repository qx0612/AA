using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Match3
{
    public class TutorialManager : MonoBehaviour
    {
        public Animator anim;
        public Text tutorialText;
        public Queue<string> sentences;

        private void Start()
        {
            sentences = new Queue<string>();
        }

        public void StartTutorial(Tutorial tutorial)          //function to start the tutorial
        {
            Time.timeScale = 0f;
            anim.SetBool("IsOpen", true);
            sentences.Clear();

            foreach (string sentence in tutorial.sentences)
            {
                sentences.Enqueue(sentence);                 //queues all the sentences
            }
            DisplayNextSentence();                           
        }

        public void DisplayNextSentence()                     //function to display the next sentence
        {
            if (sentences.Count == 0)                         //if there are no more sentences, end dialouge
            {
                EndDialogue();
                return;
            }
            string sentence = sentences.Dequeue(); 
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
        }

        IEnumerator TypeSentence(string sentence)            //coroutine that types out the sentence
        {
            tutorialText.text = "";
            foreach (char letter in sentence)
            {
                tutorialText.text += letter;
                yield return null;
            }
        }

        void EndDialogue()                             //function to close the tutorial box
        {
            Time.timeScale = 1f;
            anim.SetBool("IsOpen", false);             //plays the closing animation
        }
    }
}