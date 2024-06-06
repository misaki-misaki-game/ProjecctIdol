using UnityEngine;
using TMPro;

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
        ShowHighScore(); // �n�C�X�R�A��\��
    }

    private void ShowHighScore()
    {
        scoreText[0].text = string.Format("{0:00000000}Pt", dataManager.data.puzzleHighScore); // �p�Y�����[�h�X�R�A�����o��
        scoreText[1].text = string.Format(dataManager.data.puzzleHighScoreRank); // �p�Y�����[�h�����N�����o��
        scoreText[2].text = string.Format("{0:00000000}Pt", dataManager.data.rhythmRanking[0]); // ���Y�����[�h�X�R�A�����o��
        scoreText[3].text = string.Format(dataManager.data.rhythmHighScoreRank); // ���Y�����[�h�����N�����o��
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

    [EnumIndex(typeof(HighScore))]
    [SerializeField] private TextMeshProUGUI[] scoreText = new TextMeshProUGUI[4]; // �e�L�X�g�z��

    [SerializeField] private DataManager dataManager; // DataManager�ϐ�

    /// ------private�ϐ�------- ///
    /// -------�v���p�e�B------- ///



    /// -------�v���p�e�B------- ///
    /// --------�ϐ��ꗗ-------- ///
}