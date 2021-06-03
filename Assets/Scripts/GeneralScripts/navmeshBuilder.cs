using UnityEngine;
using UnityEngine.AI;

public class navmeshBuilder : MonoBehaviour
{
    public NavMeshSurface surface;
    private void OnEnable()
    {
        RebuildNavMesh();
        Door.OnEnvChange += RebuildNavMesh;
    }

    private void OnDisable()
    {
        Door.OnEnvChange -= RebuildNavMesh;
    }

    private void RebuildNavMesh()
    {
        if (surface)
        {
            surface.BuildNavMesh();  
        }
        
    }
}
