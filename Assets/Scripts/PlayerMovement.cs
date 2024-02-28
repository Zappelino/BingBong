using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;

    //Adding option to add Layer to PlayerMovement
    [SerializeField] private LayerMask jumpableGround;

    private float dirX = 0;

    //Adding option to change values in Unity directly
    [SerializeField] private float jumpStrength = 14f;
    [SerializeField] private float walkingSpeed = 7f;

    private enum MovementState { idel, running, jumping, falling }

    [SerializeField] private AudioSource jumpSoundEffect;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        //Impelement player walking
        dirX = Input.GetAxisRaw("Horizontal");

        //Adding impact of waling to velocity vector
        rb.velocity = new Vector2 (walkingSpeed * dirX, rb.velocity.y);

        //implement player jumping
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            
            //Adding jump to velocity vector
            rb.velocity = new Vector2(rb.velocity.x, jumpStrength);

            //Call jump sound effect
            jumpSoundEffect.Play();

        }

        UpdateAnimationState();

    }

    //Providing the ocrrect animation depeding on the direction the player is moving
    private void UpdateAnimationState()
    {
        MovementState state;

        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idel;
        }

        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);
    }

    //Use IsGrounded to make sure they player can only jump once before he need to be on the ground again
    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

}
