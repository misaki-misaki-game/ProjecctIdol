using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
        InitializationScore();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPosition = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            // スワイプによる移動距離を取得
            currentPosition = Input.mousePosition;
            float diffDistanceX = (currentPosition.x - startPosition.x);

            if (Mathf.Abs(diffDistanceX) >= targetDistanceX)
            {
                if (swipeArrow.sprite == arrowImages[0] && startPosition.x < currentPosition.x)
                {
                    swipeArrow.sprite = arrowImages[1];
                    psylliumAnim.SetTrigger("isShakeRight");
                }
                else if (swipeArrow.sprite == arrowImages[1] && startPosition.x > currentPosition.x)
                {
                    swipeArrow.sprite = arrowImages[0];
                    psylliumAnim.SetTrigger("isShakeLeft");
                }
                else return;
                score += addScore;
                scoreText.text = string.Format("{0:000000}", score);
                startPosition = currentPosition;
            }
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

    private Vector2 startPosition; // スワイプを開始したポジション
    private Vector2 currentPosition; // 現在のポジション

    private float score = 0; // スコア変数
    [SerializeField] private float addScore = 500; // 加算するスコア変数

    [SerializeField] private float targetDistanceX = 200; // 目標移動値

    [SerializeField] private Image swipeArrow; // スワイプ方向オブジェクト

    [SerializeField] private Animator psylliumAnim; // サイリウムアニメーション変数 

    [SerializeField] private Sprite[] arrowImages; // スワイプ方向画像配列

    [SerializeField] private TextMeshProUGUI scoreText; // スコアテキスト変数

    /// ------private変数------- ///
    #endregion

    #region プロパティ
    /// -------プロパティ------- ///

    // scoreのセット関数
    public float GetStarScore { get { return score;  } }

    /// -------プロパティ------- ///
    #endregion

    /// --------変数一覧-------- ///
}