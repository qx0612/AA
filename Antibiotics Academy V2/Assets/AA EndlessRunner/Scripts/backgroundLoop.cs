using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundLoop : MonoBehaviour
{
    public GameObject[] levels; // array to store background and grass
    private Camera mainCamera; // get the main camera in the scene
    private Vector2 screenBounds; // screen size
    public float choke; // offset
    public float scrollSpeed; // speed the background moves

    public GameObject player; // player game object
    PlayerController playercontroller; // player controller script

    GameObject bg1;
    GameObject bg2;
    GameObject bg3;
    GameObject grass1;
    GameObject grass2;

    SpriteRenderer bgSprite1; 
    SpriteRenderer grassSprite1; 
    SpriteRenderer bgSprite2;
    SpriteRenderer grassSprite2;
    SpriteRenderer bgSprite3;

    public Sprite morningGrass; // morning grass sprite
    public Sprite morningBG; // morning background sprite

    public Sprite eveningGrass; // evening grass sprite
    public Sprite eveningBG; // evening background sprite

    public Sprite nightGrass; // night grass sprite
    public Sprite nightBG; // night background sprite

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // find player game object
        playercontroller = player.GetComponent<PlayerController>(); // get the playercontroller component in the player game object
        mainCamera = gameObject.GetComponent<Camera>(); // get the main camera component of the gameobject this script is attached to
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z)); // set the screen size relevant to the camera
        foreach (GameObject obj in levels)
        {
            loadChildObjects(obj); // function to spawn the grass and background
        }
    }
    void loadChildObjects(GameObject obj)
    {
        float objectWidth = obj.GetComponent<SpriteRenderer>().bounds.size.x - choke; // set the width of the obj in the array 
        int childsNeeded = (int)Mathf.Ceil(screenBounds.x * 2 / objectWidth); // find the amount of obj needed to cover the scren size
        GameObject clone = Instantiate(obj) as GameObject; // instantiate the obj as a clone
        for (int i = 0; i <= childsNeeded; i++) // for each of the amount of obj needed
        {
            GameObject c = Instantiate(clone) as GameObject; // instantiate as clone
            c.transform.SetParent(obj.transform); // set the position of the clone to be the child of the current obj
            c.transform.position = new Vector3(objectWidth * i, obj.transform.position.y, obj.transform.position.z); // change the position of the clone game object
            c.name = obj.name + i; // set name of clone
        }
        Destroy(clone); // destroy clone game object
        Destroy(obj.GetComponent<SpriteRenderer>()); // destroy the sprite renderer component of the obj
    }
    void repositionChildObjects(GameObject obj) // function to reposition the child objects
    {
        Transform[] children = obj.GetComponentsInChildren<Transform>(); // store the position of the child obj in an array
        if (children.Length > 1) // if more than 1
        {
            GameObject firstChild = children[1].gameObject; // set children[1] gameobject to be the first child
            GameObject lastChild = children[children.Length - 1].gameObject; // set the last child to be the [total children - 1] game object
            float halfObjectWidth = lastChild.GetComponent<SpriteRenderer>().bounds.extents.x - choke; 
            if (transform.position.x + screenBounds.x > lastChild.transform.position.x + halfObjectWidth)
            {
                firstChild.transform.SetAsLastSibling();
                firstChild.transform.position = new Vector3(lastChild.transform.position.x + halfObjectWidth * 2, lastChild.transform.position.y, lastChild.transform.position.z);
            }
            else if (transform.position.x - screenBounds.x < firstChild.transform.position.x - halfObjectWidth)
            {
                lastChild.transform.SetAsFirstSibling();
                lastChild.transform.position = new Vector3(firstChild.transform.position.x - halfObjectWidth * 2, firstChild.transform.position.y, firstChild.transform.position.z);
            }
        }
    }
    void Update()
    {
        Vector3 velocity = Vector3.zero;
        Vector3 desiredPosition = transform.position + new Vector3(scrollSpeed, 0, 0); // set the desired position
        Vector3 smoothPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, 0.3f); // make the changes in position smooth
        transform.position = smoothPosition;
    }

    void LateUpdate()
    {
        foreach (GameObject obj in levels)
        {
            repositionChildObjects(obj); // repositioning of the backgrounds and grass
        }
    }

    void FixedUpdate()
    {
        // getting the sprite renderer of the game objects
        grass1 = GameObject.Find("Grass0");
        grass2 = GameObject.Find("Grass1");
        bg1 = GameObject.Find("Background0");
        bg2 = GameObject.Find("Background1");
        bg3 = GameObject.Find("Background2");

        grassSprite1 = grass1.GetComponent<SpriteRenderer>();
        grassSprite2 = grass2.GetComponent<SpriteRenderer>();
        bgSprite1 = bg1.GetComponent<SpriteRenderer>();
        bgSprite2 = bg2.GetComponent<SpriteRenderer>();
        bgSprite3 = bg3.GetComponent<SpriteRenderer>();

        if (playercontroller.yourScore > 0 && playercontroller.yourScore <= 20) // if speed 8
        {
            // morning sprites
            bgSprite1.sprite = morningBG;
            grassSprite1.sprite = morningGrass;
            bgSprite2.sprite = morningBG;
            grassSprite2.sprite = morningGrass;
            bgSprite3.sprite = morningBG;
        }
        else if (playercontroller.yourScore > 20 && playercontroller.yourScore <= 75) // if speed 10
        {
            // evening sprite
            bgSprite1.sprite = eveningBG;
            grassSprite1.sprite = eveningGrass;
            bgSprite2.sprite = eveningBG;
            grassSprite2.sprite = eveningGrass;
            bgSprite3.sprite = eveningBG;
        }
        else if (playercontroller.yourScore > 75) // if speed 12
        {
            // night sprites
            bgSprite1.sprite = nightBG;
            grassSprite1.sprite = nightGrass;
            bgSprite2.sprite = nightBG;
            grassSprite2.sprite = nightGrass;
            bgSprite3.sprite = nightBG;
        }
    }
}