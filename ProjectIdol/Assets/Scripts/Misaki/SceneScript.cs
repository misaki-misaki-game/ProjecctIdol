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
    public async void SceneChange(string sceneName)
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

    /// <summary>
    /// �V�[���`�F���W�֐��@�{�^������ȂǂŌĂяo�� 
    /// �r���h����Ă��郂�[�h�ɑJ�ڂ���
    /// </summary>
    public async void SceneChange()
    {
        if (SEAudioSource)
        {
            SEAudioSource.Play(); // �I��SE��炷
            int timeSE = (int)SEAudioSource.clip.length * 100; // SE�̒�������
            await Task.Delay(timeSE); // ������܂ő҂�
        }
        // �p�Y���V�[���ƃ��Y���V�[�����擾
        int puzzle = SceneUtility.GetBuildIndexByScenePath("PuzzleScene");
        int rhythm = SceneUtility.GetBuildIndexByScenePath("RhythmGame");

        // �p�Y���V�[���ƃ��Y���V�[�����r���h����Ă��邩��bool�ɗ��Ƃ�����
        bool isPuzzleBuilt = puzzle != -1;
        bool isRhythmBuilt = rhythm != -1;

        fadeCanvas.GetComponent<FadeManager>().fadeOut(); // �t�F�[�h�A�E�g�֐����Ăяo��
        await Task.Delay((int)fadeCanvas.GetComponent<FadeManager>().fadeSpeed * 1000); // �Ó]����܂ő҂�

        // �V�[���`�F���W
        if (isPuzzleBuilt && isRhythmBuilt || !isPuzzleBuilt && !isRhythmBuilt) Debug.LogError("�ǂ�����r���h����Ă���A�܂��́A�ǂ�����r���h����Ă��܂���");
        else if (isPuzzleBuilt) SceneManager.LoadScene("PuzzleScene");
        else if (isRhythmBuilt) SceneManager.LoadScene("RhythmGame");
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
        Invoke("FindFadeObject", 0.02f); // �N�����p��Canvas�̏�����������Ƒ҂�
    }

    /// <summary>
    /// ��������Canvas�̃t�F�[�h�C���t���O�𗧂Ă�֐�
    /// </summary>
    private void FindFadeObject()
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