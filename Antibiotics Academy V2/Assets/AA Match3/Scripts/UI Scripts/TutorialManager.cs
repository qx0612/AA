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

        public void StartTutorial(Tutorial tutorial)
        {
            Time.timeScale = 0f;
            anim.SetBool("IsOpen", true);
            sentences.Clear();

            foreach (string sentence in tutorial.sentences)
            {
                sentences.Enqueue(sentence);
            }
            DisplayNextSentence();
        }

        public void DisplayNextSentence()
        {
            if (sentences.Count == 0)
            {
                EndDialogue();
                return;
            }
            string sentence = sentences.Dequeue();
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
        }

        IEnumerator TypeSentence(string sentence)
        {
            tutorialText.text = "";
            foreach (char letter in sentence)
            {
                tutorialText.text += letter;
                yield return null;
            }
        }

        void EndDialogue()
        {
            Time.timeScale = 1f;
            anim.SetBool("IsOpen", false);
        }
    }
}