using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Klak.Timeline.Midi;
using System.Drawing;
using static ScoreDirector;
using static RhythmDiamondMesh;
using System.Net.Http.Headers;


public class UiManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;         //�Q�[����ʂ̍���ɕ\������X�R�A�e�L�X�g
    [SerializeField] TextMeshProUGUI lastScoreText;     //�Q�[�����I�������Ƃ��ɕ\�����邽�߂̃X�R�A�e�L�X�g
    [SerializeField] TextMeshProUGUI comboText;         //�Q�[����ʂ̉E��ɕ\�����邽�߂̃R���{�e�L�X�g
    [SerializeField] TextMeshProUGUI timeText;          //�^�C���e�L�X�g�@�Q�[����ʂɂ͕\�����Ă��Ȃ����A����g���\�������邽�ߎc���Ă���
    [SerializeField] TextMeshProUGUI rankText;          //�Q�[���I�����Ƀ����N��\�����邽�߂̃e�L�X�g

    [SerializeField] DataManager dataManager;           // DataManager�ϐ�
    [SerializeField] RhythmDiamondMesh rhythmDiamondMesh;

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



    //�������scoreDirector��������Ă���
    [EnumIndex(typeof(UltimateType))]
    public string[] rank = new string[5]; // ultimateScore�̗v�f���Ɠ����@[D,C,B,A,S]�̂ǂꂩ�ɂ��� [5]�͋��Ƀg�[�^���X�R�A


    [SerializeField] GameObject resultPanel;
    //public Animator AiAnimation;

    float score = 0;
    float bonusScore;
    int combo = 0;
    float time = 180;

    [SerializeField] TextMeshProUGUI pointText;
    [SerializeField] TextMeshProUGUI totalRankText;
    [SerializeField] int bluePoints = 0;
    [SerializeField] int redPoints = 0;
    [SerializeField] int whitePoints = 0;
    [SerializeField] int yellowPoints = 0;

    int totalPoints = 0;
    [SerializeField] int totalRankPointS = 506;
    [SerializeField] int totalRankPointA = 369;
    [SerializeField] int totalRankPointB = 280;
    [SerializeField] int totalRankPointC =  112;
    [SerializeField] int totalRankPointD = 56;
    string colorPoint;

    [SerializeField] int blueRankPointS=132;
    [SerializeField] int blueRankPointA=94;
    [SerializeField] int blueRankPointB=78;
    [SerializeField] int blueRankPointC=36;
    [SerializeField] int blueRankPointD=18;

    [SerializeField] int redRankPointS=128;
    [SerializeField] int redRankPointA=96;
    [SerializeField] int redRankPointB=72;
    [SerializeField] int redRankPointC=28;
    [SerializeField] int redRankPointD=14;

    [SerializeField] int whiteRankPointS=120;
    [SerializeField] int whiteRankPointA=83;
    [SerializeField] int whiteRankPointB=60;
    [SerializeField] int whiteRankPointC=20;
    [SerializeField] int whiteRankPointD=10;

    [SerializeField] int yellowRankPointS=126;
    [SerializeField] int yellowRankPointA=96;
    [SerializeField] int yellowRankPointB=70;
    [SerializeField] int yellowRankPointC=28;
    [SerializeField] int yellowRankPointD=14;

    public string blueRank;
    public string redRank;
    public string whiteRank;
    public string yellowRank;


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
        //�R���{���ɍ��킹�ă{�[�i�X�X�R�A���l������
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
    public void AddBluePoint (int colorPoint)
    {
        bluePoints += colorPoint;
    }
    public void AddRedPoint (int colorPoint)
    {
        redPoints += colorPoint;
    }
    public void AddWhitePoint (int colorPoint)
    {
        whitePoints += colorPoint;
    }
    public void AddYellowPoint (int colorPoint)
    {
        yellowPoints += colorPoint;
    }

    //�e�F�̃|�C���g�̃����N��\������
    public string EvaluateRank(int colorPoint, string tag)
    {
    //�p�����[�^�[�̕]�����s���֐���ǉ�
        string rank = "";
        switch (tag)
        {
            case "BlueNotes":
                if      (colorPoint <= blueRankPointS) rank = "S";
                else if (colorPoint <= blueRankPointA) rank = "A";
                else if (colorPoint <= blueRankPointB) rank = "B";
                else if (colorPoint <= blueRankPointC) rank = "C";
                else if (colorPoint <= blueRankPointD) rank = "D";
                break;

            case "RedNotes":
                if      (colorPoint <= redRankPointS) rank = "S";
                else if (colorPoint <= redRankPointA) rank = "A";
                else if (colorPoint <= redRankPointB) rank = "B";
                else if (colorPoint <= redRankPointC) rank = "C";
                else if (colorPoint <= redRankPointD) rank = "D";
                break;

            case "WhiteNotes":
                if      (colorPoint <= whiteRankPointS) rank = "S";
                else if (colorPoint <= whiteRankPointA) rank = "A";
                else if (colorPoint <= whiteRankPointB) rank = "B";
                else if (colorPoint <= whiteRankPointC) rank = "C";
                else if (colorPoint <= whiteRankPointD) rank = "D";
                break;

            case "YellowNotes":
                if      (colorPoint <= yellowRankPointS) rank = "S";
                else if (colorPoint <= yellowRankPointA) rank = "A";
                else if (colorPoint <= yellowRankPointB) rank = "B";
                else if (colorPoint <= yellowRankPointC) rank = "C";
                else if (colorPoint <= yellowRankPointD) rank = "D";
                break;
        }
        return rank;
    }

    //�|�C���g�̍��v�̃����N��\������
    public void PointRank()
    {
        Debug.Log("rank");
        //�e�V�O�i���̃|�C���g�̍��v
        totalPoints = bluePoints + redPoints + whitePoints + yellowPoints;
        string rank = "";
        if      (0               < totalPoints && totalPoints <= totalRankPointD) rank = "-D-";
        else if (totalRankPointD < totalPoints && totalPoints <= totalRankPointC) rank = "-C-";
        else if (totalRankPointC < totalPoints && totalPoints <= totalRankPointB) rank = "-B-";
        else if (totalRankPointB < totalPoints && totalPoints <= totalRankPointA) rank = "-A-";
        else if (totalRankPointA < totalPoints && totalPoints <= totalRankPointS) rank = "-S-";

        totalRankText.text= rank;
    }
    public void ShowResult()
    {
        //�e�V�O�i���̃|�C���g�̍��v
        totalPoints = bluePoints + redPoints + whitePoints + yellowPoints;

        // �Q�[���I�����̃����N��\�����郁�\�b�h
        // �e�p�����[�^�[�̃����N���擾

        blueRank = EvaluateRank(bluePoints, "BlueNotes");
        redRank = EvaluateRank(redPoints, "RedNotes");
        whiteRank = EvaluateRank(whitePoints, "WhiteNotes");
        yellowRank = EvaluateRank(yellowPoints, "YellowNotes");

        // �����N���e�L�X�g�ɕ\��
        pointText.text =   blueRank + "  �V�O�i��: " + bluePoints   + "pt\n" +
                           redRank  + "  �ԃV�O�i��: " + redPoints    + "pt\n" +
                          whiteRank + "  ���V�O�i��: " + whitePoints  + "pt\n" +
                         yellowRank + "  ���V�O�i��: " + yellowPoints + "pt\n" +
                         "�ŏI�|�C���g" + totalPoints;
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
