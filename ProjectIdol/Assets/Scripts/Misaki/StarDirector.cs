using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

public class StarDirector : MonoBehaviour
{
    public bool isStarMode; // お星様モードかどうか
    public Image Gauge; // お星様ゲージ
    public Animator animAi; // アイのアニメーション用変数

    // Start is called before the first frame update
    void Start()
    {
        // 初期設定
        Gauge.fillAmount = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isStarMode) // isStarModeが真のとき
        {
            animAi.SetBool("isStarMode", true); // アイのスターモードアニメーションをスタートする
            Gauge.fillAmount -= Time.deltaTime / 10; // ゲージを減少させる(10秒間)
            if (Gauge.fillAmount <= 0) // ゲージがなくなったら
            {
                animAi.SetBool("isStarMode", false); // アイのスターモードアニメーションを終了する
                isStarMode = false; // isStarModeが偽のとき
            }
        }
    }

    public void GetStar(float chain) // ゲットスター関数
    {
        if (!isStarMode) // isStarModeが偽のとき
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
                // isStarModeを真にする
                isStarMode = true;
            }
        }
    }
}
