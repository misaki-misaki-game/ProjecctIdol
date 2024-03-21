using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Klak.Timeline.Midi;
using System.Drawing;
using static ScoreDirector;
using static RhythmDiamondMesh;
using System.Net.Http.Headers;


public class UiManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;         //ゲーム画面の左上に表示するスコアテキスト
    [SerializeField] TextMeshProUGUI lastScoreText;     //ゲームが終了したときに表示するためのスコアテキスト
    [SerializeField] TextMeshProUGUI comboText;         //ゲーム画面の右上に表示するためのコンボテキスト
    [SerializeField] TextMeshProUGUI timeText;          //タイムテキスト　ゲーム画面には表示していないが、今後使う可能性があるため残している
    [SerializeField] TextMeshProUGUI rankText;          //ゲーム終了時にランクを表示するためのテキスト

    [SerializeField] DataManager dataManager;           // DataManager変数
    [SerializeField] RhythmDiamondMesh rhythmDiamondMesh;

    //リザルト関係
    [SerializeField] GameObject currentScoreObject;     // 現在のスコアオブジェクト変数
    [SerializeField] GameObject rankingObject;          // ランキングオブジェクト変数
    [SerializeField] AudioSource SEAudioSource;         // SE用オーディオソース
    [SerializeField] AudioClip SEAudioClip;             // 決定音クリップ
    [SerializeField] Ranking ranking;                   // Ranking変数
    public float scoreAnimTime = 2f;                    // スコアのカウントアニメーションの時間変数
    public TextMeshProUGUI scoreShowText;               // スコアテキスト表示用
    bool isScoreCount = false;                          // スコアをカウントしているかどうか
    float showScore = 0f;                               // 加算されるスコア変数
    float timer = 0;                                    // 加算する時間変数



    //岬さんのscoreDirectorからもってきた
    [EnumIndex(typeof(UltimateType))]
    public string[] rank = new string[5]; // ultimateScoreの要素数と同じ　[D,C,B,A,S]のどれかにする [5]は究極トータルスコア


    [SerializeField] GameObject resultPanel;
    //public Animator AiAnimation;

    float score = 0;
    float bonusScore;
    int combo = 0;
    float time = 180;

    [SerializeField] TextMeshProUGUI pointText;
    [SerializeField] TextMeshProUGUI totalRankText;
    [SerializeField] int bluePoints = 0;
    [SerializeField] int redPoints = 0;
    [SerializeField] int whitePoints = 0;
    [SerializeField] int yellowPoints = 0;

    int totalPoints = 0;
    [SerializeField] int totalRankPointS = 506;
    [SerializeField] int totalRankPointA = 369;
    [SerializeField] int totalRankPointB = 280;
    [SerializeField] int totalRankPointC =  112;
    [SerializeField] int totalRankPointD = 56;
    string colorPoint;

    [SerializeField] int blueRankPointS=132;
    [SerializeField] int blueRankPointA=94;
    [SerializeField] int blueRankPointB=78;
    [SerializeField] int blueRankPointC=36;
    [SerializeField] int blueRankPointD=18;

    [SerializeField] int redRankPointS=128;
    [SerializeField] int redRankPointA=96;
    [SerializeField] int redRankPointB=72;
    [SerializeField] int redRankPointC=28;
    [SerializeField] int redRankPointD=14;

    [SerializeField] int whiteRankPointS=120;
    [SerializeField] int whiteRankPointA=83;
    [SerializeField] int whiteRankPointB=60;
    [SerializeField] int whiteRankPointC=20;
    [SerializeField] int whiteRankPointD=10;

    [SerializeField] int yellowRankPointS=126;
    [SerializeField] int yellowRankPointA=96;
    [SerializeField] int yellowRankPointB=70;
    [SerializeField] int yellowRankPointC=28;
    [SerializeField] int yellowRankPointD=14;

    public string blueRank;
    public string redRank;
    public string whiteRank;
    public string yellowRank;


    public void FixedUpdate()
    {
        time -= Time.deltaTime;

        scoreText.text = "Score\n" + score.ToString();
        lastScoreText.text = "Score:" + score.ToString();
        comboText.text = "Combo:" + combo.ToString();
        //timeText.text = "Time :" + time.ToString();
        //timeText.text = string.Format("Time :{0:0}s", time);

        if (time <= 120.5f && time >= 98)
        {
            //Debug.Log("アイのアニメーション再生する");
            //AiAnimation.Play("Miss_Anim");
        }
        BonusCalculate();
        // トータルスコアが確定したらスコアを加算するアニメーションを呼び出す
        CountShowScore(score, scoreAnimTime);
    }

    public void AddScore(int point)
    {
        //参照先でpointに数値を入れるとその数値分scoreにプラスすることができる
        //JudgmentAreaスクリプトのSignalJudgmentで使用している
        score += point;
    }
    public void AddCombo()
    {
        //参照先でcomboを1ずつプラスすることができる
        //JudgmentAreaスクリプトのSignalJudgmentで使用している
        combo++;
    }
    public void NoteMiss()
    {
        //参照先でシグナルを消すのを失敗したときに使用する
        //JudgmentAreaスクリプトのSignalJudgmentで使用している
        if (score <= 0 && combo <= 0)
        {
        //scoreとcomboに数値が入ってないときは減らさないようにする
            score = 0;
            combo = 0;
        }
        else
        {
            score -= 25;
            combo = 0;
        }
    }
    public void BonusCalculate()
    {
        //コンボ数に合わせてボーナススコアを獲得する
        if (combo <= 50 && combo >= 99)
        {
            // スコアに現在のスコアの10％を加算する
            float bonusScore = score * 10 / 100;
            score += bonusScore;
        }
        else if (combo <= 100 && combo >= 149)
        {
            // スコアに現在のスコアの20％を加算する
            float bonusScore = score * 20 / 100;
            score += bonusScore;
        }
        else if (combo <= 150 && combo >= 199)
        {
            // スコアに現在のスコアの30％を加算する
            float bonusScore = score * 30 / 100;
            score += bonusScore;
        }
        else if (combo <= 200 && combo >= 249)
        {
            // スコアに現在のスコアの40％を加算する
            float bonusScore = score * 40 / 100;
            score += bonusScore;
        }
        else if (combo <= 250 && combo >= 299)
        {
            // スコアに現在のスコアの50％を加算する
            float bonusScore = score * 50 / 100;
            score += bonusScore;
        }
    }
    public void AddBluePoint (int colorPoint)
    {
        bluePoints += colorPoint;
    }
    public void AddRedPoint (int colorPoint)
    {
        redPoints += colorPoint;
    }
    public void AddWhitePoint (int colorPoint)
    {
        whitePoints += colorPoint;
    }
    public void AddYellowPoint (int colorPoint)
    {
        yellowPoints += colorPoint;
    }

    //各色のポイントのランクを表示する
    public string EvaluateRank(int colorPoint, string tag)
    {
    //パラメーターの評価を行う関数を追加
        string rank = "";
        switch (tag)
        {
            case "BlueNotes":
                if      (colorPoint <= blueRankPointS) rank = "S";
                else if (colorPoint <= blueRankPointA) rank = "A";
                else if (colorPoint <= blueRankPointB) rank = "B";
                else if (colorPoint <= blueRankPointC) rank = "C";
                else if (colorPoint <= blueRankPointD) rank = "D";
                break;

            case "RedNotes":
                if      (colorPoint <= redRankPointS) rank = "S";
                else if (colorPoint <= redRankPointA) rank = "A";
                else if (colorPoint <= redRankPointB) rank = "B";
                else if (colorPoint <= redRankPointC) rank = "C";
                else if (colorPoint <= redRankPointD) rank = "D";
                break;

            case "WhiteNotes":
                if      (colorPoint <= whiteRankPointS) rank = "S";
                else if (colorPoint <= whiteRankPointA) rank = "A";
                else if (colorPoint <= whiteRankPointB) rank = "B";
                else if (colorPoint <= whiteRankPointC) rank = "C";
                else if (colorPoint <= whiteRankPointD) rank = "D";
                break;

            case "YellowNotes":
                if      (colorPoint <= yellowRankPointS) rank = "S";
                else if (colorPoint <= yellowRankPointA) rank = "A";
                else if (colorPoint <= yellowRankPointB) rank = "B";
                else if (colorPoint <= yellowRankPointC) rank = "C";
                else if (colorPoint <= yellowRankPointD) rank = "D";
                break;
        }
        return rank;
    }

    //ポイントの合計のランクを表示する
    public void PointRank()
    {
        Debug.Log("rank");
        //各シグナルのポイントの合計
        totalPoints = bluePoints + redPoints + whitePoints + yellowPoints;
        string rank = "";
        if      (0               < totalPoints && totalPoints <= totalRankPointD) rank = "-D-";
        else if (totalRankPointD < totalPoints && totalPoints <= totalRankPointC) rank = "-C-";
        else if (totalRankPointC < totalPoints && totalPoints <= totalRankPointB) rank = "-B-";
        else if (totalRankPointB < totalPoints && totalPoints <= totalRankPointA) rank = "-A-";
        else if (totalRankPointA < totalPoints && totalPoints <= totalRankPointS) rank = "-S-";

        totalRankText.text= rank;
    }
    public void ShowResult()
    {
        //各シグナルのポイントの合計
        totalPoints = bluePoints + redPoints + whitePoints + yellowPoints;

        // ゲーム終了時のランクを表示するメソッド
        // 各パラメーターのランクを取得

        blueRank = EvaluateRank(bluePoints, "BlueNotes");
        redRank = EvaluateRank(redPoints, "RedNotes");
        whiteRank = EvaluateRank(whitePoints, "WhiteNotes");
        yellowRank = EvaluateRank(yellowPoints, "YellowNotes");

        // ランクをテキストに表示
        pointText.text =   blueRank + "  青シグナル: " + bluePoints   + "pt\n" +
                           redRank  + "  赤シグナル: " + redPoints    + "pt\n" +
                          whiteRank + "  白シグナル: " + whitePoints  + "pt\n" +
                         yellowRank + "  黄シグナル: " + yellowPoints + "pt\n" +
                         "最終ポイント" + totalPoints;
    }

    /// <summary>
    /// スコアを加算するアニメーション関数
    /// </summary>
    /// <param name="totalScore">今回のゲームのトータルスコア</param>
    /// <param name="countTime">加算するアニメーションの秒数</param>
    void CountShowScore(float totalScore, float countTime)
    {
        if (!isScoreCount) return;                                              // isScoreCountがfalseならリターンを返す
        if (showScore < totalScore)
        {
            timer += Time.deltaTime;                                            // deltaTimeを加算する
            float progress = Mathf.Clamp01(timer / countTime);                  // timer/countTimeの割合を代入
            showScore = Mathf.Lerp(0, totalScore, progress);                    // 0からtotalScoreまでの値に対して割合(progress)を代入
            scoreShowText.text = string.Format("{0:00000000}", showScore);      // スコアを表示
        }
        // showScoreがtotalScoreを超えたら表記ずれしないようにtotalScoreを表示する
        else
        {
            scoreShowText.text = string.Format("{0:00000000}", totalScore);     // スコアを表示
            isScoreCount = false;                                               // CountShowScoreを呼び出さないようにfalseにする
        }
    }
    public void ShowScore()
    {
        SEAudioSource.clip = SEAudioClip;       // SEAudioClipを代入して次のボタンのときにも同じ音を鳴らす
        SEAudioSource.Play();                   // SEを鳴らす
        currentScoreObject.SetActive(true);     // スコア画面を表示
        isScoreCount = true;                    // isScoreCountをtrueにしてCountShowScoreを呼び出すようにする
    }
    public void ShowRanking()
    {
        SEAudioSource.clip = SEAudioClip;                               // SEAudioClipを代入して次のボタンのときにも同じ音を鳴らす
        SEAudioSource.Play();                                           // SEを鳴らす
        currentScoreObject.SetActive(false);                            // スコア画面を非表示
        rankingObject.SetActive(true);                                  // ランキング画面を表示
        ranking.CheckRankin(dataManager.data.rhythmRanking, score);     // ランキングに入っているかのチェック
    }
}
