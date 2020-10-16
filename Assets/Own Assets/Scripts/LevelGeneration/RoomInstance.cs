using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class RoomInstance : MonoBehaviour
{
    public Vector2 gridPos;
    Vector2 roomPos;

    public RoomType type;

    public int numberOfNeighbours;

    public bool isActive = false; // set from door script
    public bool isCompleted = false;

    [HideInInspector]
    public bool doorTop, doorBot, doorLeft, doorRight;

    public CinemachineVirtualCamera CMCamera;

    public List<Door> thisRoomsDoors = new List<Door>();

    public Enemy[] mortalEnemiesInRoom;
    int mortalEnemiesInRoomCount;

    public static float leftRightDoorPosOffset = 0.45f;
    public static float upDownDorPosOffset = 0.45f;

    //for minimap
    public Sprite roomIconForMap;
    public Color activeRoomColor;
    public Color defaultRoomColor;

    public GameObject objectWithMapSprite;

    public void Setup(Vector2 _gridPos, Vector2 _roomPos, RoomType _type, bool _doorTop, bool _doorRight, bool _doorBot, bool _doorLeft, int _numberOfNeighbours, Sprite _roomIconForMap)
    {
        gridPos = _gridPos;
        roomPos = _roomPos;
        type = _type;
        doorTop = _doorTop;
        doorBot = _doorBot;
        doorLeft = _doorLeft;
        doorRight = _doorRight;
        numberOfNeighbours = _numberOfNeighbours;
        roomIconForMap = _roomIconForMap;
        defaultRoomColor = MapSpriteSelector.PickColor(type);
        activeRoomColor = MapSpriteSelector.PickColor(RoomType.Active);
        mortalEnemiesInRoom = GetComponentsInChildren<Enemy>();

        CMCamera = GetComponentInChildren<CinemachineVirtualCamera>();
        if (type == RoomType.BaseRoom)
        {
            CMCamera.Priority = 15;
            isActive = true;
        }
        MakeDoors();
    }

    void MakeDoors()
    {
        Vector2 topDoorPos = roomPos + new Vector2(0.5f, -7.5f + upDownDorPosOffset);
        Vector2 botDoorPos = roomPos + new Vector2(0.5f, -17.5f - upDownDorPosOffset);
        Vector2 leftDoorPos = roomPos + new Vector2(-8.5f - leftRightDoorPosOffset, -12.5f);
        Vector2 rightDoorPos = roomPos + new Vector2(9.5f + leftRightDoorPosOffset, -12.5f);

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


    public void InstantiateMapSprite(Transform parent)
    {
        objectWithMapSprite = new GameObject();
        objectWithMapSprite.name = "Map Piece";

        SpriteRenderer renderer = objectWithMapSprite.AddComponent<SpriteRenderer>();
        renderer.sprite = roomIconForMap;
        renderer.color = MapSpriteSelector.PickColor(type);

        Vector2 drawPos = gridPos;
        drawPos.x *= 16;//aspect ratio of map sprite HAS TO BE THE SAME LIKE IN LEVEL GENERATION IN DRAWMAP
        drawPos.y *= 8;

        drawPos += new Vector2(-500, 500);

        objectWithMapSprite.transform.position = drawPos;
        if(parent != null) objectWithMapSprite.transform.parent = parent;
       // Instantiate(objectWithMapSprite, drawPos, Quaternion.identity);
    }

    private void OnDestroy()
    {
        Destroy(objectWithMapSprite);
    }

    private void Start()
    {
        mortalEnemiesInRoomCount = mortalEnemiesInRoom.Length;
    }

    private void Update()
    {
        if (isActive)
        {
            int i=0;
            foreach(Enemy enemy in mortalEnemiesInRoom)
            {
                if (enemy == null) i++;
            }
            if (i == mortalEnemiesInRoomCount)
            {
                foreach(Door door in thisRoomsDoors)
                {
                    door.spriteRenderer.color = door.defaultDoorColor;
                    door.isLocked = false;
                }
            }
            else
            {
                foreach (Door door in thisRoomsDoors)
                {
                    door.spriteRenderer.color = door.lockedDoorColor;
                    door.isLocked = true;
                }
            }
        }
    }
}

