using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UiManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI comboText;
    [SerializeField] TextMeshProUGUI timeText;

    int score = 0;
    int bonusScore = 50;
    int combo = 0;
    float time = 180;
    float star = 0;

    public void Update()
    {
        time -= Time.deltaTime;

        //時間が0秒になるとリザルト画面に移動する
        if (time <= 0)
        {
            //
        }

        scoreText.text = "Score:" + score.ToString();
        comboText.text = "Combo:" + combo.ToString();
        timeText.text = "Time: " + time.ToString();
    }


    public void AddScore(int point)
    {
        score += point;
        bonusScore++;
    }

    public void AddCombo()
    {
        combo++;

        //ボーナス
        if (combo % 50 == 0)
        {
            score += bonusScore * (combo / 50 * 10) / 100;
        }
    }

    public void AddStar(int gauge)
    {
        star += gauge;

        //お星さまモード
        if (star <= 100)
        {
            Debug.Log("お星さまモード突入");
        }
        else if (star >= 100)
        {
            Debug.Log("お星さまモード終了");
        }
    }

    public void NoteMiss()
    {
        if (score <= 0 && star <= 0)
        {
            score = 0;
            combo = 0;
            star = 0;
        }
        else
        {
            score -= 25;
            combo = 0;
            star -= -1;
        }
    }

}
