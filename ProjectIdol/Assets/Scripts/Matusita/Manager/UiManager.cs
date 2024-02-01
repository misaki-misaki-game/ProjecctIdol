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

    [SerializeField] DataManager dataManager; // DataManager�ϐ�

    //���U���g�֌W
    [SerializeField] GameObject currentScoreObject; // ���݂̃X�R�A�I�u�W�F�N�g�ϐ�
    [SerializeField] GameObject rankingObject; // �����L���O�I�u�W�F�N�g�ϐ�
    [SerializeField] AudioSource SEAudioSource; // SE�p�I�[�f�B�I�\�[�X
    [SerializeField] AudioClip SEAudioClip; // ���艹�N���b�v
    [SerializeField] Ranking ranking; // Ranking�ϐ�
    public float scoreAnimTime = 2f; // �X�R�A�̃J�E���g�A�j���[�V�����̎��ԕϐ�
    public TextMeshProUGUI scoreShowText; // �X�R�A�e�L�X�g�\���p
    bool isScoreCount = false; // �X�R�A���J�E���g���Ă��邩�ǂ���
    float showScore = 0f; // ���Z�����X�R�A�ϐ�
    float timer = 0; // ���Z���鎞�ԕϐ�

    //public Animator AiAnimation;

    float score = 0;
    float bonusScore;
    int combo = 0;
    float time = 180;
    float star = 0;

    public void FixedUpdate()
    {
        time -= Time.deltaTime;

        scoreText.text = "Score\n" + score.ToString();
        lastScoreText.text = "Score:" + score.ToString();
        comboText.text = "Combo:" + combo.ToString();
        //timeText.text = "Time :" + time.ToString();
        //timeText.text = string.Format("Time :{0:0}s", time);

        if (time <= 120.5f && time >= 98)
        {
            //Debug.Log("�A�C�̃A�j���[�V�����Đ�����");
            //AiAnimation.Play("Miss_Anim");
        }
        BonusCalculate();
        // �g�[�^���X�R�A���m�肵����X�R�A�����Z����A�j���[�V�������Ăяo��
        CountShowScore(score, scoreAnimTime);
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
        }
        else
        {
            score -= 25;
            combo = 0;
        }
    }

    public void BonusCalculate()
    {
        if (combo <= 50 && combo >= 99)
        {
            // �X�R�A�Ɍ��݂̃X�R�A��10�������Z����
            float bonusScore = score * 10 / 100;
            score += bonusScore;
        }
        else if (combo <= 100 && combo >= 149)
        {
            // �X�R�A�Ɍ��݂̃X�R�A��20�������Z����
            float bonusScore = score * 20 / 100;
            score += bonusScore;
        }
        else if (combo <= 150 && combo >= 199)
        {
            // �X�R�A�Ɍ��݂̃X�R�A��30�������Z����
            float bonusScore = score * 30 / 100;
            score += bonusScore;
        }
        else if (combo <= 200 && combo >= 249)
        {
            // �X�R�A�Ɍ��݂̃X�R�A��40�������Z����
            float bonusScore = score * 40 / 100;
            score += bonusScore;
        }
        else if (combo <= 250 && combo >= 299)
        {
            // �X�R�A�Ɍ��݂̃X�R�A��50�������Z����
            float bonusScore = score * 50 / 100;
            score += bonusScore;
        }
        else if (combo <= 300 && combo >= 349)
        {
            // �X�R�A�Ɍ��݂̃X�R�A��60�������Z����
            float bonusScore = score * 60 / 100;
            score += bonusScore;
        }
        else if (combo <= 350 && combo >= 399)
        {
            // �X�R�A�Ɍ��݂̃X�R�A��70�������Z����
            float bonusScore = score * 70 / 100;
            score += bonusScore;
        }
        else if (combo <= 400 && combo >= 449)
        {
            // �X�R�A�Ɍ��݂̃X�R�A��80�������Z����
            float bonusScore = score * 80 / 100;
            score += bonusScore;
        }
        else if (combo <= 450 && combo >= 499)
        {
            // �X�R�A�Ɍ��݂̃X�R�A��90�������Z����
            float bonusScore = score * 90 / 100;
            score += bonusScore;
        }
        else if (combo <= 500 && combo >= 549)
        {
            // �X�R�A�Ɍ��݂̃X�R�A��100�������Z����
            float bonusScore = score * 100 / 100;
            score += bonusScore;
        }

    }

    public void Rank()
    {
        Debug.Log("rank");
        if(score >= 0 && score <= 1000)
        {
            rankText.text = "-G-";
        }

    }
    /// <summary>
    /// �X�R�A�����Z����A�j���[�V�����֐�
    /// </summary>
    /// <param name="totalScore">����̃Q�[���̃g�[�^���X�R�A</param>
    /// <param name="countTime">���Z����A�j���[�V�����̕b��</param>
    void CountShowScore(float totalScore, float countTime)
    {
        if (!isScoreCount) return; // isScoreCount��false�Ȃ烊�^�[����Ԃ�
        if (showScore < totalScore)
        {
            timer += Time.deltaTime; // deltaTime�����Z����
            float progress = Mathf.Clamp01(timer / countTime); // timer/countTime�̊�������
            showScore = Mathf.Lerp(0, totalScore, progress); // 0����totalScore�܂ł̒l�ɑ΂��Ċ���(progress)����
            scoreShowText.text = string.Format("{0:00000000}", showScore); // �X�R�A��\��
        }
        // showScore��totalScore�𒴂�����\�L���ꂵ�Ȃ��悤��totalScore��\������
        else
        {
            scoreShowText.text = string.Format("{0:00000000}", totalScore); // �X�R�A��\��
            isScoreCount = false; // CountShowScore���Ăяo���Ȃ��悤��false�ɂ���
        }
    }
    public void ShowScore()
    {
        SEAudioSource.clip = SEAudioClip; // SEAudioClip�������Ď��̃{�^���̂Ƃ��ɂ���������炷
        SEAudioSource.Play(); // SE��炷
        currentScoreObject.SetActive(true); // �X�R�A��ʂ�\��
        isScoreCount = true; // isScoreCount��true�ɂ���CountShowScore���Ăяo���悤�ɂ���
    }
    public void ShowRanking()
    {
        SEAudioSource.clip = SEAudioClip; // SEAudioClip�������Ď��̃{�^���̂Ƃ��ɂ���������炷
        SEAudioSource.Play(); // SE��炷
        currentScoreObject.SetActive(false); // �X�R�A��ʂ��\��
        rankingObject.SetActive(true); // �����L���O��ʂ�\��
        ranking.CheckRankin(dataManager.data.rhythmRanking, score); // �����L���O�ɓ����Ă��邩�̃`�F�b�N
    }
}
