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

    [SerializeField]
    private float adjustRotation;
    [SerializeField]
    private int roomBeforeTheBoss = 5;

    private const string stdRString = "StandardRoom";
    private const string bossRString = "BossRoom";

    private int roomCount;
    private bool nextIsBoss = false;
    private bool stageClear = false;

    

    #region init

    void Start()
    {
        RoomRandomSelector();
    }

    private void OnEnable()
    {
        Door.OnEnvChange += SetStageIsClear;
    }

    private void OnDisable()
    {
        Door.OnEnvChange -= SetStageIsClear;
    }

    #endregion

    #region room_generation

    private void RoomRandomSelector()
    {
        if (!nextIsBoss)
        {

            int randomN = Random.Range(0, stdRoomVariants.Length);
            stdRoomVariants[randomN].SetActive(true);

            nextIsBoss = false;

            //Debug.Log(stdRoomVariants[randomN].name);
        }
        else if (nextIsBoss)
        {
            GameObject.FindGameObjectWithTag(bossRString).SetActive(false);

            int randomN = Random.Range(0, bossRoomVariants.Length);
            bossRoomVariants[randomN].SetActive(true);

            nextIsBoss = false;
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
        fallenDoorInstance = Instantiate(fallenDoorPrefab, fallenDoorPrefab.transform.position, doorMustFall.transform.localRotation);
        Vector3 rotationOffset = new Vector3(0, adjustRotation, 0);
        fallenDoorInstance.transform.Rotate(rotationOffset, Space.Self);
        doorMustFall.SetActive(false);
    }

    public void NextRoom(GameObject entryDoor)
    {
        if (stageClear == true)
        {
            GameObject.FindGameObjectWithTag("StandardRoom").SetActive(false);

            NewFallenDoor(entryDoor);
            RoomRandomSelector();
        }
        stageClear = false;
    }

    private void UpdateBossRoom()
    {
        roomCount++;
        if(roomCount >= roomBeforeTheBoss)
        {
            nextIsBoss = true;
            roomCount = 0;
        }
    }
  

    public void SetNextIsBoss(bool isBoss)
    {
        nextIsBoss = isBoss;
    }

    public void SetStageIsClear()
    {
        stageClear = true;
    }

    #endregion
}
