using UnityEngine;

public class CannonController : MonoBehaviour
{
    public GameObject projectilePrefab;  // Projectile prefab
    public Transform firePoint;  // Projectile spawn
    public float acceleration = 9.8f; // Gravity acceleration
    public float velocity = 10f; // Initial velocity

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            FireProjectile();
        }
    }

    void FireProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        float time = 1f;
        Vector3 force = 0.5f * acceleration * time * velocity * firePoint.forward; // Formula

        rb.AddForce(force, ForceMode.Impulse);

        StartCoroutine(TrackDistance(projectile));
    }

    System.Collections.IEnumerator TrackDistance(GameObject projectile)
    {
        Vector3 startPos = projectile.transform.position;
        while (projectile != null && projectile.transform.position.y > 0)
        {
            yield return null;
        }
        float distance = Vector3.Distance(startPos, projectile.transform.position);
        Debug.Log("Distance Travelled: " + distance);
    }

}
