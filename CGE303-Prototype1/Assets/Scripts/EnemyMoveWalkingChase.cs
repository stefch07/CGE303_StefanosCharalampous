using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;

// Require a Rigidbody2D and an Animator on the enemy
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class EnemyMoveWalkingChase : MonoBehaviour
{
    // Range at which the enemy will chase the player
    public float chaseRange = 4f;
    
    // Speed at which the enemy will move
    public float enemyMovementSpeed = 1.5f;
    
    // private references
    
    // Transform of the player
    private Transform playerTransform;
    
    // Rigidbody2D of the enemy
    private Rigidbody2D rb;
    
    // Animator of the enemy
    private Animator anim;
    
    // sprite renderer of the enemy
    private SpriteRenderer sr;
    
    // Start is called before the first frame update
    void Start()
    {
        // setting up the references
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerTransform = GameObject.FindWithTag("Player").transform;
        
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Vector2 from the enemy to the player
        Vector2 playerDirection = playerTransform.position - transform.position;
        
        // distance from the enemy to the player
        float distanceToPlayer = playerDirection.magnitude;
        
        // check if the player is within the chase range
        if (distanceToPlayer <= chaseRange) {
            // we need the direction to move toward the player
            // and we're not moving up or down with this enemy
            
            // Normalizing the vector
            // this gives us the direction without the magnitude or length or distance
            playerDirection.Normalize();
            
            // set the y axis to 0 so the enemy doesn't move up or down
            playerDirection.y = 0f;
            
            // Rotate the enemy to face the player
            FacePlayer(playerDirection);
            
            // if there is ground ahead of the enemy
            if (IsGroundAhead()) {
                MoveTowardPlayer(playerDirection);
            }
            // if there is no ground ahead of the enemy, stop moving
            else {
                StopMoving();
                Debug.Log("No ground ahead");
            }
        }
        else {
            // stop moving if the player is not within the chase range
            StopMoving();
        }
    }
    
    // bool function to check if there is ground ahead of the enemy
    bool IsGroundAhead () {
        // ground check variables
        // Distance to check for ground
        float groundCheckDistance = 2.0f;
        
        // LayerMask for the ground
        LayerMask groundLayer = LayerMask.GetMask("Ground");
        
        // determine which direction the enemy is facing
        Vector2 enemyFacingDirection = (sr.flipX == false) ? Vector2.left : Vector2.right;
        
        // Raycast to check for ground ahead of the enemy
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down + enemyFacingDirection, groundCheckDistance, groundLayer);
        
        // draw a line to visualize the raycast
        Debug.DrawRay(transform.position, Vector2.down + enemyFacingDirection, Color.red);
        
        // Return true if ground is detected
        return hit.collider != null;
    }
    
    private void FacePlayer(Vector2 playerDirection) {
        // if the player is to the right of the enemy
        if (playerDirection.x < 0) {
            // face right
            //transform.rotation = Quaternion.Euler(0, 0, 0);
            sr.flipX = false;
        }
        // if the player is to the left of the enemy
        else if (playerDirection.x > 0) {
            // face left
            //transform.rotation = Quaternion.Euler(0, 180, 0);
            sr.flipX = true;
        }
    }
    
    private void MoveTowardPlayer(Vector2 playerDirection) {
        // move the enemy toward the player
        rb.velocity = new Vector2(playerDirection.x * enemyMovementSpeed, rb.velocity.y);
        // set the animator to move
        anim.SetBool("isMoving", true);
    }
    
    private void StopMoving() {
        // Stop moving if the player is out of range
        rb.velocity = new Vector2(0, rb.velocity.y);
        
        // set the animator parameter to stop moving
        anim.SetBool("isMoving", false);
    }
}
