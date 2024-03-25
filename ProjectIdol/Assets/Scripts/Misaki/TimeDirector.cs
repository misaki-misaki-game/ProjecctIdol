using UnityEngine;
using TMPro;

public class TimeDirector : MonoBehaviour
{
    bool gameOver = false; // ゲームオーバーかどうか
    public bool gameStart = false; // ゲームがスタートしたかどうか
    public float cdTime; // カウントダウンタイム変数
    public TextMeshProUGUI timeText; // カウントダウンテキスト表示用
    public StarDirector starDirector; // StarDirector変数
    public GameObject gameStartcd; // ゲームスタート時のカウントダウン変数
    public GameObject graphBackImage; // グラフ背景用変数
    public GameObject nextButton; // ボタン変数
    public GameObject buttonFrame; // フレーム変数
    public ButtonScript buttonScript; // ButtonScript変数
    public ScoreDirector scoreDirector; // ScoreDirector変数
    public DiamondMesh diamondFrame; // フレーム用DiamondMesh変数
    public DiamondMesh diamondGraph; // グラフ用DiamondMesh変数
    public Animator animAi; // キャラクターのアニメーション用変数
    public AudioSource SEAudioSource; // SE用オーディオソース
    Animator aniScd; // gameStartcdのanimator変数

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60; // 60fpsに固定
        aniScd = gameStartcd.GetComponent<Animator>(); // Animetorを格納する
        aniScd.GetComponent<GameStartCountDownScript>().CountDown();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        TimeCountDown(); // 時間のカウントダウンを行う
        GameOver(); // ゲーム終了の処理を行う
    }
    /// <summary>
    /// ゲーム終了の処理関数
    /// </summary>
    private void GameOver() 
    {
        if (gameOver) // ゲームオーバーであれば
        {
            this.gameOver = false; // falseにすることでこのif文を1回だけ呼び出す
            this.gameStart = false; // 上記を行うと上のif文が呼び出されるのでそれを行わないためにfalseにする
            animAi.SetTrigger("isDanceEnd"); // アイのアニメーションを終了する
            buttonScript.ButtonsDestroy(); // ボタンを全て破壊する
            buttonFrame.SetActive(false); // フレームを非表示する
            scoreDirector.SetRank(); // 各スコアに応じてランクを設定する
            graphBackImage.SetActive(true); // グラフ用背景を表示
            /// diamondFrameとdiamondGraphのセットアップを行う ///
            diamondFrame.SetUp();
            diamondGraph.SetUp();
            nextButton.SetActive(true); // ボタンを表示する
        }
    }
    /// <summary>
    /// 時間のカウントダウンを行う
    /// </summary>
    private void TimeCountDown()
    {
        // ゲームがスタート,かつ,ゲームオーバーになっていない,かつ,通常モードではないのであれば
        if (gameStart && !gameOver && starDirector.starState == StarDirector.StarState.NormalMode)
        {
            // カウントダウンタイムを表示
            timeText.text = string.Format("TIME:{0:0}s", cdTime);
            // 経過時刻を引いていく
            cdTime -= Time.deltaTime;
            // 0秒以下になったらカウントダウンタイムを0に固定し,ゲームオーバーにする
            if (cdTime <= 0)
            {
                cdTime = 0;
                gameOver = true;
            }
        }
    }
}
