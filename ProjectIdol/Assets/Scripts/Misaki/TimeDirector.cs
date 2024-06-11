using UnityEngine;
using TMPro;

public partial class TimeDirector : MonoBehaviour
{

    /// --------�֐��ꗗ-------- ///
    /// -------public�֐�------- ///



    /// -------public�֐�------- ///
    /// -----protected�֐�------ ///



    /// -----protected�֐�------ ///
    /// ------private�֐�------- ///

    private void Start()
    {
        Application.targetFrameRate = 60; // 60fps�ɌŒ�
        aniScd = gameStartcd.GetComponent<Animator>(); // Animetor���i�[����
        aniScd.GetComponent<GameStartCountDownScript>().CountDown();
    }

    private void FixedUpdate()
    {
        TimeCountDown(); // ���Ԃ̃J�E���g�_�E�����s��
        GameOver(); // �Q�[���I���̏������s��
    }

    /// <summary>
    /// �Q�[���I���̏����֐�
    /// </summary>
    private void GameOver()
    {
        if (gameOver) // �Q�[���I�[�o�[�ł����
        {
            this.gameOver = false; // false�ɂ��邱�Ƃł���if����1�񂾂��Ăяo��
            this.gameStart = false; // ��L���s���Ə��if�����Ăяo�����̂ł�����s��Ȃ����߂�false�ɂ���
            animAi.SetTrigger("isDanceEnd"); // �A�C�̃A�j���[�V�������I������
            buttonScript.ButtonsDestroy(); // �{�^����S�Ĕj�󂷂�
            buttonFrame.SetActive(false); // �t���[�����\������
            scoreDirector.SetRank(); // �e�X�R�A�ɉ����ă����N��ݒ肷��
            starGauge.SetActive(false); // �����l�Q�[�W���\���ɂ���
            graphBackImage.SetActive(true); // �O���t�p�w�i��\��
            /// diamondFrame��diamondGraph�̃Z�b�g�A�b�v���s�� ///
            diamondFrame.SetUp();
            diamondGraph.SetUp();
            nextButton.SetActive(true); // �{�^����\������
        }
    }

    /// <summary>
    /// ���Ԃ̃J�E���g�_�E�����s��
    /// </summary>
    private void TimeCountDown()
    {
        // �Q�[�����X�^�[�g,����,�Q�[���I�[�o�[�ɂȂ��Ă��Ȃ�,����,�ʏ탂�[�h�ł͂Ȃ��̂ł����
        // �ǋL�@�����l���[�h���Ɏ��Ԃ��~�߂Ȃ��悤�ɕύX(2024-03-29)
        if (gameStart && !gameOver)
        {
            // �J�E���g�_�E���^�C����\��
            timeText.text = string.Format("TIME:{0:0}s", cdTime);
            // �o�ߎ����������Ă���
            cdTime -= Time.deltaTime;
            // 0�b�ȉ��ɂȂ�����J�E���g�_�E���^�C����0�ɌŒ肵,�Q�[���I�[�o�[�ɂ���
            if (cdTime <= 0)
            {
                cdTime = 0;
                gameOver = true;
            }
        }
    }

    /// ------private�֐�------- ///
    /// --------�֐��ꗗ-------- ///
}
public partial class TimeDirector
{
    /// --------�ϐ��ꗗ-------- ///
    /// -------public�ϐ�------- ///

    public bool gameStart = false; // �Q�[�����X�^�[�g�������ǂ���

    /// -------public�ϐ�------- ///
    /// -----protected�ϐ�------ ///



    /// -----protected�ϐ�------ ///
    /// ------private�ϐ�------- ///

    private bool gameOver = false; // �Q�[���I�[�o�[���ǂ���
    private Animator aniScd; // gameStartcd��animator�ϐ�

    [SerializeField] private float cdTime; // �J�E���g�_�E���^�C���ϐ�

    [SerializeField] private TextMeshProUGUI timeText; // �J�E���g�_�E���e�L�X�g�\���p

    [SerializeField] private StarDirector starDirector; // StarDirector�ϐ�

    [SerializeField] private GameObject gameStartcd; // �Q�[���X�^�[�g���̃J�E���g�_�E���ϐ�
    [SerializeField] private GameObject graphBackImage; // �O���t�w�i�p�ϐ�
    [SerializeField] private GameObject nextButton; // �{�^���ϐ�
    [SerializeField] private GameObject buttonFrame; // �t���[���ϐ�
    [SerializeField] private GameObject starGauge; // �����l�Q�[�W

    [SerializeField] private ButtonScript buttonScript; // ButtonScript�ϐ�

    [SerializeField] private ScoreDirector scoreDirector; // ScoreDirector�ϐ�

    [SerializeField] private DiamondMesh diamondFrame; // �t���[���pDiamondMesh�ϐ�
    [SerializeField] private DiamondMesh diamondGraph; // �O���t�pDiamondMesh�ϐ�

    [SerializeField] private AudioSource SEAudioSource; // SE�p�I�[�f�B�I�\�[�X

    [SerializeField] private Animator animAi; // �L�����N�^�[�̃A�j���[�V�����p�ϐ�

    /// ------private�ϐ�------- ///
    /// -------�v���p�e�B------- ///



    /// -------�v���p�e�B------- ///
    /// --------�ϐ��ꗗ-------- ///
}
