using TMPro;
using UnityEngine;
using UnityEngine.UI;

public partial class GameStartCountDownScript : MonoBehaviour
{
    /// --------関数一覧-------- ///
    /// -------public関数------- ///

    /// <summary>
    /// ゲームのカウントダウン開始の関数
    /// </summary>
    public void CountDown()
    {
        // countの値によって表示する文字を変更
        // count == 0の場合のみGameStartのアニメーションを開始
        switch (count)
        {
            case 3:
                gameStartText.text = string.Format("3");
                break;
            case 2:
                gameStartText.text = string.Format("2");
                break;
            case 1:
                gameStartText.text = string.Format("1");
                break;
            case 0:
                gameStartText.text = string.Format("Live\nStart!!");
                aniScd.SetTrigger("GameStart");
                return;
        }
        count--; // 次のアニメーションの際に別の文字を表示させるためにcountを減らす
        aniScd.SetTrigger("CountDown"); // カウントダウンのアニメーションを動かす
    }

    /// <summary>
    /// ゲームスタート関数
    /// </summary>
    public void GameStart()
    {
        timeDirector.gameStart = true; // 制限時間をスタートする
        buttonScript.gameStart = true; // ボタンのクリックを許可する
        buttonScript.resetButton.GetComponent<Button>().interactable = true; // ボタンを押せるようにする
        Music.Play("Main Camera"); // BGMを鳴らす
        animAi.SetTrigger("isDanceStart"); // アイのアニメーションをスタートする
        gameStartcd.SetActive(false); // カウントキャンパスを非表示にする
    }


    /// -------public関数------- ///
    /// -----protected関数------ ///



    /// -----protected関数------ ///
    /// ------private関数------- ///

    private void Start()
    {
        aniScd = gameObject.GetComponent<Animator>(); // Animatorを格納
    }


    /// ------private関数------- ///
    /// --------関数一覧-------- ///
}
public partial class GameStartCountDownScript
{
    /// --------変数一覧-------- ///
    /// -------public変数------- ///



    /// -------public変数------- ///
    /// -----protected変数------ ///



    /// -----protected変数------ ///
    /// ------private変数------- ///

    private int count = 3; // カウントダウンの値

    private Animator aniScd; // gameStartcdのanimator変数

    [SerializeField] private TextMeshProUGUI gameStartText; // ゲームスタート時のカウントダウンテキスト表示用

    [SerializeField] private TimeDirector timeDirector; // timeDirector変数

    [SerializeField] private GameObject gameStartcd; // ゲームスタート時のカウントダウン変数

    [SerializeField] private ButtonScript buttonScript; // ButtonScript変数

    [SerializeField] private Animator animAi; // アイのアニメーション用変数

    [SerializeField] private AudioSource BGMAudioSource; // BGM用オーディオソース

    //[SerializeField] private MusicUnity music; // ミュージックユニティ変数

    /// ------private変数------- ///
    /// -------プロパティ------- ///



    /// -------プロパティ------- ///
    /// --------変数一覧-------- ///
}