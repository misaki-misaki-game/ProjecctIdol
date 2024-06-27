using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public partial class HighScoreWrite : MonoBehaviour
{
    /// --------�֐��ꗗ-------- ///
    /// -------public�֐�------- ///

    public void ResetHighScore()
    {
        dataManager.ResetHighScore(); // �n�C�X�R�A�����Z�b�g
        ShowHighScore(); // �n�C�X�R�A��\������
    }

    /// -------public�֐�------- ///
    /// -----protected�֐�------ ///



    /// -----protected�֐�------ ///
    /// ------private�֐�------- ///

    private void Start()
    {
        // �p�Y���V�[���ƃ��Y���V�[�����擾
        puzzle = SceneUtility.GetBuildIndexByScenePath("PuzzleScene");
        rhythm = SceneUtility.GetBuildIndexByScenePath("RhythmGame");

        // �p�Y���V�[���ƃ��Y���V�[�����r���h����Ă��邩��bool�ɗ��Ƃ�����
        isPuzzleBuilt = puzzle != -1;
        isRhythmBuilt = rhythm != -1;

        ShowHighScore(); // �n�C�X�R�A��\��
    }

    private void ShowHighScore()
    {
        if (isPuzzleBuilt && isRhythmBuilt || !isPuzzleBuilt && !isRhythmBuilt) Debug.LogError("�ǂ�����r���h����Ă���A�܂��́A�ǂ�����r���h����Ă��܂���");
        else if (isPuzzleBuilt)
        {
            scoreText[0].text = string.Format("{0:00000000}Pt", dataManager.data.puzzleHighScore); // �p�Y�����[�h�X�R�A�����o��
            scoreText[1].text = string.Format(dataManager.data.puzzleHighScoreRank); // �p�Y�����[�h�����N�����o��
        }
        else if (isRhythmBuilt)
        {
            scoreText[0].text = string.Format("{0:00000000}Pt", dataManager.data.rhythmRanking[0]); // ���Y�����[�h�X�R�A�����o��
            scoreText[1].text = string.Format(dataManager.data.rhythmHighScoreRank); // ���Y�����[�h�����N�����o��
        }
    }


    /// ------private�֐�------- ///
    /// --------�֐��ꗗ-------- ///
}
public partial class HighScoreWrite
{
    /// --------�ϐ��ꗗ-------- ///
    /// -------public�ϐ�------- ///

    public enum HighScore // HighScore�̗񋓌^
    {
        PuzzleScore, // �p�Y�����[�h�̃X�R�A
        PuzzleRank, // �p�Y�����[�h�̃����N
        RhythmScore, // ���Y�����[�h�̃X�R�A
        RhythmRank // ���Y�����[�h�̃����N
    }

    /// -------public�ϐ�------- ///
    /// -----protected�ϐ�------ ///



    /// -----protected�ϐ�------ ///
    /// ------private�ϐ�------- ///

    // �r���h���Ă��邩�ǂ���(bool)
    bool isPuzzleBuilt;
    bool isRhythmBuilt;

    // �r���h���Ă��邩�ǂ���(int)
    int puzzle;
    int rhythm;

    [EnumIndex(typeof(HighScore))]
    [SerializeField] private TextMeshProUGUI[] scoreText = new TextMeshProUGUI[2]; // �e�L�X�g�z��

    [SerializeField] private DataManager dataManager; // DataManager�ϐ�

    /// ------private�ϐ�------- ///
    /// -------�v���p�e�B------- ///



    /// -------�v���p�e�B------- ///
    /// --------�ϐ��ꗗ-------- ///
}