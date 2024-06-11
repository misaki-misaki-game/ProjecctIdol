using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public partial class ScoreDirector : SignalScript
{
    /// --------�֐��ꗗ-------- ///
    /// -------public�֐�------- ///

    /// <summary>
    /// �Q�b�g�X�R�A�֐�
    /// </summary>
    /// <param name="chain">�������V�O�i���̃`�F�C����</param>
    /// <param name="isChain">�������V�O�i�����`�F�C�����Ă��邩�ǂ���</param>
    /// <param name="state">�������V�O�i���̃X�e�[�^�X</param>
    public void GetScore(float chain, bool isChain, STATE state)
    {
        // ��b�X�R�A�����Z
        score = AddBaseValue(chain, isChain);
        // �R���{�ɂ��{����������
        score = MultiplyCombo(score);
        // �X�^�[���[�h�ɂ��{����������
        score = MultiplyStarMode(score);
        // ���ɃX�R�A���v�Z����
        CalculateUltimateScore(chain, state);
        // �g�[�^���X�R�A�ɉ��Z
        totalScore += score;
        // �X�R�A��\��
        scoreText.text = string.Format("{0:00000000}", totalScore);
        // �R���{��\��
        comboText.text = string.Format("{0:0}", combo);
    }

    /// <summary>
    /// �����l���[�h�I�����ɃX�R�A���擾����֐�
    /// </summary>
    /// <param name="score">�l�������X�R�A</param>
    public void GetStarScore(float score)
    {
        totalScore += score;
        // �X�R�A��\��
        scoreText.text = string.Format("{0:00000000}", totalScore);
        // �R���{��\��
        comboText.text = string.Format("{0:0}", combo);
    }

    /// <summary>
    /// �e�X�e�[�^�X�̃����N������֐�
    /// </summary>
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
            UltText[i].text = string.Format(rank[i] + "  {0:000}Pt", ultimateScore[i]); // �e���ɃX�R�A�������o��
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
        UltRankText.text = string.Format(rank[4]); // �����N�����o��
        CheckMission(); // �~�b�V�������N���A�������̊m�F
        RecordUpdate(); // �n�C�X�R�A�̍X�V
    }

    /// <summary>
    /// ���Ƀp�����[�^��\������֐�
    /// </summary>
    public void ShowUltScore()
    {
        SEAudioSource.clip = SEAudioClip; // SEAudioClip�������Ď��̃{�^���̂Ƃ��ɂ���������炷
        SEAudioSource.Play(); // SE��炷
        nextButton.SetActive(false); // �{�^�����\��
        graphObject.SetActive(false); // �O���t���\��
        resultObject.SetActive(true); // ���U���g��\��
    }

    /// <summary>
    /// �X�R�A��\������֐�
    /// </summary>
    public void ShowScore()
    {
        SEAudioSource.clip = SEAudioClip; // SEAudioClip�������Ď��̃{�^���̂Ƃ��ɂ���������炷
        SEAudioSource.Play(); // SE��炷
        if (resultObject) resultObject.SetActive(false); // ���U���g���\��
        currentScoreObject.SetActive(true); // �X�R�A��ʂ�\��
        isScoreCount = true; // isScoreCount��true�ɂ���CountShowScore���Ăяo���悤�ɂ���
    }

    /// <summary>
    /// �����L���O��\������֐�
    /// </summary>
    public void ShowRanking()
    {
        SEAudioSource.clip = SEAudioClip; // SEAudioClip�������Ď��̃{�^���̂Ƃ��ɂ���������炷
        SEAudioSource.Play(); // SE��炷
        currentScoreObject.SetActive(false); // �X�R�A��ʂ��\��
        rankingObject.SetActive(true); // �����L���O��ʂ�\��
        ranking.CheckRankin(dataManager.data.puzzleRanking, totalScore); // �����L���O�ɓ����Ă��邩�̃`�F�b�N
    }

    /// -------public�֐�------- ///
    /// -----protected�֐�------ ///



    /// -----protected�֐�------ ///
    /// ------private�֐�------- ///

    private void Start()
    {
        InitializationUI(); // �R���{�ƃg�[�^���X�R�A������������
    }

    private void FixedUpdate()
    {
        // �R���{�����̏���
        ContinuationCombo();
        // �g�[�^���X�R�A���m�肵����X�R�A�����Z����A�j���[�V�������Ăяo��
        CountShowScore(totalScore, scoreAnimTime);
    }

    /// <summary>
    /// �R���{�ƃg�[�^���X�R�A������������֐�
    /// </summary>
    private void InitializationUI()
    {
        // �g�[�^���X�R�A��������
        totalScore = default;
        // �R���{��������
        combo = default;
        // �X�R�A��\��
        scoreText.text = string.Format("{0:00000000}", totalScore);
        // �R���{��\��
        comboText.text = string.Format("{0:0}", combo);
    }

    /// <summary>
    /// �R���{�����̏����֐�
    /// </summary>
    private void ContinuationCombo()
    {
        if (isCombo) // �R���{���Ă���̂ł����
        {
            // �R���{�������Ԃ����炷
            comboTime -= Time.deltaTime;
            Gauge.fillAmount -= Time.deltaTime / comboDurationTime; // �Q�[�W������������(2�b��)
            // �R���{�𒆒f���鏈��
            if (comboTime < 0) // �R���{�������Ԃ�0�Ȃ�
            {
                // 0����
                comboTime = default;
                // isCombo�ϐ���false�ɂ���
                isCombo = default;
                // �R���{�������Z�b�g
                combo = default;
                // �R���{��\��
                comboText.text = string.Format("{0:0}", combo);
                // �R���{�Q�[�W��0�ɂ���
                Gauge.fillAmount = 0;
            }
        }
    }

    /// <summary>
    /// ��b�X�R�A���v�Z����֐�
    /// </summary>
    /// <param name="chain">�������V�O�i���̃`�F�C����</param>
    /// <param name="isChain">�������V�O�i�����`�F�C�����Ă��邩�ǂ���</param>
    /// <returns>�v�Z��̃X�R�A</returns>
    private float AddBaseValue(float chain, bool isChain)
    {
        float score;
        if (isChain) // �`�F�C�����������Ă����
        {
            // �R���{��1���Z
            combo += 1;
            // �R���{�������Ԃ����Z�b�g
            comboTime = comboDurationTime;
            // �R���{�Q�[�W�����Z�b�g
            Gauge.fillAmount = 1;
            // isCombo�ϐ���true�ɂ���
            isCombo = true;
            if (starDirector.starState == StarDirector.StarState.StarMode) // �����l���[�h�ł����
            {
                score = scoreBaseValue * multiplyStarBase * (2 * chain); // ��b�X�R�A50��1.5�{
            }
            else
            {
                // 50��(chain�~2)�{
                score = scoreBaseValue * (2 * chain);
            }
        }
        else // �`�F�C�����������Ă��Ȃ����
        {
            score = scoreBaseValue; // 50�����Z
        }
        return score;
    }

    /// <summary>
    /// �R���{���ɂ��{�����|����֐�
    /// </summary>
    /// <param name="score">��b�X�R�A�v�Z��̃X�R�A</param>
    /// <returns>�R���{�{���v�Z��̃X�R�A</returns>
    private float MultiplyCombo(float score)
    {
        if (combo >= comboMin && combo < comboMax) // �R���{��50�ȏ�100����
        {
            // ���Z����X�R�A��1.1�{
            score *= multiplyCombo[0];
        }
        else if (combo >= comboMax) // �R���{��100�ȏ�
        {
            // ���Z����X�R�A��1.2�{
            score *= multiplyCombo[1];
        }
        return score;
    }

    /// <summary>
    /// �X�^�[���[�h�ɂ��{�����|����
    /// </summary>
    /// <param name="score">�R���{�v�Z��̃X�R�A</param>
    /// <returns>�X�^�[���[�h�{���v�Z��̃X�R�A</returns>
    private float MultiplyStarMode(float score)
    {
        if (starDirector.starState == StarDirector.StarState.StarMode) // �����l���[�h�ł����
        {
            score += score * addStarMode; // �l���X�R�A�ɑ΂���50%�����Z����
        }

        return score;
    }

    /// <summary>
    /// �A���e�B���b�g�X�R�A���v�Z����֐�
    /// </summary>
    /// <param name="chain">�������V�O�i���̃`�F�C����</param>
    /// <param name="state">�������V�O�i���̃X�e�[�^�X</param>
    private void CalculateUltimateScore(float chain, STATE state)
    {
        switch (state)
        {
            // state�ɂ���ăX�e�[�^�X�|�C���g�����Z
            case STATE.RED:
                ultimateScore[1] += chain + addStatePoint;
                break;
            case STATE.BLUE:
                ultimateScore[0] += chain + addStatePoint;
                break;
            case STATE.YELLOW:
                ultimateScore[3] += chain + addStatePoint;
                break;
            case STATE.WHITE:
                ultimateScore[2] += chain + addStatePoint;
                break;
            case STATE.SPECIAL:
                // X���{���ɂ���ĉ󂵂��V�O�i����state�ɂ���ăX�e�[�^�X�|�C���g�����Z
                for (int i = 0; i < detonationStates.Count; i++)
                {
                    switch (detonationStates[i])
                    {
                        case STATE.RED:
                            ultimateScore[1] += addStatePoint;
                            break;
                        case STATE.BLUE:
                            ultimateScore[0] += addStatePoint;
                            break;
                        case STATE.YELLOW:
                            ultimateScore[3] += addStatePoint;
                            break;
                        case STATE.WHITE:
                            ultimateScore[2] += addStatePoint;
                            break;
                    }
                }
                detonationStates = new List<STATE>(); // ���X�g����ɂ���
                break;
        }
    }

    /// <summary>
    /// �~�b�V�������N���A�������̊m�F����֐�
    /// </summary>
    private void CheckMission()
    {
        OverRankA(); // ���Ƀp�����[�^��A�����N�𒴂��Ă��邩���`�F�b�N
        // �X�^�[���[�h��2��ȏ�N���A������
        if (starDirector.starCount > 1)
        {
            totalScore += addBonus;
        }
        // �����ꂩ�̋��Ƀp�����[�^��A�����N�ȏォ
        if (isOverRank)
        {
            totalScore += addBonus;
        }
        // �����Ƀp�����[�^��A�����N�ȏォ
        if (isOverTotalRank)
        {
            totalScore += addBonus;
        }
        // �X�R�A��\��
        scoreText.text = string.Format("{0:00000000}", totalScore);
    }

    /// <summary>
    /// ���Ƀp�����[�^��A�����N�𒴂��Ă��邩���`�F�b�N����֐�
    /// </summary>
    private void OverRankA()
    {
        // �e���Ƀp�����[�^��A�����N�𒴂��Ă��邩���`�F�b�N�@�����Ă����isOverRank�܂���isOverTotalRank��true�ɂȂ�
        for (int i = 0; i < ultimateScore.Length; i++)
        {
            if (minRankA <= ultimateScore[i])
            {
                isOverRank = true;
                break;
            }
        }
        if (minTotalA <= totalUltimateScore) isOverTotalRank = true;
    }

    /// <summary>
    /// �n�C�X�R�A�̍X�V�֐�
    /// </summary>
    private void RecordUpdate()
    {
        if (totalScore > dataManager.data.puzzleRanking[0]) // ����̃X�R�A���n�C�X�R�A�𒴂����
        {
            dataManager.data.puzzleHighScore = totalScore; // �n�C�X�R�A���X�V����
            dataManager.data.puzzleHighScoreRank = rank[4]; // �����N���X�V����
            dataManager.Save(dataManager.data); // �Z�[�u����
        }
    }

    /// <summary>
    /// �X�R�A�����Z����A�j���[�V�����֐�
    /// </summary>
    /// <param name="totalScore">����̃Q�[���̃g�[�^���X�R�A</param>
    /// <param name="countTime">���Z����A�j���[�V�����̕b��</param>
    private void CountShowScore(float totalScore, float countTime)
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

    /// ------private�֐�------- ///
    /// --------�֐��ꗗ-------- ///
}
public partial class ScoreDirector
{
    /// --------�ϐ��ꗗ-------- ///
    /// -------public�ϐ�------- ///

    public float score = 0; // �X�R�A�v�Z�p�ϐ�

    [EnumIndex(typeof(UltimateType))]
    public string[] rank = new string[5]; // ultimateScore�̗v�f���Ɠ����@[D,C,B,A,S]�̂ǂꂩ�ɂ��� [5]�͋��Ƀg�[�^���X�R�A

    public List<STATE> detonationStates; // �U�������V�O�i����state�i�[�p


    /// -------public�ϐ�------- ///
    /// -----protected�ϐ�------ ///



    /// -----protected�ϐ�------ ///
    /// ------private�ϐ�------- ///

    private bool isCombo = false; // �R���{���Ă��邩�ǂ���
    private bool isScoreCount = false; // �X�R�A���J�E���g���Ă��邩�ǂ���
    private bool isOverRank = false; // A�����N�𒴂��Ă��邩
    private bool isOverTotalRank = false; // �g�[�^�������N��A�𒴂��Ă��邩

    private int combo = 0; // �R���{�ϐ�

    [SerializeField] private int comboMin = 50; // �R���{�{�����|���n�߂鉺��
    [SerializeField] private int comboMax = 100; // �R���{�{�����|���n�߂���

    private float showScore = 0f; // ���Z�����X�R�A�ϐ�
    private float timer = 0; // ���Z���鎞�ԕϐ�
    private float comboTime = 0; // �R���{�����^�C���ϐ�
    private float comboDurationTime = 1f; // �R���{�p�����ԕϐ�
    private float totalScore = 0; // �g�[�^���X�R�A�ϐ�
    private float totalUltimateScore; // ���Ƀg�[�^���X�R�A
    private float scoreAnimTime = 2f; // �X�R�A�̃J�E���g�A�j���[�V�����̎��ԕϐ�

    [SerializeField] private float scoreBaseValue = 50; // ��b�X�R�A�ϐ�
    [SerializeField] private float multiplyStarBase = 1.5f; // �����l���[�h���̊�b�X�R�A�{��
    [Header("�R���{���̔{�� [0]...�R���{����50<=x<100�̎�,[1]...�R���{����x<=100�̎�")]
    [SerializeField] private float[] multiplyCombo = { 1.1f, 1.2f }; // �R���{���̔{��
    [SerializeField] private float addStarMode = 0.5f; // �����l���[�h���̉��Z�{��
    [SerializeField] private float addStatePoint = 1f; // ���Z����X�e�[�^�X�|�C���g
    [SerializeField] private float addBonus = 20000f; // ���Z����~�b�V�����N���A�{�[�i�X

    /// <summary> �e���Ƀ����N�̉����̒l </summary>
    [SerializeField] private float minRankS = 151f;
    [SerializeField] private float minRankA = 101f;
    [SerializeField] private float minRankB = 61f;
    [SerializeField] private float minRankC = 31f;
    /// <summary> �e���Ƀ����N�̉����̒l </summary>

    /// <summary> �e���Ƀg�[�^�������N�̉����̒l </summary>
    [SerializeField] private float minTotalS = 701f;
    [SerializeField] private float minTotalA = 401f;
    [SerializeField] private float minTotalB = 241f;
    [SerializeField] private float minTotalC = 121f;
    /// <summary> �e���Ƀg�[�^�������N�̉����̒l </summary>

    public enum UltimateType // �X�R�A���̗񋓌^
    {
        intelligence = 0,
        charisma,
        acting,
        guts,
        total
    }

    [EnumIndex(typeof(UltimateType))]
    [SerializeField] private float[] ultimateScore = new float[4]; // ���ɃX�R�A
    /*
    ultimateScore[0] intelligenceScore; // �C���e���W�F���X�X�R�A(�V�O�i��)
    ultimateScore[1] charismaScore; // �J���X�}�X�R�A(�ԃV�O�i��)
    ultimateScore[2] actingScore; // �A�N�e�B���O�X�R�A(���V�O�i��)
    ultimateScore[3] gutsScore; // �K�b�c�X�R�A(���F�V�O�i��)
    */
    [EnumIndex(typeof(UltimateType))]
    [SerializeField] private TextMeshProUGUI[] UltText = new TextMeshProUGUI[5]; // ���Ƀp�����[�^�[�e�L�X�g�\���p
    [SerializeField] private TextMeshProUGUI UltRankText; // �����N�e�L�X�g�\���p
    [SerializeField] private TextMeshProUGUI scoreText; // �X�R�A�e�L�X�g�\���p
    [SerializeField] private TextMeshProUGUI scoreShowText; // �X�R�A�e�L�X�g�\���p
    [SerializeField] private TextMeshProUGUI comboText; // �R���{�e�L�X�g�\���p

    [SerializeField] private StarDirector starDirector; // StarDirector�ϐ�

    [SerializeField] private GameObject nextButton; // �{�^���ϐ�
    [SerializeField] private GameObject resultObject; // ���U���g�I�u�W�F�N�g�ϐ�
    [SerializeField] private GameObject graphObject; // �O���t�I�u�W�F�N�g�ϐ�
    [SerializeField] private GameObject currentScoreObject; // ���݂̃X�R�A�I�u�W�F�N�g�ϐ�
    [SerializeField] private GameObject rankingObject; // �����L���O�I�u�W�F�N�g�ϐ�

    [SerializeField] private AudioSource SEAudioSource; // SE�p�I�[�f�B�I�\�[�X

    [SerializeField] private AudioClip SEAudioClip; // ���艹�N���b�v

    [SerializeField] private DataManager dataManager; // DataManager�ϐ�

    [SerializeField] private Ranking ranking; // Ranking�ϐ�

    [SerializeField] private Image Gauge; // �R���{�Q�[�W

    /// ------private�ϐ�------- ///
    /// -------�v���p�e�B------- ///



    /// -------�v���p�e�B------- ///
    /// --------�ϐ��ꗗ-------- ///
}