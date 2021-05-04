using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    private GameObject dungeon;

    private DungeonGenerator dungeonGenerator;
    private RandomizeConfig envRandomizer;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(dungeonGenerator.name);
        dungeonGenerator = dungeon.GetComponent<DungeonGenerator>();
        envRandomizer = dungeon.GetComponent<RandomizeConfig>();

    }

      private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Debug.Log(other.gameObject.name);
            dungeonGenerator.SetStageIsClear(true);
            GameObject.FindGameObjectWithTag("StandardRoom").SetActive(false);

            envRandomizer.ChangeEnv();
            dungeonGenerator.NextRoom(gameObject);
        }
    }
    #region debug

    private void Update()
    {
    }

    #endregion
}
