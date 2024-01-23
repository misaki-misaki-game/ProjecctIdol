using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightNoteMove : MonoBehaviour
{
    int speed = 60;

    //オブジェクトの移動方向を保持するために必要
    Vector3 direction;

    void Start()
    {
        int noteRand = Random.Range(0, 3);

        if (noteRand == 0)
        {
            direction = new Vector3(2.8f, 6.5f, 0).normalized;
        }
        else if (noteRand == 1)
        {
            direction = new Vector3(6.5f, 2.8f, 0).normalized;
        }
        else if (noteRand == 2)
        {
            direction = new Vector3(6.5f, -2.8f, 0).normalized;
        }
        else if (noteRand == 3)
        {
            direction = new Vector3(2.8f, -6.5f, 0).normalized;
        }
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }
}