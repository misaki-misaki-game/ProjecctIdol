using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
// maincameraなど常にいるものにアタッチすること推奨
// スクリプト「FadeManager」と共に使用する

public class SceneScript : MonoBehaviour
{
    [SerializeField] GameObject fadeCanvas; //prefabのFadeCanvasを入れる
    public AudioSource SEAudioSource; // SE用オーディオソース
    GameObject scoreSaveObj;                //スコア保存用オブジェクト


    // Start is called before the first frame update
    void Start()
    {
        if (!FadeManager.isFadeInstance)  // フェード用Canvasが召喚できていなければ
        {
            Instantiate(fadeCanvas);     // Canvas召喚
        }
        Invoke("findFadeObject", 0.02f); // 起動時用にCanvasの召喚をちょっと待つ
    }

    void findFadeObject()                                      // 召喚したCanvasのフェードインフラグを立てる関数
    {
        fadeCanvas = GameObject.FindGameObjectWithTag("Fade"); // FadeCanvasを見つける
        fadeCanvas.GetComponent<FadeManager>().fadeIn();       // フェードイン関数を呼び出し
    }

    public async void sceneChange(string sceneName)       // シーンチェンジ関数　ボタン操作などで呼び出す
    {
        if (SEAudioSource) SEAudioSource.Play(); // 選択SEを鳴らす
        await Task.Delay(600);                           // 音が鳴るまで待つ
        fadeCanvas.GetComponent<FadeManager>().fadeOut(); // フェードアウト関数を呼び出し
        await Task.Delay(1000);                           // 暗転するまで待つ
        SceneManager.LoadScene(sceneName);                // シーンチェンジ
    }

    public async void resultSceneChange(string sceneName)  // シーンチェンジ関数　ScoreSaveObjectを消す用
    {
        scoreSaveObj = GameObject.Find("ScoreSaveObject"); // スコア保存用オブジェクトを捜索
        Destroy(scoreSaveObj);                      　     // スコア保存用オブジェクトを削除
        fadeCanvas.GetComponent<FadeManager>().fadeOut();  // フェードアウト関数を呼び出し
        await Task.Delay(1000);                            // 暗転するまで待つ
        SceneManager.LoadScene(sceneName);                 // シーンチェンジ
    }

}

