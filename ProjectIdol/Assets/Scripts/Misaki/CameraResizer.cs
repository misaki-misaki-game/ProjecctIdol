using UnityEngine;

[ExecuteAlways]
public partial class CameraResizer : MonoBehaviour
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

    private void Awake()
    {
        m_camera = GetComponent<Camera>();

        ResizeCameraOrthographicSize();
    }

    private void Update()
    {
        if (isUpdate)
        {
            ResizeCameraOrthographicSize();
        }
    }

    /// <summary>
    /// CameraのorthographicSizeを機種の解像度に合わせて変更させる関数
    /// </summary>
    private void ResizeCameraOrthographicSize()
    {
        var currentResolution = new Vector2(Screen.width, Screen.height); // 現在の解像度を取得
        var targetAspect = targetResolution.x / targetResolution.y; // 目標のアスペクト比を取得
        var currentAspect = currentResolution.x / currentResolution.y; // 現在のアスペクト比を取得

        // 現在のアスペクト比が目標のアスペクト比以上であれば
        // 現在の正投影サイズに目標の正投影サイズを代入
        if (currentAspect >= targetAspect)
        {
            m_camera.orthographicSize = targetOrthographicSize;
            return;
        }

        // 目標の正投影サイズ×目標と現在のアスペクト比の割合を計算
        // 現在のカメラの正投影サイズに計算した正投影サイズを代入
        var orthoGraphicSize = targetOrthographicSize * (targetAspect / currentAspect);
        m_camera.orthographicSize = orthoGraphicSize;
    }

    /// ------private関数------- ///
    #endregion

    /// --------関数一覧-------- ///
}
public partial class CameraResizer
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

    [SerializeField] private bool isUpdate = true; // アップデートしているかどうか

    [SerializeField] private float targetOrthographicSize = 5; // 目標の正投影サイズ

    [SerializeField] private Vector2 targetResolution; // 目標の解像度

    private Camera m_camera; // カメラ変数

    /// ------private変数------- ///
    #endregion

    #region プロパティ
    /// -------プロパティ------- ///



    /// -------プロパティ------- ///
    #endregion

    /// --------変数一覧-------- ///
}
