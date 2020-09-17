using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ClickSound : MonoBehaviour
{
    public AudioClip sound; // get the clicking audio

    private Button button { get { return GetComponent<Button>(); } } // get the button component from the button game object
    private AudioSource source { get { return GetComponent<AudioSource>(); } } // get the audio source from the button game object

    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<AudioSource>(); // add a audiosource component to the button this script is attached to
        source.clip = sound;
        source.playOnAwake = false; // set the audio to not play when the game starts

        button.onClick.AddListener(() => PlaySound()); // each time the player clicks on the button, play the clicking sound effect
    }

    public void PlaySound()
    {
        source.PlayOneShot(sound); // play the clicking sound effect
    }

}
