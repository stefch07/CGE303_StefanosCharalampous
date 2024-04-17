using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    private Rigidbody2D rb;

    public float speed = 20f;

    public int damage = 20;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
       Enemy enemy = hitInfo.GetComponent<Enemy>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }

        if (hitInfo.gameObject.tag != "Player")
        {
            Destroy(gameObject);
        }
       

    }

}
