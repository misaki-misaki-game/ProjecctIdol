using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class East_SouthEast : MonoBehaviour
{
    //東南東の方向に
    int speed = 70;
    Vector3 direction;

    void Start()
    {
        direction = new Vector3(6.5f, -2.8f, 0).normalized;
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

}
