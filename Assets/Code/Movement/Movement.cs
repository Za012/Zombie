using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 2f;
    public float runningMultiplier = 3;
    public float gravity = -19.62f;
    public float jumpHeight = 1.5f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask; 

    
    public Vector3 velocity;
    bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        groundCheck = GameObject.Find("GroundCheck").transform;
        //controller = Game.PLAYER.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    private void Walk()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            controller.Move(move * (speed * runningMultiplier) * Time.deltaTime);
        }
        else
        {
            controller.Move(move * speed * Time.deltaTime);
        }
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        Walk();

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime); 
        
    }

}
