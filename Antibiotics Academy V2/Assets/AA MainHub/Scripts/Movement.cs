using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Transform player;

    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    public Animator animator;

    Vector2 movement;

    public RuntimeAnimatorController FemaleController;

    public void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        CheckGender(); // check gender of player
    }

        // Update is called once per frame
        void Update()
    {
        // Input
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    void FixedUpdate()
    {
        // Movement
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    public void LeftBtn()
    {
        movement.x = -1;
        movement.y = 0;
        animator.SetBool("FacingLeft", true);
        animator.SetBool("FacingRight", false);
        animator.SetBool("FacingUp", false);
        animator.SetBool("FacingDown", false);
    }
    public void RightBtn()
    {
        movement.x = 1;
        movement.y = 0;
        animator.SetBool("FacingRight", true);
        animator.SetBool("FacingLeft", false);
        animator.SetBool("FacingUp", false);
        animator.SetBool("FacingDown", false);
    }
    public void UpBtn()
    {
        movement.y = 1;
        movement.x = 0;
        animator.SetBool("FacingUp", true);
        animator.SetBool("FacingRight", false);
        animator.SetBool("FacingLeft", false);
        animator.SetBool("FacingDown", false);
    }
    public void DownBtn()
    {
        movement.y = -1;
        movement.x = 0;
        animator.SetBool("FacingDown", true);
        animator.SetBool("FacingRight", false);
        animator.SetBool("FacingUp", false);
        animator.SetBool("FacingLeft", false);
    }
    public void StopBtnLeft()
    {
        movement.x = 0;
        movement.y = 0;
    }
    public void StopBtnRight()
    {
        movement.x = 0;
        movement.y = 0;
    }
    public void StopBtnUp()
    {
        movement.x = 0;
        movement.y = 0;
    }
    public void StopBtnDown()
    {
        movement.x = 0;
        movement.y = 0;
    }

    public void CheckGender()
    {
        if (PlayerCharacterCustomization.IsMale == false)
        {
            animator.runtimeAnimatorController = FemaleController;
        }

    }
}
