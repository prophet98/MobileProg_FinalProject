using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject doorUp;
    [SerializeField]
    private GameObject doorDown;
    [SerializeField]
    private GameObject doorLeft;
    [SerializeField]
    private GameObject doorRight;

    [SerializeField]
    private GameObject fallenDoorPrefab;
    private GameObject fallenDoorInstance;

    private GameObject doorMustFall;

    [SerializeField]
    private GameObject[] stdRoomVariants;
    [SerializeField]
    private GameObject[] bossRoomVariants;

    private const string stdRString = "StandardRoom";
    private const string bossRString = "BossRoom";

    private bool nextIsBoss = false;
    private bool stageClear = false;

#region init

    void Awake()
    {
        RoomRandomSelector();

        //InitRooms();
    }

    //private void InitRooms()
    //{
    //    //init standard Rooms array
    //    var nStdRooms = GameObject.FindGameObjectsWithTag(stdRString).Length;
    //    stdRoomVariants = new GameObject[nStdRooms];
    //
    //    for (int i = 0; i < nStdRooms; i++)
    //    {
    //        stdRoomVariants[i] = GameObject.FindGameObjectsWithTag(stdRString)[i];
    //    }
    //
    //    //deactivate rooms
    //    for (int i = 0; i < stdRoomVariants.Length; i++)
    //    {
    //        stdRoomVariants[i].SetActive(false);
    //        //stdRoomVariants[i].SetActive(false);
    //        //Debug.Log(stdRoomVariants[i].name);
    //    }
    //
    //    //init boss Rooms array
    //    var nBossRooms = GameObject.FindGameObjectsWithTag(bossRString).Length;
    //    bossRoomVariants = new GameObject[nBossRooms];
    //
    //    for (int i = 0; i < nBossRooms; i++)
    //    {
    //        bossRoomVariants[i] = GameObject.FindGameObjectsWithTag(bossRString)[i];
    //        //bossRoomVariants[i].SetActive(false);
    //        //Debug.Log(bossRoomVariants[i].name);
    //    }
    //
    //    for (int i = 0; i < bossRoomVariants.Length; i++)
    //    {
    //        bossRoomVariants[i].SetActive(false);
    //        //stdRoomVariants[i].SetActive(false);
    //        //Debug.Log(stdRoomVariants[i].name);
    //    }
    //
    //    RoomRandomSelector();
    //}

    #endregion

    #region room_generation

    private void RoomRandomSelector()
    {
        if (!nextIsBoss)
        {

            int randomN = Random.Range(0, stdRoomVariants.Length);
            stdRoomVariants[randomN].SetActive(true);

            nextIsBoss = false;

            Debug.Log(stdRoomVariants[randomN].name);
        }
        else if (nextIsBoss)
        {
            GameObject.FindGameObjectWithTag(bossRString).SetActive(false);

            int randomN = Random.Range(0, bossRoomVariants.Length);
            bossRoomVariants[randomN].SetActive(true);
            
            Debug.Log(bossRoomVariants[randomN].name);
        }
    }

    private void NewFallenDoor(GameObject door)
    {
        if (fallenDoorInstance != null)
        {
            doorMustFall.SetActive(true);
            Destroy(fallenDoorInstance);
        }

        doorMustFall = door;
        fallenDoorInstance = Instantiate(fallenDoorPrefab, doorMustFall.transform.localPosition, doorMustFall.transform.rotation);
        doorMustFall.SetActive(false);
    }

    public void NextRoom(GameObject entryDoor)
    {
        if (stageClear == true)
        {
            NewFallenDoor(entryDoor);
            RoomRandomSelector();
            Debug.Log("next room");
        }
        stageClear = false;
    }

    public void SetNextIsBoss(bool isBoss)
    {
        nextIsBoss = isBoss;
    }

    public void SetStageIsClear(bool clear)
    {
        stageClear = clear;
        Debug.Log("clear");
    }

    #endregion
}
