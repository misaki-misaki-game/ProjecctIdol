using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
// maincamera�ȂǏ�ɂ�����̂ɃA�^�b�`���邱�Ɛ���
// �X�N���v�g�uFadeManager�v�Ƌ��Ɏg�p����

public partial class SceneScript : MonoBehaviour
{
    /// --------�֐��ꗗ-------- ///
    /// -------public�֐�------- ///

    /// <summary>
    /// �V�[���`�F���W�֐��@�{�^������ȂǂŌĂяo��
    /// </summary>
    /// <param name="sceneName">�J�ڂ������V�[����</param>
    public async void sceneChange(string sceneName)       // �V�[���`�F���W�֐��@�{�^������ȂǂŌĂяo��
    {
        if (SEAudioSource)
        {
            SEAudioSource.Play(); // �I��SE��炷
            int timeSE = (int)SEAudioSource.clip.length * 100; // SE�̒�������
            await Task.Delay(timeSE); // ������܂ő҂�
        }
        fadeCanvas.GetComponent<FadeManager>().fadeOut(); // �t�F�[�h�A�E�g�֐����Ăяo��
        await Task.Delay((int)fadeCanvas.GetComponent<FadeManager>().fadeSpeed * 1000); // �Ó]����܂ő҂�
        SceneManager.LoadScene(sceneName);                // �V�[���`�F���W
    }

    /// -------public�֐�------- ///
    /// -----protected�֐�------ ///



    /// -----protected�֐�------ ///
    /// ------private�֐�------- ///

    private void Start()
    {
        if (!FadeManager.isFadeInstance)  // �t�F�[�h�pCanvas�������ł��Ă��Ȃ����
        {
            Instantiate(fadeCanvas);     // Canvas����
        }
        Invoke("findFadeObject", 0.02f); // �N�����p��Canvas�̏�����������Ƒ҂�
    }

    /// <summary>
    /// ��������Canvas�̃t�F�[�h�C���t���O�𗧂Ă�֐�
    /// </summary>
    private void findFadeObject()
    {
        fadeCanvas = GameObject.FindGameObjectWithTag("Fade"); // FadeCanvas��������
        fadeCanvas.GetComponent<FadeManager>().fadeIn();       // �t�F�[�h�C���֐����Ăяo��
    }

    /// ------private�֐�------- ///
    /// --------�֐��ꗗ-------- ///
}
public partial class SceneScript
{
    /// --------�ϐ��ꗗ-------- ///
    /// -------public�ϐ�------- ///



    /// -------public�ϐ�------- ///
    /// -----protected�ϐ�------ ///



    /// -----protected�ϐ�------ ///
    /// ------private�ϐ�------- ///

    [SerializeField] private GameObject fadeCanvas; //prefab��FadeCanvas������
    [SerializeField] private AudioSource SEAudioSource; // SE�p�I�[�f�B�I�\�[�X

    /// ------private�ϐ�------- ///
    /// -------�v���p�e�B------- ///



    /// -------�v���p�e�B------- ///
    /// --------�ϐ��ꗗ-------- ///
}