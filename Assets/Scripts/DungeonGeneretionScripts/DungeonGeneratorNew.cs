using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGeneratorNew
    : MonoBehaviour
{
    [SerializeField]
    private GameObject doorUp;
    [SerializeField]
    private GameObject doorDown;
    [SerializeField]
    private GameObject doorLeft;
    [SerializeField]
    private GameObject doorRight;

    private GameObject[] _doors;
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

    [SerializeField]
    private float adjustRotation;

    #region init

    void Start()
    {
        RoomRandomSelector();
        _doors = new[] {doorUp, doorDown, doorLeft, doorRight};
        
        foreach (var door in _doors)
        {
            door.GetComponent<Door>().ResetDoor();
        }
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

            Debug.Log(stdRoomVariants[randomN].name);
        }
        else if (nextIsBoss)
        {
            GameObject.FindGameObjectWithTag(bossRString).SetActive(false);

            int randomN = Random.Range(0, bossRoomVariants.Length);
            bossRoomVariants[randomN].SetActive(true);
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
        fallenDoorInstance = Instantiate(fallenDoorPrefab, doorMustFall.transform.position, Quaternion.identity);
        //Vector3 rotationOffset = new Vector3(0, adjustRotation, 0);
        //fallenDoorInstance.transform.Rotate(rotationOffset, Space.Self);
        doorMustFall.SetActive(false);
    }

    public void NextRoom(GameObject entryDoor)
    {
        if (stageClear)
        {
            GameObject.FindGameObjectWithTag("StandardRoom").SetActive(false);
            foreach (var door in _doors)
            {
                door.GetComponent<Door>().ResetDoor();
            }
            NewFallenDoor(entryDoor);
            RoomRandomSelector();
        }
        stageClear = false;
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