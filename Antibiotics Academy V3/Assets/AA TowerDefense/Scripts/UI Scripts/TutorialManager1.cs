using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class TutorialManager1 : MonoBehaviour
    {
        public Animator anim;
        public Text tutorialText;
        public Queue<string> sentences;
        public GameObject heart;
        private AudioSource src;

        public GameObject tutorialGameObject;
        public GameObject tutorialUI;

        private SpriteRenderer image1;
        private SpriteRenderer image2;
        private SpriteRenderer image3;

        private Text text1;
        private Text text2;
        private Text text3;

        private void Start()
        {
            sentences = new Queue<string>(); // queue the sentences in the tutorial
            image1 = GameObject.Find("Image1").GetComponent<SpriteRenderer>(); // placeholder for image 1 in tutorial
            image2 = GameObject.Find("Image2").GetComponent<SpriteRenderer>(); // placeholder for image 2 in tutorial
            image3 = GameObject.Find("Image3").GetComponent<SpriteRenderer>(); // placeholder for image 3 in tutorial

            text1 = GameObject.Find("Text1").GetComponent<Text>(); // placeholder for text 1 in tutorial
            text2 = GameObject.Find("Text2").GetComponent<Text>(); // placeholder for text 2 in tutorial
            text3 = GameObject.Find("Text3").GetComponent<Text>(); // placeholder for text 3 in tutorial
        }

        public void StartTutorial(Tutorial1 tutorial) // function to start the tutorial
        {
            Time.timeScale = 0f; // pause the game whie the tutorial is active
            tutorialGameObject.SetActive(true);
            anim.SetBool("IsOpen", true);
            sentences.Clear();

            foreach (string sentence in tutorial.sentences)
            {
                sentences.Enqueue(sentence);
            }
            DisplayNextSentence(); // function to display the next sentence in the tutorial
        }

        public void DisplayNextSentence() // function to display the next sentence in the tutorial
        {
            if (sentences.Count == 2) // if first page of tutorial
            {
                // show the different turret sprites and text
                image1.sprite = Resources.Load<Sprite>("Turret_Antibiotic_1");
                image2.sprite = Resources.Load<Sprite>("Turret_Water_1");
                image3.sprite = Resources.Load<Sprite>("Turret_Eat_1");

                text1.text = "Antibiotic Turret";
                text2.text = "Water Turret";
                text3.text = "Fruit Turret";
            }

            else if (sentences.Count == 1) // if second page of tutorial
            {
                // show the different turret upgrade sprites and text
                image1.sprite = Resources.Load<Sprite>("Turret_Eat_1");
                image2.sprite = Resources.Load<Sprite>("Turret_Eat_2");
                image3.sprite = Resources.Load<Sprite>("Turret_Eat_3");

                text1.text = "Level 1";
                text2.text = "Level 2";
                text3.text = "Level 3";
            }

            else if (sentences.Count == 0 ) // if no more sentences in tutorial to display
            {
                EndDialogue(); // end the tutorial
                return;
            }

            string sentence = sentences.Dequeue();
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
        }

        IEnumerator TypeSentence(string sentence) // function to show the sentences letter by letter in the tutorial
        {
            tutorialText.text = "";
            foreach (char letter in sentence)
            {
                tutorialText.text += letter;
                yield return null;
            }
        }

        void EndDialogue() // function to close the tutorial ui since tutorial ended
        {
            Time.timeScale = 1f; // resume the game
            src = heart.GetComponent<AudioSource>(); 

            anim.SetBool("IsOpen", false);
            tutorialUI.SetActive(false);

            if (!src.isPlaying)
            {
                src.Play();
            }
        }
    }
}