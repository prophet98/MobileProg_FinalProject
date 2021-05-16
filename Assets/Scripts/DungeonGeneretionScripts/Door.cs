
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    private DungeonGenerator dungeonGenerator;
    [SerializeField]
    private GameObject oppositeDoor;
    [SerializeField]
    private Transform oppositeRespawn;

    public delegate void ChangeEnv();
    public static event ChangeEnv OnEnvChange;

    public delegate void Teleport(Transform oppRespawn);
    public static event Teleport OnTeleport;

    private void Start()
    {
        UnityEditor.AI.NavMeshBuilder.BuildNavMesh();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.GetComponent<BattleRewardSystem>().canPassGate)
            {
                OnTeleport?.Invoke(oppositeRespawn);

                OnEnvChange?.Invoke();

                dungeonGenerator.NextRoom(oppositeDoor);

                UnityEditor.AI.NavMeshBuilder.ClearAllNavMeshes();
                UnityEditor.AI.NavMeshBuilder.BuildNavMesh(); //TODO: find a better place
            }
            
        }
    }
}
