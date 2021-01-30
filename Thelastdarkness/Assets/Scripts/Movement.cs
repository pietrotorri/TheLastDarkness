using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{ 
    public CharacterController controller;

    public float speed;
    public float gravity = -9.81f;
    public float jumpHeight;

    Vector3 velocity;

    public Transform groundCheck;
    public float radius;
    public LayerMask GroundLayer;
    public bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, radius, GroundLayer);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        //for Walk
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 dir = transform.right * x + transform.forward * z;

        controller.Move(dir * speed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
