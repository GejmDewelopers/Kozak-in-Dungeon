﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Door : MonoBehaviour
{
    /*
     0 - top
     1 - right
     2 - bot
     3 - left
     */

    public static List<Door> allDoors = new List<Door>();
    public static List<GameObject> doorPrefabs = new List<GameObject>();

    [HideInInspector] public int direction;
    [HideInInspector] public Vector2 roomPos;
    [HideInInspector] public Vector2 doorPos;

    bool isLocked = false;
    Door doorToLock;

    bool wasRecentlyUsed = false;

    int teleportValue;

    RoomInstance parentRoom;

    GameObject player;
    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject;
        parentRoom = GetComponentInParent<RoomInstance>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isLocked || wasRecentlyUsed || collision.tag != "Player") return;
        ChoosePositionAndTeleportPlayer();
    }

    private void ChoosePositionAndTeleportPlayer()
    {
        Vector2 afterTeleportationPos = doorPos;
        switch (direction)
        {
            case 0:
                teleportValue = 54;
                afterTeleportationPos = doorPos + new Vector2(0, teleportValue);
                doorToLock = FindDoorToLockAndChangeCamera(afterTeleportationPos);
                StartCoroutine(LockDoorForTime(doorToLock));
                break;
            case 1:
                teleportValue = 46;
                afterTeleportationPos = doorPos + new Vector2(teleportValue, 0);
                doorToLock = FindDoorToLockAndChangeCamera(afterTeleportationPos);
                StartCoroutine(LockDoorForTime(doorToLock));
                break;
            case 2:
                teleportValue = -54;
                afterTeleportationPos = doorPos + new Vector2(0, teleportValue);
                doorToLock = FindDoorToLockAndChangeCamera(afterTeleportationPos);
                StartCoroutine(LockDoorForTime(doorToLock));
                break;
            case 3:
                teleportValue = -46;
                afterTeleportationPos = doorPos + new Vector2(teleportValue, 0);
                doorToLock = FindDoorToLockAndChangeCamera(afterTeleportationPos);
                StartCoroutine(LockDoorForTime(doorToLock));
                break;
        }

        player.transform.position = afterTeleportationPos;
    }

    Door FindDoorToLockAndChangeCamera(Vector2 doorLocation)
    {
        foreach(Door door in allDoors)
        {
            if (door.doorPos != doorLocation) continue;

            this.parentRoom.CMCamera.Priority = 10;
            door.parentRoom.CMCamera.Priority = 15;
            return door;
        }
        return null;
    }

    IEnumerator LockDoorForTime(Door door)
    {
        door.wasRecentlyUsed = true;
        yield return new WaitForSeconds(1f);
        door.wasRecentlyUsed = false;
    }
}