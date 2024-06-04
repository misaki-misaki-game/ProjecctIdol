using TMPro;
using UnityEngine;

public partial class GameStartCountDownScript : MonoBehaviour
{
    private void Start()
    {
        aniScd = gameObject.GetComponent<Animator>(); // Animator���i�[
    }
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
        BGMAudioSource.Play(); // BGM��炷
        animAi.SetTrigger("isDanceStart"); // �A�C�̃A�j���[�V�������X�^�[�g����
        gameStartcd.SetActive(false); // �J�E���g�L�����p�X���\���ɂ���
    }
    /// --------�֐��ꗗ-------- ///
    /// -------public�֐�------- ///



    /// -------public�֐�------- ///
    /// -----protected�֐�------ ///



    /// -----protected�֐�------ ///
    /// ------private�֐�------- ///



    /// ------private�֐�------- ///
    /// --------�֐��ꗗ-------- ///
}
public partial class GameStartCountDownScript
{
    /// --------�ϐ��ꗗ-------- ///
    /// -------public�ϐ�------- ///

    public TextMeshProUGUI gameStartText; // �Q�[���X�^�[�g���̃J�E���g�_�E���e�L�X�g�\���p

    public TimeDirector timeDirector; // timeDirector�ϐ�

    public GameObject gameStartcd; // �Q�[���X�^�[�g���̃J�E���g�_�E���ϐ�

    public ButtonScript buttonScript; // ButtonScript�ϐ�

    public Animator animAi; // �A�C�̃A�j���[�V�����p�ϐ�

    public AudioSource BGMAudioSource; // BGM�p�I�[�f�B�I�\�[�X


    /// -------public�ϐ�------- ///
    /// -----protected�ϐ�------ ///



    /// -----protected�ϐ�------ ///
    /// ------private�ϐ�------- ///

    private int count = 3; // �J�E���g�_�E���̒l

    private Animator aniScd; // gameStartcd��animator�ϐ�

    /// ------private�ϐ�------- ///
    /// -------�v���p�e�B------- ///



    /// -------�v���p�e�B------- ///
    /// --------�ϐ��ꗗ-------- ///
}