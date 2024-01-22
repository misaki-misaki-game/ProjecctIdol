using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftNoteMove : MonoBehaviour
{
    int speed = 60;

    //�I�u�W�F�N�g�̈ړ�������ێ����邽�߂ɕK�v
    Vector3 direction;

    void Start()
    {
        int noteRand = Random.Range(0, 3);

        if (noteRand == 0)
        {
            //��
            direction = new Vector3(0, -1, 0);
        }
        else if (noteRand == 1)
        {
            //��
            direction = new Vector3(-1, 0, 0);
        }
        else if (noteRand == 2)
        {
            //����
            direction = new Vector3(-1, -1, 0).normalized;
        }
        else if (noteRand == 3)
        {
            //����
            direction = new Vector3(-1, 1, 0).normalized;
        }
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }
}