using System.Collections;
using System.Collections.Generic;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (OnTeleport != null)
                OnTeleport(oppositeRespawn);
            
            OnTeleport?.Invoke(oppositeRespawn);

            OnEnvChange?.Invoke();

            dungeonGenerator.NextRoom(oppositeDoor);
        }
    }
}
