using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteInstance : MonoBehaviour
{
    [SerializeField] GameObject l_prefabBlue;     //左側専用の青
    [SerializeField] GameObject l_prefabRed;      //左側専用の赤
    [SerializeField] GameObject l_prefabWhite;    //左側専用の白
    [SerializeField] GameObject l_prefabYellow;   //左側専用の黄
    [SerializeField] GameObject r_prefabBlue;     //右側専用の青
    [SerializeField] GameObject r_prefabRed;      //右側専用の赤
    [SerializeField] GameObject r_prefabWhite;    //右側専用の白
    [SerializeField] GameObject r_prefabYellow;   //右側専用の黄


    public void NoteEventCC()
    {
        //生成位置
        Vector3 pos = new Vector3(0, -23, 0);
        Instantiate(l_prefabWhite, pos, Quaternion.identity);
    }
    public void NoteEventC()
    {
        //生成位置
        Vector3 pos = new Vector3(0, -23, 0);
        Instantiate(r_prefabRed, pos, Quaternion.identity);
    }
    public void NoteEventDD()
    {
        //生成位置
        Vector3 pos = new Vector3(0, -23, 0);
        Instantiate(l_prefabRed, pos, Quaternion.identity);

    }
    public void NoteEventD()
    {
        //生成位置
        Vector3 pos = new Vector3(0, -23, 0);
        Instantiate(l_prefabWhite, pos, Quaternion.identity);
    }
    public void NoteEventE()
    {
        //生成位置
        Vector3 pos = new Vector3(0, -23, 0);
        Instantiate(l_prefabBlue, pos, Quaternion.identity);
    }
    public void NoteEventFF()
    {
        //生成位置
        Vector3 pos = new Vector3(0, -23, 0);
        Instantiate(l_prefabYellow, pos, Quaternion.identity);
    }
    public void NoteEventF()
    {
        //生成位置
        Vector3 pos = new Vector3(0, -23, 0);
        Instantiate(r_prefabBlue, pos, Quaternion.identity);
    }
    public void NoteEventGG()
    {
        //生成位置
        Vector3 pos = new Vector3(0, -23, 0);
        Instantiate(r_prefabWhite, pos, Quaternion.identity);
    }
    public void NoteEventG()
    {
        //生成位置
        Vector3 pos = new Vector3(0, -23, 0);
        Instantiate(r_prefabRed, pos, Quaternion.identity);
    }
    public void NoteEventAA()
    {
        //生成位置
        Vector3 pos = new Vector3(0, -23, 0);
        Instantiate(l_prefabYellow, pos, Quaternion.identity);
    }
    public void NoteEventA()
    {
        //生成位置
        Vector3 pos = new Vector3(0, -23, 0);
        Instantiate(r_prefabYellow, pos, Quaternion.identity);
    }
    public void NoteEventB()
    {
        //生成位置
        Vector3 pos = new Vector3(0, -23, 0);
        Instantiate(r_prefabBlue, pos, Quaternion.identity);
    }
}

