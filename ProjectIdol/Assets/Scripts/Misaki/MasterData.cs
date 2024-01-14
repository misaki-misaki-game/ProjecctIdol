using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class MasterData // jsonデータとして保存するclass
{
    public float puzzleHighScore = 0; // パズルハイスコア
    public float rhythmHighScore = 0; // リズムハイスコア
    public string puzzleHighScoreRank = "D"; // パズルハイスコアランク
    public string rhythmHighScoreRank = "D"; // リズムハイスコアランク
    public string defaultRank = "D"; // デフォルトランク
}

