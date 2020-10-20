using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSpriteSelector : MonoBehaviour
{

    public Sprite spU, spD, spR, spL,
            spUD, spRL, spUR, spUL, spDR, spDL,
            spULD, spRUL, spDRU, spLDR, spUDRL;
    public bool up, down, left, right;
    public RoomType type; // 0: normal, 1: enter
    public Color normalColor, enterColor;

    public Sprite ReturnPickedSprite()
    { //picks correct sprite based on the four door bools
        if (up)
        {
            if (down)
            {
                if (right)
                {
                    if (left)
                    {
                        return spUDRL;
                    }
                    else
                    {
                        return spDRU;
                    }
                }
                else if (left)
                {
                    return spULD;
                }
                else
                {
                    return spUD;
                }
            }
            else
            {
                if (right)
                {
                    if (left)
                    {
                        return spRUL;
                    }
                    else
                    {
                        return spUR;
                    }
                }
                else if (left)
                {
                    return spUL;
                }
                else
                {
                    return spU;
                }
            }
        }
        if (down)
        {
            if (right)
            {
                if (left)
                {
                    return spLDR;
                }
                else
                {
                    return spDR;
                }
            }
            else if (left)
            {
                return spDL;
            }
            else
            {
                return spD;
            }
        }
        if (right)
        {
            if (left)
            {
                return spRL;
            }
            else
            {
                return spR;
            }
        }
        else
        {
            return spL;
        }
    }

    public static Color PickColor(RoomType type)
    {
        if (type == RoomType.NormalRoom) return new Color(1f, 1f, 1f, 1f);
        if (type == RoomType.BaseRoom) return new Color(0f, 1f, 0f, 1f);
        if (type == RoomType.BossRoom) return new Color(0f, 0f, 0f, 1f);
        if (type == RoomType.Shop) return new Color(1f, 1f, 0f, 1f);
        if (type == RoomType.ItemRoom) return new Color(0f, 0f, 1f, 1f);
        if (type == RoomType.Active) return new Color(1f, 0f, 0f, 1f); //for active room
        return new Color(0f, 0f, 0f, 1f);
    }


}
