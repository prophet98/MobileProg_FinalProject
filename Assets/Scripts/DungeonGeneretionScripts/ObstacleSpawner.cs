using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    private GameObject obstacleInstance;
    private EnvConfiguration envConfig;

    private void OnEnable()
    {
        envConfig = GameObject.FindObjectOfType<RandomizeConfig>().GetCurrentConfig;
        SpawnObstacle();
    }
    private void OnDisable()
    {
        Destroy(obstacleInstance);
    }
    private void SpawnObstacle()
    {
        obstacleInstance = Instantiate(envConfig.OstacleGO, transform.position, transform.rotation);
    }
}
