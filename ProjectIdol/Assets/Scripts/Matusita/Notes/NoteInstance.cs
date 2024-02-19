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
        //音階のC#がなった時にシグナルを生成させる
        Vector3 pos = new Vector3(0, -23, 0);                   //円の中心に生成するように設定している
        Instantiate(l_prefabWhite, pos, Quaternion.identity);   //白色のシグナルを西南西の方向に移動させる
    }
    public void NoteEventC()
    {
        //音階のCがなった時にシグナルを生成させる
        Vector3 pos = new Vector3(0, -23, 0);                   //円の中心に生成するように設定している
        Instantiate(r_prefabRed, pos, Quaternion.identity);     //赤色のシグナルを南南東の方向に移動させる
    }
    public void NoteEventDD()
    {
        //音階のD#がなった時にシグナルを生成させる
        Vector3 pos = new Vector3(0, -23, 0);                   //円の中心に生成するように設定している
        Instantiate(l_prefabRed, pos, Quaternion.identity);     //赤色のシグナルを西北西の方向に移動させる
    }
    public void NoteEventD()
    {
        //音階のDがなった時にシグナルを生成させる
        Vector3 pos = new Vector3(0, -23, 0);                   //円の中心に生成するように設定している
        Instantiate(l_prefabWhite, pos, Quaternion.identity);   //白色のシグナルを西南西の方向に移動させる
    }
    public void NoteEventE()
    {
        //音階のEがなった時にシグナルを生成させる
        Vector3 pos = new Vector3(0, -23, 0);                   //円の中心に生成するように設定している
        Instantiate(l_prefabBlue, pos, Quaternion.identity);    //青色のシグナルを北北西の方向に移動させる
    }
    public void NoteEventFF()
    {
        //音階のF#がなった時にシグナルを生成させる
        Vector3 pos = new Vector3(0, -23, 0);                   //円の中心に生成するように設定している
        Instantiate(l_prefabYellow, pos, Quaternion.identity);  //黄色のシグナルを西南西の方向に移動させる
    }
    public void NoteEventF()
    {
        //音階のFがなった時にシグナルを生成させる
        Vector3 pos = new Vector3(0, -23, 0);                   //円の中心に生成するように設定している
        Instantiate(r_prefabBlue, pos, Quaternion.identity);    //青色のシグナルを東南東の方向に移動させる
    }
    public void NoteEventGG()
    {
        //音階のG#がなった時にシグナルを生成させる
        Vector3 pos = new Vector3(0, -23, 0);                   //円の中心に生成するように設定している
        Instantiate(r_prefabWhite, pos, Quaternion.identity);   //白色のシグナルを東北東の方向に移動させる
    }
    public void NoteEventG()
    {
        //音階のGがなった時にシグナルを生成させる
        Vector3 pos = new Vector3(0, -23, 0);                   //円の中心に生成するように設定している
        Instantiate(r_prefabRed, pos, Quaternion.identity);     //赤色のシグナルを南南東の方向に移動させる
    }
    public void NoteEventAA()
    {
        //音階のA#がなった時にシグナルを生成させる
        Vector3 pos = new Vector3(0, -23, 0);                   //円の中心に生成するように設定している
        Instantiate(l_prefabYellow, pos, Quaternion.identity);  //黄色色のシグナルを南南西の方向に移動させる
    }
    public void NoteEventA()
    {
        //音階のAがなった時にシグナルを生成させる
        Vector3 pos = new Vector3(0, -23, 0);                   //円の中心に生成するように設定している
        Instantiate(r_prefabYellow, pos, Quaternion.identity);  //黄色のシグナルを北北東の方向に移動させる
    }
    public void NoteEventB()
    {
        //音階のBがなった時にシグナルを生成させる
        Vector3 pos = new Vector3(0, -23, 0);                   //円の中心に生成するように設定している
        Instantiate(r_prefabBlue, pos, Quaternion.identity);    //青色のシグナルを東南東の方向に移動させる
    }
}

