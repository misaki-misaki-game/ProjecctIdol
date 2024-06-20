using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public partial class ButtonScript : MonoBehaviour
{
    /// --------関数一覧-------- ///
    /// -------public関数------- ///

    /// <summary>
    /// リザルト画面時にボタンを消す関数
    /// </summary>
    public void ButtonsDestroy()
    {
        for (int j = 0; j < column; j++) // 列をチェック
        {
            for (int i = 0; i < row; i++) // 行をチェック
            {
                if (!signals[j * 10 + i]) continue; // nullなら次の処理に移行する
                Destroy(signals[j * 10 + i]);// 全てのシグナルを破壊する
            }
        }
        resetButton.gameObject.SetActive(false); // リセットボタンを非表示にする
    }

    /// <summary>
    /// 全てのシグナルをリセットする関数
    /// </summary>
    public void AllButtonsReset()
    {
        if (resetStock > 0) // 残りリセット回数が0を超過しているなら
        {
            for (int j = 0; j < column; j++) // 列をチェック
            {
                for (int i = 0; i < row; i++) // 行をチェック
                {
                    SignalScript ss = signals[j * 10 + i].GetComponent<SignalScript>();
                    ss.BreakSignal(false); // ブレイクシグナルを呼び出す
                    ss.SetSignal(); // セットシグナルを呼び出す
                }
            }
            resetStock -= 1; // 残りリセット回数を1減らす
            // resetStockによってリセットボタン画像を変更する
            switch (resetStock)
            {
                case 0:
                    resetButton.sprite = resetImage[0];
                    resetButton.GetComponent<Button>().interactable = false;
                    break;
                case 1:
                    resetButton.sprite = resetImage[1];
                    break;
                case 2:
                    resetButton.sprite = resetImage[2];
                    break;
            }
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

    /// -------public関数------- ///
    /// -----protected関数------ ///



    /// -----protected関数------ ///
    /// ------private関数------- ///

    private void Start()
    {
        rightMost = (signals.Length % 10) - 1; // 右端を代入
        bottom = signals.Length / 10; // 最下段を代入
        row = signals.Length % 10; // 行数を代入
        column = (signals.Length / 10) + 1; // 列数を代入
        resetButton.GetComponent <Button>().interactable = false; // ボタンを押せないようにする
    }

    private void Update()
    {
        SignalClick(); // シグナルをクリックしたときの処理
    }

    /// <summary>
    /// シグナルをクリックしたときの関数
    /// </summary>
    private void SignalClick()
    {
        // 左クリックしたらかつゲームがスタートしていればかつ通常モードなら
        if (Input.GetMouseButtonDown(0) && gameStart && StarDirector.starState == StarDirector.StarState.NormalMode) 
        {
            // シグナルをクリックできたかの処理
            RaycastHit2D hitSprite = CheckHit();
            if (hitSprite == true) // レイが当たったら
            {
                InitializationChain(); // チェイン系の変数を初期化
                clickedGameObject = hitSprite.transform.gameObject; // clickedGameObjectにレイが当たったオブジェクトを格納
                ProcessingSignal(clickedGameObject); // clickedGameObjectを基にしたシグナルの処理を行う
            }
        }
    }

    /// <summary>
    /// 渡されたシグナルを基に処理を行う関数
    /// </summary>
    /// <param name="signal">選択したシグナル</param>
    private void ProcessingSignal(GameObject signal)
    {
        SignalScript ss = signal.GetComponent<SignalScript>(); // SignalScriptを代入
        if (CheckSignalState(signal)) return; // シグナルの色をチェックしてNOTHINGならリターンする
        if (ss.state == SignalScript.STATE.SPECIAL) // X字ボムの場合
        {
            CheckDetonation(signal); // X字ボムの誘爆範囲を確認する
            DetonationBreak(); // 誘爆したシグナルを破壊する
        }
        else // それ以外の場合
        {
            if (!CheckChainSignal(signal)) return; // チェイン確認関数を呼び出し チェインしていなければリターンする
        }
        // SEを鳴らす
        if (ss.state == SignalScript.STATE.SPECIAL) SEAudioSource.PlayOneShot(SEClips[1]);
        else SEAudioSource.PlayOneShot(SEClips[0]);

        ss.BreakSignal(isChain, chain); // ブレイク関数を呼び出し
        ResurrectionSignal(); // stateがNOTHINGのシグナル全てにsetSignalPointを1加算する
        ScoreDirector.GetScore(chain, isChain, state); // ゲットスコア関数を呼び出し
        ShowGetScore(signal); // ゲットしたスコアを表示する
        StarDirector.GetStar(chain); // ゲットスター関数を呼び出し
    }

    /// <summary>
    /// ヒットしているかのチェック関数
    /// </summary>
    /// <returns>ヒットしたRaycastHit2D</returns>
    private RaycastHit2D CheckHit()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // rayにクリックしたポジションを格納
        RaycastHit2D hitSprite = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction); // hitSpriteにレイが当たったオブジェクトを格納
        return hitSprite; //  hitSpriteをリターンする
    }

    /// <summary>
    /// チェイン系の変数を初期化する関数
    /// </summary>
    private void InitializationChain()
    {
        chain = default; // 初期化
        isChain = default; // 初期化
    }

    /// <summary>
    /// シグナルの色チェック
    /// </summary>
    /// <param name="gameObject"></param>
    /// <returns>STATEがNOTHINGならtrue</returns>
    private bool CheckSignalState(GameObject gameObject)
    {
        // STATEがNOTHINGならtrueを返す
        return gameObject.GetComponent<SignalScript>().state == SignalScript.STATE.NOTHING;
    }

    /// <summary>
    /// クリックされたオブジェクトの斜め4方向にある全てのシグナル確認をする関数
    /// </summary>
    /// <param name="gameObject">クリックしたオブジェクト</param>
    private void CheckDetonation(GameObject gameObject)
    {

        // 始めの爆発または爆発するシグナルが重複していないなら爆発したシグナルとして格納
        // 重複していれば処理を中止
        if (explotionObjects == null || !explotionObjects.Contains(gameObject)) explotionObjects.Add(gameObject);
        else if (explotionObjects.Contains(gameObject)) return;

        centerSignalTP = default; // 10の位 列
        centerSignalDP = default; // 1の位 行
        centerSignalSS = null; // クリックしたオブジェクトのSignalScript格納用
        comparisonSignalSS = null; // クリックしていないオブジェクトのSignalScript格納用

        // クリックしたオブジェクトが配列内オブジェクトのどれなのかを探す
        SearchSignal(gameObject, ref centerSignalTP, ref centerSignalDP);
        // クリックしたオブジェクトのSignalScriptを格納
        centerSignalSS = signals[centerSignalTP * 10 + centerSignalDP].GetComponent<SignalScript>();

        // 各斜め方向を探索　既に格納したオブジェクトは処理を行わない 
        // クリックしたオブジェクトの行が右端でなければ
        if (centerSignalDP < rightMost && !detonationObjects.Contains(signals[centerSignalTP * 10 + centerSignalDP + 1])) 
        {
            // クリックしたオブジェクトの1つ右上(centerSignalDPが偶数と0の場合は右下)のオブジェクトを格納
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
        // クリックしたオブジェクトの行が左端でなければ
        if (centerSignalDP > 0 && !detonationObjects.Contains(signals[centerSignalTP * 10 + centerSignalDP - 1]))
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
        // クリックしたオブジェクトの列が最上段かつ行が右端かつあまりが0ならば
        if (centerSignalTP > 0 && centerSignalDP < rightMost && centerSignalDP % 2 == 0 && !detonationObjects.Contains(signals[(centerSignalTP - 1) * 10 + centerSignalDP + 1]))
        {
            // クリックしたオブジェクトの1つ右上のオブジェクトを格納
            detonationObjects.Add(signals[(centerSignalTP - 1) * 10 + centerSignalDP + 1]);
            // 右上への探索を開始する
            RecursiveCheckTopRight(detonationObjects[detonationObjects.Count - 1]);
        }
        // クリックしたオブジェクトの列が最上段かつ行が左端かつあまりが0ならば
        if (centerSignalTP > 0 && centerSignalDP > 0 && centerSignalDP % 2 == 0 && !detonationObjects.Contains(signals[(centerSignalTP - 1) * 10 + centerSignalDP - 1]))
        {
            // クリックしたオブジェクトの1つ左上のオブジェクトを格納
            detonationObjects.Add(signals[(centerSignalTP - 1) * 10 + centerSignalDP - 1]);
            // 左上への探索を開始する
            RecursiveCheckTopLeft(detonationObjects[detonationObjects.Count - 1]);
        }
        // クリックしたオブジェクトの列が最下段かつ行が右端かつあまりが0を超過するならば
        if (centerSignalTP < bottom && centerSignalDP < rightMost && centerSignalDP % 2 > 0 && !detonationObjects.Contains(signals[(centerSignalTP + 1) * 10 + centerSignalDP + 1]))
        {
            // クリックしたオブジェクトの1つ右下のSignalScriptを格納
            detonationObjects.Add(signals[(centerSignalTP + 1) * 10 + centerSignalDP + 1]);
            // 右下への探索を開始する
            RecursiveCheckBottomRight(detonationObjects[detonationObjects.Count - 1]);
        }
        // クリックしたオブジェクトの列が最下段かつ行が左端かつあまりが0を超過するならば
        if (centerSignalTP < bottom && centerSignalDP > 0 && centerSignalDP % 2 > 0 && !detonationObjects.Contains(signals[(centerSignalTP + 1) * 10 + centerSignalDP - 1]))
        {
            // クリックしたオブジェクトの1つ左下のSignalScriptを格納
            detonationObjects.Add(signals[(centerSignalTP + 1) * 10 + centerSignalDP - 1]);
            // 左下への探索を開始する
            RecursiveCheckBottomLeft(detonationObjects[detonationObjects.Count - 1]);
        }
        // 誘爆したオブジェクトの中にボムがあれば、それを中心に爆発処理を行う
        for (int i = 0; i < detonationObjects.Count; i++)
        {
            if (detonationObjects[i].GetComponent<SignalScript>().state == SignalScript.STATE.SPECIAL)
            {
                CheckDetonation(detonationObjects[i]);
            }
        }
        state = centerSignalSS.state; // 押したボタンのstateを代入する
        isChain = true; // チェインしているのでtrueにする
    }

    /// <summary>
    /// 右上への誘爆を探索する再帰関数
    /// </summary>
    /// <param name="gameObject">基準となるオブジェクト</param>
    private void RecursiveCheckTopRight(GameObject gameObject)
    {
        detonationSignalTP = default; // 10の位 列
        detonationSignalDP = default; // 1の位 行
        // 対象のオブジェクトが配列内オブジェクトのどれなのかを探す
        SearchSignal(gameObject, ref detonationSignalTP, ref detonationSignalDP);
        if (detonationSignalDP < rightMost && detonationSignalDP % 2 > 0) // 対象のオブジェクトの行が右端でなければ 
        {
            // 対象のオブジェクトの1つ右上(centerSignalDPが偶数と0の場合は右下)のオブジェクトを格納
            detonationObjects.Add(signals[detonationSignalTP * 10 + detonationSignalDP + 1]);
            // 条件を満たさなくなるまで探索する
            RecursiveCheckTopRight(detonationObjects[detonationObjects.Count - 1]);
        }
        else if (detonationSignalTP > 0 && detonationSignalDP < rightMost && detonationSignalDP % 2 == 0) // 対象のオブジェクトの列が最上段かつ行が右端かつあまりが0ならば
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
        if (detonationSignalDP < rightMost && detonationSignalDP % 2 == 0) // クリックしたオブジェクトの行が右端でなければ 
        {
            // クリックしたオブジェクトの1つ右上(centerSignalDPが偶数と0の場合は右下)のオブジェクトを格納
            detonationObjects.Add(signals[detonationSignalTP * 10 + detonationSignalDP + 1]);
            // 条件を満たさなくなるまで探索する
            RecursiveCheckBottomRight(detonationObjects[detonationObjects.Count - 1]);
        }
        else if (detonationSignalTP < bottom && detonationSignalDP < rightMost && detonationSignalDP % 2 > 0) // クリックしたオブジェクトの列が最下段かつ行が右端かつあまりが0を超過するならば
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
        if (detonationSignalTP < bottom && detonationSignalDP > 0 && detonationSignalDP % 2 > 0) // クリックしたオブジェクトの列が最下段かつ行が左端かつあまりが0を超過するならば
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
        // 誘爆範囲のシグナルのsetSignalSointを+1増やしておく SignalClick()でさらに+1されるので実質+2
        for (int i = 0; i < detonationObjects.Count; i++)
        {
            SignalScript ss = detonationObjects[i].GetComponent<SignalScript>();
            if (ss.state != SignalScript.STATE.NOTHING)
            {
                chain += 1; // チェイン数を1ずつ加算する
                ScoreDirector.detonationStates.Add(ss.state);
                ss.BreakSignal(false);
            }
            ss.AddSetSignalPoint();
        }
        // 中身を空にする
        detonationObjects = new List<GameObject>();
        explotionObjects = new List<GameObject>();
    }

    /// <summary>
    /// クリックされたオブジェクトの6方向をチェックし、チェインを確認する関数
    /// </summary>
    /// <param name="gameObject">クリックしたオブジェクト</param>
    /// <returns>チェインしているかどうか</returns>
    private bool CheckChainSignal(GameObject gameObject)
    {
        centerSignalTP = default; // 10の位 列
        centerSignalDP = default; // 1の位 行
        centerSignalSS = null; // クリックしたオブジェクトのSignalScript格納用
        comparisonSignalSS = null; // クリックしていないオブジェクトのSignalScript格納用

        // クリックしたオブジェクトが配列内オブジェクトのどれなのかを探す
        SearchSignal(gameObject, ref centerSignalTP, ref centerSignalDP);
        // クリックしたオブジェクトのSignalScriptを格納
        centerSignalSS = signals[centerSignalTP * 10 + centerSignalDP].GetComponent<SignalScript>();

        if (centerSignalDP < rightMost) // クリックしたオブジェクトの行が右端でなければ 
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
        if (centerSignalTP > 0 && centerSignalDP < rightMost && centerSignalDP % 2 == 0) // クリックしたオブジェクトの列が最上段かつ行が右端かつあまりが0ならば
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
        if (centerSignalTP < bottom) // クリックしたオブジェクトの列が最下段でなければ
        {
            // クリックしたオブジェクトの1つ下のSignalScriptを格納
            comparisonSignalSS = signals[(centerSignalTP + 1) * 10 + centerSignalDP].GetComponent<SignalScript>();
            CheckSameSignal(centerSignalSS, comparisonSignalSS);
        }
        if (centerSignalTP < bottom && centerSignalDP < rightMost && centerSignalDP % 2 > 0) // クリックしたオブジェクトの列が最下段かつ行が右端かつあまりが0を超過するならば
        {
            // クリックしたオブジェクトの1つ右下のSignalScriptを格納
            comparisonSignalSS = signals[(centerSignalTP + 1) * 10 + centerSignalDP + 1].GetComponent<SignalScript>();
            CheckSameSignal(centerSignalSS, comparisonSignalSS);
        }
        if (centerSignalTP < bottom && centerSignalDP > 0 && centerSignalDP % 2 > 0) // クリックしたオブジェクトの列が最下段かつ行が左端かつあまりが0を超過するならば
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

    /// <summary>
    ///  対象のオブジェクトが配列内オブジェクトのどれなのかを探す関数 ref...初期化必須
    /// </summary>
    /// <param name="gameObject">クリックしたオブジェクト</param>
    /// <param name="centerSignalTP">10の位</param>
    /// <param name="centerSignalDP">1の位</param>
    private void SearchSignal(GameObject gameObject, ref int centerSignalTP, ref int centerSignalDP)
    {
        for (int j = 0; j < column; j++) // 列をチェック
        {
            for (int i = 0; i < row; i++) // 行をチェック
            {
                if (gameObject == signals[j * 10 + i]) // 対象のオブジェクトと配列内のオブジェクトが同じなら
                {
                    centerSignalTP = j; // 列を格納
                    centerSignalDP = i; // 行を格納
                }
            }
        }
    }

    /// <summary>
    /// 同じstateかどうかを確認する関数
    /// </summary>
    /// <param name="centerSignalSS">比較元のオブジェクト</param>
    /// <param name="comparisonSignalSS">比較先のオブジェクト</param>
    private void CheckSameSignal(SignalScript centerSignalSS, SignalScript comparisonSignalSS)
    {
        if (centerSignalSS.state == comparisonSignalSS.state) // stateが同じなら
        {
            chain++; // チェインカウントを1増やす
            comparisonSignalSS.BreakSignal(true);
        }
    }

    /// <summary>
    /// stateがNOTHINGのシグナル全てにsetSignalPointを1加算する関数
    /// </summary>
    private void ResurrectionSignal()
    {
        if (!isChain) return; // チェインしていないならリターンする
        for (int j = 0; j < column; j++) // 列をチェック
        {
            for (int i = 0; i < row; i++) // 行をチェック
            {
                if (signals[j * 10 + i].GetComponent<SignalScript>().state == SignalScript.STATE.NOTHING) // そのSignalのstateがNOTHINGなら
                {
                    signals[j * 10 + i].GetComponent<SignalScript>().AddSetSignalPoint(); // そのSignalのAddSetSignalPointを呼び出す
                }
            }
        }
    }

    /// <summary>
    /// ゲットしたスコアを表示する関数
    /// </summary>
    /// <param name="clickedObject">クリックしたオブジェクト</param>
    private void ShowGetScore(GameObject clickedObject)
    {
        scoreTextObject = clickedObject.transform.GetChild(1).gameObject; // クリックしたボタンのテキストを取得
        getScoreText = scoreTextObject.GetComponent<TextMeshPro>(); // TextMeshProを代入
        getScoreText.text = string.Format("+ {0:0}", ScoreDirector.score); // ゲットしたスコアを代入
        scoreTextObject.GetComponent<Animator>().SetTrigger("GetScore"); // アニメーションを再生
    }

    /// ------private関数------- ///
    /// --------関数一覧-------- ///
}
public partial class ButtonScript
{
    /// --------変数一覧-------- ///
    /// -------public変数------- ///

    public bool gameStart = false; // ゲームがスタートしているかどうか

    public List<GameObject> specialSignals; // X字ボム格納用

    public Image resetButton; // リセットボタン変数

    /// -------public変数------- ///
    /// -----protected変数------ ///



    /// -----protected変数------ ///
    /// ------private変数------- ///

    private bool isChain = false; // チェインしているかどうか

    private int centerSignalTP = 0; // 10の位 列(通常シグナル時)
    private int centerSignalDP = 0; // 1の位 行(通常シグナル時)
    private int detonationSignalTP = 0; // 10の位 列(誘爆時)
    private int detonationSignalDP = 0; // 1の位 行(誘爆時)
    private int resetStock = 3; // リセット回数
    private int rightMost = 0; // シグナルの並びの右端
    private int bottom = 0; // シグナルの並びの最下段
    private int row = 0; // 行数
    private int column = 0; // 列数

    private float chain = 0; // チェイン変数

    private GameObject scoreTextObject; // テキストのオブジェクト変数

    private SignalScript.STATE state; //SignalScriptのSTATE変数
    private SignalScript centerSignalSS; // クリックしたオブジェクトのSignalScript格納用
    private SignalScript comparisonSignalSS; // クリックしていないオブジェクトのSignalScript格納用

    [SerializeField] private AudioSource SEAudioSource; // SE用オーディオソース

    [Header("[0]...通常シグナルSE,[1]...ボムシグナル")]
    [SerializeField] private AudioClip[] SEClips = new AudioClip[2];

    [SerializeField] private GameObject clickedGameObject; // クリックしたオブジェクトを格納する変数
    [SerializeField] private GameObject[] signals; // シグナル格納用
    [SerializeField] private List<GameObject> detonationObjects; // 誘爆したシグナル格納用
    [SerializeField] private List<GameObject> explotionObjects; // 爆発したシグナル格納用

    [SerializeField] private ScoreDirector ScoreDirector; // ScoreDirector変数

    [SerializeField] private StarDirector StarDirector; // StarDirector変数

    [SerializeField] private TextMeshPro getScoreText; // 取得したスコアを表示するテキスト変数

    [SerializeField] private Sprite[] resetImage = new Sprite[3]; // リセットボタンのimage変数

    /// ------private変数------- ///
    /// -------プロパティ------- ///



    /// -------プロパティ------- ///
    /// --------変数一覧-------- ///
}