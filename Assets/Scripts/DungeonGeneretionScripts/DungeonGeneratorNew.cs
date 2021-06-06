using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGeneratorNew : MonoBehaviour
{
    //doors
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
    private GameObject _fallenDoorInstance;
    private GameObject _doorMustFall;


    [SerializeField]
    private GameObject[] stdRoomVariants;
    [SerializeField]
    private GameObject[] bossRoomVariants;

    private const string StdRString = "StandardRoom";
    private const string BossRString = "BossRoom";

    public bool nextIsBoss = false;
    private bool _stageClear = false;

    [SerializeField]
    private float adjustRotation;

    [SerializeField]
    private int roomBeforeTheBoss = 1;
    [SerializeField]
    private GameObject bossFightText;
    private int _roomCount;

    #region init

    void Start()
    {
        RoomRandomSelector();

        bossFightText.SetActive(false);

        _doors = new[] { doorUp, doorDown, doorLeft, doorRight };

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
            bossFightText.SetActive(false);

            int randomN = Random.Range(0, stdRoomVariants.Length);
            stdRoomVariants[randomN].SetActive(true);

        }
        else if (nextIsBoss)
        {
            bossFightText.SetActive(true);

            int randomN = Random.Range(0, bossRoomVariants.Length);
            bossRoomVariants[randomN].SetActive(true);
        }
    }

    private void NewFallenDoor(GameObject door)
    {
        if (_fallenDoorInstance != null)
        {
            _doorMustFall.SetActive(true);
            Destroy(_fallenDoorInstance);
        }

        _doorMustFall = door;
        _fallenDoorInstance = Instantiate(fallenDoorPrefab, _doorMustFall.transform.position, Quaternion.identity);
        //Vector3 rotationOffset = new Vector3(0, adjustRotation, 0);
        //fallenDoorInstance.transform.Rotate(rotationOffset, Space.Self);
        _doorMustFall.SetActive(false);
    }

    private void CheckForBossRoom()
    {
        _roomCount++;
        int roomBeforeTheBossLocal = Random.Range(roomBeforeTheBoss, roomBeforeTheBoss * 2);
        if (_roomCount >= roomBeforeTheBossLocal)
        {
            nextIsBoss = true;
            _roomCount = 0;
            Debug.Log("r count: " + _roomCount.ToString());
        }
        else
        {
            nextIsBoss = false;
        }
    }

    public void NextRoom(GameObject entryDoor)
    {
        if (_stageClear)
        {
            CheckForBossRoom();
            GameObject.FindGameObjectWithTag("StandardRoom").SetActive(false);
            foreach (var door in _doors)
            {
                door.GetComponent<Door>().ResetDoor();
            }
            NewFallenDoor(entryDoor);
            RoomRandomSelector();
        }
        _stageClear = false;
    }

    //public void SetNextIsBoss(bool isBoss)
    //{
    //    nextIsBoss = isBoss;
    //}

    public void SetStageIsClear()
    {
        _stageClear = true;
    }

    #endregion
}
