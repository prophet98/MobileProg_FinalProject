using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    private DungeonGenerator dungeonGenerator;

      private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            EnvEventManager.Change();
            dungeonGenerator.NextRoom(gameObject);
        }
    }
}
