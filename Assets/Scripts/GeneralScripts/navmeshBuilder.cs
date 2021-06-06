using UnityEngine;
using UnityEngine.AI;

public class NavmeshBuilder : MonoBehaviour
{
    public NavMeshSurface surface;
    private void Awake()
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
        
        surface.BuildNavMesh();  
        
    }
}
