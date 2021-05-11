
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyType;

    private void Start()
    {
        SpawnEnemy();
    }


    public void SpawnEnemy()
    {
        Instantiate(enemyType, transform.position, transform.rotation);
    }
}
