using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;
using static ScoreDirector;
using TMPro;

public class SignalScript : MonoBehaviour
{
    SpriteRenderer sp; // 画像を切り替える

    public enum STATE // ステータス
    {
        NOTHING, // シグナル無し 0
        BLUE, // 青シグナル 1
        RED, // 赤シグナル 2
        WHITE, // 白シグナル 3
        YELLOW // 黄シグナル 4
    }
    public STATE state; // state変数
    public enum Effect // エフェクトステータス
    {
        RESURRECTIONEFFECT, // シグナルが復活するとき 0
        NOTHINGEFFECT, // シグナル無しのとき 1
        BREAKEFFECT // シグナルが押されたとき 2
    }
    public Effect effectState; // エフェクト変数
    public int setSignalPoint = 0; // 再セットするためのポイントを数える変数
    public float destroyDeleteTime = 1.0f; // エフェクトを消すまでの時間変数
    [SerializeField] int needPoint = 3; // シグナル再セットに必要なポイント変数
    [EnumIndex(typeof(Effect))]
    public GameObject[] effects = new GameObject[3]; // エフェクト配列
    [EnumIndex(typeof(STATE))]
    public Sprite[] signals = new Sprite[5]; // シグナル画像配列


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
        EffectDestroy(Effect.NOTHINGEFFECT); // NOTHINGEFFECT時のエフェクトを消す
        effectState = Effect.RESURRECTIONEFFECT; // エフェクトステータスを復活するときに変更
        PlayEffect(effectState); // エフェクトを呼び出す
        int rnd = Random.Range(1, 5); // 1～4の範囲でランダム
        state = (STATE)Enum.ToObject(typeof(STATE), rnd); // stateをランダムで設定
        switch (state)
        {
            // stateによってイメージを変更
            case STATE.RED:
                sp.sprite = signals[2]; // 赤シグナルを設定
                //sp.color = Color.red;
                break;
            case STATE.BLUE:
                sp.sprite = signals[1]; // 青シグナルを設定
                //sp.color = Color.blue;
                break;
            case STATE.YELLOW:
                sp.sprite = signals[4]; // 黄シグナルを設定
                //sp.color = Color.yellow;
                break;
            case STATE.WHITE:
                sp.sprite = signals[3]; // 白シグナルを設定
                //sp.color = Color.white;
                break;
        }
    }
    private void EffectDestroy(Effect effectCondition) // 仮引数と同じエフェクトステータスなら子オブジェクト(エフェクト)を破壊する関数
    {
        if (effectState == effectCondition) // エフェクトステータスが仮引数と同じなら
        {
            GameObject child = transform.GetChild(2).gameObject; // 子オブジェクト(エフェクト)を検索
            Destroy(child); // 子オブジェクトを破壊する
        }
    }
    public void BreakSignal(bool isChain) // ブレイクシグナル関数
    {
        effectState = Effect.BREAKEFFECT; // エフェクトステータスを押されたときに変更
        PlayEffect(effectState); // エフェクトを呼び出す
        setSignalPoint = default; // setSignalPointにdefaultPointを代入する
        if (isChain)
        {
            setSignalPoint -= 1; // チェインしている場合はシグナルをクリックした時点でAddSetSignalPointが呼び出されてしまうので、
                                 // setSignalPointを-1にすることで調整
        }
        state = STATE.NOTHING; // stateをNOTHINGにする
        sp.sprite = null; // シグナル画像をnullにする
        effectState = Effect.NOTHINGEFFECT; // エフェクトステータスをなにもないときに変更
        PlayEffect(effectState); // エフェクトを呼び出す
    }
    public void AddSetSignalPoint() // SetSignalPointを加算する関数
    {
        setSignalPoint += 1;
    }
    private void PlayEffect(Effect effectState) // エフェクトを再生する関数
    {
        GameObject clone = null; // GameObjectを生成
        switch (effectState)
        {
            case Effect.RESURRECTIONEFFECT: // シグナルが復活したときのエフェクトを呼び出す
                clone = Instantiate(effects[0], // なにを召喚するか
                                               this.transform.position, // 画面のどこに召喚するか
                                               Quaternion.identity); // ローテーションはどうするのか
                clone.transform.parent = this.transform; // シグナルの子にする
                Destroy(clone, destroyDeleteTime); // destroyDeleteTime秒後にエフェクトを破壊する
                break;
            case Effect.NOTHINGEFFECT: // シグナルがないときのエフェクトを呼び出す(復活するまでエフェクトを発生させ続ける)
                clone = Instantiate(effects[1], // なにを召喚するか
                                               this.transform.position, // 画面のどこに召喚するか
                                               Quaternion.identity); // ローテーションはどうするのか
                clone.transform.parent = this.transform; // シグナルの子にする
                break;
            case Effect.BREAKEFFECT: // シグナルが押されたときのエフェクトを呼び出す
                clone = Instantiate(effects[2], // なにを召喚するか
                                               this.transform.position, // 画面のどこに召喚するか
                                               Quaternion.identity); // ローテーションはどうするのか
                clone.transform.parent = this.transform; // シグナルの子にする
                Destroy(clone, destroyDeleteTime); // destroyDeleteTime秒後にエフェクトを破壊する
                break;
        }
    }
}
