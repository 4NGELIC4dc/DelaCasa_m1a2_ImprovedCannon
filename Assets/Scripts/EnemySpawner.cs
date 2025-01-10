using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int enemiesPerWave = 5;
    public float spawnInterval = 2f;
    public int totalWaves = 3;

    private int currentWave = 0;
    private bool spawning = true;

    void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        while (currentWave < totalWaves && spawning)
        {
            for (int i = 0; i < enemiesPerWave; i++)
            {
                if (!spawning) yield break; 

                Instantiate(enemyPrefab, transform.position, Quaternion.identity);
                yield return new WaitForSeconds(spawnInterval);
            }
            currentWave++;
            yield return new WaitForSeconds(5f); 
        }
    }

    public void StopSpawning()
    {
        spawning = false;
    }
}
