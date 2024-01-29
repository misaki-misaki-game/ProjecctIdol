using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftNoteMove : MonoBehaviour
{
    int speed = 70;

    //オブジェクトの移動方向を保持するために必要
    Vector3 direction;

    void Start()
    {
        int noteRand = Random.Range(0, 4);

        if (noteRand == 0)
        {
            //北北西の方向に移動する
            direction = new Vector3(-2.8f, -6.5f, 0).normalized;
        }
        else if (noteRand == 1)
        {
            //西北西の方向に移動する
            direction = new Vector3(-6.5f, -2.8f, 0).normalized;
        }
        else if (noteRand == 2)
        {
            //西南西の方向に移動する
            direction = new Vector3(-6.5f, 2.8f, 0).normalized;
        }
        else if (noteRand == 3)
        {
            //南南西の方向に移動する
            direction = new Vector3(-2.8f, 6.5f, 0).normalized;
        }
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }
}