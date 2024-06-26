using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
// maincameraなど常にいるものにアタッチすること推奨
// スクリプト「FadeManager」と共に使用する

public partial class SceneScript : MonoBehaviour
{
    /// --------関数一覧-------- ///
    /// -------public関数------- ///

    /// <summary>
    /// シーンチェンジ関数　ボタン操作などで呼び出す
    /// </summary>
    /// <param name="sceneName">遷移したいシーン名</param>
    public async void SceneChange(string sceneName)
    {
        if (SEAudioSource)
        {
            SEAudioSource.Play(); // 選択SEを鳴らす
            int timeSE = (int)SEAudioSource.clip.length * 100; // SEの長さを代入
            await Task.Delay(timeSE); // 音が鳴るまで待つ
        }
        fadeCanvas.GetComponent<FadeManager>().fadeOut(); // フェードアウト関数を呼び出し
        await Task.Delay((int)fadeCanvas.GetComponent<FadeManager>().fadeSpeed * 1000); // 暗転するまで待つ
        SceneManager.LoadScene(sceneName);                // シーンチェンジ
    }

    /// <summary>
    /// シーンチェンジ関数　ボタン操作などで呼び出す 
    /// ビルドされているモードに遷移する
    /// </summary>
    public async void SceneChange()
    {
        if (SEAudioSource)
        {
            SEAudioSource.Play(); // 選択SEを鳴らす
            int timeSE = (int)SEAudioSource.clip.length * 100; // SEの長さを代入
            await Task.Delay(timeSE); // 音が鳴るまで待つ
        }
        // パズルシーンとリズムシーンを取得
        int puzzle = SceneUtility.GetBuildIndexByScenePath("PuzzleScene");
        int rhythm = SceneUtility.GetBuildIndexByScenePath("RhythmGame");

        // パズルシーンとリズムシーンがビルドされているかをboolに落とし込む
        bool isPuzzleBuilt = puzzle != -1;
        bool isRhythmBuilt = rhythm != -1;

        fadeCanvas.GetComponent<FadeManager>().fadeOut(); // フェードアウト関数を呼び出し
        await Task.Delay((int)fadeCanvas.GetComponent<FadeManager>().fadeSpeed * 1000); // 暗転するまで待つ

        // シーンチェンジ
        if (isPuzzleBuilt && isRhythmBuilt || !isPuzzleBuilt && !isRhythmBuilt) Debug.LogError("どちらもビルドされている、または、どちらもビルドされていません");
        else if (isPuzzleBuilt) SceneManager.LoadScene("PuzzleScene");
        else if (isRhythmBuilt) SceneManager.LoadScene("RhythmGame");
    }

    /// -------public関数------- ///
    /// -----protected関数------ ///



    /// -----protected関数------ ///
    /// ------private関数------- ///

    private void Start()
    {
        if (!FadeManager.isFadeInstance)  // フェード用Canvasが召喚できていなければ
        {
            Instantiate(fadeCanvas);     // Canvas召喚
        }
        Invoke("FindFadeObject", 0.02f); // 起動時用にCanvasの召喚をちょっと待つ
    }

    /// <summary>
    /// 召喚したCanvasのフェードインフラグを立てる関数
    /// </summary>
    private void FindFadeObject()
    {
        fadeCanvas = GameObject.FindGameObjectWithTag("Fade"); // FadeCanvasを見つける
        fadeCanvas.GetComponent<FadeManager>().fadeIn();       // フェードイン関数を呼び出し
    }

    /// ------private関数------- ///
    /// --------関数一覧-------- ///
}
public partial class SceneScript
{
    /// --------変数一覧-------- ///
    /// -------public変数------- ///



    /// -------public変数------- ///
    /// -----protected変数------ ///



    /// -----protected変数------ ///
    /// ------private変数------- ///

    [SerializeField] private GameObject fadeCanvas; //prefabのFadeCanvasを入れる
    [SerializeField] private AudioSource SEAudioSource; // SE用オーディオソース

    /// ------private変数------- ///
    /// -------プロパティ------- ///



    /// -------プロパティ------- ///
    /// --------変数一覧-------- ///
}