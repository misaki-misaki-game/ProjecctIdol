using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TimeDirector : MonoBehaviour
{
    bool gameOver = false; // ゲームオーバーかどうか
    public bool gameStart = false; // ゲームがスタートしたかどうか
    public float cdTime; // カウントダウンタイム変数
    public TextMeshProUGUI timeText; // カウントダウンテキスト表示用
    public StarDirector starDirector; // StarDirector変数
    public GameObject gameStartcd; // ゲームスタート時のカウントダウン変数
    public GameObject graphBackImage; // グラフ背景用変数
    public ButtonScript buttonScript; // ButtonScript変数
    public ScoreDirector scoreDirector; // ScoreDirector変数
    public DiamondMesh diamondFrame; // フレーム用DiamondMesh変数
    public DiamondMesh diamondGraph; // グラフ用DiamondMesh変数
    public Animator animAi; // アイのアニメーション用変数
    Animator aniScd; // gameStartcdのanimator変数

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60; // 60fpsに固定
        aniScd = gameStartcd.GetComponent<Animator>(); // Animetorを格納する
        Count3(); // ゲームが始まるまでのカウントダウンをスタートする
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // ゲームがスタート,かつ,ゲームオーバーになっていない,かつ,スターモードではないのであれば
        if (gameStart && !gameOver && starDirector.isStarMode == false)
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
        if (gameOver) // ゲームオーバーであれば
        {
            this.gameOver = false; // falseにすることでこのif文を1回だけ呼び出す
            this.gameStart = false; // 上記を行うと上のif文が呼び出されるのでそれを行わないためにfalseにする
            animAi.SetTrigger("isDanceEnd"); // アイのアニメーションを終了する
            buttonScript.gameStart = false; // falseにしてボタンをクリックできないようにする
            scoreDirector.SetRank(); // 各スコアに応じてランクを設定する
            graphBackImage.SetActive(true); // グラフ用背景を表示
            // diamondFrameとdiamondGraphのセットアップを行う
            diamondFrame.SetUp();
            diamondGraph.SetUp();
        }
    }
    public void Count3()
    {
        gameStartcd.SetActive(true); // gameStartcdを表示する
        aniScd.SetBool("isCountdownStart", true); // animatorのisCountdownStart変数を真にする
    }
}
