using UnityEngine;
using TMPro;

public partial class HighScoreWrite : MonoBehaviour
{
    /// --------関数一覧-------- ///
    /// -------public関数------- ///

    public void ResetHighScore()
    {
        dataManager.ResetHighScore(); // ハイスコアをリセット
        ShowHighScore(); // ハイスコアを表示する
    }

    /// -------public関数------- ///
    /// -----protected関数------ ///



    /// -----protected関数------ ///
    /// ------private関数------- ///

    private void Start()
    {
        ShowHighScore(); // ハイスコアを表示
    }

    private void ShowHighScore()
    {
        scoreText[0].text = string.Format("{0:00000000}Pt", dataManager.data.puzzleHighScore); // パズルモードスコア書き出し
        scoreText[1].text = string.Format(dataManager.data.puzzleHighScoreRank); // パズルモードランク書き出し
        scoreText[2].text = string.Format("{0:00000000}Pt", dataManager.data.rhythmRanking[0]); // リズムモードスコア書き出し
        scoreText[3].text = string.Format(dataManager.data.rhythmHighScoreRank); // リズムモードランク書き出し
    }


    /// ------private関数------- ///
    /// --------関数一覧-------- ///
}
public partial class HighScoreWrite
{
    /// --------変数一覧-------- ///
    /// -------public変数------- ///

    public enum HighScore // HighScoreの列挙型
    {
        PuzzleScore, // パズルモードのスコア
        PuzzleRank, // パズルモードのランク
        RhythmScore, // リズムモードのスコア
        RhythmRank // リズムモードのランク
    }

    /// -------public変数------- ///
    /// -----protected変数------ ///



    /// -----protected変数------ ///
    /// ------private変数------- ///

    [EnumIndex(typeof(HighScore))]
    [SerializeField] private TextMeshProUGUI[] scoreText = new TextMeshProUGUI[4]; // テキスト配列

    [SerializeField] private DataManager dataManager; // DataManager変数

    /// ------private変数------- ///
    /// -------プロパティ------- ///



    /// -------プロパティ------- ///
    /// --------変数一覧-------- ///
}