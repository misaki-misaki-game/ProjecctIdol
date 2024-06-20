using UnityEngine;
using System;
using Random = UnityEngine.Random;
using System.Collections.Generic;

public partial class SignalScript : MonoBehaviour
{
    /// --------関数一覧-------- ///
    /// -------public関数------- ///

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
        sp.color = Color.white; // 画像色を標準色(白)にする
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
            blink.SetActive(true); // ブリンクを表示
            // ボムの個数制限を超えていたら
            if (buttonScript.specialSignals.Count > bombMax)
            {
                // もっとも古いボムを自動爆破する
                buttonScript.BombTimeOver(buttonScript.specialSignals[0]);
            }
        }
    }

    /// <summary>
    /// ブレイクシグナル関数
    /// </summary>
    /// <param name="isChain">チェインしているか</param>
    /// <param name="chain">チェイン数</param>
    public void BreakSignal(bool isChain, float chain = 0, bool isDetonation = false)
    {
        if (state == STATE.NOTHING) return; // stateがNOTHINGがリターンする
        if (state == STATE.SPECIAL) // stateがSPECIALなら
        {
            // X字ボムリストから自分を取り除く
            buttonScript.specialSignals.Remove(this.gameObject);
            blink.SetActive(false); // ブリンクを非表示
        }
        // setSignalPointがneedPointを超過していたら超過分を代入　それ以外は0にする
        if (setSignalPoint > needPoint) setSignalPoint -= needPoint;
        else setSignalPoint = default; // setSignalPointにdefaultを代入する
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
        sp.color = Color.clear; // 画像を透明にする
        PlayEffect(effectState); // エフェクトを呼び出す
    }

    /// <summary>
    /// SetSignalPointを加算する関数
    /// </summary>
    public void AddSetSignalPoint()
    {
        setSignalPoint += 1;
    }

    /// -------public関数------- ///
    /// -----protected関数------ ///



    /// -----protected関数------ ///
    /// ------private関数------- ///

    private void Start()
    {
        // SpriteRenderer格納
        sp = GetComponent<SpriteRenderer>();
        // ButtonScript格納
        buttonScript = GameObject.FindGameObjectWithTag("GameDirector").GetComponent<ButtonScript>();
        // 子オブジェクトの0番目を代入
        blink = transform.GetChild(0).gameObject;
        // シグナルをセットする
        SetSignal();
    }

    private void Update()
    {
        if (setSignalPoint >= needPoint && state == STATE.NOTHING) SetSignal(isBomb); // NOTHINGのシグナルが再セットまでのポイントを必要数満たしていたらシグナルをセットする
    }

    /// <summary>
    /// 仮引数と同じエフェクトステータスなら子オブジェクト(エフェクト)を破壊する関数
    /// </summary>
    /// <param name="effectCondition">エフェクトステータス</param>
    private void EffectDestroy(Effect effectCondition)
    {
        if (effectState == effectCondition) // エフェクトステータスが仮引数と同じなら
        {
            // 親オブジェクトのTransformコンポーネントを取得
            Transform parentTransform = transform;

            // 子オブジェクトを格納するリストを作成
            List<GameObject> childrenWithTag = new List<GameObject>();

            // 親オブジェクトの子オブジェクトを再帰的に探索
            foreach (Transform childTransform in parentTransform)
            {
                // 子オブジェクトが特定のタグを持っているか確認
                if (childTransform.CompareTag("Eternity"))
                {
                    // 子オブジェクトが特定のタグを持っている場合、リストに追加
                    childrenWithTag.Add(childTransform.gameObject);
                }
            }

            // リスト内のオブジェクトを処理する
            foreach (GameObject childObject in childrenWithTag)
            {
                // 子オブジェクトの処理を行う
                Destroy(childObject); // 子オブジェクトを破壊する
            }
            childrenWithTag.Clear(); // リストの中身を破棄する
        }
    }

    /// <summary>
    /// エフェクトを再生する関数
    /// </summary>
    /// <param name="effectState">エフェクトステータス</param>
    private void PlayEffect(Effect effectState)
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

    /// ------private関数------- ///
    /// --------関数一覧-------- ///
}
public partial class SignalScript
{
    /// --------変数一覧-------- ///
    /// -------public変数------- ///

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

    /// -------public変数------- ///
    /// -----protected変数------ ///



    /// -----protected変数------ ///
    /// ------private変数------- ///

    private bool isBomb = false; // そのシグナルがボムになるかどうかの真偽

    private int bombMax = 3; // X字ボムの個数上限
    private int setSignalPoint = 0; // 再セットするためのポイントを数える変数

    [SerializeField] private int needPoint = 3; // シグナル再セットに必要なポイント変数

    [SerializeField] private float destroyDeleteTime = 1.0f; // エフェクトを消すまでの時間変数
    [SerializeField] private const float bombRequirement = 3; // ボムをセットする条件変数

    [EnumIndex(typeof(Effect))]
    [SerializeField] private GameObject[] effects = new GameObject[4]; // エフェクト配列
    [SerializeField] private GameObject blink; // 点滅オブジェクト

    [EnumIndex(typeof(STATE))]
    [SerializeField] private Sprite[] signals = new Sprite[6]; // シグナル画像配列

    private Effect effectState; // エフェクト変数

    private SpriteRenderer sp; // 画像を切り替える

    private ButtonScript buttonScript; // ButtonScript変数

    /// ------private変数------- ///
    /// -------プロパティ------- ///



    /// -------プロパティ------- ///
    /// --------変数一覧-------- ///
}