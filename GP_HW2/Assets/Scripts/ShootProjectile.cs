using UnityEngine;
using System.Linq;

public class ShootProjectile : MonoBehaviour
{
    [Header("Shooting Settings")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 0.3f;   // Time between shots
    public float shootRange = 10f;  // Maximum distance to target

    private float nextFireTime = 0f;

    void Update()
    {
        HandleAutoShooting();
    }

    // -------------------
    // Auto-shooting logic
    // -------------------
    void HandleAutoShooting()
    {
        // Only shoot when enough time has passed
        if (Time.time < nextFireTime)
            return;

        Transform target = FindClosestEnemy();

        if (target != null)
        {
            Shoot(target);
            nextFireTime = Time.time + fireRate;
        }
    }

    Transform FindClosestEnemy()
    {
        // Find all objects with the "Enemy" tag
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0)
            return null;

        // Find the closest one
        Transform closest = enemies
            .OrderBy(e => Vector3.Distance(transform.position, e.transform.position))
            .First()
            .transform;

        // Only shoot if within range
        if (Vector3.Distance(transform.position, closest.position) <= shootRange)
            return closest;

        return null;
    }

    void Shoot(Transform target)
    {
        // Spawn bullet at fire point
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Optional: rotate to face the target before shooting
        Vector3 direction = (target.position - firePoint.position).normalized;
        bullet.transform.forward = direction;

        // If your Bullet script has a target method, pass it here
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
            bulletScript.SetTarget(target);
    }
}
