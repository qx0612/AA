using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText; // text box to show npc name
    public Text dialogueText; // text box to show npc dialogue
    public Animator animator; // get animator of the dialogue text

    public bool spawned; // bool to check if the dialogue box spawned

    public Queue<string> sentences; // queue the sentences as dialogues

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>(); // get the sentences and queue them
    }

    public void StartDialogue (Dialogue dialogue) // function to show the dialogue
    {
        spawned = true; // set bool to true since dialogue spawned
        animator.SetBool("IsOpen", true); // set animation of dialogue box open

        nameText.text = dialogue.name; // set the name text to be the name of the npc that is talking

        sentences.Clear(); // clear the sentences when done

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence); // function to enqueue the sentences so they would appear as the next sentence
        }

        DisplayNextSentence();  // function to show the next sentence
    }

    public void DisplayNextSentence() // function to show the next sentence
    {
        if (sentences.Count == 0) // if no more sentences left to display
        {
            EndDialogue(); // function to end dialogue
            spawned = false; // set bool to false since dialogue box closed
            return;
        }

        string sentence = sentences.Dequeue(); // dequeue the sentences
        StopAllCoroutines(); // stop the coroutine for that sentence
        StartCoroutine(TypeSentence(sentence)); // start coroutine for the next sentence
    }

    IEnumerator TypeSentence (string sentence) // function to show the next sentence
    {
        dialogueText.text = " "; // place to display the dialogue text
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter; // show the dialogue text letter by letter
            yield return null;
        }
    }

    void EndDialogue() // function to end dialogue
    {
        //dialogueBox.SetActive(false);
        animator.SetBool("IsOpen", false); // set animation of dialogue box closing
    }
}
