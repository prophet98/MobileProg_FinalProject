using UnityEngine;
using UnityEngine.AI;

public class navmeshBuilder : MonoBehaviour
{
    public NavMeshSurface surface;
    private void Start()
    {
        RebuildNavMesh();
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
        Debug.Log("building nav mesh");
        if (surface)
        {
            surface.BuildNavMesh();  
        }
        
    }
}
