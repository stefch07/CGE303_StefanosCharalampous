



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platofrmerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;
    private BoxCollider2D coll;

    public float moveSpeed = 7f;
    public float jumpForce = 14f;

    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.1f;
    private bool isGrounded;

    private AudioSource jumpSoundEffect;
    private float horizontalInput;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
        jumpSoundEffect = GetComponent<AudioSource>();

        if (groundCheck == null)
        {
            Debug.LogError("Groundcheck is not assigned to player controller!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jumpSoundEffect.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        MovementState state;

        if (Mathf.Abs(horizontalInput) > 0f)
        {
            state = MovementState.running;
            sprite.flipX = horizontalInput < 0f;
        }
        else
        {
            state = MovementState.idle;
        }

        if (!isGrounded)
        {
            state = rb.velocity.y > 0.1f ? MovementState.jumping : MovementState.falling;
        }

        anim.SetInteger("state", (int)state);
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);

        // check if the player is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (horizontalInput > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (horizontalInput < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

    private enum MovementState { idle, running, jumping, falling }
}
