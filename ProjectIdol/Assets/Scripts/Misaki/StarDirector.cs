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
                Gauge.fillAmount += chain / 100;
            }
            // お星様ゲージが1になったら
            if (Gauge.fillAmount == 1)
            {
                starState = StarState.StarMode; // スターモードに変更
            }
        }
    }

    /// -------public関数------- ///
    /// -----protected関数------ ///



    /// -----protected関数------ ///
    /// ------private関数------- ///

    private void Start()
    {
        // 初期設定
        Gauge.fillAmount = 0;
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
            Gauge.fillAmount -= Time.deltaTime / starTime; // ゲージを減少させる(10秒間)
        }
        else if (starState == StarState.StarMode && backImageObjects[0].activeSelf) // スターモードの時
        {
            Gauge.fillAmount -= Time.deltaTime / starTime; // ゲージを減少させる(10秒間)
            if (Gauge.fillAmount <= 0) // ゲージがなくなったら
            {
                // お星様モード用の背景に変更
                for (int i = 0; i < backImageObjects.Length; i++)
                {
                    backImageObjects[i].SetActive(false); // ステージ背景を通常モードにする
                }
                starState = StarState.NormalMode; // 通常モードに変更
                starCount += 1; // お星様モードを加算する
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
    [SerializeField] private GameObject[] backImageObjects = new GameObject[2]; // 背景オブジェクト配列
    [SerializeField] private Image Gauge; // お星様ゲージ

    /// ------private変数------- ///
    /// -------プロパティ------- ///



    /// -------プロパティ------- ///
    /// --------変数一覧-------- ///
}