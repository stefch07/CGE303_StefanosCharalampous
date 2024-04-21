using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int health = 100;

    public GameObject deathEffect;

    private DisplayBar healthBar;

    private void Start()
    {

        healthBar = GetComponentInChildren<DisplayBar>();

        if (healthBar == null)
        {

            Debug.LogError("HealthBar (DisplayBar script) not found");
            return;

        }

        healthBar.SetMaxValue(health);

    }

    public void TakeDamage(int damage)
    {

        health -= damage;

        healthBar.SetValue(health);

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }

}
