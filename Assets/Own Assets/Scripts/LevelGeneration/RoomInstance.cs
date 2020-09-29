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

    [HideInInspector]
    public bool doorTop, doorBot, doorLeft, doorRight;

    [HideInInspector]
    public CinemachineVirtualCamera CMCamera;

    public void Setup(Vector2 _gridPos, Vector2 _roomPos, int _type, bool _doorTop, bool _doorRight, bool _doorBot, bool _doorLeft)
    {
        gridPos = _gridPos;
        roomPos = _roomPos;
        type = _type;
        doorTop = _doorTop;
        doorBot = _doorBot;
        doorLeft = _doorLeft;
        doorRight = _doorRight;
        MakeDoors();
    }

    private void MakeDoors()
    {
        print("LOL");
    }
}
