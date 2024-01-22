using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftNoteMove : MonoBehaviour
{
    int speed = 60;

    //オブジェクトの移動方向を保持するために必要
    Vector3 direction;

    void Start()
    {
        int noteRand = Random.Range(0, 3);

        if (noteRand == 0)
        {
            //下
            direction = new Vector3(0, -1, 0);
        }
        else if (noteRand == 1)
        {
            //左
            direction = new Vector3(-1, 0, 0);
        }
        else if (noteRand == 2)
        {
            //左下
            direction = new Vector3(-1, -1, 0).normalized;
        }
        else if (noteRand == 3)
        {
            //左上
            direction = new Vector3(-1, 1, 0).normalized;
        }
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }
}