using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;

public partial class SwipeScript : MonoBehaviour
{
    /// --------関数一覧-------- ///

    #region public関数
    /// -------public関数------- ///

    /// <samary>
    /// お星様モードのスコアを初期化する関数
    /// </samary>
    public void InitializationScore()
    {
        score = default;
        states.Clear();
    }

    /// <summary>
    /// 判定の終了関数
    /// </summary>
    public void DicisionEnd()
    {
        // 待機アニメーションを呼び出し、非表示にする
        dicisionAnim.SetTrigger("Idle");
        dicisionIcon.gameObject.SetActive(false);
    }

    /// -------public関数------- ///
    #endregion

    #region protected関数
    /// -----protected関数------ ///



    /// -----protected関数------ ///
    #endregion

    #region private関数
    /// ------private関数------- ///

    private void Start()
    {
        InitializationScore(); // スコアを初期化

        // サイリウムの成功範囲を計算
        float sixty_fourthNote = 1f / note;
        float a_quarter = 1f / 4f;
        float a_half = 1f / 2f;
        float three_quarters = 3f / 4f;
        safeRange[0, 0] = 1f - sixty_fourthNote * safeFrame;
        safeRange[0, 1] = 0 + sixty_fourthNote * safeFrame;
        safeRange[1, 0] = a_quarter - sixty_fourthNote * safeFrame;
        safeRange[1, 1] = a_quarter + sixty_fourthNote * safeFrame;
        safeRange[2, 0] = a_half - sixty_fourthNote * safeFrame;
        safeRange[2, 1] = a_half + sixty_fourthNote * safeFrame;
        safeRange[3, 0] = three_quarters - sixty_fourthNote * safeFrame;
        safeRange[3, 1] = three_quarters + sixty_fourthNote * safeFrame;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // マウスポジションを代入
            startPosition = Input.mousePosition;
            mouseXPosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            // スワイプによる移動距離を取得
            currentPosition = Input.mousePosition;
            float diffDistanceX = (currentPosition.x - startPosition.x);
            psylliumDistanceX = (currentPosition.x - mouseXPosition.x);

            // 移動距離が指定の距離以上なら
            if (Mathf.Abs(diffDistanceX) >= targetDistanceX)
            {
                // 矢印の方向と移動距離の向きが同じならサイリウムの成功判定を行う
                // →の処理
                if (swipeArrow.sprite == arrowImages[0] && startPosition.x < currentPosition.x)
                {
                    SwipeCheck();
                }
                // ←の処理
                else if (swipeArrow.sprite == arrowImages[1] && startPosition.x > currentPosition.x)
                {
                    SwipeCheck();
                }
            }

            // マウスのx軸の位置に基づいて回転角度を計算
            float angle = Mathf.Clamp(psylliumDistanceX / Screen.width * 60f, -30f, 30f); // -30度から30度に制限

            // オブジェクトの回転を設定（z軸のみ）
            psyllium.localRotation = Quaternion.Euler(new Vector3(0, 0, -angle));
        }
        ArrowChange(); // サイリウムを振る方向を変更
    }

    /// <summary>
    ///  振る方向の画像を差し替える関数
    /// </summary>
    private void ArrowChange()
    {
        // 1拍のタイミングでサイリウムを振る方向を切り替える
        if (Music.IsNearChangedBeat())
        {
            if (swipeArrow.sprite == arrowImages[0])
            {
                swipeArrow.sprite = arrowImages[1];
            }
            else if (swipeArrow.sprite == arrowImages[1])
            {
                swipeArrow.sprite = arrowImages[0];
            }

            // スワイプをfalseにしてスワイプを受け付ける
            swipe = false;
        }
    }

    /// <summary>
    /// サイリウムをタイミング良く振ることができたかどうかの関数
    /// </summary>
    private void SwipeCheck()
    {
        if (swipe) return; // 既にスワイプしていればリターン

        float BPMCos = Music.MusicalCos(); // BPM周期をCosで取得
        bool dicision; // 成功したかどうか

        swipe = true; // スワイプをtrueにしてスワイプを重複させない

        // サイリウムが触れたかどうかを確認
        if (BPMCos <= safeRange[0, 1] || safeRange[1, 0] <= BPMCos && BPMCos <= safeRange[1, 1] ||
            safeRange[2, 0] <= BPMCos && BPMCos <= safeRange[2, 1] ||
            safeRange[3, 0] <= BPMCos && BPMCos <= safeRange[3, 1] || safeRange[0, 0] <= BPMCos)
        {
            // スコアを加算
            score += addScore;
            scoreText.text = string.Format("{0:000000}", score);

            // 計測始点を現在の位置に変更
            startPosition = currentPosition;
            // 成功とする
            dicision = true;
        }
        else
        {
            // 失敗とする
            dicision = false;
        }
        
        // 合否判定を出す
        SwipeDicision(dicision);
    }

    /// <summary>
    /// 合否判定を行い、アニメーションを呼び出す関数
    /// </summary>
    /// <param name="dicision">合否判定</param>
    private void SwipeDicision(bool dicision)
    {
        // 合否判定オブジェクトを表示し、引数の値によって合否の画像を入れ替えアニメーションを動かす
        dicisionIcon.gameObject.SetActive(true);

        // 成功時
        if (dicision)
        {
            dicisionIcon.sprite = dicisionIcons[0];
            dicisionAnim.SetTrigger("Good");

            // シグナルをランダムで設定
            int rnd = Random.Range(1, 5); // 1～4の範囲でランダム
            Color setColor = new Color(); // マテリアルにセットするための変数
            SignalScript.STATE state = (SignalScript.STATE)Enum.ToObject(typeof(SignalScript.STATE), rnd); // stateをランダムで設定
            switch (state)
            {
                // stateによって色を変更
                case SignalScript.STATE.RED:
                    setColor = s_Red; // 赤シグナルを設定
                    break;
                case SignalScript.STATE.BLUE:
                    setColor = s_Blue; // 青シグナルを設定
                    break;
                case SignalScript.STATE.YELLOW:
                    setColor = s_Yellow; // 黄シグナルを設定
                    break;
                case SignalScript.STATE.WHITE:
                    setColor = s_White; // 白シグナルを設定
                    break;
            }

            // シグナルステートリストにランダムに設定されたstateを追加
            states.Add(state);

            // 色変更してパーティクルを再生
            successParticle.GetComponent<Renderer>().sharedMaterial.SetColor("Color", setColor);
            successParticle.Play();

            // SEを流す
            audioSource.PlayOneShot(clip);
        }
        // 失敗時
        else
        {
            dicisionIcon.sprite = dicisionIcons[1];
            dicisionAnim.SetTrigger("Miss");
        }
    }

    /// ------private関数------- ///
    #endregion

    /// --------関数一覧-------- ///
}
public partial class SwipeScript
{
    /// --------変数一覧-------- ///

    #region public変数
    /// -------public変数------- ///



    /// -------public変数------- ///
    #endregion

    #region protected変数
    /// -----protected変数------ ///



    /// -----protected変数------ ///
    #endregion

    #region private変数
    /// ------private変数------- ///

    private Vector3 startPosition; // スワイプを開始したポジション
    private Vector3 mouseXPosition; // サイリウムRotate変更用ポジション
    private Vector3 currentPosition; // 現在のポジション

    private List<SignalScript.STATE> states = new List<SignalScript.STATE>(); // シグナルステートリスト

    private bool swipe = false; // スワイプしたかどうか

    private float score = 0; // スコア変数
    private float note = 64; // 1小節を何分割するか
    private float psylliumDistanceX; // サイリウムRotate変更用距離変数
    private float[,] safeRange = new float[4,2]; // サイリウムのセーフ範囲

    [SerializeField] private float addScore = 500; // 加算するスコア変数
    [Header("1拍の±フレームを入力")]
    [SerializeField] private float safeFrame;
    [SerializeField] private float targetDistanceX = 200; // 目標移動値

    [SerializeField] private AudioSource audioSource; // オーディオソース

    [SerializeField] private AudioClip clip; // SE

    [SerializeField] private RectTransform psyllium; // サイリウムオブジェクト

    [SerializeField] private Image swipeArrow; // スワイプ方向オブジェクト
    [SerializeField] private Image dicisionIcon; // 合否オブジェクト

    [SerializeField] private Animator dicisionAnim; // 合否判定アニメーター変数

    [SerializeField] private Sprite[] arrowImages; // スワイプ方向画像配列
    [SerializeField] private Sprite[] dicisionIcons; // スワイプ合否判定配列

    private Color s_Red = new Color(0.9803922f, 0.2039216f, 0.4980392f, 1f); // 赤シグナル色FA347F
    private Color s_Blue = new Color(0.0784313f, 0.4588235f, 0.8235294f, 1f); // 青シグナル色1475D2
    private Color s_Yellow = new Color(0.9960784f, 0.7333333f, 0.3098039f, 1f); // 黄シグナル色FEBB4F
    private Color s_White = new Color(0.8588235f, 0.9137255f, 0.8901961f, 1f); // 白シグナル色DBE9E3 

    [SerializeField] private TextMeshProUGUI scoreText; // スコアテキスト変数

    [SerializeField] private ParticleSystem successParticle; // 成功時のパーティクル変数

    /// ------private変数------- ///
    #endregion

    #region プロパティ
    /// -------プロパティ------- ///

    // scoreのゲット関数
    public float GetStarScore { get { return score;  } }

    // パーティクルシステムのゲット関数
    public ParticleSystem GetParticleSystem { get { return successParticle; } }

    // シグナルステートリストのゲット関数
    public List<SignalScript.STATE> GetStarState { get { return states; } }

    /// -------プロパティ------- ///
    #endregion

    /// --------変数一覧-------- ///
}