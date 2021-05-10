using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    private List<GameObject> obstacleSpawnPoints;
    private List<GameObject> prefabsObstacleSpawns;
    private EnvConfiguration envConfig;

    private void Start()
    {
        prefabsObstacleSpawns = new List<GameObject>();
        obstacleSpawnPoints = new List<GameObject>();
        Refresh();
    }
    private void OnEnable()
    {
        Door.OnEnvChange += Refresh;
    }

    private void OnDisable()
    {
        Door.OnEnvChange += Refresh;
    }

    private void Refresh()
    {
        DestroyOldObstacles();

        envConfig = GetComponent<RandomizeConfig>().GetCurrentConfig;
        Debug.Log(envConfig.name);

        obstacleSpawnPoints.AddRange(GameObject.FindGameObjectsWithTag("Obstacle"));

        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Obstacle").Length; i++)
        {
            prefabsObstacleSpawns.Add(Instantiate(envConfig.OstacleGO, obstacleSpawnPoints[i].transform.position, Quaternion.identity));
        }
        Debug.Log(obstacleSpawnPoints.Count.ToString());
    }

    private void DestroyOldObstacles()
    {
        for (int i = 0; i < prefabsObstacleSpawns.Count; i++)
        {
            Destroy(prefabsObstacleSpawns[i], 0f);
            Debug.Log("Destroying");
        }

        obstacleSpawnPoints.Clear();
        prefabsObstacleSpawns.Clear();
    }
}
