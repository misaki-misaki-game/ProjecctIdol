using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ButtonScript : MonoBehaviour
{
    SignalScript.STATE state; //SignalScriptのSTATE変数
    public float chain = 0; // チェイン変数
    public bool isChain = false; // チェインしているかどうか
    public bool gameStart = false; // ゲームがスタートしているかどうか
    public ScoreDirector ScoreDirector; // ScoreDirector変数
    public StarDirector StarDirector; // StarDirector変数
    public GameObject clickedGameObject; // クリックしたオブジェクトを格納する変数
    public GameObject[] signals; // シグナル格納用
    public AudioSource SEAudioSource; // SE用オーディオソース

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
                CheckChainSignal(clickedGameObject); // チェイン確認関数を呼び出し
                clickedGameObject.GetComponent<SignalScript>().BreakSignal(isChain); // ブレイク関数を呼び出し
                ResurrectionSignal(); // stateがNOTHINGのシグナル全てにsetSignalPointを1加算する
                ScoreDirector.GetScore(chain, isChain, state); // ゲットスコア関数を呼び出し
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
    private void CheckChainSignal(GameObject gameObject) // クリックされたオブジェクトの8方向をチェックし、チェインを確認する関数
    {
        SEAudioSource.Play(); // SEを鳴らす
        int centerSignalTP = 0; // 10の位 列
        int centerSignalDP = 0; // 1の位 行
        SignalScript centerSignalSS; // クリックしたオブジェクトのSignalScript格納用
        SignalScript comparisonSignalSS; // クリックしていないオブジェクトのSignalScript格納用

        // クリックしたオブジェクトが配列内オブジェクトのどれなのかを探す
        SearchSignal(ref centerSignalTP, ref centerSignalDP);
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
        }
        state = centerSignalSS.state; // 押したボタンのstateを代入する
    }
    private void SearchSignal(ref int centerSignalTP, ref int centerSignalDP) // クリックしたオブジェクトが配列内オブジェクトのどれなのかを探す関数 ref...初期化必須
    {
        for (int j = 0; j < 6; j++) // 列をチェック
        {
            for (int i = 0; i < 5; i++) // 行をチェック
            {
                if (clickedGameObject == signals[j * 10 + i]) // クリックしたオブジェクトと配列内のオブジェクトが同じなら
                {
                    centerSignalTP = j; // 列を格納
                    centerSignalDP = i; // 行を格納
                }
            }
        }
    }
    private void CheckSameSignal(SignalScript centerSignalSS, SignalScript comparisonSignalSS)
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
}
