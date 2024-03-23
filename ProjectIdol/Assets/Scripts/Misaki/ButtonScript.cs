using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonScript : MonoBehaviour
{
    int centerSignalTP = 0; // 10の位 列(通常シグナル時)
    int centerSignalDP = 0; // 1の位 行(通常シグナル時)
    int detonationSignalTP = 0; // 10の位 列(誘爆時)
    int detonationSignalDP = 0; // 1の位 行(誘爆時)
    GameObject scoreTextObject; // テキストのオブジェクト変数
    SignalScript.STATE state; //SignalScriptのSTATE変数
    SignalScript centerSignalSS; // クリックしたオブジェクトのSignalScript格納用
    SignalScript comparisonSignalSS; // クリックしていないオブジェクトのSignalScript格納用
    public bool isChain = false; // チェインしているかどうか
    public bool gameStart = false; // ゲームがスタートしているかどうか
    public float chain = 0; // チェイン変数
    public int resetStock = 3; // リセット回数
    public AudioSource SEAudioSource; // SE用オーディオソース
    public GameObject clickedGameObject; // クリックしたオブジェクトを格納する変数
    public GameObject[] signals; // シグナル格納用
    public List<GameObject> specialSignals; // X字ボム格納用
    public List<GameObject> detonationObjects; // 誘爆したシグナル格納用
    public ScoreDirector ScoreDirector; // ScoreDirector変数
    public StarDirector StarDirector; // StarDirector変数
    public TextMeshPro getScoreText; // 取得したスコアを表示するテキスト変数

    // Update is called once per frame
    void Update()
    {
        SignalClick(); // シグナルをクリックしたときの処理
    }

    private void SignalClick() // シグナルをクリックしたときの関数
    {
        if (Input.GetMouseButtonDown(0) && gameStart) // 左クリックしたらかつゲームがスタートしていれば
        {
            // シグナルをクリックできたかの処理
            RaycastHit2D hitSprite = CheckHit();
            if (hitSprite == true) // レイが当たったら
            {
                InitializationChain(); // チェイン系の変数を初期化
                clickedGameObject = hitSprite.transform.gameObject; // clickedGameObjectにレイが当たったオブジェクトを格納
                if (CheckSignalState(clickedGameObject)) return; // シグナルの色をチェックしてNOTHINGならリターンする
                if (clickedGameObject.GetComponent<SignalScript>().state == SignalScript.STATE.SPECIAL) // X字ボムの場合
                {
                    CheckDetonation(clickedGameObject); // X字ボムの誘爆範囲を確認する
                    DetonationBreak(); // 誘爆したシグナルを破壊する
                }
                else // それ以外の場合
                {
                    if (!CheckChainSignal(clickedGameObject)) return; // チェイン確認関数を呼び出し チェインしていなければリターンする
                }
                SEAudioSource.Play(); // SEを鳴らす
                clickedGameObject.GetComponent<SignalScript>().BreakSignal(isChain,chain); // ブレイク関数を呼び出し
                ResurrectionSignal(); // stateがNOTHINGのシグナル全てにsetSignalPointを1加算する
                ScoreDirector.GetScore(chain, isChain, state); // ゲットスコア関数を呼び出し
                ShowGetScore(clickedGameObject); // ゲットしたスコアを表示する
                StarDirector.GetStar(chain); // ゲットスター関数を呼び出し
                Debug.Log(state + " 色シグナルをクリック");
            }
        }
    }
    private RaycastHit2D CheckHit() // ヒットしているかのチェック関数
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // rayにクリックしたポジションを格納
        RaycastHit2D hitSprite = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction); // hitSpriteにレイが当たったオブジェクトを格納
        return hitSprite; //  hitSpriteをリターンする
    }
    private void InitializationChain() // チェイン系の変数を初期化する関数
    {
        chain = default; // 初期化
        isChain = default; // 初期化
    }
    private bool CheckSignalState(GameObject gameObject) // シグナルの色チェック
    {
        // STATEがNOTHINGならtrueを返す
        return gameObject.GetComponent<SignalScript>().state == SignalScript.STATE.NOTHING;
    }
    /// <summary>
    /// クリックされたオブジェクトの斜め4方向にある全てのシグナル確認をする関数
    /// </summary>
    /// <param name="gameObject">クリックしたオブジェクト</param>
    /// <returns></returns>
    private void CheckDetonation(GameObject gameObject)
    {
        centerSignalTP = default; // 10の位 列
        centerSignalDP = default; // 1の位 行
        centerSignalSS = null; // クリックしたオブジェクトのSignalScript格納用
        comparisonSignalSS = null; // クリックしていないオブジェクトのSignalScript格納用

        // クリックしたオブジェクトが配列内オブジェクトのどれなのかを探す
        SearchSignal(gameObject, ref centerSignalTP, ref centerSignalDP);
        // クリックしたオブジェクトのSignalScriptを格納
        centerSignalSS = signals[centerSignalTP * 10 + centerSignalDP].GetComponent<SignalScript>();
        if (centerSignalDP < 4) // クリックしたオブジェクトの行が右端でなければ 
        {
            // クリックしたオブジェクトの1つ右上(centerSignalDPが偶数と0の場合は右下)のオブジェクトを格納
            Debug.Log(centerSignalTP + "と" + centerSignalDP);
            detonationObjects.Add(signals[centerSignalTP * 10 + centerSignalDP + 1]);
            if (centerSignalDP % 2 == 0) // 右下の場合
            {
                // 右下への探索を開始する
                RecursiveCheckBottomRight(detonationObjects[detonationObjects.Count - 1]);
            }
            else // 右上の場合
            {
                // 右上への探索を開始する
                RecursiveCheckTopRight(detonationObjects[detonationObjects.Count - 1]);
            }
        }
        if (centerSignalDP > 0) // クリックしたオブジェクトの行が左端でなければ
        {
            // クリックしたオブジェクトの1つ左上(centerSignalDPが偶数と0の場合は左下)のオブジェクトを格納
            detonationObjects.Add(signals[centerSignalTP * 10 + centerSignalDP - 1]);
            if (centerSignalDP % 2 == 0) // 左下の場合
            {
                // 左下への探索を開始する
                RecursiveCheckBottomLeft(detonationObjects[detonationObjects.Count - 1]);
            }
            else
            {
                // 左上への探索を開始する
                RecursiveCheckTopLeft(detonationObjects[detonationObjects.Count - 1]);
            }
        }
        if (centerSignalTP > 0 && centerSignalDP < 4 && centerSignalDP % 2 == 0) // クリックしたオブジェクトの列が最上段かつ行が右端かつあまりが0ならば
        {
            // クリックしたオブジェクトの1つ右上のオブジェクトを格納
            detonationObjects.Add(signals[(centerSignalTP - 1) * 10 + centerSignalDP + 1]);
            // 右上への探索を開始する
            RecursiveCheckTopRight(detonationObjects[detonationObjects.Count - 1]);
        }
        if (centerSignalTP > 0 && centerSignalDP > 0 && centerSignalDP % 2 == 0) // クリックしたオブジェクトの列が最上段かつ行が左端かつあまりが0ならば
        {
            // クリックしたオブジェクトの1つ左上のオブジェクトを格納
            detonationObjects.Add(signals[(centerSignalTP - 1) * 10 + centerSignalDP - 1]);
            // 左上への探索を開始する
            RecursiveCheckTopLeft(detonationObjects[detonationObjects.Count - 1]);
        }
        if (centerSignalTP < 5 && centerSignalDP < 4 && centerSignalDP % 2 > 0) // クリックしたオブジェクトの列が最下段かつ行が右端かつあまりが0を超過するならば
        {
            // クリックしたオブジェクトの1つ右下のSignalScriptを格納
            detonationObjects.Add(signals[(centerSignalTP + 1) * 10 + centerSignalDP + 1]);
            // 右下への探索を開始する
            RecursiveCheckBottomRight(detonationObjects[detonationObjects.Count - 1]);
        }
        if (centerSignalTP < 5 && centerSignalDP > 0 && centerSignalDP % 2 > 0) // クリックしたオブジェクトの列が最下段かつ行が左端かつあまりが0を超過するならば
        {
            // クリックしたオブジェクトの1つ左下のSignalScriptを格納
            detonationObjects.Add(signals[(centerSignalTP + 1) * 10 + centerSignalDP - 1]);
            // 左下への探索を開始する
            RecursiveCheckBottomLeft(detonationObjects[detonationObjects.Count - 1]);
        }
        state = centerSignalSS.state; // 押したボタンのstateを代入する
        isChain = true; // チェインしているのでtrueにする
    }
    /// <summary>
    /// 右上への誘爆を探索する再帰関数
    /// </summary>
    /// <param name="gameObject">基準となるオブジェクト</param>
    private void RecursiveCheckTopRight (GameObject gameObject)
    {
        detonationSignalTP = default; // 10の位 列
        detonationSignalDP = default; // 1の位 行
        // 対象のオブジェクトが配列内オブジェクトのどれなのかを探す
        SearchSignal(gameObject, ref detonationSignalTP, ref detonationSignalDP);
        if (detonationSignalDP < 4 && detonationSignalDP % 2 > 0) // 対象のオブジェクトの行が右端でなければ 
        {
            // 対象のオブジェクトの1つ右上(centerSignalDPが偶数と0の場合は右下)のオブジェクトを格納
            detonationObjects.Add(signals[detonationSignalTP * 10 + detonationSignalDP + 1]);
            // 条件を満たさなくなるまで探索する
            RecursiveCheckTopRight(detonationObjects[detonationObjects.Count - 1]);
        }
        else if (detonationSignalTP > 0 && detonationSignalDP < 4 && detonationSignalDP % 2 == 0) // 対象のオブジェクトの列が最上段かつ行が右端かつあまりが0ならば
        {
            // 対象のオブジェクトの1つ右上のオブジェクトを格納
            detonationObjects.Add(signals[(detonationSignalTP - 1) * 10 + detonationSignalDP + 1]);
            // 条件を満たさなくなるまで探索する
            RecursiveCheckTopRight(detonationObjects[detonationObjects.Count - 1]);
        }
    }
    /// <summary>
    /// 左上への誘爆を探索する再帰関数
    /// </summary>
    /// <param name="gameObject">基準となるオブジェクト</param>
    private void RecursiveCheckTopLeft(GameObject gameObject)
    {
        detonationSignalTP = default; // 10の位 列
        detonationSignalDP = default; // 1の位 行
        // クリックしたオブジェクトが配列内オブジェクトのどれなのかを探す
        SearchSignal(gameObject, ref detonationSignalTP, ref detonationSignalDP);
        if (detonationSignalDP > 0 && detonationSignalDP % 2 > 0) // クリックしたオブジェクトの行が左端でなければ
        {
            // クリックしたオブジェクトの1つ左上(centerSignalDPが偶数と0の場合は左下)のオブジェクトを格納
            detonationObjects.Add(signals[detonationSignalTP * 10 + detonationSignalDP - 1]);
            // 条件を満たさなくなるまで探索する
            RecursiveCheckTopLeft(detonationObjects[detonationObjects.Count - 1]);
        }
        else if (detonationSignalTP > 0 && detonationSignalDP > 0 && detonationSignalDP % 2 == 0) // クリックしたオブジェクトの列が最上段かつ行が左端かつあまりが0ならば
        {
            // クリックしたオブジェクトの1つ左上のオブジェクトを格納
            detonationObjects.Add(signals[(detonationSignalTP - 1) * 10 + detonationSignalDP - 1]);
            // 条件を満たさなくなるまで探索する
            RecursiveCheckTopLeft(detonationObjects[detonationObjects.Count - 1]);
        }
    }
    /// <summary>
    /// 右下への誘爆を探索する再帰関数
    /// </summary>
    /// <param name="gameObject">基準となるオブジェクト</param>
    private void RecursiveCheckBottomRight(GameObject gameObject)
    {
        detonationSignalTP = default; // 10の位 列
        detonationSignalDP = default; // 1の位 行
        // クリックしたオブジェクトが配列内オブジェクトのどれなのかを探す
        SearchSignal(gameObject, ref detonationSignalTP, ref detonationSignalDP);
        if (detonationSignalDP < 4 && detonationSignalDP % 2 == 0) // クリックしたオブジェクトの行が右端でなければ 
        {
            // クリックしたオブジェクトの1つ右上(centerSignalDPが偶数と0の場合は右下)のオブジェクトを格納
            detonationObjects.Add(signals[detonationSignalTP * 10 + detonationSignalDP + 1]);
            // 条件を満たさなくなるまで探索する
            RecursiveCheckBottomRight(detonationObjects[detonationObjects.Count - 1]);
        }
        else if (detonationSignalTP < 5 && detonationSignalDP < 4 && detonationSignalDP % 2 > 0) // クリックしたオブジェクトの列が最下段かつ行が右端かつあまりが0を超過するならば
        {
            // クリックしたオブジェクトの1つ右下のSignalScriptを格納
            detonationObjects.Add(signals[(detonationSignalTP + 1) * 10 + detonationSignalDP + 1]);
            // 条件を満たさなくなるまで探索する
            RecursiveCheckBottomRight(detonationObjects[detonationObjects.Count - 1]);
        }
    }
    /// <summary>
    /// 左下への誘爆を探索する再帰関数
    /// </summary>
    /// <param name="gameObject">基準となるオブジェクト</param>
    private void RecursiveCheckBottomLeft(GameObject gameObject)
    {
        detonationSignalTP = default; // 10の位 列
        detonationSignalDP = default; // 1の位 行
        // クリックしたオブジェクトが配列内オブジェクトのどれなのかを探す
        SearchSignal(gameObject, ref detonationSignalTP, ref detonationSignalDP);
        if (detonationSignalDP > 0 && detonationSignalDP % 2 == 0) // クリックしたオブジェクトの行が左端でなければ
        {
            // クリックしたオブジェクトの1つ左上(centerSignalDPが偶数と0の場合は左下)のオブジェクトを格納
            detonationObjects.Add(signals[detonationSignalTP * 10 + detonationSignalDP - 1]);
            // 条件を満たさなくなるまで探索する
            RecursiveCheckBottomLeft(detonationObjects[detonationObjects.Count - 1]);
        }
        if (detonationSignalTP < 5 && detonationSignalDP > 0 && detonationSignalDP % 2 > 0) // クリックしたオブジェクトの列が最下段かつ行が左端かつあまりが0を超過するならば
        {
            // クリックしたオブジェクトの1つ左下のSignalScriptを格納
            detonationObjects.Add(signals[(detonationSignalTP + 1) * 10 + detonationSignalDP - 1]);
            // 条件を満たさなくなるまで探索する
            RecursiveCheckBottomLeft(detonationObjects[detonationObjects.Count - 1]);
        }
    }
    /// <summary>
    /// 誘爆したシグナルを破壊する関数
    /// </summary>
    private void DetonationBreak()
    {
        // 誘爆したシグナルのstateをdetonationStatesに代入してBreakSignal関数を呼び出す(NOTHING以外)
        for (int i = 0; i < detonationObjects.Count; i++)
        {
            if (detonationObjects[i].GetComponent<SignalScript>().state != SignalScript.STATE.NOTHING)
            {
                chain += 1; // チェイン数を1ずつ加算する
                ScoreDirector.detonationStates.Add(detonationObjects[i].GetComponent<SignalScript>().state);
                detonationObjects[i].GetComponent<SignalScript>().BreakSignal(false);
            }
        }
        detonationObjects = new List<GameObject>(); // 中身を空にする
    }
    private bool CheckChainSignal(GameObject gameObject) // クリックされたオブジェクトの6方向をチェックし、チェインを確認する関数
    {
        centerSignalTP = default; // 10の位 列
        centerSignalDP = default; // 1の位 行
        centerSignalSS = null; // クリックしたオブジェクトのSignalScript格納用
        comparisonSignalSS = null; // クリックしていないオブジェクトのSignalScript格納用

        // クリックしたオブジェクトが配列内オブジェクトのどれなのかを探す
        SearchSignal(gameObject, ref centerSignalTP, ref centerSignalDP);
        // クリックしたオブジェクトのSignalScriptを格納
        centerSignalSS = signals[centerSignalTP * 10 + centerSignalDP].GetComponent<SignalScript>();

        if (centerSignalDP < 4) // クリックしたオブジェクトの行が右端でなければ 
        {
            // クリックしたオブジェクトの1つ右上(centerSignalDPが偶数と0の場合は右下)のSignalScriptを格納
            comparisonSignalSS = signals[centerSignalTP * 10 + centerSignalDP + 1].GetComponent<SignalScript>();
            CheckSameSignal(centerSignalSS, comparisonSignalSS);
        }
        if (centerSignalDP > 0) // クリックしたオブジェクトの行が左端でなければ
        {
            // クリックしたオブジェクトの1つ左上(centerSignalDPが偶数と0の場合は左下)のSignalScriptを格納
            comparisonSignalSS = signals[centerSignalTP * 10 + centerSignalDP - 1].GetComponent<SignalScript>();
            CheckSameSignal(centerSignalSS, comparisonSignalSS);
        }
        if (centerSignalTP > 0) // クリックしたオブジェクトの列が最上段でなければ
        {
            // クリックしたオブジェクトの1つ上のSignalScriptを格納
            comparisonSignalSS = signals[(centerSignalTP - 1) * 10 + centerSignalDP].GetComponent<SignalScript>();
            CheckSameSignal(centerSignalSS, comparisonSignalSS);
        }
        if (centerSignalTP > 0 && centerSignalDP < 4 && centerSignalDP % 2 == 0) // クリックしたオブジェクトの列が最上段かつ行が右端かつあまりが0ならば
        {
            // クリックしたオブジェクトの1つ右上のSignalScriptを格納
            comparisonSignalSS = signals[(centerSignalTP - 1) * 10 + centerSignalDP + 1].GetComponent<SignalScript>();
            CheckSameSignal(centerSignalSS, comparisonSignalSS);
        }
        if (centerSignalTP > 0 && centerSignalDP > 0 && centerSignalDP % 2 == 0) // クリックしたオブジェクトの列が最上段かつ行が左端かつあまりが0ならば
        {
            // クリックしたオブジェクトの1つ左上のSignalScriptを格納
            comparisonSignalSS = signals[(centerSignalTP - 1) * 10 + centerSignalDP - 1].GetComponent<SignalScript>();
            CheckSameSignal(centerSignalSS, comparisonSignalSS);
        }
        if (centerSignalTP < 5) // クリックしたオブジェクトの列が最下段でなければ
        {
            // クリックしたオブジェクトの1つ下のSignalScriptを格納
            comparisonSignalSS = signals[(centerSignalTP + 1) * 10 + centerSignalDP].GetComponent<SignalScript>();
            CheckSameSignal(centerSignalSS, comparisonSignalSS);
        }
        if (centerSignalTP < 5 && centerSignalDP < 4 && centerSignalDP % 2 > 0) // クリックしたオブジェクトの列が最下段かつ行が右端かつあまりが0を超過するならば
        {
            // クリックしたオブジェクトの1つ右下のSignalScriptを格納
            comparisonSignalSS = signals[(centerSignalTP + 1) * 10 + centerSignalDP + 1].GetComponent<SignalScript>();
            CheckSameSignal(centerSignalSS, comparisonSignalSS);
        }
        if (centerSignalTP < 5 && centerSignalDP > 0 && centerSignalDP % 2 > 0) // クリックしたオブジェクトの列が最下段かつ行が左端かつあまりが0を超過するならば
        {
            // クリックしたオブジェクトの1つ左下のSignalScriptを格納
            comparisonSignalSS = signals[(centerSignalTP + 1) * 10 + centerSignalDP - 1].GetComponent<SignalScript>();
            CheckSameSignal(centerSignalSS, comparisonSignalSS);
        }
        if (chain > 0) // チェイン数が0より多いなら
        {
            isChain = true; // isChainを真にする
            // ここでチェイン数がわかる //
        }
        state = centerSignalSS.state; // 押したボタンのstateを代入する
        // チェインしていたらtrueを返す
        return isChain == true;
    }
    private void SearchSignal(GameObject gameObject, ref int centerSignalTP, ref int centerSignalDP) // 対象のオブジェクトが配列内オブジェクトのどれなのかを探す関数 ref...初期化必須
    {
        for (int j = 0; j < 6; j++) // 列をチェック
        {
            for (int i = 0; i < 5; i++) // 行をチェック
            {
                if (gameObject == signals[j * 10 + i]) // 対象のオブジェクトと配列内のオブジェクトが同じなら
                {
                    centerSignalTP = j; // 列を格納
                    centerSignalDP = i; // 行を格納
                }
            }
        }
    }
    private void CheckSameSignal(SignalScript centerSignalSS, SignalScript comparisonSignalSS) // 同じstateかどうかを確認する関数
    {
        if (centerSignalSS.state == comparisonSignalSS.state) // stateが同じなら
        {
            chain++; // チェインカウントを1増やす
            comparisonSignalSS.BreakSignal(true);
        }
    }
    public void ButtonsDestroy() // リザルト画面時にボタンを消す
    {
        for (int j = 0; j < 6; j++) // 列をチェック
        {
            for (int i = 0; i < 5; i++) // 行をチェック
            {
                Destroy(signals[j * 10 + i]);// 全てのシグナルを破壊する
            }
        }
    }
    private void ResurrectionSignal() // stateがNOTHINGのシグナル全てにsetSignalPointを1加算する関数
    {
        if (!isChain) return; // チェインしていないならリターンする
        for (int j = 0; j < 6; j++) // 列をチェック
        {
            for (int i = 0; i < 5; i++) // 行をチェック
            {
                if (signals[j * 10 + i].GetComponent<SignalScript>().state == SignalScript.STATE.NOTHING) // そのSignalのstateがNOTHINGなら
                {
                    signals[j * 10 + i].GetComponent<SignalScript>().AddSetSignalPoint(); // そのSignalのAddSetSignalPointを呼び出す
                }
            }
        }
    }
    private void ShowGetScore(GameObject clickedObject) // ゲットしたスコアを表示する関数
    {
        scoreTextObject = clickedObject.transform.GetChild(1).gameObject; // クリックしたボタンのテキストを取得
        getScoreText = scoreTextObject.GetComponent<TextMeshPro>(); // TextMeshProを代入
        getScoreText.text = string.Format("+ {0:0}", ScoreDirector.score); // ゲットしたスコアを代入
        scoreTextObject.GetComponent<Animator>().SetTrigger("GetScore"); // アニメーションを再生
    }
    public void AllButtonsReset() // 全てのシグナルをリセットする
    {
        if (resetStock > 0) // 残りリセット回数が0を超過しているなら
        {
            for (int j = 0; j < 6; j++) // 列をチェック
            {
                for (int i = 0; i < 5; i++) // 行をチェック
                {
                    signals[j * 10 + i].GetComponent<SignalScript>().BreakSignal(false); // ブレイクシグナルを呼び出す
                    signals[j * 10 + i].GetComponent<SignalScript>().SetSignal(); // セットシグナルを呼び出す
                }
            }
            resetStock -= 1; // 残りリセット回数を1減らす
        }
    }
    /// <summary>
    /// ボムの自動爆破関数
    /// </summary>
    /// <param name="special">爆発させるボムオブジェクト</param>
    public void BombTimeOver(GameObject special)
    {
        InitializationChain(); // チェイン系の変数を初期化
        CheckDetonation(special); // X字ボムの誘爆範囲を確認する
        DetonationBreak(); // 誘爆したシグナルを破壊する
        SEAudioSource.Play(); // SEを鳴らす
        special.GetComponent<SignalScript>().BreakSignal(isChain, chain); // ブレイク関数を呼び出し
        ResurrectionSignal(); // stateがNOTHINGのシグナル全てにsetSignalPointを1加算する
        ScoreDirector.GetScore(chain, isChain, state); // ゲットスコア関数を呼び出し
        ShowGetScore(special); // ゲットしたスコアを表示する
        StarDirector.GetStar(chain); // ゲットスター関数を呼び出し
        Debug.Log(special + "が自動で爆発しました");
    }
}
