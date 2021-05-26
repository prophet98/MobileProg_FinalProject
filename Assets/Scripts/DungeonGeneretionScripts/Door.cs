
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    private DungeonGeneratorNew dungeonGenerator; //WARNING: doesn't work with dungeon_new
    [SerializeField]
    private GameObject oppositeDoor;
    [SerializeField]
    private Transform oppositeRespawn;
    //Unlock Doors
    [SerializeField]
    private GameObject PassGO;
    [SerializeField]
    private GameObject NoPassGO;

    public delegate void ChangeEnv();
    public static event ChangeEnv OnEnvChange;

    public delegate void Teleport(Transform oppRespawn);
    public static event Teleport OnTeleport;

    //provisional
    private bool DoorsChanged;
    [SerializeField]
    private BattleRewardSystem PlayerBRS;

    private void Start()
    {
        DoorsChanged = false;
        ResetDoor();
        //UnityEditor.AI.NavMeshBuilder.BuildNavMesh();
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

                ResetDoor();
            }
            
        }
    }

    private void UnlockDoor()
    {
        PassGO.SetActive(true);
        NoPassGO.SetActive(false);
        DoorsChanged = true;
    }

    private void ResetDoor()
    {
        UnityEditor.AI.NavMeshBuilder.ClearAllNavMeshes();
        UnityEditor.AI.NavMeshBuilder.BuildNavMesh(); //TODO: find a better place

        NoPassGO.SetActive(true);
        PassGO.SetActive(false);
        DoorsChanged = false;
    }

    private void Update()
    {
        if((PlayerBRS.canPassGate == true)&&(DoorsChanged == false))
        {
            UnlockDoor();
        } else if((PlayerBRS.canPassGate == false) && (DoorsChanged == true))
        {
            ResetDoor();
        }
    }
}
