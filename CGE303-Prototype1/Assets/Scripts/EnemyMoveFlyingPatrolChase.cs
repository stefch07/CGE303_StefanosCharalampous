using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveFlyingPatrolChase : MonoBehaviour
{
    // an array of waypoints the enemy will move between
    public GameObject[] patrolPoints;
    
    // Current patrol point index
    private int currentPatrolPointIndex = 0;
    
    // public variables for movement
    public float speed = 2f;
    public float chaseRange = 3f;
    
    // Enemy state enum
    public enum EnemyState {
        PATROLLING,
        CHASING
    }
    
    // Enemy state variable for the enemy's current state
    public EnemyState currentState = EnemyState.PATROLLING;
    
    // variables for targeting where the enemy will move
    public GameObject target;
    private GameObject player;
    
    // Rigidbody2D component for the enemy
    private Rigidbody2D rb;
    
    // SpriteRenderer component for the enemy
    private SpriteRenderer sr;
    
    
    // Start is called before the first frame update
    void Start()
    {
        // Find the player object
        player = GameObject.FindWithTag("Player");
        
        // get the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();
        
        // get the SpriteRenderer component
        sr = GetComponent<SpriteRenderer>();
        
        // check if the patrolPoints array is empty
        if (patrolPoints == null || patrolPoints.Length == 0) {
            // if it is, log an error message
            Debug.LogError("No patrol points assigned!");
        }
        
        // set the target to the first patrol point
        target = patrolPoints[currentPatrolPointIndex];
    }

    // Update is called once per frame
    void Update()
    {
        // Update the state based on player and target distance
        UpdateState();
        
        // Move and face based on current state
        switch (currentState) {
            case EnemyState.PATROLLING:
                Patrol();
                break;
            case EnemyState.CHASING:
                ChasePlayer();
                break;
        }
        
        // Draw a line from the enemy to its target in the Scene view
        Debug.DrawLine(transform.position, target.transform.position, Color.red);
    }
    
    void UpdateState() {
        if (IsPlayerInChaseRange() && currentState == EnemyState.PATROLLING) {
            currentState = EnemyState.CHASING;
        }
        else if (!IsPlayerInChaseRange() && currentState == EnemyState.CHASING) {
            currentState = EnemyState.PATROLLING;
        }
    }
    
    bool IsPlayerInChaseRange() {
        if (player == null) {
            Debug.LogError("Player not found");
            return false;
        }
        
        // Calculate the distance between the player and the enemy
        float distance = Vector2.Distance(transform.position, player.transform.position);
        
        // Return true if the distance is less than the chase range
        return distance <= chaseRange;
    }
    
    void Patrol() {
        // Check if reached current target
        if (Vector2.Distance(transform.position, target.transform.position) <= 0.5f) {
            // Update target to the next patrol point (wrap around)
            currentPatrolPointIndex = (currentPatrolPointIndex + 1) % patrolPoints.Length;
        }
        
        // set the target to the current patrol point
        target = patrolPoints[currentPatrolPointIndex];
        
        MoveTowardTarget();
    }
    
    void ChasePlayer() {
        target = player;
        MoveTowardTarget();
    }
    
    void MoveTowardTarget() {
        // calculate direction toward target
        Vector2 direction = target.transform.position - transform.position;
        
        // Normalize direction
        direction.Normalize();
        
        // Move toward target with rb
        rb.velocity = direction * speed;
        
        // face forward
        FaceForward(direction);
    }
    
    private void FaceForward(Vector2 direction) {
        if (direction.x < 0) {
            sr.flipX = false;
        }
        else if (direction.x > 0) {
            sr.flipX = true;
        }
    }
    
    // draw circles for patrol points in the Scene view
    private void OnDrawGizmos() {
        if (patrolPoints != null) {
            Gizmos.color = Color.green;
            foreach(GameObject point in patrolPoints) {
                Gizmos.DrawWireSphere(point.transform.position, 0.5f);
            }
        }
    }
}
