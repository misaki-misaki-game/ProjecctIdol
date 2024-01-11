using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class SignalScript : MonoBehaviour
{
    
    SpriteRenderer sp; // 色をさせるための変数

    public enum STATE // ステータス
    {
        NOTHING, // シグナル無し 0
        BLUE, // 青シグナル 1
        RED, // 赤シグナル 2
        WHITE, // 白シグナル 3
        YELLOW // 黄シグナル 4
    }
    public STATE state; // state変数
    public int setSignalPoint = 0; // 再セットするためのポイントを数える変数
    [SerializeField] int needPoint = 3; // シグナル再セットに必要なポイント変数


    // public float setSignalDelayTime = 20; // シグナル再召喚までのフレーム変数 仕様変更のためコメント化

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
        if(setSignalPoint == needPoint && state == STATE.NOTHING) SetSignal(); // NOTHINGのシグナルが再セットまでのポイントを必要数満たしていたらシグナルをセットする
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
    public void BreakSignal(bool isChain) // ブレイクシグナル関数
    {
        setSignalPoint = default; // setSignalPointにdefaultPointを代入する
        if (isChain)
        {
            setSignalPoint -= 1; // チェインしている場合はシグナルをクリックした時点でAddSetSignalPointが呼び出されてしまうので、
                                  // setSignalPointを-1にすることで調整
        }
        state = STATE.NOTHING; // stateをNOTHINGにする
        sp.color = new Color32(55, 52, 52, 0); // シグナルの色を透明にする(グレー)
        // StartCoroutine(DelayCoroutine()); // ディレイコルーチンを呼び出し 仕様変更のためコメント化
    }

    public void AddSetSignalPoint() // SetSignalPointを加算する関数
    {
        setSignalPoint += 1;
    }

    /*
    private IEnumerator DelayCoroutine() // ディレイコルーチン　仕様変更のためコメント化
    {
        // delayTimeの値F(初期値20F)分待つ
        for (var i = 0; i < setSignalDelayTime; i++)
        {
            yield return null;
        }
        // delayTimeの値Fに色を変更
        SetSignal();
    }
    */
}
