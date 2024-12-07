using System;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    private Rigidbody2D body;
    private Animator animator;
    private BoxCollider2D BoxCollider2D; 
    private float wallJumpCooldown;
    private float horizontalInput;
    //private bool isGrounded;
    
    private void Awake()
    {
        //* Grab the components we need from the player object
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        BoxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {

        horizontalInput = Input.GetAxis("Horizontal");

        //* Flip the player sprite based on the direction they are moving
        if (horizontalInput > 0.01f)
        {
            transform.localScale = Vector3.one;
        }
        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        
        //* Get animator parameters
        animator.SetBool("Run", horizontalInput != 0);
        animator.SetBool("Ground", isGrounded());

        //* Wall Jump
        if (wallJumpCooldown > 0.2f)
        {
            body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);
            
            if (onWall() && !isGrounded())
            {
                body.gravityScale = 0;
                body.linearVelocity = Vector2.zero;
            }
            else
            {
                body.gravityScale = 2;
            }
            
            if (Input.GetKey(KeyCode.Space))
            {
                Jump();
            }
        }
        else
        {
            wallJumpCooldown += Time.deltaTime;
        }
    }


    private void Jump()
    {
        if (isGrounded())
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpPower);
            animator.SetTrigger("Jump");
        }
        else if (onWall() && !isGrounded())
        {
            
            if (horizontalInput == 0 )
            {
                //* IF the player want to get out from the wall, the player will jump up with 0 units, and the player will jump to the left or right with 10 units.
                body.linearVelocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
                //* The player will jump up with 0 units, make sure that when the player jump from left/right -> right/left (get out from the wall)
                //* the player will not throw up. (Y axis)
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y,
                    transform.localScale.z);
                //* Flip the player sprite based on the direction they are moving
                
            }
            else
            {
                body.linearVelocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);
            }
            wallJumpCooldown = 0;
            //* Think simple, Mathf.Sign will return 1 if the player is facing right, -1 if the player is facing left.
            //* So, if the player is facing right, the player will jump to the left. Otherwise, the player will jump to the right.
            //* The player will jump up with 6 units. (Y axis)
            //* The player will jump to the left or right with 3 units. (X axis)
        }
    }

    private bool isGrounded()
    {
        //* Raycast is a line that is cast from a point in a direction
        //* BoxCast is a box that is cast from a point in a direction. But it has an one exception, it can detect the collider in the box.
        RaycastHit2D raycast =
            Physics2D.BoxCast(BoxCollider2D.bounds.center, BoxCollider2D.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
                                                //* origin, size, angle, direction, distance, layerMask
        return raycast.collider != null;
        //* If the raycast hits something, it will return true. Otherwise, it will return false if the player is flying (player is not in the ground).
    }
    
    private bool onWall()
    {
        //* On wall, so that we have to change the direction of the player, layerMask should be changed to wallLayer
        RaycastHit2D raycast =
            Physics2D.BoxCast(BoxCollider2D.bounds.center, BoxCollider2D.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycast.collider != null;
    }

    public bool canAttack()
    {
        return horizontalInput == 0 && isGrounded() && !onWall();
    }
}
