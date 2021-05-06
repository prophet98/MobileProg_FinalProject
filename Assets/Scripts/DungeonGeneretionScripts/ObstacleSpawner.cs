using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    private GameObject[] obstaclesSpwanPoints;
    private EnvConfiguration envConfig;

    private void Start()
    {
    }
    private void OnEnable()
    {
        Door.OnEnvChange += Refresh;
    }

    private void Refresh()
    {
        envConfig = GetComponent<RandomizeConfig>().GetCurrentConfig;

        obstaclesSpwanPoints = GameObject.FindGameObjectsWithTag("Obstacle");

        for(int i = 0; i < obstaclesSpwanPoints.Length; i++)
        {
            Instantiate(envConfig.OstacleGO, obstaclesSpwanPoints[i].transform);
        }
    }
}
