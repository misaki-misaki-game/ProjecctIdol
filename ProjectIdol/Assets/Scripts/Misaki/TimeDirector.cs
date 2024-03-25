using UnityEngine;
using TMPro;

public class TimeDirector : MonoBehaviour
{
    bool gameOver = false; // �Q�[���I�[�o�[���ǂ���
    public bool gameStart = false; // �Q�[�����X�^�[�g�������ǂ���
    public float cdTime; // �J�E���g�_�E���^�C���ϐ�
    public TextMeshProUGUI timeText; // �J�E���g�_�E���e�L�X�g�\���p
    public StarDirector starDirector; // StarDirector�ϐ�
    public GameObject gameStartcd; // �Q�[���X�^�[�g���̃J�E���g�_�E���ϐ�
    public GameObject graphBackImage; // �O���t�w�i�p�ϐ�
    public GameObject nextButton; // �{�^���ϐ�
    public GameObject buttonFrame; // �t���[���ϐ�
    public ButtonScript buttonScript; // ButtonScript�ϐ�
    public ScoreDirector scoreDirector; // ScoreDirector�ϐ�
    public DiamondMesh diamondFrame; // �t���[���pDiamondMesh�ϐ�
    public DiamondMesh diamondGraph; // �O���t�pDiamondMesh�ϐ�
    public Animator animAi; // �L�����N�^�[�̃A�j���[�V�����p�ϐ�
    public AudioSource SEAudioSource; // SE�p�I�[�f�B�I�\�[�X
    Animator aniScd; // gameStartcd��animator�ϐ�

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60; // 60fps�ɌŒ�
        aniScd = gameStartcd.GetComponent<Animator>(); // Animetor���i�[����
        aniScd.GetComponent<GameStartCountDownScript>().CountDown();
    }

    // Update is called once per frame
    void FixedUpdate()
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
        if (gameStart && !gameOver && starDirector.starState == StarDirector.StarState.NormalMode)
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
}
