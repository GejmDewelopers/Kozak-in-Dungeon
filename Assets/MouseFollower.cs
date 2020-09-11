using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollower : MonoBehaviour
{
    [SerializeField] Camera cam;
    void Update()
    {
        Vector2 pos = cam.ScreenToWorldPoint(Input.mousePosition);
        transform.position = pos;
    }
}
