using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingProjectile : MonoBehaviour
{
    public GameObject projectilePrefab;

    public Transform firePoint;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject fireProjectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        Destroy(fireProjectile, 3f);
    }

}
