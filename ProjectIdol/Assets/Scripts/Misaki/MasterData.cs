using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class MasterData // jsonデータとして保存するclass
{
    public float puzzleHighScore = 0f; // パズルハイスコア
    public float rhythmHighScore = 0f; // リズムハイスコア
    public string puzzleHighScoreRank = "D"; // パズルハイスコアランク
    public string rhythmHighScoreRank = "D"; // リズムハイスコアランク
    public string defaultRank = "D"; // デフォルトランク
}

