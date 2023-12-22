using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStartCountDownScript : MonoBehaviour
{
    public TextMeshProUGUI gameStartText; // ゲームスタート時のカウントダウンテキスト表示用
    Animator aniScd; // gameStartcdのanimator変数
    public TimeDirector timeDirector; // timeDirector変数
    public GameObject gameStartcd; // ゲームスタート時のカウントダウン変数
    public ButtonScript buttonScript; // ButtonScript変数
    public Animator animAi; // アイのアニメーション用変数

    void Start()
    {
        aniScd = gameObject.GetComponent<Animator>(); // Animatorを格納
    }

    public void Count2()
    {
        gameStartText.text = string.Format("2"); // テキストの文字を2にする
        aniScd.SetBool("isCount3", true); // AnimatorのisCount3を変更
    }
    public void Count1()
    {
        gameStartText.text = string.Format("1"); // テキストの文字を1にする
        aniScd.SetBool("isCount2", true); // AnimatorのisCount2を変更
    }
    public void Count0()
    {
        gameStartText.text = string.Format("Start"); // テキストの文字をStartにする
        aniScd.SetBool("isCount1", true); // AnimatorのisCount1を変更
    }
    public void GameStart()
    {
        timeDirector.gameStart = true; // 制限時間をスタートする
        buttonScript.gameStart = true; // ボタンのクリックを許可する
        animAi.SetTrigger("isDanceStart"); // アイのアニメーションをスタートする
        gameStartcd.SetActive(false); // カウントキャンパスを非表示にする
    }
}
