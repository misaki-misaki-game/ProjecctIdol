using UnityEngine;

[ExecuteAlways, RequireComponent(typeof(RectTransform))]
public partial class SetSafeArea : MonoBehaviour
{
    /// --------関数一覧-------- ///

    #region public関数
    /// -------public関数------- ///



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
        rect = GetComponent<RectTransform>(); // RectTransformを取得
    }

    private void Update()
    {
        // デバイスがHandheld(手に持てるサイズのもの)なら
        if (UnityEngine.Device.SystemInfo.deviceType == DeviceType.Handheld)
        {
            var area = Screen.safeArea; // 画面のセーフエリアを取得
            var resolution = Screen.currentResolution; // 現在の解像度を取得

            rect.sizeDelta = Vector2.zero; // sizeDeltaを0にする(矩形サイズとアンカーサイズを同じにする)
            // アンカーにセーフエリアと画面の解像度の割合を代入
            rect.anchorMin = new Vector2(area.xMin / resolution.width, area.yMin / resolution.height);
            rect.anchorMax = new Vector2(area.xMax / resolution.width, area.yMax / resolution.height);
        }

        else
        {
            // アンカーをストレッチに変更
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
        }
    }

    /// ------private関数------- ///
    #endregion

    /// --------関数一覧-------- ///
}
public partial class SetSafeArea
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

    private RectTransform rect; // RectTransform変数

    /// ------private変数------- ///
    #endregion

    #region プロパティ
    /// -------プロパティ------- ///



    /// -------プロパティ------- ///
    #endregion

    /// --------変数一覧-------- ///
}
