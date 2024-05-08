using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class EnemyMoveFlyingPatrolChase : MonoBehaviour
{

    public GameObject[] patrolPoints;
    public float speed = 2f;
    public float chaseRange = 3f;
    public enum EnemyState
    {
        PATROLLING,
        CHASING
    }

    public EnemyState currentState = EnemyState.PATROLLING;
    public GameObject target;
    public GameObject player;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private int currentPatrolPointIndex = 0;
 
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        if(patrolPoints == null || patrolPoints.Length < 1)
        {
            Debug.LogError("Patrol Points are not set up for");
        }
        target = patrolPoints[currentPatrolPointIndex];
        
    }

    // Update is called once per frame
    void Update()
    
    {
        UpdateState();

        switch(currentState){
            case EnemyState.PATROLLING:
            //Patrol();
            break;
            case EnemyState.CHASING:
            //ChasePlayer();
            break;
            
        }

        
    }
    
    void UpdateState()
    {

    }
}
