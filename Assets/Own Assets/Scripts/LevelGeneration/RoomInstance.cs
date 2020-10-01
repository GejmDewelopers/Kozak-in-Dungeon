using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class RoomInstance : MonoBehaviour
{
    public Vector2 gridPos;
    Vector2 roomPos;

    public int type;

    public bool isActive = false; // set from door script
    public bool isCompleted = false;

    [HideInInspector]
    public bool doorTop, doorBot, doorLeft, doorRight;

    public CinemachineVirtualCamera CMCamera;

    public List<Door> thisRoomsDoors = new List<Door>();

    public Enemy[] mortalEnemiesInRoom;

    public void Setup(Vector2 _gridPos, Vector2 _roomPos, int _type, bool _doorTop, bool _doorRight, bool _doorBot, bool _doorLeft)
    {
        gridPos = _gridPos;
        roomPos = _roomPos;
        type = _type;
        doorTop = _doorTop;
        doorBot = _doorBot;
        doorLeft = _doorLeft;
        doorRight = _doorRight;

        mortalEnemiesInRoom = GetComponentsInChildren<Enemy>();

        CMCamera = GetComponentInChildren<CinemachineVirtualCamera>();
        if (type == 1)
        {
            CMCamera.Priority = 15;
            isActive = true;
        }
        MakeDoors();
    }

    void MakeDoors()
    {
        Vector2 topDoorPos = roomPos + new Vector2(0.5f, -7.5f);
        Vector2 botDoorPos = roomPos + new Vector2(0.5f, -17.5f);
        Vector2 leftDoorPos = roomPos + new Vector2(-8.5f, -12.5f);
        Vector2 rightDoorPos = roomPos + new Vector2(9.5f, -12.5f);

        if (doorTop)
        {
            SetupDoor(topDoorPos, 0);
        }
        if (doorRight)
        {
            SetupDoor(rightDoorPos, 1);
        }
        if (doorBot)
        {
            SetupDoor(botDoorPos, 2);
        }
        if (doorLeft)
        {
            SetupDoor(leftDoorPos, 3);
        }
    }

    void SetupDoor(Vector2 doorPos, int direction)
    {
        Door door = Instantiate(Door.doorPrefabs[0], doorPos, Quaternion.identity).GetComponent<Door>();
        thisRoomsDoors.Add(door);
        Door.allDoors.Add(door);
        door.gameObject.transform.parent = this.gameObject.transform;
        door.direction = direction;
        door.roomPos = roomPos;
        door.doorPos = doorPos;
    }

    public void SetRoomAndEnemiesInRoomActive()
    {
        isActive = true;
        foreach (Enemy enemy in mortalEnemiesInRoom)
        {
            enemy.enemyState = EnemyState.Active;
        }
    }
}

