using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class SignalScript : MonoBehaviour
{
    // 色をさせるための変数
    SpriteRenderer sp;
    public enum STATE // ステータス
    {
        NOTHING, // シグナル無し 0
        BLUE, // 青シグナル 1
        RED, // 赤シグナル 2
        WHITE, // 白シグナル 3
        YELLOW // 黄シグナル 4
    }
    public float setSignalDelayTime = 20; // シグナル再召喚までのフレーム変数
    public STATE state; // state変数

    // Start is called before the first frame update
    void Start()
    {
        // SpriteRenderer格納
        sp= GetComponent<SpriteRenderer>();
        // シグナルをセットする
        SetSignal();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetSignal() // セットシグナル関数
    {
        int rnd = Random.Range(1, 5); // 1～4の範囲でランダム
        state = (STATE)Enum.ToObject(typeof(STATE), rnd); // stateをランダムで設定
        switch(state)
        {
            // stateによって色を変更
            case STATE.RED:
                sp.color = Color.red;
                break;
            case STATE.BLUE:
                sp.color = Color.blue;
                break;
            case STATE.YELLOW:
                sp.color = Color.yellow;
                break;
            case STATE.WHITE:
                sp.color = Color.white;
                break;
        }
    }
    public void BreakSignal() // ブレイクシグナル関数
    {
        state= STATE.NOTHING; // stateをNOTHINGにする
        sp.color = new Color32(55, 52, 52, 255); // シグナルの色を初期色にする(グレー)
        StartCoroutine(DelayCoroutine()); // ディレイコルーチンを呼び出し
    }
    private IEnumerator DelayCoroutine() // ディレイコルーチン
    {
        // delayTimeの値F(初期値20F)分待つ
        for (var i = 0; i < setSignalDelayTime; i++)
        {
            yield return null;
        }
        // delayTimeの値Fに色を変更
        SetSignal();
    }
}
