using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class North_NorthWest : MonoBehaviour
{
    //�k�k���̕����ɐi��
    int speed = 70;
    Vector3 direction;

    void Start()
    {
        direction = new Vector3(-2.8f, 6.5f, 0).normalized;
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }
}
