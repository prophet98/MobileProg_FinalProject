using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private DungeonGenerator dungeonGenerator;

    // Start is called before the first frame update
    void Start()
    {
        dungeonGenerator = GameObject.Find("Dungeon").GetComponent<DungeonGenerator>();
        //Debug.Log(dungeonGenerator.name);
    }

      private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Debug.Log(other.gameObject.name);
            dungeonGenerator.SetStageIsClear(true);
            GameObject.FindGameObjectWithTag("StandardRoom").SetActive(false);

            dungeonGenerator.NextRoom(gameObject);
        }
    }
    #region debug

    private void Update()
    {
    }

    #endregion
}
