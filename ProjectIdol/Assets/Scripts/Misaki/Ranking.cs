using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public partial class Ranking : MonoBehaviour
{

    /// --------関数一覧-------- ///
    /// -------public関数------- ///

    /// <summary>
    /// ランクインしているかの関数
    /// </summary>
    public void CheckRankin(float[] ranking, float currentScore)
    {
        if (ranking[9] < currentScore) // 10位より獲得スコアが大きい場合
        {
            ranking[9] = currentScore; // 10位を上書きする
            UpdateRanking(ranking); // ランキングの整理を行う
            int element = SearchDescending(ranking, currentScore);
            // ランクインしたスコア順位のanimatorのisCurrentScore変数を真にする
            nodeAnimator[element].SetBool("isCurrentScore", true);
            // スクロールの目標値をelementによって設定する
            if (element < 4) // 4未満の場合
            {
                scrollTargetValue = 1f;
            }
            else if (element == 4) // 4のとき
            {
                scrollTargetValue = 0.75f;
                isScroll = true; // スクロールを許可する
            }
            else if (element == 5) // 5のとき
            {
                scrollTargetValue = 0.5f;
                isScroll = true; // スクロールを許可する
            }
            else if (element == 6) // 6のとき
            {
                scrollTargetValue = 0.25f;
                isScroll = true; // スクロールを許可する
            }
            else // 7以上のとき
            {
                scrollTargetValue = 0f;
                isScroll = true; // スクロールを許可する
            }
        }
        WriteRanking(ranking); // ランキングを表示
    }

    /// <param name="array">対象の配列</param>
    /// <param name="left">ソート範囲の最初のインデックス</param>
    /// <param name="right">ソート範囲の最後のインデックス</param>
    public static void QuickSortAscending<T>(T[] array, int left, int right) where T : IComparable<T>
    {
        // 範囲が一つだけなら処理を抜ける
        if (left >= right) return;

        // ピポットを選択(範囲の先頭・真ん中・末尾の中央値を使用)
        T pivot = MedianAscending(array[left], array[(left + right) / 2], array[right]);

        int i = left;
        int j = right;

        while (i <= j)
        {
            // array[i] < pivot まで左から探索
            while (i < right && array[i].CompareTo(pivot) < 0) i++;
            // array[i] >= pivot まで右から探索
            while (j > left && array[j].CompareTo(pivot) >= 0) j--;

            if (i > j) break;
            Swap<T>(ref array[i], ref array[j]);

            // 交換を行った要素の次の要素にインデックスを進める
            i++;
            j--;
        }
        // 配列の中央より左側の要素でクイックソート
        QuickSortAscending(array, left, i - 1);
        // 配列の中央より右側の要素でクイックソート
        QuickSortAscending(array, i, right);
    }

    /// <summary>
    /// クイックソート（降順）
    /// </summary>
    /// <param name="array">対象の配列</param>
    /// <param name="left">ソート範囲の最初のインデックス</param>
    /// <param name="right">ソート範囲の最後のインデックス</param>
    public static void QuickSortDescending<T>(T[] array, int left, int right) where T : IComparable<T>
    {
        // 範囲が一つだけなら処理を抜ける
        if (left >= right) return;

        // ピポットを選択(範囲の先頭・真ん中・末尾の中央値を使用)
        T pivot = MedianDescending(array[left], array[(left + right) / 2], array[right]);

        int i = left;
        int j = right;

        while (i <= j)
        {
            // array[i] > pivot まで左から探索
            while (i < right && array[i].CompareTo(pivot) > 0) i++;
            // array[i] <= pivot まで右から探索
            while (j > left && array[j].CompareTo(pivot) <= 0) j--;

            if (i > j) break;
            Swap<T>(ref array[i], ref array[j]);

            // 交換を行った要素の次の要素にインデックスを進める
            i++;
            j--;
        }

        QuickSortDescending(array, left, i - 1);
        QuickSortDescending(array, i, right);
    }

    /// <summary>
    /// 2分探索関数昇順用
    /// </summary>
    /// <typeparam name="T">任意の配列</typeparam>
    /// <param name="arr">ソートされた配列</param>
    /// <param name="target">探索したい値</param>
    /// <returns></returns>
    public static int SearchAscending<T>(T[] arr, T target) where T : IComparable<T>
    {
        int left = 0;
        int right = arr.Length - 1;

        while (left <= right)
        {
            int mid = left + (right - left) / 2;

            // もし中央の要素がターゲットと等しい場合
            if (arr[mid].CompareTo(target) == 0)
            {
                return mid;
            }
            // もし中央の要素よりも大きい場合、右側を探索
            else if (arr[mid].CompareTo(target) < 0)
            {
                left = mid + 1;
            }
            // もし中央の要素よりも小さい場合、左側を探索
            else
            {
                right = mid - 1;
            }
        }
        // ターゲットが見つからない場合
        return -1;
    }

    /// <summary>
    /// 2分探索関数降順用
    /// </summary>
    /// <typeparam name="T">任意の配列</typeparam>
    /// <param name="arr">ソートされた配列</param>
    /// <param name="target">探索したい値</param>
    /// <returns></returns>
    public static int SearchDescending<T>(T[] arr, T target) where T : IComparable<T>
    {
        int left = 0;
        int right = arr.Length - 1;

        while (left <= right)
        {
            int mid = left + (right - left) / 2;

            // もし中央の要素がターゲットと等しい場合
            if (arr[mid].CompareTo(target) == 0)
            {
                return mid;
            }
            // もし中央の要素よりも小さい場合、右側を探索
            else if (arr[mid].CompareTo(target) > 0)
            {
                left = mid + 1;
            }
            // もし中央の要素よりも大きい場合、左側を探索
            else
            {
                right = mid - 1;
            }
        }
        // ターゲットが見つからない場合
        return -1;
    }

    /// -------public関数------- ///
    /// -----protected関数------ ///



    /// -----protected関数------ ///
    /// ------private関数------- ///

    private void FixedUpdate()
    {
        ScrollAnim(scrollbar, scrollTargetValue); // スクロールアニメーションを呼び出す
    }

    /// <summary>
    /// スクロールする関数
    /// </summary>
    /// <param name="scrollbar">対象のスクロールバー</param>
    /// <param name="scrollTarget">スクロールの目標値</param>
    private void ScrollAnim(Scrollbar scrollbar, float scrollTarget)
    {
        if (!isScroll) return; // isScrollがfalseならリターンする
        if (scrollTarget < scrollbar.value)
        {
            scrollValue -= Time.deltaTime; // deltaTimeを加算する
            float progress = Mathf.Clamp01(scrollValue / scrollTime); // scrollValue / scrollTimeの割合を代入
            scrollbar.value -= Mathf.Lerp(0, scrollTarget, progress); // 0からscrollTargetValueまでの値に対して割合(progress)を代入
        }
        // showScoreがtotalScoreを超えたら表記ずれしないようにtotalScoreを表示する
        else
        {
            scrollbar.value = scrollTarget; // scrollTargetValueを代入する
            isScroll = false; // ScrollAnimを呼び出さないようにfalseにする
        }
    }

    /// <summary>
    /// ランキングの整理をする関数
    /// </summary>
    private void UpdateRanking(float[] ranking)
    {
        // クイックソート(降順)を行う
        QuickSortDescending(ranking, 0, ranking.Length - 1);
        dataManager.Save(dataManager.data); // セーブする
    }

    /// <summary>
    /// ランキングの書き込み関数
    /// </summary>
    private void WriteRanking(float[] ranking)
    {
        for (int i = 0; i < rankingScoreText.Length; i++)
        {
            rankingScoreText[i].text = string.Format("{0:0000000}", ranking[i]); // 各順位を書き出し
        }
    }

    /// <summary>
    /// 中央値を求める
    /// </summary>
    private static T MedianAscending<T>(T x, T y, T z) where T : IComparable<T>
    {
        // 左の変数 > 右の変数 なら1以上の整数値が返される
        if (x.CompareTo(y) > 0) Swap(ref x, ref y);
        if (x.CompareTo(z) > 0) Swap(ref x, ref z);
        if (y.CompareTo(z) > 0) Swap(ref y, ref z);
        return y;
    }

    /// <summary>
    /// 中央値を求める（降順）
    /// </summary>
    private static T MedianDescending<T>(T x, T y, T z) where T : IComparable<T>
    {
        // 左の変数 < 右の変数 なら-1以下の整数値が返される
        if (x.CompareTo(y) < 0) Swap(ref x, ref y);
        if (x.CompareTo(z) < 0) Swap(ref x, ref z);
        if (y.CompareTo(z) < 0) Swap(ref y, ref z);
        return y;
    }

    /// <summary>
    /// 参照を入れ替える(値型だと変数のコピーになってしまうため)
    /// </summary>
    private static void Swap<T>(ref T x, ref T y) where T : IComparable<T>
    {
        var tmp = x;
        x = y;
        y = tmp;
    }

    /// ------private関数------- ///
    /// --------関数一覧-------- ///
}
public partial class Ranking
{
    /// --------変数一覧-------- ///
    /// -------public変数------- ///



    /// -------public変数------- ///
    /// -----protected変数------ ///



    /// -----protected変数------ ///
    /// ------private変数------- ///

    private bool isScroll = false; // スクロールするかどうか

    private float scrollValue = 1f; // スクロールの現在値
    private float scrollTargetValue = 0f; // スクロールの目標値

    [SerializeField] private float scrollTime = 1f; // スクロールに掛かる時間

    [SerializeField] private DataManager dataManager; // DataManager変数

    [SerializeField] private TextMeshProUGUI[] rankingScoreText = new TextMeshProUGUI[10]; // ランキングスコアの配列

    [SerializeField] private Animator[] nodeAnimator = new Animator[10]; // ノードのAnimator配列

    [SerializeField] private Scrollbar scrollbar; // Scrollbar変数

    /// ------private変数------- ///
    /// -------プロパティ------- ///



    /// -------プロパティ------- ///
    /// --------変数一覧-------- ///
}