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
            sentences = new Queue<string>();
            image1 = GameObject.Find("Image1").GetComponent<SpriteRenderer>();
            image2 = GameObject.Find("Image2").GetComponent<SpriteRenderer>();
            image3 = GameObject.Find("Image3").GetComponent<SpriteRenderer>();

            text1 = GameObject.Find("Text1").GetComponent<Text>();
            text2 = GameObject.Find("Text2").GetComponent<Text>();
            text3 = GameObject.Find("Text3").GetComponent<Text>();
        }

        public void StartTutorial(Tutorial1 tutorial)
        {
            Time.timeScale = 0f;
            tutorialGameObject.SetActive(true);
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
            if (sentences.Count == 2)
            {
                image1.sprite = Resources.Load<Sprite>("Turret_Antibiotic_1");
                image2.sprite = Resources.Load<Sprite>("Turret_Water_1");
                image3.sprite = Resources.Load<Sprite>("Turret_Eat_1");

                text1.text = "Antibiotic Turret";
                text2.text = "Water Turret";
                text3.text = "Eat Turret";
            }

            else if (sentences.Count == 1)
            {
                image1.sprite = Resources.Load<Sprite>("Turret_Eat_1");
                image2.sprite = Resources.Load<Sprite>("Turret_Eat_2");
                image3.sprite = Resources.Load<Sprite>("Turret_Eat_3");

                text1.text = "Level 1";
                text2.text = "Level 2";
                text3.text = "Level 3";
            }

            else if (sentences.Count == 0 )
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