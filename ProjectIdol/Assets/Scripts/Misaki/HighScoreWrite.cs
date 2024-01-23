using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoreWrite : MonoBehaviour
{
    public enum HighScore // HighScore�̗񋓌^
    {
        PuzzleScore, // �p�Y�����[�h�̃X�R�A
        PuzzleRank, // �p�Y�����[�h�̃����N
        RhythmScore, // ���Y�����[�h�̃X�R�A
        RhythmRank // ���Y�����[�h�̃����N
    }
    public DataManager dataManager; // DataManager�ϐ�
    [EnumIndex(typeof(HighScore))]
    public TextMeshProUGUI[] scoreText = new TextMeshProUGUI[4]; // �e�L�X�g�z��

    // Start is called before the first frame update
    void Start()
    {
        ShowHighScore(); // �n�C�X�R�A��\��
    }
    public void ResetHighScore()
    {
        dataManager.ResetHighScore(); // �n�C�X�R�A�����Z�b�g
        ShowHighScore(); // �n�C�X�R�A��\������
    }
    private void ShowHighScore()
    {
        scoreText[0].text = string.Format("{0:00000000}Pt", dataManager.data.puzzleHighScore); // �p�Y�����[�h�X�R�A�����o��
        scoreText[1].text = string.Format(dataManager.data.puzzleHighScoreRank); // �p�Y�����[�h�����N�����o��
        scoreText[2].text = string.Format("{0:00000000}Pt", dataManager.data.rhythmHighScore); // ���Y�����[�h�X�R�A�����o��
        scoreText[3].text = string.Format(dataManager.data.rhythmHighScoreRank); // ���Y�����[�h�����N�����o��
    }
}
