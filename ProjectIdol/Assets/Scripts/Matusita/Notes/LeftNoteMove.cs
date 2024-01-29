using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftNoteMove : MonoBehaviour
{
    int speed = 70;

    //�I�u�W�F�N�g�̈ړ�������ێ����邽�߂ɕK�v
    Vector3 direction;

    void Start()
    {
        int noteRand = Random.Range(0, 4);

        if (noteRand == 0)
        {
            //�k�k���̕����Ɉړ�����
            direction = new Vector3(-2.8f, -6.5f, 0).normalized;
        }
        else if (noteRand == 1)
        {
            //���k���̕����Ɉړ�����
            direction = new Vector3(-6.5f, -2.8f, 0).normalized;
        }
        else if (noteRand == 2)
        {
            //���쐼�̕����Ɉړ�����
            direction = new Vector3(-6.5f, 2.8f, 0).normalized;
        }
        else if (noteRand == 3)
        {
            //��쐼�̕����Ɉړ�����
            direction = new Vector3(-2.8f, 6.5f, 0).normalized;
        }
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }
}