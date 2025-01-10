using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    private int hitCount = 0;
    private Renderer rend;
    private Rigidbody rb;
    private bool isFrozen = false;

    void Start()
    {
        rend = GetComponent<Renderer>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!isFrozen)
        {
            transform.position = Vector3.MoveTowards(transform.position, GameManager.Instance.CannonBasePosition, speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            TakeHit();
            StartCoroutine(DestroyProjectileAfterDelay(other.gameObject));
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeHit();
            StartCoroutine(DestroyProjectileAfterDelay(collision.gameObject));
        }
    }

    IEnumerator DestroyProjectileAfterDelay(GameObject projectile)
    {
        yield return new WaitForSeconds(0.25f);
        if (projectile != null)
        {
            Destroy(projectile);
        }
    }

    void TakeHit()
    {
        hitCount++;

        // Ensure material changes to red
        if (rend == null)
        {
            rend = GetComponent<Renderer>();
        }
        rend.material.color = Color.red;

        if (hitCount >= 3)
        {
            Destroy(gameObject);
        }
    }

    public void Freeze()
    {
        if (GetComponent<Rigidbody>() != null)
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().isKinematic = true;
        }
        this.enabled = false; 
    }

}
