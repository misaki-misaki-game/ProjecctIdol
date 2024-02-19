using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;
using static ScoreDirector;
using TMPro;

public class SignalScript : MonoBehaviour
{
    bool isBomb = false; // そのシグナルがボムになるかどうかの真偽
    int bombMax = 3; // X字ボムの個数上限
    SpriteRenderer sp; // 画像を切り替える
    ButtonScript buttonScript; // ButtonScript変数

    public enum STATE // ステータス
    {
        NOTHING, // シグナル無し 0
        BLUE, // 青シグナル 1
        RED, // 赤シグナル 2
        WHITE, // 白シグナル 3
        YELLOW, // 黄シグナル 4
        SPECIAL // X字ボム 5
    }
    public STATE state; // state変数
    public enum Effect // エフェクトステータス
    {
        RESURRECTIONEFFECT, // シグナルが復活するとき 0
        NOTHINGEFFECT, // シグナル無しのとき 1
        BOMBSETEFFECT, // シグナルが無し兼ボムをセットするとき 2
        BREAKEFFECT // シグナルが押されたとき 3
    }
    public Effect effectState; // エフェクト変数
    public int setSignalPoint = 0; // 再セットするためのポイントを数える変数
    public float destroyDeleteTime = 1.0f; // エフェクトを消すまでの時間変数
    public const float bombRequirement = 4; // ボムをセットする条件変数
    [SerializeField] int needPoint = 3; // シグナル再セットに必要なポイント変数
    [EnumIndex(typeof(Effect))]
    public GameObject[] effects = new GameObject[4]; // エフェクト配列
    [EnumIndex(typeof(STATE))]
    public Sprite[] signals = new Sprite[6]; // シグナル画像配列

    // Start is called before the first frame update
    void Start()
    {
        // SpriteRenderer格納
        sp= GetComponent<SpriteRenderer>();
        // ButtonScript格納
        buttonScript = GameObject.FindGameObjectWithTag("GameDirector").GetComponent<ButtonScript>();
        // シグナルをセットする
        SetSignal();
    }

    // Update is called once per frame
    void Update()
    {
        if(setSignalPoint == needPoint && state == STATE.NOTHING) SetSignal(isBomb); // NOTHINGのシグナルが再セットまでのポイントを必要数満たしていたらシグナルをセットする
    }

    /// <summary>
    /// セットシグナル関数
    /// </summary>
    /// <param name="isBomb">ボムを生成するかどうか</param>
    public void SetSignal(bool isBomb = false)
    {
        EffectDestroy(Effect.NOTHINGEFFECT); // NOTHINGEFFECT時のエフェクトを消す
        EffectDestroy(Effect.BOMBSETEFFECT); // BOMBSETEFFECT時のエフェクトを消す
        effectState = Effect.RESURRECTIONEFFECT; // エフェクトステータスを復活するときに変更
        PlayEffect(effectState); // エフェクトを呼び出す
        if (!isBomb) // ボムが生成されない場合
        {
            int rnd = Random.Range(1, 5); // 1～4の範囲でランダム
            state = (STATE)Enum.ToObject(typeof(STATE), rnd); // stateをランダムで設定
            switch (state)
            {
                // stateによってイメージを変更
                case STATE.RED:
                    sp.sprite = signals[2]; // 赤シグナルを設定
                    break;
                case STATE.BLUE:
                    sp.sprite = signals[1]; // 青シグナルを設定
                    break;
                case STATE.YELLOW:
                    sp.sprite = signals[4]; // 黄シグナルを設定
                    break;
                case STATE.WHITE:
                    sp.sprite = signals[3]; // 白シグナルを設定
                    break;
            }
        }
        else // ボムが生成される場合
        {
            state = STATE.SPECIAL; // STATEをX字ボムに設定
            buttonScript.specialSignals.Add(this.gameObject); // ボムを格納する
            sp.sprite = signals[5]; // X字ボムを設定
            // ボムの個数制限を超えていたら
            if (buttonScript.specialSignals.Count > bombMax ) 
            {
                // もっとも古いボムを自動爆破する
                buttonScript.BombTimeOver(buttonScript.specialSignals[0]);
            }
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
    public void BreakSignal(bool isChain, float chain = 0) // ブレイクシグナル関数
    {
        if (state == STATE.NOTHING) return; // stateがNOTHINGがリターンする
        if (state == STATE.SPECIAL) // stateがSPECIALなら
        {
            // X字ボムリストから自分を取り除く
            buttonScript.specialSignals.Remove(this.gameObject);
        }
        setSignalPoint = default; // setSignalPointにdefaultPointを代入する
        if (isChain)
        {
            setSignalPoint -= 1; // チェインしている場合はシグナルをクリックした時点でAddSetSignalPointが呼び出されてしまうので、
                                 // setSignalPointを-1にすることで調整
        }
        effectState = Effect.BREAKEFFECT; // エフェクトステータスを押されたときに変更
        PlayEffect(effectState); // エフェクトを呼び出す
        if (chain >= bombRequirement && state != STATE.SPECIAL) // チェイン数がボムをセットする条件より多い場合 かつ stateがSPECIAL以外の場合
        {
            effectState = Effect.BOMBSETEFFECT; // エフェクトステータスをシグナルが無し兼ボムをセットするときに変更
            isBomb = true; // ボムにするためにtrueにする
        }
        else // それ以外
        {
            effectState = Effect.NOTHINGEFFECT; // エフェクトステータスをなにもないときに変更
            isBomb = false; // ボムにしないためにtrueにする
        }
        state = STATE.NOTHING; // stateをNOTHINGにする
        sp.sprite = null; // シグナル画像をnullにする
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
            case Effect.BOMBSETEFFECT: // シグナルが無し兼ボムをセットするときのエフェクトを呼び出す(復活するまでエフェクトを発生させ続ける)
                clone = Instantiate(effects[2], // なにを召喚するか
                                               this.transform.position, // 画面のどこに召喚するか
                                               Quaternion.identity); // ローテーションはどうするのか
                clone.transform.parent = this.transform; // シグナルの子にする
                break;
            case Effect.BREAKEFFECT: // シグナルが押されたときのエフェクトを呼び出す
                clone = Instantiate(effects[3], // なにを召喚するか
                                               this.transform.position, // 画面のどこに召喚するか
                                               Quaternion.identity); // ローテーションはどうするのか
                clone.transform.parent = this.transform; // シグナルの子にする
                Destroy(clone, destroyDeleteTime); // destroyDeleteTime秒後にエフェクトを破壊する
                break;
        }
    }
}
