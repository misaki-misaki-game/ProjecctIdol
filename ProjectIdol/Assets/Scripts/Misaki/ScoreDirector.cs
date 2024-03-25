using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreDirector : SignalScript
{
    bool isCombo = false; // コンボしているかどうか
    bool isScoreCount = false; // スコアをカウントしているかどうか
    bool isOverRank = false;
    bool isOverTotalRank = false;
    float showScore = 0f; // 加算されるスコア変数
    float timer = 0; // 加算する時間変数
    public float score = 0; // スコア計算用変数
    public float comboTime = 0; // コンボ持続タイム変数
    public float scoreBaseValue = 50; // 基礎スコア変数
    public float comboDurationTime = 1f; // コンボ継続時間変数
    public float totalScore = 0; // トータルスコア変数
    public float totalUltimateScore; // 究極トータルスコア
    public float scoreAnimTime = 2f; // スコアのカウントアニメーションの時間変数

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

    public int combo = 0; // コンボ変数
    public enum UltimateType // スコア名称列挙型
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
    public TextMeshProUGUI UltRankText; // ランクテキスト表示用
    public TextMeshProUGUI scoreText; // スコアテキスト表示用
    public TextMeshProUGUI scoreShowText; // スコアテキスト表示用
    public TextMeshProUGUI comboText; // コンボテキスト表示用
    public StarDirector starDirector; // StarDirector変数
    public GameObject nextButton; // ボタン変数
    public GameObject resultObject; // リザルトオブジェクト変数
    public GameObject graphObject; // グラフオブジェクト変数
    public GameObject currentScoreObject; // 現在のスコアオブジェクト変数
    public GameObject rankingObject; // ランキングオブジェクト変数
    public AudioSource SEAudioSource; // SE用オーディオソース
    public AudioClip SEAudioClip; // 決定音クリップ
    public DataManager dataManager; // DataManager変数
    public Ranking ranking; // Ranking変数
    public Image Gauge; // コンボゲージ
    public List<STATE> detonationStates; // 誘爆したシグナルのstate格納用

    // Start is called before the first frame update
    void Start()
    {
        InitializationUI(); // コンボとトータルスコアを初期化する
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // コンボ持続の処理
        ContinuationCombo();
        // トータルスコアが確定したらスコアを加算するアニメーションを呼び出す
        CountShowScore(totalScore, scoreAnimTime);
    }
    /// <summary>
    /// コンボとトータルスコアを初期化する関数
    /// </summary>
    private void InitializationUI()
    {
        // トータルスコアを初期化
        totalScore = default;
        // コンボを初期化
        combo = default;
        // スコアを表示
        scoreText.text = string.Format("SCORE:\n{0:00000000}", totalScore);
        // コンボを表示
        comboText.text = string.Format("{0:0}combo", combo);
    }
    /// <summary>
    /// コンボ持続の処理関数
    /// </summary>
    private void ContinuationCombo()
    {
        if (isCombo) // コンボしているのであれば
        {
            // コンボ持続時間を減らす
            comboTime -= Time.deltaTime;
            Gauge.fillAmount -= Time.deltaTime / comboDurationTime; // ゲージを減少させる(2秒間)
            // コンボを中断する処理
            if (comboTime < 0) // コンボ持続時間が0なら
            {
                // 0を代入
                comboTime = default;
                // isCombo変数をfalseにする
                isCombo = default;
                // コンボ数をリセット
                combo = default;
                // コンボを表示
                comboText.text = string.Format("{0:0}combo", combo);
                // コンボゲージを0にする
                Gauge.fillAmount = 0;
            }
        }
    }
    /// <summary>
    /// ゲットスコア関数
    /// </summary>
    /// <param name="chain">消したシグナルのチェイン数</param>
    /// <param name="isChain">消したシグナルがチェインしているかどうか</param>
    /// <param name="state">消したシグナルのステータス</param>
    public void GetScore(float chain,bool isChain, STATE state)
    {
        // 基礎スコアを加算
        score = AddBaseValue(chain, isChain);
        // コンボによる倍率をかける
        score = MultiplyCombo(score);
        // スターモードによる倍率をかける
        score = MultiplyStarMode(score);
        // 究極スコアを計算する
        CalculateUltimateScore(chain, state);
        // トータルスコアに加算
        totalScore += score;
        // スコアを表示
        scoreText.text = string.Format("SCORE:\n{0:00000000}", totalScore);
        // コンボを表示
        comboText.text = string.Format("{0:0}combo", combo);
    }
    /// <summary>
    /// 基礎スコアを計算する関数
    /// </summary>
    /// <param name="chain">消したシグナルのチェイン数</param>
    /// <param name="isChain">消したシグナルがチェインしているかどうか</param>
    /// <returns>計算後のスコア</returns>
    private float AddBaseValue(float chain, bool isChain)
    {
        float score;
        if (isChain) // チェインが発生していれば
        {
            // コンボを1加算
            combo += 1;
            // コンボ持続時間をリセット
            comboTime = comboDurationTime;
            // コンボゲージをリセット
            Gauge.fillAmount = 1;
            // isCombo変数をtrueにする
            isCombo = true;
            if (starDirector.starState == StarDirector.StarState.StarMode) // お星様モードであれば
            {
                score = scoreBaseValue * 1.5f * (2 * chain); // 基礎スコア50を1.5倍
            }
            else
            {
                // 50に(chain×2)倍
                score = scoreBaseValue * (2 * chain);
            }
        }
        else // チェインが発生していなければ
        {
            score = scoreBaseValue; // 50を加算
        }
        return score;
    }
    /// <summary>
    /// コンボ数による倍率を掛ける関数
    /// </summary>
    /// <param name="score">基礎スコア計算後のスコア</param>
    /// <returns>コンボ倍率計算後のスコア</returns>
    private float MultiplyCombo(float score)
    {
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
        return score;
    }
    /// <summary>
    /// スターモードによる倍率を掛ける
    /// </summary>
    /// <param name="score">コンボ計算後のスコア</param>
    /// <returns>スターモード倍率計算後のスコア</returns>
    private float MultiplyStarMode(float score) 
    {
        if (starDirector.starState == StarDirector.StarState.StarMode) // お星様モードであれば
        {
            score += score * 0.5f; // 獲得スコアに対して50%を加算する
        }

        return score;
    }
    /// <summary>
    /// アルティメットスコアを計算する関数
    /// </summary>
    /// <param name="chain">消したシグナルのチェイン数</param>
    /// <param name="state">消したシグナルのステータス</param>
    private void CalculateUltimateScore(float chain, STATE state)
    {
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
            case STATE.SPECIAL:
                // X字ボムによって壊したシグナルのstateによってステータスポイントを加算
                for (int i = 0; i < detonationStates.Count; i++)
                {
                    switch (detonationStates[i])
                    {
                        case STATE.RED:
                            ultimateScore[1] += 1f;
                            break;
                        case STATE.BLUE:
                            ultimateScore[0] += 1f;
                            break;
                        case STATE.YELLOW:
                            ultimateScore[3] += 1f;
                            break;
                        case STATE.WHITE:
                            ultimateScore[2] += 1f;
                            break;
                    }
                }
                detonationStates = new List<STATE>(); // リストを空にする
                break;
        }
    }
    /// <summary>
    /// 各ステータスのランクをつける関数
    /// </summary>
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
            else if (minRankB <= ultimateScore[i]) // 61以上100以下ならBランク
            {
                rank[i] = "B";
            }
            else if (minRankC <= ultimateScore[i]) // 31以上60以下ならCランク
            {
                rank[i] = "C";
            }
            else  // 0以上30以下ならDランク
            {
                rank[i] = "D";
            }
            UltText[i].text = string.Format(rank[i] + "  {0:000}Pt", ultimateScore[i]); // 各究極スコアを書き出し
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
        UltRankText.text = string.Format(rank[4]); // ランク書き出し
        CheckMission(); // ミッションをクリアしたかの確認
        RecordUpdate(); // ハイスコアの更新
    }
    /// <summary>
    /// ミッションをクリアしたかの確認する関数
    /// </summary>
    private void CheckMission()
    {
        OverRankA(); // 究極パラメータがAランクを超えているかをチェック
        // スターモードを2回以上クリアしたか
        if (starDirector.starCount > 1)
        {
            totalScore += 20000;
            Debug.Log("ミッション1クリア");
        }
        // いずれかの究極パラメータがAランク以上か
        if (isOverRank)
        {
            totalScore += 20000;
            Debug.Log("ミッション2クリア");
        }
        // 総究極パラメータがAランク以上か
        if (isOverTotalRank)
        {
            totalScore += 20000;
            Debug.Log("ミッション3クリア");
        }
        // スコアを表示
        scoreText.text = string.Format("SCORE:\n{0:00000000}", totalScore);
    }
    /// <summary>
    /// 究極パラメータがAランクを超えているかをチェックする関数
    /// </summary>
    private void OverRankA()
    {
        // 各究極パラメータがAランクを超えているかをチェック　超えていればisOverRankまたはisOverTotalRankがtrueになる
        for (int i = 0; i < ultimateScore.Length; i++)
        {
            if (minRankA <= ultimateScore[i])
            {
                isOverRank = true;
                break;
            }
        }
        if (minTotalA <= totalUltimateScore) isOverTotalRank = true;
    }
    /// <summary>
    /// ハイスコアの更新関数
    /// </summary>
    private void RecordUpdate() 
    {
        Debug.Log("RecordUpdate関数に入りました");
        if (totalScore > dataManager.data.puzzleRanking[0]) // 今回のスコアがハイスコアを超えれば
        {
            dataManager.data.puzzleHighScore = totalScore; // ハイスコアを更新する
            dataManager.data.puzzleHighScoreRank = rank[4]; // ランクを更新する
            dataManager.Save(dataManager.data); // セーブする
        }
    }
    /// <summary>
    /// スコアを加算するアニメーション関数
    /// </summary>
    /// <param name="totalScore">今回のゲームのトータルスコア</param>
    /// <param name="countTime">加算するアニメーションの秒数</param>
    void CountShowScore(float totalScore, float countTime)
    {
        if (!isScoreCount) return; // isScoreCountがfalseならリターンを返す
        if (showScore < totalScore)
        {
            timer += Time.deltaTime; // deltaTimeを加算する
            float progress = Mathf.Clamp01(timer / countTime); // timer/countTimeの割合を代入
            showScore = Mathf.Lerp(0, totalScore, progress); // 0からtotalScoreまでの値に対して割合(progress)を代入
            scoreShowText.text = string.Format("{0:00000000}", showScore); // スコアを表示
        }
        // showScoreがtotalScoreを超えたら表記ずれしないようにtotalScoreを表示する
        else
        {
            scoreShowText.text = string.Format("{0:00000000}", totalScore); // スコアを表示
            isScoreCount = false; // CountShowScoreを呼び出さないようにfalseにする
        }
    }
    /// <summary>
    /// 究極パラメータを表示する関数
    /// </summary>
    public void ShowUltScore()
    {
        SEAudioSource.clip = SEAudioClip; // SEAudioClipを代入して次のボタンのときにも同じ音を鳴らす
        SEAudioSource.Play(); // SEを鳴らす
        nextButton.SetActive(false); // ボタンを非表示
        graphObject.SetActive(false); // グラフを非表示
        resultObject.SetActive(true); // リザルトを表示
    }
    /// <summary>
    /// スコアを表示する関数
    /// </summary>
    public void ShowScore()
    {
        SEAudioSource.clip = SEAudioClip; // SEAudioClipを代入して次のボタンのときにも同じ音を鳴らす
        SEAudioSource.Play(); // SEを鳴らす
        if(resultObject) resultObject.SetActive(false); // リザルトを非表示
        currentScoreObject.SetActive(true); // スコア画面を表示
        isScoreCount = true; // isScoreCountをtrueにしてCountShowScoreを呼び出すようにする
    }
    /// <summary>
    /// ランキングを表示する関数
    /// </summary>
    public void ShowRanking()
    {
        SEAudioSource.clip = SEAudioClip; // SEAudioClipを代入して次のボタンのときにも同じ音を鳴らす
        SEAudioSource.Play(); // SEを鳴らす
        currentScoreObject.SetActive(false); // スコア画面を非表示
        rankingObject.SetActive(true); // ランキング画面を表示
        ranking.CheckRankin(dataManager.data.puzzleRanking, totalScore); // ランキングに入っているかのチェック
    }
}