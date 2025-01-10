using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Transform cannonBase;
    public Transform cannonGun; 
    private bool isGameOver = false;

    void Awake()
    {
        Instance = this;
    }

    public Vector3 CannonBasePosition => cannonBase.position;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && !isGameOver)
        {
            GameOver();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!isGameOver && collision.gameObject.CompareTag("Enemy"))
        {
            if (collision.gameObject.transform.position.z >= cannonBase.position.z ||
                collision.gameObject.transform.position.z >= cannonGun.position.z)
            {
                GameOver();
            }
        }
    }


    void GameOver()
    {
        if (isGameOver) return;
        isGameOver = true;

        if (cannonBase != null)
            cannonBase.GetComponent<Renderer>().material.color = Color.red;
        if (cannonGun != null)
            cannonGun.GetComponent<Renderer>().material.color = Color.red;

        Enemy[] enemies = FindObjectsOfType<Enemy>();
        foreach (Enemy enemy in enemies)
        {
            enemy.Freeze();
        }

        EnemySpawner spawner = FindObjectOfType<EnemySpawner>();
        if (spawner != null)
            spawner.StopSpawning();

        CannonController cannon = FindObjectOfType<CannonController>();
        if (cannon != null)
            cannon.enabled = false;

        Time.timeScale = 0;
    }
}
