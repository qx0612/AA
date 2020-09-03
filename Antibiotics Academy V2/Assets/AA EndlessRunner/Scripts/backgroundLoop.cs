using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundLoop : MonoBehaviour
{
    public GameObject[] levels;
    private Camera mainCamera;
    private Vector2 screenBounds;
    public float choke;
    public float scrollSpeed;

    public GameObject player;
    PlayerController playercontroller;

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

    public Sprite morningGrass;
    public Sprite morningBG;

    public Sprite eveningGrass;
    public Sprite eveningBG;

    public Sprite nightGrass;
    public Sprite nightBG;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playercontroller = player.GetComponent<PlayerController>();
        mainCamera = gameObject.GetComponent<Camera>();
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        foreach (GameObject obj in levels)
        {
            loadChildObjects(obj);
        }
    }
    void loadChildObjects(GameObject obj)
    {
        float objectWidth = obj.GetComponent<SpriteRenderer>().bounds.size.x - choke;
        int childsNeeded = (int)Mathf.Ceil(screenBounds.x * 2 / objectWidth);
        GameObject clone = Instantiate(obj) as GameObject;
        for (int i = 0; i <= childsNeeded; i++)
        {
            GameObject c = Instantiate(clone) as GameObject;
            c.transform.SetParent(obj.transform);
            c.transform.position = new Vector3(objectWidth * i, obj.transform.position.y, obj.transform.position.z);
            c.name = obj.name + i;
        }
        Destroy(clone);
        Destroy(obj.GetComponent<SpriteRenderer>());
    }
    void repositionChildObjects(GameObject obj)
    {
        Transform[] children = obj.GetComponentsInChildren<Transform>();
        if (children.Length > 1)
        {
            GameObject firstChild = children[1].gameObject;
            GameObject lastChild = children[children.Length - 1].gameObject;
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
        Vector3 desiredPosition = transform.position + new Vector3(scrollSpeed, 0, 0);
        Vector3 smoothPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, 0.3f);
        transform.position = smoothPosition;
    }

    void LateUpdate()
    {
        foreach (GameObject obj in levels)
        {
            repositionChildObjects(obj);
        }
    }

    void FixedUpdate()
    {
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

        if (playercontroller.yourScore > 0 && playercontroller.yourScore <= 20) //speed 8
        {
            Debug.Log("morning");
            bgSprite1.sprite = morningBG;
            grassSprite1.sprite = morningGrass;
            bgSprite2.sprite = morningBG;
            grassSprite2.sprite = morningGrass;
            bgSprite3.sprite = morningBG;
        }
        else if (playercontroller.yourScore > 20 && playercontroller.yourScore <= 75) //speed 10
        {
            Debug.Log("evening");
            bgSprite1.sprite = eveningBG;
            grassSprite1.sprite = eveningGrass;
            bgSprite2.sprite = eveningBG;
            grassSprite2.sprite = eveningGrass;
            bgSprite3.sprite = eveningBG;
        }
        else if (playercontroller.yourScore > 75) //speed 12
        {
            Debug.Log("night");
            bgSprite1.sprite = nightBG;
            grassSprite1.sprite = nightGrass;
            bgSprite2.sprite = nightBG;
            grassSprite2.sprite = nightGrass;
            bgSprite3.sprite = nightBG;
        }
    }
}