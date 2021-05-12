
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyType;

    private void OnEnable()
    {
        SpawnEnemy();
    }


    private void SpawnEnemy()
    {
        Instantiate(enemyType, transform.position, transform.rotation);
    }
}
