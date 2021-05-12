using UnityEngine;


public class navmeshBuilder : MonoBehaviour
{
    private void Start()
    {
        //RebuildNavMesh();
    }

    private void OnEnable()
    {
        Door.OnEnvChange += RebuildNavMesh;
    }

    private void OnDisable()
    {
        Door.OnEnvChange -= RebuildNavMesh;
    }

    private void RebuildNavMesh()
    {
       
    }
}
