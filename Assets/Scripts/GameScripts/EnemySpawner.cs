using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    private GameObject spawnedEnemy;
    private bool checkpointReached;
    private bool canSpawnEnemy=true;

    private void Start()
    {
        GameEvents.current.onCheckpointTouched += SetCheckPointReached;
    }
    private void OnDestroy()
    {
        GameEvents.current.onCheckpointTouched -= SetCheckPointReached;
    }
    private void OnEnable()
    {
        if (canSpawnEnemy)
        {
            spawnedEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            spawnedEnemy.GetComponent<EnemyHealthController>().onDeath += EnemyDead;
        }
        
    }
    private void EnemyDead()
    {
        spawnedEnemy.GetComponent<EnemyHealthController>().onDeath -= EnemyDead;
        canSpawnEnemy = false;
    }
    private void OnDisable()
    {
        Destroy(spawnedEnemy);
    }
    private void SetCheckPointReached()
    {
        if(!canSpawnEnemy)
            canSpawnEnemy=true;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position,1.5f);
    }
}
