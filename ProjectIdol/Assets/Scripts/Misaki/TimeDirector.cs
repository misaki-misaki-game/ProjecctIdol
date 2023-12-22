using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TimeDirector : MonoBehaviour
{
    bool gameOver = false; // �Q�[���I�[�o�[���ǂ���
    public bool gameStart = false; // �Q�[�����X�^�[�g�������ǂ���
    public float cdTime; // �J�E���g�_�E���^�C���ϐ�
    public TextMeshProUGUI timeText; // �J�E���g�_�E���e�L�X�g�\���p
    public StarDirector starDirector; // StarDirector�ϐ�
    public GameObject gameStartcd; // �Q�[���X�^�[�g���̃J�E���g�_�E���ϐ�
    public GameObject graphBackImage; // �O���t�w�i�p�ϐ�
    public ButtonScript buttonScript; // ButtonScript�ϐ�
    public ScoreDirector scoreDirector; // ScoreDirector�ϐ�
    public DiamondMesh diamondFrame; // �t���[���pDiamondMesh�ϐ�
    public DiamondMesh diamondGraph; // �O���t�pDiamondMesh�ϐ�
    public Animator animAi; // �A�C�̃A�j���[�V�����p�ϐ�
    Animator aniScd; // gameStartcd��animator�ϐ�

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60; // 60fps�ɌŒ�
        aniScd = gameStartcd.GetComponent<Animator>(); // Animetor���i�[����
        Count3(); // �Q�[�����n�܂�܂ł̃J�E���g�_�E�����X�^�[�g����
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // �Q�[�����X�^�[�g,����,�Q�[���I�[�o�[�ɂȂ��Ă��Ȃ�,����,�X�^�[���[�h�ł͂Ȃ��̂ł����
        if (gameStart && !gameOver && starDirector.isStarMode == false)
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
        if (gameOver) // �Q�[���I�[�o�[�ł����
        {
            this.gameOver = false; // false�ɂ��邱�Ƃł���if����1�񂾂��Ăяo��
            this.gameStart = false; // ��L���s���Ə��if�����Ăяo�����̂ł�����s��Ȃ����߂�false�ɂ���
            animAi.SetTrigger("isDanceEnd"); // �A�C�̃A�j���[�V�������I������
            buttonScript.gameStart = false; // false�ɂ��ă{�^�����N���b�N�ł��Ȃ��悤�ɂ���
            scoreDirector.SetRank(); // �e�X�R�A�ɉ����ă����N��ݒ肷��
            graphBackImage.SetActive(true); // �O���t�p�w�i��\��
            // diamondFrame��diamondGraph�̃Z�b�g�A�b�v���s��
            diamondFrame.SetUp();
            diamondGraph.SetUp();
        }
    }
    public void Count3()
    {
        gameStartcd.SetActive(true); // gameStartcd��\������
        aniScd.SetBool("isCountdownStart", true); // animator��isCountdownStart�ϐ���^�ɂ���
    }
}
