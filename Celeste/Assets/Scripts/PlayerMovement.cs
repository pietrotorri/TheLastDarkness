using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CollisionDetection coll;
    private Rigidbody2D rb;

    //For jump and run
    public float speed = 4f;
    public float jumpVelocity = 7.5f;
    public float SlideSpeed = 1;

    //For wall jump
    public bool wallJumping;
    public float side;
    public float wallJumpTime = 0.4f;
    private float wallJumpCounter;

    //For better mechanics
    public float hangTime = 0.2f;
    private float hangCounter;
    public float jumpBufferLenght = 0.1f;
    private float jumpBufferCount;

    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<CollisionDetection>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame


    void Update()
    {
        if (wallJumpCounter <= 0)
        {
            //Run
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");
            Vector2 dir = new Vector2(x, y);

            Walk(dir);

            //Jump
            if (coll.onGround)
            {
                hangCounter = hangTime;
            }
            else
            {
                hangCounter -= Time.deltaTime;
            }

            if (Input.GetButtonDown("Jump"))
            {
                jumpBufferCount = jumpBufferLenght;
            }
            else
            {
                jumpBufferCount -= Time.deltaTime;
            }

            if (jumpBufferCount >= 0 && hangCounter > 0f)
            {
                Jump();
                jumpBufferCount = 0;
            }

            //Slide
            if (coll.onRightWall && !coll.onGround || coll.onLeftWall && !coll.onGround)
            {
                WallSlide();
            }

            //Wall Jump
            if (x > 0)
            {
                side = 1;
            }
            if (x < 0)
            {
                side = -1;
            }

            wallJumping = false;
            if (coll.onRightWall && !coll.onGround || coll.onLeftWall && !coll.onGround)
            {
                wallJumping = true;
            }

            if (wallJumping)
            {

                if (Input.GetButtonDown("Jump"))
                {
                    wallJumpCounter = wallJumpTime;
                    WallJump();
                    wallJumping = false;
                }
  
            }
            
        }else
        {
            wallJumpCounter -= Time.deltaTime;
        }
    }

    private void Walk(Vector2 dir)
    {
        rb.velocity = (new Vector2(dir.x * speed, rb.velocity.y));
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.velocity += Vector2.up * jumpVelocity;
    }

    private void WallSlide()
    {
        rb.velocity = new Vector2(rb.velocity.x, -SlideSpeed);
    }

    private void WallJump()
    {
        if (coll.onRightWall)
        {
            rb.velocity = new Vector2(-1 * speed, jumpVelocity);
        }
        if (coll.onLeftWall)
        {
            rb.velocity = new Vector2(1 * speed, jumpVelocity);
        }

        
    }
}
