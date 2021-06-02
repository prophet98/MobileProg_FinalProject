
using System;
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
    
    [SerializeField]
    private BattleRewardSystem PlayerBRS;

    private void OnEnable()
    {
        PlayerBRS.GetComponent<BattleRewardSystem>().UnlockDoors += UnlockDoor;
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
                
                SoundManager.instance?.Play(dungeonGenerator.nextIsBoss ? Sound.Names.BossTheme : Sound.Names.BattleTheme01);
            }
            
        }
    }

    private void UnlockDoor()
    {
        PassGO.SetActive(true);
        NoPassGO.SetActive(false);
    }

    public void ResetDoor()
    {
        NoPassGO.SetActive(true);
        PassGO.SetActive(false);
    }
    
}
