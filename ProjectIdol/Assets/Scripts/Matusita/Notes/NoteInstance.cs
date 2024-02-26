using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class NoteInstance : MonoBehaviour
{
    [SerializeField] GameObject l_prefabBlue;     //左側専用の青  西南西(Sキー)の方向に移動する
    [SerializeField] GameObject l_prefabRed;      //左側専用の赤  北北西(Fキー)の方向に移動する
    [SerializeField] GameObject l_prefabWhite;    //左側専用の白  南南西(Aキー)の方向に移動する
    [SerializeField] GameObject l_prefabYellow;   //左側専用の黄  西北西(Dキー)の方向に移動する
    [SerializeField] GameObject r_prefabBlue;     //右側専用の青  北北東(Jキー)の方向に移動する
    [SerializeField] GameObject r_prefabRed;      //右側専用の赤  東南東(Lキー)の方向に移動する
    [SerializeField] GameObject r_prefabWhite;    //右側専用の白  東北東(Kキー)の方向に移動する
    [SerializeField] GameObject r_prefabYellow;   //右側専用の黄  南南東(;キー)の方向に移動する

    public void NoteEventCC()
    {
        //音階のC#がなった時にシグナルを生成させる
        Vector3 pos = new Vector3(0, -23, 0);                       //円の中心に生成するように設定している
        Instantiate(l_prefabWhite, pos, Quaternion.identity);       //白色のシグナルを南南西(Aキー)の方向に移動させる
    }
    public void NoteEventC()
    {
        //音階のCがなった時にシグナルを生成させる
        Vector3 pos = new Vector3(0, -23, 0);                       //円の中心に生成するように設定している
        Instantiate(r_prefabYellow, pos, Quaternion.identity);      //黄色のシグナルを南南東(;キー)の方向に移動させる
    }
    public void NoteEventDD()
    {
        //音階のD#がなった時にシグナルを生成させる
        Vector3 pos = new Vector3(0, -23, 0);                       //円の中心に生成するように設定している
        Instantiate(r_prefabYellow, pos, Quaternion.identity);      //白色のシグナルを東北東(Kキー)の方向に移動させる
    }
    public void NoteEventD()
    {
        //音階のDがなった時にシグナルを生成させる
        Vector3 pos = new Vector3(0, -23, 0);                       //円の中心に生成するように設定している
        Instantiate(r_prefabRed, pos, Quaternion.identity);         //赤色のシグナルを東南東(Lキー)の方向に移動させる
    }
    public void NoteEventE()
    {
        //音階のEがなった時にシグナルを生成させる
        Vector3 pos = new Vector3(0, -23, 0);                       //円の中心に生成するように設定している
        Instantiate(l_prefabRed, pos, Quaternion.identity);         //赤色のシグナルを北北西(Fキー)の方向に移動させる
    }
    public void NoteEventFF()
    {
        //音階のF#がなった時にシグナルを生成させる
        Vector3 pos = new Vector3(0, -23, 0);                       //円の中心に生成するように設定している
        Instantiate(l_prefabBlue, pos, Quaternion.identity);        //青色のシグナルを西南西(Sキー)の方向に移動させる
    }
    public void NoteEventF()
    {
        //音階のFがなった時にシグナルを生成させる
        Vector3 pos = new Vector3(0, -23, 0);                       //円の中心に生成するように設定している
        Instantiate(l_prefabWhite, pos, Quaternion.identity);       //白色のシグナルを南南西(Aキー)の方向に移動させる
    }
    public void NoteEventGG()
    {
        //音階のG#がなった時にシグナルを生成させる
        Vector3 pos = new Vector3(0, -23, 0);                       //円の中心に生成するように設定している
        Instantiate(l_prefabYellow, pos, Quaternion.identity);      //黄色のシグナルを西北西(Dキー)の方向に移動させる
    }
    public void NoteEventG()
    {
        //音階のGがなった時にシグナルを生成させる
        Vector3 pos = new Vector3(0, -23, 0);                       //円の中心に生成するように設定している
        Instantiate(r_prefabRed, pos, Quaternion.identity);         //赤色のシグナルを東南東(Lキー)の方向に移動させる
    }
    public void NoteEventAA()
    {
        //音階のA#がなった時にシグナルを生成させる
        Vector3 pos = new Vector3(0, -23, 0);                       //円の中心に生成するように設定している
        Instantiate(l_prefabYellow, pos, Quaternion.identity);      //黄色色のシグナルを南南西の方向に移動させる
    }
    public void NoteEventA()
    {
        //音階のAがなった時にシグナルを生成させる
        Vector3 pos = new Vector3(0, -23, 0);                       //円の中心に生成するように設定している
        Instantiate(r_prefabBlue, pos, Quaternion.identity);        //青色のシグナルを北北東(Jキー)の方向に移動させる
    }
    public void NoteEventB()
    {
        //音階のBがなった時にシグナルを生成させる
        Vector3 pos = new Vector3(0, -23, 0);                       //円の中心に生成するように設定している
        Instantiate(r_prefabYellow, pos, Quaternion.identity);      //黄色のシグナルを東南東(;キー)の方向に移動させる
    }
}

