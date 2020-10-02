using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSpriteSelector : MonoBehaviour
{

    public Sprite spU, spD, spR, spL,
            spUD, spRL, spUR, spUL, spDR, spDL,
            spULD, spRUL, spDRU, spLDR, spUDRL;
    public bool up, down, left, right;
    public int type; // 0: normal, 1: enter
    public Color normalColor, enterColor;
    Color mainColor;
    SpriteRenderer rend;
    //void Start()
    //{
    //    rend = GetComponent<SpriteRenderer>();
    //    mainColor = normalColor;
    //    PickSprite();
    //    PickColor();
    //}

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

    //Color PickColor()
    //{ //changes color based on what type the room is
    //    if (type == 0)
    //    {
    //        return normalColor;
    //    }
    //    else if (type == 1)
    //    {
    //        return enterColor;
    //    }
    //    return mainColor;
    //}

    public static Color PickColor(int type)
    {
        if (type == 0) return new Color(1f, 1f, 1f, 1f);
        if (type == 1) return new Color(0f, 1f, 0f, 1f);
        if (type == -1) return new Color(1f, 0f, 0f, 1f); //for active room
        return new Color(0f, 0f, 0f, 1f);
    }


}
