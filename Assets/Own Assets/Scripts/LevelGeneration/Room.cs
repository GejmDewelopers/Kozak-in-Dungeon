using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
    public Vector2 gridPos;

    public RoomType type;

    public bool doorTop, doorBot, doorLeft, doorRight;

    public int numberOfNeighbours;

    [SerializeField] GameObject doorU, doorR, doorB, doorL;


    public Room(Vector2 _gridPos, RoomType _type)
    {
        gridPos = _gridPos;
        type = _type;
    }

}
