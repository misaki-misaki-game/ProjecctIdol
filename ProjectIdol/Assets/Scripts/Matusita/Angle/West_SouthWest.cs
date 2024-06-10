                                            using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class West_SouthWest : MonoBehaviour
{
    [SerializeField] int speed = 70;    //シグナルのスピードを設定する
    Vector3 direction;                  //移動させたい方向を入れ込むための変数

    void Start()
    {
    //西南西の方向に移動する
        direction = new Vector3(-6.5f, -2.8f, 0).normalized;
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

}
