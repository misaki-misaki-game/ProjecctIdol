using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using static DiamondMesh;

public class ScoreDirector : SignalScript
{
    // bool isgameOver = false; // �Q�[���I�[�o�[���ǂ���
    bool isCombo = false; // �R���{���Ă��邩�ǂ���
    public float comboTime; // �R���{�����^�C���ϐ�
    public float scoreBaseValue = 50; // ��b�X�R�A�ϐ�
    public float comboDurationTime = 1f; // �R���{�p�����ԕϐ�
    public float totalScore; // �g�[�^���X�R�A�ϐ�
    public float totalUltimateScore; // ���Ƀg�[�^���X�R�A

    /// <summary> �e���Ƀ����N�̉����̒l </summary>
    public float minRankS = 151f;
    public float minRankA = 101f;
    public float minRankB = 61f;
    public float minRankC = 31f;
    /// <summary> �e���Ƀ����N�̉����̒l </summary>
    
    /// <summary> �e���Ƀg�[�^�������N�̉����̒l </summary>
    public float minTotalS = 701f;
    public float minTotalA = 401f;
    public float minTotalB = 241f;
    public float minTotalC = 121f;
    /// <summary> �e���Ƀg�[�^�������N�̉����̒l </summary>

    public int combo; // �R���{�ϐ�
    public int chain; // �`�F�C���ϐ�
    public enum UltimateType // �X�R�A���̗񋓌^
    {
        intelligence = 0,
        charisma,
        acting,
        guts,
        total
    }

    [EnumIndex(typeof(UltimateType))]
    public float[] ultimateScore = new float[4]; // ���ɃX�R�A
    /*
    ultimateScore[0] intelligenceScore; // �C���e���W�F���X�X�R�A(�V�O�i��)
    ultimateScore[1] charismaScore; // �J���X�}�X�R�A(�ԃV�O�i��)
    ultimateScore[2] actingScore; // �A�N�e�B���O�X�R�A(���V�O�i��)
    ultimateScore[3] gutsScore; // �K�b�c�X�R�A(���F�V�O�i��)
    */
    [EnumIndex(typeof(UltimateType))]
    public string[] rank = new string[5]; // ultimateScore�̗v�f���Ɠ����@[D,C,B,A,S]�̂ǂꂩ�ɂ��� [5]�͋��Ƀg�[�^���X�R�A
    [EnumIndex(typeof(UltimateType))]
    public TextMeshProUGUI[] UltText = new TextMeshProUGUI[5]; // ���Ƀp�����[�^�[�e�L�X�g�\���p
    public TextMeshProUGUI UltRankText; // �����N�e�L�X�g�\���p
    public TextMeshProUGUI scoreText; // �X�R�A�e�L�X�g�\���p
    public TextMeshProUGUI comboText; // �R���{�e�L�X�g�\���p
    public StarDirector starDirector; // StarDirector�ϐ�
    public GameObject nextButton; // �{�^���ϐ�
    public GameObject resultObject; // ���U���g�I�u�W�F�N�g�ϐ�
    public GameObject graphObject; // �O���t�I�u�W�F�N�g�ϐ�

    // Start is called before the first frame update
    void Start()
    {
        // �g�[�^���X�R�A��������
        totalScore = 0;
        // �R���{��������
        combo = 0;
        // �X�R�A��\��
        scoreText.text = string.Format("SCORE:\n{0:00000000}", totalScore);
        // �R���{��\��
        comboText.text = string.Format("{0:0}combo", combo);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // �R���{�����̏���
        if (isCombo ) // �R���{���Ă���̂ł����
        {
            // �R���{�������Ԃ����炷
            comboTime -= Time.deltaTime;
            // �R���{�𒆒f���鏈��
            if ( comboTime < 0 ) // �R���{�������Ԃ�0�Ȃ�
            {
                // 0����
                comboTime = 0f;
                // isCombo�ϐ���false�ɂ���
                isCombo = false;
                // �R���{�������Z�b�g
                combo = 0;
                // �R���{��\��
                comboText.text = string.Format("{0:0}combo", combo);
            }
        }
    }
    
    public void GetScore(float chain,bool isChain, STATE state) // �Q�b�g�X�R�A�֐�
    {
        float score = 0; // �X�R�A�v�Z�p�ϐ�
        // ���Z����X�R�A���v�Z
        if (isChain) // �`�F�C�����������Ă����
        {
            // �R���{��1���Z
            combo += 1;
            // �R���{�������Ԃ����Z�b�g
            comboTime = comboDurationTime;
            // isCombo�ϐ���true�ɂ���
            isCombo = true;
            if (starDirector.isStarMode == true) // �����l���[�h�ł����
            {
                score = scoreBaseValue * 1.5f * (2 * chain); // ��b�X�R�A50��1.5�{
            }
            else
            {
                // 50��(chain�~2)�{���ĉ��Z
                score = scoreBaseValue * (2 * chain);
            }
        }
        else // �`�F�C�����������Ă��Ȃ����
        {
            score = scoreBaseValue; // 50�����Z
        }
        // ���Z����X�R�A�ɔ{����������
        if (combo >= 50 && combo < 100) // �R���{��50�ȏ�100����
        {
            // ���Z����X�R�A��1.1�{
            score *= 1.1f; 
        }
        else if (combo >= 100) // �R���{��100�ȏ�
        {
            // ���Z����X�R�A��1.2�{
            score *= 1.2f;
        }
        if (starDirector.isStarMode == true) // �����l���[�h�ł����
        {
            score += score * 0.5f; // �l���X�R�A�ɑ΂���50%�����Z����
        }
        switch (state)
        {
            // state�ɂ���ăX�e�[�^�X�|�C���g�����Z
            case STATE.RED:
                ultimateScore[1] += chain + 1f;
                break;
            case STATE.BLUE:
                ultimateScore[0] += chain + 1f;
                break;
            case STATE.YELLOW:
                ultimateScore[3] += chain + 1f;
                break;
            case STATE.WHITE:
                ultimateScore[2] += chain + 1f;
                break;
        }
        // �g�[�^���X�R�A�ɉ��Z
        totalScore += score;
        // �X�R�A��\��
        scoreText.text = string.Format("SCORE:\n{0:00000000}", totalScore);
        // �R���{��\��
        comboText.text = string.Format("{0:0}combo", combo);
    }
    public void SetRank()
    {
        // �e�p�����[�^�Ƀ����N��t����+totalUltimateScore���Z�o
        for (int i = 0; i < 4; i++)
        {
            if (minRankS <= ultimateScore[i]) // 151�ȏ�Ȃ�S�����N
            {
                rank[i] = "S";
            }
            else if (minRankA <= ultimateScore[i]) // 101�ȏ�150�ȉ��Ȃ�A�����N
            {
                rank[i] = "A";
            }
            else if (minRankB <= ultimateScore[i]) // 61�ȏ�100�ȉ��Ȃ�B�����N
            {
                rank[i] = "B";
            }
            else if (minRankC <= ultimateScore[i]) // 31�ȏ�60�ȉ��Ȃ�C�����N
            {
                rank[i] = "C";
            }
            else  // 0�ȏ�30�ȉ��Ȃ�D�����N
            {
                rank[i] = "D";
            }
            UltText[i].text = string.Format("{0:000}Pt", ultimateScore[i]); // �e���ɃX�R�A�������o��
            totalUltimateScore += ultimateScore[i]; // �e�X�R�A�����Z����
        }

        // totalUltimateScore�̃����N��t����
        if (minTotalS <= totalUltimateScore) // 701�ȏ�Ȃ�S�����N
        {
            rank[4] = "S";
        }
        else if (minTotalA <= totalUltimateScore) // 401�ȏ�700�ȉ��Ȃ�A�����N
        {
            rank[4] = "A";
        }
        else if (minTotalB <= totalUltimateScore) // 241�ȏ�400�ȉ��Ȃ�B�����N
        {
            rank[4] = "B";
        }
        else if (minTotalC <= totalUltimateScore) // 121�ȏ�240�ȉ��Ȃ�C�����N
        {
            rank[4] = "C";
        }
        else  // 0�ȏ�120�ȉ��Ȃ�D�����N
        {
            rank[4] = "D";
        }
        UltText[4].text = string.Format("{0:0000}Pt", totalUltimateScore); // �e���ɃX�R�A�������o��
    }
    
    public void ShowResult()
    {
        nextButton.SetActive(false); // �{�^�����\��
        graphObject.SetActive(false); // �O���t���\��
        resultObject.SetActive(true); // ���U���g��\��
    }
}
