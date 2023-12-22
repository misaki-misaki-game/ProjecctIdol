using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using static DiamondMesh;

public class ScoreDirector : SignalScript
{
    // bool isgameOver = false; // ゲームオーバーかどうか
    bool isCombo = false; // コンボしているかどうか
    public float comboTime; // コンボ持続タイム変数
    public float scoreBaseValue = 50; // 基礎スコア変数
    public float comboDurationTime = 1f; // コンボ継続時間変数
    public float totalScore; // トータルスコア変数
    public float totalUltimateScore; // 究極トータルスコア

    /// <summary> 各究極ランクの下限の値 </summary>
    public float minRankS = 151f;
    public float minRankA = 101f;
    public float minRankB = 61f;
    public float minRankC = 31f;
    /// <summary> 各究極ランクの下限の値 </summary>
    
    /// <summary> 各究極トータルランクの下限の値 </summary>
    public float minTotalS = 701f;
    public float minTotalA = 401f;
    public float minTotalB = 241f;
    public float minTotalC = 121f;
    /// <summary> 各究極トータルランクの下限の値 </summary>
    /// 

    public int combo; // コンボ変数
    public int chain; // チェイン変数
    public enum UltimateType
    {
        intelligence = 0,
        charisma,
        acting,
        guts,
        total
    }

    [EnumIndex(typeof(UltimateType))]
    public float[] ultimateScore = new float[4]; // 究極スコア
    /*
    ultimateScore[0] intelligenceScore; // インテリジェンススコア(青シグナル)
    ultimateScore[1] charismaScore; // カリスマスコア(赤シグナル)
    ultimateScore[2] actingScore; // アクティングスコア(白シグナル)
    ultimateScore[3] gutsScore; // ガッツスコア(黄色シグナル)
    */
    [EnumIndex(typeof(UltimateType))]
    public string[] rank = new string[5]; // ultimateScoreの要素数と同じ　[D,C,B,A,S]のどれかにする [5]は究極トータルスコア
    [EnumIndex(typeof(UltimateType))]
    public TextMeshProUGUI[] UltText = new TextMeshProUGUI[5]; // 究極パラメーターテキスト表示用

    public TextMeshProUGUI scoreText; // スコアテキスト表示用
    public TextMeshProUGUI comboText; // コンボテキスト表示用
    public StarDirector starDirector; // StarDirector変数
    
    // Start is called before the first frame update
    void Start()
    {
        // トータルスコアを初期化
        totalScore = 0;
        // コンボを初期化
        combo = 0;
        // スコアを表示
        scoreText.text = string.Format("SCORE:\n{0:00000000}", totalScore);
        // コンボを表示
        comboText.text = string.Format("{0:0}combo", combo);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // コンボ持続の処理
        if (isCombo ) // コンボしているのであれば
        {
            // コンボ持続時間を減らす
            comboTime -= Time.deltaTime;
            // コンボを中断する処理
            if ( comboTime < 0 ) // コンボ持続時間が0なら
            {
                // 0を代入
                comboTime = 0f;
                // isCombo変数をfalseにする
                isCombo = false;
                // コンボ数をリセット
                combo = 0;
                // コンボを表示
                comboText.text = string.Format("{0:0}combo", combo);
            }
        }
    }
    
    public void GetScore(float chain,bool isChain, STATE state) // ゲットスコア関数
    {
        float score = 0; // スコア計算用変数
        // 加算するスコアを計算
        if (isChain) // チェインが発生していれば
        {
            // コンボを1加算
            combo += 1;
            // コンボ持続時間をリセット
            comboTime = comboDurationTime;
            // isCombo変数をtrueにする
            isCombo = true;
            if (starDirector.isStarMode == true) // お星様モードであれば
            {
                score = scoreBaseValue * 1.5f * (2 * chain); // 基礎スコア50を1.5倍
            }
            else
            {
                // 50に(chain×2)倍して加算
                score = scoreBaseValue * (2 * chain);
            }
        }
        else // チェインが発生していなければ
        {
            score = scoreBaseValue; // 50を加算
        }
        // 加算するスコアに倍率をかける
        if (combo >= 50 && combo < 100) // コンボが50以上100未満
        {
            // 加算するスコアを1.1倍
            score *= 1.1f; 
        }
        else if (combo >= 100) // コンボが100以上
        {
            // 加算するスコアを1.2倍
            score *= 1.2f;
        }
        if (starDirector.isStarMode == true) // お星様モードであれば
        {
            score += score * 0.5f; // 獲得スコアに対して50%を加算する
        }
        switch (state)
        {
            // stateによってステータスポイントを加算
            case STATE.RED:
                ultimateScore[1] += chain + 1f;
                break;
            case STATE.BLUE:
                ultimateScore[0] += chain + 1f;
                break;
            case STATE.YELLOW:
                ultimateScore[3] += chain + 1f;
                break;
            case STATE.WHITE:
                ultimateScore[2] += chain + 1f;
                break;
        }
        // トータルスコアに加算
        totalScore += score;
        // スコアを表示
        scoreText.text = string.Format("SCORE:\n{0:00000000}", totalScore);
        // コンボを表示
        comboText.text = string.Format("{0:0}combo", combo);
    }
    public void SetRank()
    {
        // 各パラメータにランクを付ける+totalUltimateScoreを算出
        for (int i = 0; i < 4; i++)
        {
            if (minRankS <= ultimateScore[i]) // 151以上ならSランク
            {
                rank[i] = "S";
            }
            else if (minRankA <= ultimateScore[i]) // 101以上150以下ならAランク
            {
                rank[i] = "A";
            }
            else if (minRankB <= ultimateScore[i]) // 61以上100以下ならAランク
            {
                rank[i] = "B";
            }
            else if (minRankC <= ultimateScore[i]) // 31以上60以下ならAランク
            {
                rank[i] = "C";
            }
            else  // 0以上30以下ならAランク
            {
                rank[i] = "D";
            }
            UltText[i].text = string.Format("{0:000}Pt", ultimateScore[i]); // 各究極スコアを書き出し
            totalUltimateScore += ultimateScore[i]; // 各スコアを加算する
        }

        // totalUltimateScoreのランクを付ける
        if (minTotalS <= totalUltimateScore) // 701以上ならSランク
        {
            rank[4] = "S";
        }
        else if (minTotalA <= totalUltimateScore) // 401以上700以下ならAランク
        {
            rank[4] = "A";
        }
        else if (minTotalB <= totalUltimateScore) // 241以上400以下ならBランク
        {
            rank[4] = "B";
        }
        else if (minTotalC <= totalUltimateScore) // 121以上240以下ならCランク
        {
            rank[4] = "C";
        }
        else  // 0以上120以下ならDランク
        {
            rank[4] = "D";
        }
        UltText[4].text = string.Format("{0:0000}Pt", totalUltimateScore); // 各究極スコアを書き出し

    }
}
