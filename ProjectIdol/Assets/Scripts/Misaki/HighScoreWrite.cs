using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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
        // パズルシーンとリズムシーンを取得
        puzzle = SceneUtility.GetBuildIndexByScenePath("PuzzleScene");
        rhythm = SceneUtility.GetBuildIndexByScenePath("RhythmGame");

        // パズルシーンとリズムシーンがビルドされているかをboolに落とし込む
        isPuzzleBuilt = puzzle != -1;
        isRhythmBuilt = rhythm != -1;

        ShowHighScore(); // ハイスコアを表示
    }

    private void ShowHighScore()
    {
        if (isPuzzleBuilt && isRhythmBuilt || !isPuzzleBuilt && !isRhythmBuilt) Debug.LogError("どちらもビルドされている、または、どちらもビルドされていません");
        else if (isPuzzleBuilt)
        {
            scoreText[0].text = string.Format("{0:00000000}Pt", dataManager.data.puzzleHighScore); // パズルモードスコア書き出し
            scoreText[1].text = string.Format(dataManager.data.puzzleHighScoreRank); // パズルモードランク書き出し
        }
        else if (isRhythmBuilt)
        {
            scoreText[0].text = string.Format("{0:00000000}Pt", dataManager.data.rhythmRanking[0]); // リズムモードスコア書き出し
            scoreText[1].text = string.Format(dataManager.data.rhythmHighScoreRank); // リズムモードランク書き出し
        }
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

    // ビルドしているかどうか(bool)
    bool isPuzzleBuilt;
    bool isRhythmBuilt;

    // ビルドしているかどうか(int)
    int puzzle;
    int rhythm;

    [EnumIndex(typeof(HighScore))]
    [SerializeField] private TextMeshProUGUI[] scoreText = new TextMeshProUGUI[2]; // テキスト配列

    [SerializeField] private DataManager dataManager; // DataManager変数

    /// ------private変数------- ///
    /// -------プロパティ------- ///



    /// -------プロパティ------- ///
    /// --------変数一覧-------- ///
}