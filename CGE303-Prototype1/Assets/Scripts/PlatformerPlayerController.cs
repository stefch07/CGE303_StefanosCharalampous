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
    public bool isGrounded;

    private Rigidbody2D rb;
    private float horizontalInput;

    //Audio clip
    public AudioClip jumpSound;

    public AudioSource playerAudio;

    //private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();

        playerAudio = GetComponent<AudioSource>();

        if (groundCheck == null)
        {

            Debug.LogError("Groundcheck is not assigned to player controller!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Get input values for horizontal movement
        horizontalInput = Input.GetAxisRaw("Horizontal");

        //check for jump input
        if ( Input.GetButtonDown("Jump") && isGrounded)
        {

            //Apply an upward force for jumping
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);

            playerAudio.PlayOneShot(jumpSound, 1.0f);

            /*if (horizontalInput > 0f)
            {
                anim.SetBool("running", true);
            }
            else if (horizontalInput < 0f)
            {
                anim.SetBool("running", true);
            }
            else
            {
                anim.SetBool("running", false);
            }
            */
        }
    }

    void FixedUpdate()
    {
        //move the player using rigidbody2d in FixedUpdate
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);

        // check if the player is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);


        if (horizontalInput > 0)
        {
            //transform.localScale = new Vector3(1f, 1f, 1f);
            transform.localScale = Quaternion.Euler(0, 0, 0);

        }

        else if (horizontalInput < 0)
        {
            //transform.localScale = new Vector3(-1f, 1f, 1f);
            transform.localScale = Quaternion.Euler(0, 180, 0);
        }
       
    }

}
