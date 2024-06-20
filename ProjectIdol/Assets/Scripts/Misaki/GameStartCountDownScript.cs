using TMPro;
using UnityEngine;
using UnityEngine.UI;

public partial class GameStartCountDownScript : MonoBehaviour
{
    /// --------�֐��ꗗ-------- ///
    /// -------public�֐�------- ///

    /// <summary>
    /// �Q�[���̃J�E���g�_�E���J�n�̊֐�
    /// </summary>
    public void CountDown()
    {
        // count�̒l�ɂ���ĕ\�����镶����ύX
        // count == 0�̏ꍇ�̂�GameStart�̃A�j���[�V�������J�n
        switch (count)
        {
            case 3:
                gameStartText.text = string.Format("3");
                break;
            case 2:
                gameStartText.text = string.Format("2");
                break;
            case 1:
                gameStartText.text = string.Format("1");
                break;
            case 0:
                gameStartText.text = string.Format("Live\nStart!!");
                aniScd.SetTrigger("GameStart");
                return;
        }
        count--; // ���̃A�j���[�V�����̍ۂɕʂ̕�����\�������邽�߂�count�����炷
        aniScd.SetTrigger("CountDown"); // �J�E���g�_�E���̃A�j���[�V�����𓮂���
    }

    /// <summary>
    /// �Q�[���X�^�[�g�֐�
    /// </summary>
    public void GameStart()
    {
        timeDirector.gameStart = true; // �������Ԃ��X�^�[�g����
        buttonScript.gameStart = true; // �{�^���̃N���b�N��������
        buttonScript.resetButton.GetComponent<Button>().interactable = true; // �{�^����������悤�ɂ���
        Music.Play("Main Camera"); // BGM��炷
        animAi.SetTrigger("isDanceStart"); // �A�C�̃A�j���[�V�������X�^�[�g����
        gameStartcd.SetActive(false); // �J�E���g�L�����p�X���\���ɂ���
    }


    /// -------public�֐�------- ///
    /// -----protected�֐�------ ///



    /// -----protected�֐�------ ///
    /// ------private�֐�------- ///

    private void Start()
    {
        aniScd = gameObject.GetComponent<Animator>(); // Animator���i�[
    }


    /// ------private�֐�------- ///
    /// --------�֐��ꗗ-------- ///
}
public partial class GameStartCountDownScript
{
    /// --------�ϐ��ꗗ-------- ///
    /// -------public�ϐ�------- ///



    /// -------public�ϐ�------- ///
    /// -----protected�ϐ�------ ///



    /// -----protected�ϐ�------ ///
    /// ------private�ϐ�------- ///

    private int count = 3; // �J�E���g�_�E���̒l

    private Animator aniScd; // gameStartcd��animator�ϐ�

    [SerializeField] private TextMeshProUGUI gameStartText; // �Q�[���X�^�[�g���̃J�E���g�_�E���e�L�X�g�\���p

    [SerializeField] private TimeDirector timeDirector; // timeDirector�ϐ�

    [SerializeField] private GameObject gameStartcd; // �Q�[���X�^�[�g���̃J�E���g�_�E���ϐ�

    [SerializeField] private ButtonScript buttonScript; // ButtonScript�ϐ�

    [SerializeField] private Animator animAi; // �A�C�̃A�j���[�V�����p�ϐ�

    [SerializeField] private AudioSource BGMAudioSource; // BGM�p�I�[�f�B�I�\�[�X

    //[SerializeField] private MusicUnity music; // �~���[�W�b�N���j�e�B�ϐ�

    /// ------private�ϐ�------- ///
    /// -------�v���p�e�B------- ///



    /// -------�v���p�e�B------- ///
    /// --------�ϐ��ꗗ-------- ///
}