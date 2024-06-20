using UnityEngine;
using UnityEngine.UI;

public partial class StarDirector : MonoBehaviour
{
    /// --------関数一覧-------- ///
    /// -------public関数------- ///

    /// <summary>
    /// ゲットスター関数
    /// </summary>
    /// <param name="chain">消したシグナルのチェイン数</param>
    public void GetStar(float chain)
    {
        if (starState == StarState.NormalMode) // 通常モードのとき
        {
            // お星様ゲージの増加を処理
            if (chain >= 1)
            {
                // チェインの値分ゲージを増加させる
                gauge.fillAmount += chain / 100;
            }
            // お星様ゲージが1になったら
            if (gauge.fillAmount == 1)
            {
                starState = StarState.StarMode; // スターモードに変更
            }
        }
    }

    /// <summary>
    /// お星様モードを終了する関数
    /// </summary>
    public void StarEnd()
    {
        gauge.fillAmount = 0; // マイナスにならないように0を代入

        // 通常モード用の背景に変更
        for (int i = 0; i < backImageObjects.Length; i++)
        {
            backImageObjects[i].SetActive(false); // ステージ背景を通常モードにする
        }

        // パーティクルを止める
        particle.Stop();
        swipeScript.GetParticleSystem.Stop();

        scoreDirector.GetStarScore(starUltChain, swipeScript.GetStarScore, swipeScript.GetStarState); // お星様モードでのスコアを取得する
        swipeScript.InitializationScore(); // スコアをリセット
        starCanvas.SetActive(false); // お星様モード時のキャンパスを非表示にする
        resetButton.interactable = true; // ボタンを押せるようにする
        starState = StarState.NormalMode; // 通常モードに変更
        starCount += 1; // お星様モードを加算する
    }

    /// -------public関数------- ///
    /// -----protected関数------ ///



    /// -----protected関数------ ///
    /// ------private関数------- ///

    private void Start()
    {
        // 初期設定
        gauge.fillAmount = 0;
    }

    private void FixedUpdate()
    {
        ShowStarMode(); // スターモードの演出を行う
    }

    /// <summary>
    /// スターモードの演出を行う関数
    /// </summary>
    private void ShowStarMode()
    {
        if (starState == StarState.StarMode && !backImageObjects[0].activeSelf) // スターモードの時
        {
            // お星様モード用の背景に変更
            for (int i = 0; i < backImageObjects.Length; i++)
            {
                backImageObjects[i].SetActive(true);
            }
            // パーティクルを発生させる
            particle.Play();
            starCanvas.SetActive(true); // お星様モード時のキャンパスを表示する
            swipeScript.InitializationScore(); // お星様モードのスコアを初期化する
            resetButton.interactable = false; // ボタンを押せないようにする
            gauge.fillAmount -= Time.deltaTime / starTime; // ゲージを減少させる(10秒間)
        }
        else if (starState == StarState.StarMode && backImageObjects[0].activeSelf) // スターモードの時
        {
            gauge.fillAmount -= Time.deltaTime / starTime; // ゲージを減少させる(10秒間)
            if (gauge.fillAmount <= 0) // ゲージがなくなったら
            {
                StarEnd(); // お星様モードを終了する
            }
        }
    }

    /// ------private関数------- ///
    /// --------関数一覧-------- ///
}
public partial class StarDirector
{
    /// --------変数一覧-------- ///
    /// -------public変数------- ///

    public int starCount = 0; // お星様モードになった回数
    public enum StarState
    {
        NormalMode, // 通常状態 0
        StarMode // スターモード 1
    }
    public StarState starState = StarState.NormalMode; // StarState変数

    /// -------public変数------- ///
    /// -----protected変数------ ///



    /// -----protected変数------ ///
    /// ------private変数------- ///

    [SerializeField] private float starTime = 10; // お星様モードの制限時間

    [SerializeField] private float starUltChain = 6; // お星様モードのアルティメットスコア時のチェイン数

    [SerializeField] private GameObject starCanvas; // お星様モード時のキャンパス
    [SerializeField] private GameObject[] backImageObjects = new GameObject[2]; // 背景オブジェクト配列

    [SerializeField] private Button resetButton; // リセットボタン変数

    [SerializeField] private Image gauge; // お星様ゲージ

    [SerializeField] private SwipeScript swipeScript; // スワイプスクリプト変数

    [SerializeField] private ScoreDirector scoreDirector; // スコアディレクター変数

    [SerializeField] private ParticleSystem particle; // パーティクルシステム変数

    /// ------private変数------- ///
    /// -------プロパティ------- ///



    /// -------プロパティ------- ///
    /// --------変数一覧-------- ///
}