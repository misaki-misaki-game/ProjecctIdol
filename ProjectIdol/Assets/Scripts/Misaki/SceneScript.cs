using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
// maincamera�ȂǏ�ɂ�����̂ɃA�^�b�`���邱�Ɛ���
// �X�N���v�g�uFadeManager�v�Ƌ��Ɏg�p����

public class SceneScript : MonoBehaviour
{
    [SerializeField] GameObject fadeCanvas; //prefab��FadeCanvas������
    public AudioSource SEAudioSource; // SE�p�I�[�f�B�I�\�[�X

    // Start is called before the first frame update
    void Start()
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
    void findFadeObject()
    {
        fadeCanvas = GameObject.FindGameObjectWithTag("Fade"); // FadeCanvas��������
        fadeCanvas.GetComponent<FadeManager>().fadeIn();       // �t�F�[�h�C���֐����Ăяo��
    }
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
}

