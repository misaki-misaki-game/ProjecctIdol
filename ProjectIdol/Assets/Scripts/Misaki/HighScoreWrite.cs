using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoreWrite : MonoBehaviour
{
    public enum HighScore // HighScoreの列挙型
    {
        PuzzleScore, // パズルモードのスコア
        PuzzleRank, // パズルモードのランク
        RhythmScore, // リズムモードのスコア
        RhythmRank // リズムモードのランク
    }
    public DataManager dataManager; // DataManager変数
    [EnumIndex(typeof(HighScore))]
    public TextMeshProUGUI[] scoreText = new TextMeshProUGUI[4]; // テキスト配列

    // Start is called before the first frame update
    void Start()
    {
        ShowHighScore(); // ハイスコアを表示
    }
    public void ResetHighScore()
    {
        dataManager.ResetHighScore(); // ハイスコアをリセット
        ShowHighScore(); // ハイスコアを表示する
    }
    private void ShowHighScore()
    {
        scoreText[0].text = string.Format("{0:00000000}Pt", dataManager.data.puzzleHighScore); // パズルモードスコア書き出し
        scoreText[1].text = string.Format(dataManager.data.puzzleHighScoreRank); // パズルモードランク書き出し
        scoreText[2].text = string.Format("{0:00000000}Pt", dataManager.data.rhythmHighScore); // リズムモードスコア書き出し
        scoreText[3].text = string.Format(dataManager.data.rhythmHighScoreRank); // リズムモードランク書き出し
    }
}
