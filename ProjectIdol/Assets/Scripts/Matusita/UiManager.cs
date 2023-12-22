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

    public void Update()
    {
        time -= Time.deltaTime;

        //���Ԃ�0�b�ɂȂ�ƃ��U���g��ʂɈړ�����
        if (time <= 0)
        {
            SceneManager.LoadScene("ResultScene");
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

    public void NoteMiss()
    {
        combo = 0;
    }

}
