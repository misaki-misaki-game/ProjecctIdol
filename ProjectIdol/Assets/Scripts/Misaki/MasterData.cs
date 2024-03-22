using System;

[Serializable]
public class MasterData // jsonデータとして保存するclass
{
    public float puzzleHighScore = 0f; // パズルハイスコア
    public float[] puzzleRanking = new float[10]; // パズルランキング
    public float rhythmHighScore = 0f; // リズムハイスコア
    public float[] rhythmRanking = new float[10]; // リズムランキング
    public string puzzleHighScoreRank = "D"; // パズルハイスコアランク
    public string rhythmHighScoreRank = "D"; // リズムハイスコアランク
    public string defaultRank = "D"; // デフォルトランク
}

