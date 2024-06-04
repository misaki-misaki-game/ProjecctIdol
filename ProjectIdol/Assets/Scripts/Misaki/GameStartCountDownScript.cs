using TMPro;
using UnityEngine;

public partial class GameStartCountDownScript : MonoBehaviour
{
    private void Start()
    {
        aniScd = gameObject.GetComponent<Animator>(); // Animatorを格納
    }
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
        BGMAudioSource.Play(); // BGMを鳴らす
        animAi.SetTrigger("isDanceStart"); // アイのアニメーションをスタートする
        gameStartcd.SetActive(false); // カウントキャンパスを非表示にする
    }
    /// --------関数一覧-------- ///
    /// -------public関数------- ///



    /// -------public関数------- ///
    /// -----protected関数------ ///



    /// -----protected関数------ ///
    /// ------private関数------- ///



    /// ------private関数------- ///
    /// --------関数一覧-------- ///
}
public partial class GameStartCountDownScript
{
    /// --------変数一覧-------- ///
    /// -------public変数------- ///

    public TextMeshProUGUI gameStartText; // ゲームスタート時のカウントダウンテキスト表示用

    public TimeDirector timeDirector; // timeDirector変数

    public GameObject gameStartcd; // ゲームスタート時のカウントダウン変数

    public ButtonScript buttonScript; // ButtonScript変数

    public Animator animAi; // アイのアニメーション用変数

    public AudioSource BGMAudioSource; // BGM用オーディオソース


    /// -------public変数------- ///
    /// -----protected変数------ ///



    /// -----protected変数------ ///
    /// ------private変数------- ///

    private int count = 3; // カウントダウンの値

    private Animator aniScd; // gameStartcdのanimator変数

    /// ------private変数------- ///
    /// -------プロパティ------- ///



    /// -------プロパティ------- ///
    /// --------変数一覧-------- ///
}