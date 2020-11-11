using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

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
    static float timeToBlockPlayerAction = 0.8f;
    static float timeToBlockDoors = 1.5f;

    [HideInInspector] public int direction;
    [HideInInspector] public Vector2 roomPos;
    [HideInInspector] public Vector2 doorPos;

    public Color defaultDoorColor;
    public Color lockedDoorColor;
    public SpriteRenderer spriteRenderer;

    public bool isLocked = false;
    Door doorToLock;

    bool wasRecentlyUsed = false;

    int teleportValue;

    RoomInstance parentRoom;

    GameObject player;

    AudioSource doorPassSound;

    private void OnDestroy()
    {
        //has to be done otherwise when the game is restarted via main menu, old doors are still there and there are missing refferences and one can't go through doors
        if (allDoors.Count != 0) allDoors.Clear();
    }

    private void Start()
    {
        doorPassSound = GetComponent<AudioSource>();

        defaultDoorColor = new Color(1f, 1f, 1f, 1f);
        lockedDoorColor = new Color(1f, 0.2f, 0.2f, 1f);

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = 5;
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
                doorToLock = FindDoorToLockAndChangeCamera(afterTeleportationPos + new Vector2(0, -2*RoomInstance.upDownDorPosOffset));
                StartCoroutine(LockDoorForTime(doorToLock));
                break;
            case 1:
                teleportValue = 46;
                afterTeleportationPos = doorPos + new Vector2(teleportValue, 0);
                doorToLock = FindDoorToLockAndChangeCamera(afterTeleportationPos + new Vector2(-2 * RoomInstance.leftRightDoorPosOffset, 0));
                StartCoroutine(LockDoorForTime(doorToLock));
                break;
            case 2:
                teleportValue = -54;
                afterTeleportationPos = doorPos + new Vector2(0, teleportValue);
                doorToLock = FindDoorToLockAndChangeCamera(afterTeleportationPos + new Vector2(0,2 * RoomInstance.upDownDorPosOffset));
                StartCoroutine(LockDoorForTime(doorToLock));
                break;
            case 3:
                teleportValue = -46;
                afterTeleportationPos = doorPos + new Vector2(teleportValue, 0);
                doorToLock = FindDoorToLockAndChangeCamera(afterTeleportationPos + new Vector2(2 * RoomInstance.leftRightDoorPosOffset, 0));
                StartCoroutine(LockDoorForTime(doorToLock));
                break;
        }

        player.transform.position = afterTeleportationPos;
    }

    Door FindDoorToLockAndChangeCamera(Vector2 doorLocation)
    {
        foreach(Door door in allDoors)
        {
            //this was faulty in some situations, safer to use the other if statement
            //if (door.doorPos != doorLocation) continue;
            if (door.doorPos.x <= doorLocation.x - 2f || door.doorPos.x >= doorLocation.x + 2f || door.doorPos.y <= doorLocation.y - 2f || door.doorPos.y >= doorLocation.y + 2f) continue;
            //if it reaches here, we are sure the right door was iterated and we do things accordingly to change camera, activity of the room and color on minimap
            this.parentRoom.CMCamera.Priority = 10;
            door.parentRoom.CMCamera.Priority = 15;

            StartCoroutine(BlockPlayerControllsAndActivateEnemiesAfterTime(door));

            //THIS WORKS, MAYBE UNCOMMENT LATER
            //if (this.parentRoom.objectWithMapSprite == null) this.parentRoom.InstantiateMapSprite(null);
            //if (door.parentRoom.objectWithMapSprite == null) door.parentRoom.InstantiateMapSprite(null);

            this.parentRoom.objectWithMapSprite.GetComponent<SpriteRenderer>().color = this.parentRoom.defaultRoomColor;
            door.parentRoom.objectWithMapSprite.GetComponent<SpriteRenderer>().color = door.parentRoom.activeRoomColor;
            return door;
        }
        return null;
    }

    private IEnumerator BlockPlayerControllsAndActivateEnemiesAfterTime(Door door)
    {
        this.parentRoom.isActive = false;
        PlayerHealth.state = PlayerHealthState.BlockedControlls;
        //doorPassSound.Play();

        yield return new WaitForSeconds(timeToBlockPlayerAction);

        PlayerHealth.state = PlayerHealthState.Alive;
        door.parentRoom.SetRoomAndEnemiesInRoomActive();
    }

    IEnumerator LockDoorForTime(Door door)
    {
        door.wasRecentlyUsed = true;
        yield return new WaitForSeconds(timeToBlockDoors);
        door.wasRecentlyUsed = false;
    }
}