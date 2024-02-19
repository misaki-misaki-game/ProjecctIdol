using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Klak.Timeline.Midi;

public class UiManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;         //�Q�[����ʂ̍���ɕ\������X�R�A�e�L�X�g
    [SerializeField] TextMeshProUGUI lastScoreText;     //�Q�[�����I�������Ƃ��ɕ\�����邽�߂̃X�R�A�e�L�X�g
    [SerializeField] TextMeshProUGUI comboText;         //�Q�[����ʂ̉E��ɕ\�����邽�߂̃R���{�e�L�X�g
    [SerializeField] TextMeshProUGUI timeText;          //�^�C���e�L�X�g�@�Q�[����ʂɂ͕\�����Ă��Ȃ����A����g���\�������邽�ߎc���Ă���
    [SerializeField] TextMeshProUGUI rankText;          //�Q�[���I�����Ƀ����N��\�����邽�߂̃e�L�X�g

    [SerializeField] DataManager dataManager;           // DataManager�ϐ�

    //���U���g�֌W
    [SerializeField] GameObject currentScoreObject;     // ���݂̃X�R�A�I�u�W�F�N�g�ϐ�
    [SerializeField] GameObject rankingObject;          // �����L���O�I�u�W�F�N�g�ϐ�
    [SerializeField] AudioSource SEAudioSource;         // SE�p�I�[�f�B�I�\�[�X
    [SerializeField] AudioClip SEAudioClip;             // ���艹�N���b�v
    [SerializeField] Ranking ranking;                   // Ranking�ϐ�
    public float scoreAnimTime = 2f;                    // �X�R�A�̃J�E���g�A�j���[�V�����̎��ԕϐ�
    public TextMeshProUGUI scoreShowText;               // �X�R�A�e�L�X�g�\���p
    bool isScoreCount = false;                          // �X�R�A���J�E���g���Ă��邩�ǂ���
    float showScore = 0f;                               // ���Z�����X�R�A�ϐ�
    float timer = 0;                                    // ���Z���鎞�ԕϐ�

    //public Animator AiAnimation;

    float score = 0;
    float bonusScore;
    int combo = 0;
    float time = 180;

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
        //�Q�Ɛ��point�ɐ��l������Ƃ��̐��l��score�Ƀv���X���邱�Ƃ��ł���
        //JudgmentArea�X�N���v�g��SignalJudgment�Ŏg�p���Ă���
        score += point;
    }

    public void AddCombo()
    {
        //�Q�Ɛ��combo��1���v���X���邱�Ƃ��ł���
        //JudgmentArea�X�N���v�g��SignalJudgment�Ŏg�p���Ă���
        combo++;
    }

    public void NoteMiss()
    {
        //�Q�Ɛ�ŃV�O�i���������̂����s�����Ƃ��Ɏg�p����
        //JudgmentArea�X�N���v�g��SignalJudgment�Ŏg�p���Ă���
        if (score <= 0 && combo <= 0)
        {
        //score��combo�ɐ��l�������ĂȂ��Ƃ��͌��炳�Ȃ��悤�ɂ���
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
    }

    public void Rank()
    {
        //�Q�[�����I��������Ƀ����N��\�������邽�߂̂���
        Debug.Log("rank");
        //�X�R�A��0�ȏ�1000�����̏ꍇ�Ƀ����N�Ƃ���G��\������
        if(score >= 0 && score <= 1000)
        {
            rankText.text = "-G-";
        }
        else if(score >= 1001 && score <= 2000)
        {
            rankText.text = "-A-";
        }

    }
    /// <summary>
    /// �X�R�A�����Z����A�j���[�V�����֐�
    /// </summary>
    /// <param name="totalScore">����̃Q�[���̃g�[�^���X�R�A</param>
    /// <param name="countTime">���Z����A�j���[�V�����̕b��</param>
    void CountShowScore(float totalScore, float countTime)
    {
        if (!isScoreCount) return;                                              // isScoreCount��false�Ȃ烊�^�[����Ԃ�
        if (showScore < totalScore)
        {
            timer += Time.deltaTime;                                            // deltaTime�����Z����
            float progress = Mathf.Clamp01(timer / countTime);                  // timer/countTime�̊�������
            showScore = Mathf.Lerp(0, totalScore, progress);                    // 0����totalScore�܂ł̒l�ɑ΂��Ċ���(progress)����
            scoreShowText.text = string.Format("{0:00000000}", showScore);      // �X�R�A��\��
        }
        // showScore��totalScore�𒴂�����\�L���ꂵ�Ȃ��悤��totalScore��\������
        else
        {
            scoreShowText.text = string.Format("{0:00000000}", totalScore);     // �X�R�A��\��
            isScoreCount = false;                                               // CountShowScore���Ăяo���Ȃ��悤��false�ɂ���
        }
    }
    public void ShowScore()
    {
        SEAudioSource.clip = SEAudioClip;       // SEAudioClip�������Ď��̃{�^���̂Ƃ��ɂ���������炷
        SEAudioSource.Play();                   // SE��炷
        currentScoreObject.SetActive(true);     // �X�R�A��ʂ�\��
        isScoreCount = true;                    // isScoreCount��true�ɂ���CountShowScore���Ăяo���悤�ɂ���
    }
    public void ShowRanking()
    {
        SEAudioSource.clip = SEAudioClip;                               // SEAudioClip�������Ď��̃{�^���̂Ƃ��ɂ���������炷
        SEAudioSource.Play();                                           // SE��炷
        currentScoreObject.SetActive(false);                            // �X�R�A��ʂ��\��
        rankingObject.SetActive(true);                                  // �����L���O��ʂ�\��
        ranking.CheckRankin(dataManager.data.rhythmRanking, score);     // �����L���O�ɓ����Ă��邩�̃`�F�b�N
    }
}
