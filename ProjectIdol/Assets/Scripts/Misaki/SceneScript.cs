using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
// maincamera�ȂǏ�ɂ�����̂ɃA�^�b�`���邱�Ɛ���
// �X�N���v�g�uFadeManager�v�Ƌ��Ɏg�p����

public class SceneScript : MonoBehaviour
{
    [SerializeField] GameObject fadeCanvas; //prefab��FadeCanvas������
    public AudioSource SEAudioSource; // SE�p�I�[�f�B�I�\�[�X
    GameObject scoreSaveObj;                //�X�R�A�ۑ��p�I�u�W�F�N�g


    // Start is called before the first frame update
    void Start()
    {
        if (!FadeManager.isFadeInstance)  // �t�F�[�h�pCanvas�������ł��Ă��Ȃ����
        {
            Instantiate(fadeCanvas);     // Canvas����
        }
        Invoke("findFadeObject", 0.02f); // �N�����p��Canvas�̏�����������Ƒ҂�
    }

    void findFadeObject()                                      // ��������Canvas�̃t�F�[�h�C���t���O�𗧂Ă�֐�
    {
        fadeCanvas = GameObject.FindGameObjectWithTag("Fade"); // FadeCanvas��������
        fadeCanvas.GetComponent<FadeManager>().fadeIn();       // �t�F�[�h�C���֐����Ăяo��
    }

    public async void sceneChange(string sceneName)       // �V�[���`�F���W�֐��@�{�^������ȂǂŌĂяo��
    {
        if (SEAudioSource) SEAudioSource.Play(); // �I��SE��炷
        await Task.Delay(600);                           // ������܂ő҂�
        fadeCanvas.GetComponent<FadeManager>().fadeOut(); // �t�F�[�h�A�E�g�֐����Ăяo��
        await Task.Delay(1000);                           // �Ó]����܂ő҂�
        SceneManager.LoadScene(sceneName);                // �V�[���`�F���W
    }

    public async void resultSceneChange(string sceneName)  // �V�[���`�F���W�֐��@ScoreSaveObject�������p
    {
        scoreSaveObj = GameObject.Find("ScoreSaveObject"); // �X�R�A�ۑ��p�I�u�W�F�N�g��{��
        Destroy(scoreSaveObj);                      �@     // �X�R�A�ۑ��p�I�u�W�F�N�g���폜
        fadeCanvas.GetComponent<FadeManager>().fadeOut();  // �t�F�[�h�A�E�g�֐����Ăяo��
        await Task.Delay(1000);                            // �Ó]����܂ő҂�
        SceneManager.LoadScene(sceneName);                 // �V�[���`�F���W
    }

}

