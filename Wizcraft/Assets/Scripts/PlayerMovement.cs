using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController controller;
    public float moveSpeed=12f;
    public Transform playerHead;
    public float gravity = -9.81f;
    Vector3 velocity;

    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundCheckDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;

    public Animator animator;

    // Start is called before the first frame update
    void Awake()
    {
        controller=GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        

        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * moveSpeed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight*-2f*gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (z == 0)
            animator.SetBool("isStanding", true);
        else 
        {
            animator.SetBool("isStanding", false);
            animator.SetBool("isRunning", true);
        }
        
    }
}
