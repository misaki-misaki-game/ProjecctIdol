using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI comboText;
    [SerializeField] TextMeshProUGUI timeText;

    int score = 0;
    int bonusScore = 50;
    int combo = 0;
    //float time = 183.6f;
    float star = 0;

    public void Update()
    {
        //time -= Time.deltaTime;

        scoreText.text = "Score\n" + score.ToString();
        comboText.text = "Combo:" + combo.ToString();
        //timeText.text = "Time :" + time.ToString();
        //timeText.text = string.Format("Time :{0:0}s", time);
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
