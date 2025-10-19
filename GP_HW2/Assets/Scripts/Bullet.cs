using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    private Transform target;

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        // Move toward the target
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        // Optional: destroy if too close
        if (Vector3.Distance(transform.position, target.position) < 0.3f)
        {
            Destroy(gameObject);
            Destroy(target.gameObject); // if you want to kill the enemy
        }
    }
}
