using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;
using static SignalScript;

public class StarDirector : MonoBehaviour
{
    public int starCount = 0; // お星様モードになった回数
    public enum StarState
    {
        NormalMode, // 通常状態 0
        StarMode // スターモード 1
    }
    public enum BackImageState
    {
        StageNormal, // 通常ステージ背景 0
        StageStar, // スターステージ背景 1
        ArenaNormal, // 通常アリーナ背景 2
        ArenaStar // スターアリーナ背景 3
    }
    public StarState starState = StarState.NormalMode; // StarState変数
    public Image Gauge; // お星様ゲージ
    public Animator animAi; // アイのアニメーション用変数
    [EnumIndex(typeof(BackImageState))]
    public Sprite[] backImages = new Sprite[4]; // 背景画像配列
    public GameObject[] backImageObjects = new GameObject[2]; // 背景オブジェクト配列

    // Start is called before the first frame update
    void Start()
    {
        // 初期設定
        Gauge.fillAmount = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ShowStarMode(); // スターモードの演出を行う
    }
    private void ShowStarMode() // スターモードの演出を行う関数
    {
        if (starState == StarState.StarMode) // スターモードの時
        {
            animAi.SetBool("isStarMode", true); // アイのスターモードアニメーションをスタートする
            backImageObjects[0].GetComponent<Image>().sprite = backImages[1]; // ステージ背景をスターモードにする
            backImageObjects[1].GetComponent<Image>().sprite = backImages[3]; // 観客背景をスターモードにする
            Gauge.fillAmount -= Time.deltaTime / 10; // ゲージを減少させる(10秒間)
            if (Gauge.fillAmount <= 0) // ゲージがなくなったら
            {
                backImageObjects[0].GetComponent<Image>().sprite = backImages[0]; // ステージ背景を通常モードにする
                backImageObjects[1].GetComponent<Image>().sprite = backImages[2]; // 観客背景を通常モードにする
                animAi.SetBool("isStarMode", false); // アイのスターモードアニメーションを終了する
                starState = StarState.NormalMode; // 通常モードに変更
                starCount += 1; // お星様モードを加算する
            }
        }
    }
    public void GetStar(float chain) // ゲットスター関数
    {
        if (starState == StarState.NormalMode) // 通常モードのとき
        {
            // お星様ゲージの増加を処理
            if (chain >= 1)
            {
                // チェインの値分ゲージを増加させる
                Gauge.fillAmount += chain / 100;
            }
            // お星様ゲージが1になったら
            if (Gauge.fillAmount == 1)
            {
                starState = StarState.StarMode; // スターモードに変更
            }
        }
    }
}
