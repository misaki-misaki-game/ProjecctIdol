using System;

[Serializable]
public class MasterData // json�f�[�^�Ƃ��ĕۑ�����class
{
    public float puzzleHighScore = 0f; // �p�Y���n�C�X�R�A
    public float[] puzzleRanking = new float[10]; // �p�Y�������L���O
    public float rhythmHighScore = 0f; // ���Y���n�C�X�R�A
    public float[] rhythmRanking = new float[10]; // ���Y�������L���O
    public string puzzleHighScoreRank = "D"; // �p�Y���n�C�X�R�A�����N
    public string rhythmHighScoreRank = "D"; // ���Y���n�C�X�R�A�����N
    public string defaultRank = "D"; // �f�t�H���g�����N
}

