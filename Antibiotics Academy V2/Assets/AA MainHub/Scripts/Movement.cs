using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Transform player; // get position of the player

    public float moveSpeed = 5f; // set the movement speed of the player

    private Rigidbody2D rb; // get the rigidbody of the player
    public Animator animator; // get the animator of the player game object

    Vector2 movement; // get the x and y axis movement value

    public RuntimeAnimatorController FemaleController; // set the animation controller to be the female animation ocntroller when the game starts

    public void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>(); // get the rigidbody of the gameobject this script is attached to
        CheckGender(); // check gender of player
    }

    //Update is called once per frame
    void Update()
    {
        //Input
        animator.SetFloat("Horizontal", movement.x); // set the x value of movement in the animator
        animator.SetFloat("Vertical", movement.y); // set the y value of movement in the animator
        animator.SetFloat("Speed", movement.sqrMagnitude); // set the speed of movement in the animator
    }

    void FixedUpdate()
    {
        // movement of player
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    public void LeftBtn()
    {
        movement.x = -1; // set x to -1 so that the player can move towards the left direction
        movement.y = 0; // set y to 0 since player is not moving in the y axis
        animator.SetBool("FacingLeft", true); // set the bool to true in the animator to make the player face left
        animator.SetBool("FacingRight", false); // set the other bool to be false in the animator
        animator.SetBool("FacingUp", false); // set the other bool to be false in the animator
        animator.SetBool("FacingDown", false); // set the other bool to be false in the animator
    }
    public void RightBtn()
    {
        movement.x = 1; // set x to 1 so that the player can move towards the right direction
        movement.y = 0; // set y to 0 since player is not moving in the y axis
        animator.SetBool("FacingRight", true); // set the bool to true in the animator to make the player face right
        animator.SetBool("FacingLeft", false); // set the other bool to be false in the animator
        animator.SetBool("FacingUp", false); // set the other bool to be false in the animator
        animator.SetBool("FacingDown", false); // set the other bool to be false in the animator
    }
    public void UpBtn()
    {
        movement.y = 1; // set y to 1 so that the player can move towards the upwards direction
        movement.x = 0; // set x to 0 since player is not moving in the x axis
        animator.SetBool("FacingUp", true); // set the bool to true in the animator to make the player face back/up
        animator.SetBool("FacingRight", false); // set the other bool to be false in the animator
        animator.SetBool("FacingLeft", false); // set the other bool to be false in the animator
        animator.SetBool("FacingDown", false); // set the other bool to be false in the animator
    }
    public void DownBtn()
    {
        movement.y = -1; // set y to -1 so that the player can move towards the downwards direction
        movement.x = 0; // set x to 0 since player is not moving in the x axis
        animator.SetBool("FacingDown", true); // set the bool to true in the animator to make the player face front/down
        animator.SetBool("FacingRight", false); // set the other bool to be false in the animator
        animator.SetBool("FacingUp", false); // set the other bool to be false in the animator
        animator.SetBool("FacingLeft", false); // set the other bool to be false in the animator
    }
    public void StopBtnLeft() // function when player stops clicking on the left button
    {
        // set both x and y value to 0 since not moving
        movement.x = 0; 
        movement.y = 0;
    }
    public void StopBtnRight() // function when player stops clicking on the right button
    {
        // set both x and y value to 0 since not moving
        movement.x = 0;
        movement.y = 0;
    }
    public void StopBtnUp() // function when player stops clicking on the up button
    {
        // set both x and y value to 0 since not moving
        movement.x = 0;
        movement.y = 0;
    }
    public void StopBtnDown() // function when player stops clicking on the down button
    {
        // set both x and y value to 0 since not moving
        movement.x = 0;
        movement.y = 0;
    }

    public void CheckGender() // function to check gender of the character the player chose
    {
        if (PlayerCharacterCustomization.IsMale == false) // if character selected is female (IsMale == false)
        {
            animator.runtimeAnimatorController = FemaleController; // set animator controller to be the female animator controller when the game starts
        }

    }
}
