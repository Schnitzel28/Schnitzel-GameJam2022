using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float JumpHeight = 10f;

    GroundCheck groundCheck;
    Rigidbody rb;
    Animator anim;

    float xValue;



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        groundCheck = FindObjectOfType<GroundCheck>();
        anim = GetComponent<Animator>();

    }


    void Update()
    {
        ProccessMovement();
        ProccessJump();
    }


    #region Jump
    void ProccessJump()
    {
        if(groundCheck.IsOnGround)
        {
            SetJumpAnimBool(false);
        }

        if (Input.GetKey(KeyCode.Space) && groundCheck.IsOnGround) 
        {
            Jump();
        }
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * JumpHeight, ForceMode.Impulse);
        groundCheck.SetGroundedFalse();
        SetJumpAnimBool(true);
    }

    void SetJumpAnimBool(bool isJumping)
    {
        anim.SetBool("Jump", isJumping);
    }
    #endregion

    #region Movement
    void ProccessMovement()
    {
        xValue = Input.GetAxis("Horizontal") * moveSpeed;

        Move(xValue);
        Rotate(xValue);
    }

    void Move(float xValue)
    {
        rb.velocity = new Vector3(xValue, rb.velocity.y, 0);
        int xValInt = Mathf.Abs(Mathf.RoundToInt(xValue) * 10);
        anim.SetInteger("Move",xValInt);
    }

    void Rotate(float xValue)
    {
        float direction = CalculateDirection(xValue);
        Vector3 LookDirection = new Vector3(direction, 0, 0);

        transform.rotation = Quaternion.LookRotation(LookDirection);

    }

    float CalculateDirection(float xValue)
    {
        int direction = 1;
        if (xValue > .25f) direction = 1;
        if (xValue < -.25f) direction = -1;
        return direction;
    }

    #endregion

}
