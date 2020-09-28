using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    /*
     0 - top
     1 - right
     2 - bot
     3 - left
     */

    public static List<Door> allDoors = new List<Door>();

    [HideInInspector] public int direction;
    [HideInInspector] public Vector2 roomPos;
    [HideInInspector] public Vector2 doorPos;

    bool isLocked = false;
    Door doorToLock;

    bool wasRecentlyUsed = false;

    int teleportValue;
    

    public GameObject player;
    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isLocked || wasRecentlyUsed || collision.tag != "Player") return;

        //StartCoroutine(BlockDoorForTime());


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
                doorToLock = FindDoorToLock(afterTeleportationPos);
                StartCoroutine(LockDoorForTime(doorToLock));
                break;
            case 1:
                teleportValue = 46;
                afterTeleportationPos = doorPos + new Vector2(teleportValue, 0);
                doorToLock = FindDoorToLock(afterTeleportationPos);
                StartCoroutine(LockDoorForTime(doorToLock));
                break;
            case 2:
                teleportValue = -54;
                afterTeleportationPos = doorPos + new Vector2(0, teleportValue);
                doorToLock = FindDoorToLock(afterTeleportationPos);
                StartCoroutine(LockDoorForTime(doorToLock));
                break;
            case 3:
                teleportValue = -46;
                afterTeleportationPos = doorPos + new Vector2(teleportValue, 0);
                doorToLock = FindDoorToLock(afterTeleportationPos);
                StartCoroutine(LockDoorForTime(doorToLock));
                break;
        }

        player.transform.position = afterTeleportationPos;
    }

    Door FindDoorToLock(Vector2 doorLocation)
    {
        foreach(Door door in allDoors)
        {
            if (door.doorPos != doorLocation) continue;

            return door;
        }
        return null;
    }

    IEnumerator LockDoorForTime(Door door)
    {
        if (door == null) print("chuj");
        door.wasRecentlyUsed = true;
        yield return new WaitForSeconds(2f);
        door.wasRecentlyUsed = false;
    }
}