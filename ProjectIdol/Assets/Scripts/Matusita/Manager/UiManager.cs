using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Klak.Timeline.Midi;

public class UiManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI lastScoreText;
    [SerializeField] TextMeshProUGUI comboText;
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] TextMeshProUGUI rankText;

    //public Animator AiAnimation;

    int score = 0;
    int bonusScore;
    int combo = 0;
    float time = 180;
    float star = 0;

    public void Update()
    {
        time -= Time.deltaTime;

        scoreText.text = "Score\n" + score.ToString();
        lastScoreText.text = "Score:" + score.ToString();
        comboText.text = "Combo:" + combo.ToString();
        //timeText.text = "Time :" + time.ToString();
        timeText.text = string.Format("Time :{0:0}s", time);

        if (time <= 120.5f && time >= 98)
        {
            Debug.Log("アイのアニメーション再生する");
            //AiAnimation.Play("Miss_Anim");
        }
        BonusCalculate();
    }


    public void AddScore(int point)
    {
        score += point;
    }

    public void AddCombo()
    {
        combo++;
    }

    public void NoteMiss()
    {
        if (score <= 0 && star <= 0)
        {
            score = 0;
            combo = 0;
            star = 0;
        }
        //else
        //{
        //    score -= 25;
        //    combo = 0;
        //    star -= -1;
        //}
    }

    public void BonusCalculate()
    {
        if (combo <= 50 && combo >= 99)
        {
            // スコアに現在のスコアの10％を加算する
            int bonusScore = score * 10 / 100;
            score += bonusScore;
        }
        else if (combo <= 100 && combo >= 149)
        {
            // スコアに現在のスコアの20％を加算する
            int bonusScore = score * 20 / 100;
            score += bonusScore;
        }
        else if (combo <= 150 && combo >= 199)
        {
            // スコアに現在のスコアの30％を加算する
            int bonusScore = score * 30 / 100;
            score += bonusScore;
        }
        else if (combo <= 200 && combo >= 249)
        {
            // スコアに現在のスコアの40％を加算する
            int bonusScore = score * 40 / 100;
            score += bonusScore;
        }
        else if (combo <= 250 && combo >= 299)
        {
            // スコアに現在のスコアの50％を加算する
            int bonusScore = score * 50 / 100;
            score += bonusScore;
        }
        else if (combo <= 300 && combo >= 349)
        {
            // スコアに現在のスコアの60％を加算する
            int bonusScore = score * 60 / 100;
            score += bonusScore;
        }
        else if (combo <= 350 && combo >= 399)
        {
            // スコアに現在のスコアの70％を加算する
            int bonusScore = score * 70 / 100;
            score += bonusScore;
        }
        else if (combo <= 400 && combo >= 449)
        {
            // スコアに現在のスコアの80％を加算する
            int bonusScore = score * 80 / 100;
            score += bonusScore;
        }
        else if (combo <= 450 && combo >= 499)
        {
            // スコアに現在のスコアの90％を加算する
            int bonusScore = score * 90 / 100;
            score += bonusScore;
        }
        else if (combo <= 500 && combo >= 549)
        {
            // スコアに現在のスコアの100％を加算する
            int bonusScore = score * 100 / 100;
            score += bonusScore;
        }

    }

    public void Rank()
    {
        Debug.Log("rank");
        if(score >= 0 && score <= 1000)
        {
            Debug.Log("rank表示");
            rankText.text = "G";
        }

    }

}
