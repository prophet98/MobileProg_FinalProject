
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    private GameObject _obstacleInstance;
    private EnvConfiguration _envConfig;

    private void OnEnable()
    {
        _envConfig = GameObject.FindObjectOfType<RandomizeConfigNew>().GetCurrentConfig;
        SpawnObstacle();
    }

    private void OnDisable()
    {
        Destroy(_obstacleInstance);
    }

    private void SpawnObstacle()
    {
        _obstacleInstance = Instantiate(_envConfig.OstacleGO, transform.position, transform.rotation);
    }
}