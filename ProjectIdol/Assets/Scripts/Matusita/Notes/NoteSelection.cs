using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSelection: MonoBehaviour
{
    public GameObject l_prefabBlue;     //左側専用の青
    public GameObject l_prefabRed;      //左側専用の赤
    public GameObject l_prefabWhite;    //左側専用の白
    public GameObject l_prefabYellow;   //左側専用の黄
    public GameObject r_prefabBlue;     //右側専用の青
    public GameObject r_prefabRed;      //右側専用の赤
    public GameObject r_prefabWhite;    //右側専用の白
    public GameObject r_prefabYellow;   //右側専用の黄

    void Update()
    {
        //30フレームごとにシーンにプレハブ生成
        if (Time.frameCount % 240 == 0)
        {
            int randNumber = Random.Range(0, 15);

            if (randNumber == 0)
            {
                //生成位置
                Vector3 pos = new Vector3(0, -23, 0);
                //青と青を生成させる
                Instantiate(l_prefabBlue, pos, Quaternion.identity);
                Instantiate(r_prefabBlue, pos, Quaternion.identity);
            }
            if (randNumber == 1)
            {
                //生成位置
                Vector3 pos = new Vector3(0, -23, 0);
                //青と赤を生成させる
                Instantiate(l_prefabBlue, pos, Quaternion.identity);
                Instantiate(r_prefabRed, pos, Quaternion.identity);
            }
            if (randNumber == 2)
            {
                //生成位置
                Vector3 pos = new Vector3(0, -23, 0);
                //青と白を生成させる
                Instantiate(l_prefabBlue, pos, Quaternion.identity);
                Instantiate(r_prefabWhite, pos, Quaternion.identity);
            }
            if (randNumber == 3)
            {
                //生成位置
                Vector3 pos = new Vector3(0, -23, 0);
                //青と黄を生成させる
                Instantiate(l_prefabBlue, pos, Quaternion.identity);
                Instantiate(r_prefabYellow, pos, Quaternion.identity);
            }
            if (randNumber == 4)
            {
                //生成位置
                Vector3 pos = new Vector3(0, -23, 0);
                //赤と青を生成させる
                Instantiate(l_prefabRed, pos, Quaternion.identity);
                Instantiate(r_prefabBlue, pos, Quaternion.identity);
            }
            if (randNumber == 5)
            {
                //生成位置
                Vector3 pos = new Vector3(0, -23, 0);
                //赤と赤を生成させる
                Instantiate(l_prefabRed, pos, Quaternion.identity);
                Instantiate(r_prefabRed, pos, Quaternion.identity);
            }
            if (randNumber == 6)
            {
                //生成位置
                Vector3 pos = new Vector3(0, -23, 0);
                //赤と白を生成させる
                Instantiate(l_prefabRed, pos, Quaternion.identity);
                Instantiate(r_prefabWhite, pos, Quaternion.identity);
            }
            if (randNumber == 7)
            {
                //生成位置
                Vector3 pos = new Vector3(0, -23, 0);
                //赤と黄を生成させる
                Instantiate(l_prefabRed, pos, Quaternion.identity);
                Instantiate(r_prefabYellow, pos, Quaternion.identity);
            }
            if (randNumber == 8)
            {
                //生成位置
                Vector3 pos = new Vector3(0, -23, 0);
                //白と青を生成させる
                Instantiate(l_prefabWhite, pos, Quaternion.identity);
                Instantiate(r_prefabBlue, pos, Quaternion.identity);
            }
            if (randNumber == 9)
            {
                //生成位置
                Vector3 pos = new Vector3(0, -23, 0);
                //白と赤を生成させる
                Instantiate(l_prefabWhite, pos, Quaternion.identity);
                Instantiate(r_prefabRed, pos, Quaternion.identity);
            }
            if (randNumber == 10)
            {
                //生成位置
                Vector3 pos = new Vector3(0, -23, 0);
                //白と白を生成させる
                Instantiate(l_prefabWhite, pos, Quaternion.identity);
                Instantiate(r_prefabWhite, pos, Quaternion.identity);
            }
            if (randNumber == 11)
            {
                //生成位置
                Vector3 pos = new Vector3(0, -23, 0);
                //白と黄を生成させる
                Instantiate(l_prefabWhite, pos, Quaternion.identity);
                Instantiate(r_prefabYellow, pos, Quaternion.identity);
            }
            if (randNumber == 12)
            {
                //生成位置
                Vector3 pos = new Vector3(0, -23, 0);
                //黄と青を生成させる
                Instantiate(l_prefabYellow, pos, Quaternion.identity);
                Instantiate(r_prefabBlue, pos, Quaternion.identity);
            }
            if (randNumber == 13)
            {
                //生成位置
                Vector3 pos = new Vector3(0, -23, 0);
                //黄と赤を生成させる
                Instantiate(l_prefabYellow, pos, Quaternion.identity);
                Instantiate(r_prefabRed, pos, Quaternion.identity);
            }
            if (randNumber == 14)
            {
                //生成位置
                Vector3 pos = new Vector3(0, -23, 0);
                //黄と白を生成させる
                Instantiate(l_prefabYellow, pos, Quaternion.identity);
                Instantiate(r_prefabWhite, pos, Quaternion.identity);
            }
            if (randNumber == 15)
            {
                //生成位置
                Vector3 pos = new Vector3(0, -23, 0);
                //黄と黄を生成させる
                Instantiate(l_prefabYellow, pos, Quaternion.identity);
                Instantiate(r_prefabYellow, pos, Quaternion.identity);
            }

        }
    }
}
