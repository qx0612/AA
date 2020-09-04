using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour
{

    public float maxHealth = 100;        //max health is 100
    public float currentHealth = 100;    //current health is 100
    private float originalScale;         //scale of health bar

    // Use this for initialization
    void Start()
    {
        originalScale = gameObject.transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 tmpScale = gameObject.transform.localScale;
        tmpScale.x = currentHealth / maxHealth * originalScale;  //updates the scale 
        gameObject.transform.localScale = tmpScale;              //sets the gameObject localscale to the updated scale
    }

}
