using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerPlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    private bool isGrounded;
    
    private Rigidbody2D rb;
    private float horizontalInput;
    
    public AudioClip jumpSound;
    
    private AudioSource playerAudio;
    
    public AudioSource audioPlayer;
    
    private Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        // Set reference variables to components
        playerAudio = GetComponent<AudioSource>();
        
        animator = GetComponent<Animator>();
        
        if (groundCheck == null)
        {
            Debug.LogError("GroundCheck not assigned to the player controller!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Get input values for horizontal movement
        horizontalInput = Input.GetAxis("Horizontal");
        
        // Check for jump input
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            // Apply an upward force for jumping
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            
            // Play jump sound effect
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }
    }
    
    void FixedUpdate()
    {
        if (!PlayerHealth.hitRecently) {
            // Move the player using Rigidbody2D in FixedUpdate
            rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
        }
        
        animator.SetFloat("xVelocityAbs", Mathf.Abs(rb.velocity.x));
        
        animator.SetFloat("yVelocityAbs", rb.velocity.y);
        
        // Check if the player is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        
        // Set animator parameter onGround to isGrounded
        animator.SetBool("onGround", isGrounded);
        
        if (horizontalInput > 0)
        {
            // set the rotation of the player to face right
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (horizontalInput < 0)
        {
            // set the rotation of the player to face left
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
    
    public void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "CollisionTag") {
            audioPlayer.Play();
        }
    }
}
