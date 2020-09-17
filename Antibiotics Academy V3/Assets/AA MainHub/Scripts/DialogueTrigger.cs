using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue; // reference Dialogue script
    
    public void TriggerDialogue() // function to trigger the dialogue
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue); // find DialogueManager component and trigger the StartDialogue function in the Dialogue script
    }
}
