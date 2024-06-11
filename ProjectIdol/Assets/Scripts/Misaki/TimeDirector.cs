using UnityEngine;
using TMPro;

public partial class TimeDirector : MonoBehaviour
{

    /// --------関数一覧-------- ///
    /// -------public関数------- ///



    /// -------public関数------- ///
    /// -----protected関数------ ///



    /// -----protected関数------ ///
    /// ------private関数------- ///

    private void Start()
    {
        Application.targetFrameRate = 60; // 60fpsに固定
        aniScd = gameStartcd.GetComponent<Animator>(); // Animetorを格納する
        aniScd.GetComponent<GameStartCountDownScript>().CountDown();
    }

    private void FixedUpdate()
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
            starGauge.SetActive(false); // お星様ゲージを非表示にする
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
        // 追記　お星様モード時に時間を止めないように変更(2024-03-29)
        if (gameStart && !gameOver)
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

    /// ------private関数------- ///
    /// --------関数一覧-------- ///
}
public partial class TimeDirector
{
    /// --------変数一覧-------- ///
    /// -------public変数------- ///

    public bool gameStart = false; // ゲームがスタートしたかどうか

    /// -------public変数------- ///
    /// -----protected変数------ ///



    /// -----protected変数------ ///
    /// ------private変数------- ///

    private bool gameOver = false; // ゲームオーバーかどうか
    private Animator aniScd; // gameStartcdのanimator変数

    [SerializeField] private float cdTime; // カウントダウンタイム変数

    [SerializeField] private TextMeshProUGUI timeText; // カウントダウンテキスト表示用

    [SerializeField] private StarDirector starDirector; // StarDirector変数

    [SerializeField] private GameObject gameStartcd; // ゲームスタート時のカウントダウン変数
    [SerializeField] private GameObject graphBackImage; // グラフ背景用変数
    [SerializeField] private GameObject nextButton; // ボタン変数
    [SerializeField] private GameObject buttonFrame; // フレーム変数
    [SerializeField] private GameObject starGauge; // お星様ゲージ

    [SerializeField] private ButtonScript buttonScript; // ButtonScript変数

    [SerializeField] private ScoreDirector scoreDirector; // ScoreDirector変数

    [SerializeField] private DiamondMesh diamondFrame; // フレーム用DiamondMesh変数
    [SerializeField] private DiamondMesh diamondGraph; // グラフ用DiamondMesh変数

    [SerializeField] private AudioSource SEAudioSource; // SE用オーディオソース

    [SerializeField] private Animator animAi; // キャラクターのアニメーション用変数

    /// ------private変数------- ///
    /// -------プロパティ------- ///



    /// -------プロパティ------- ///
    /// --------変数一覧-------- ///
}
