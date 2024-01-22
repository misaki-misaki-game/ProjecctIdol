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

        //���Ԃ�0�b�ɂȂ�ƃ��U���g��ʂɈړ�����
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

        //�{�[�i�X
        if (combo % 50 == 0)
        {
            score += bonusScore * (combo / 50 * 10) / 100;
        }
    }

    public void AddStar(int gauge)
    {
        star += gauge;

        //�������܃��[�h
        if (star <= 100)
        {
            Debug.Log("�������܃��[�h�˓�");
        }
        else if (star >= 100)
        {
            Debug.Log("�������܃��[�h�I��");
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
