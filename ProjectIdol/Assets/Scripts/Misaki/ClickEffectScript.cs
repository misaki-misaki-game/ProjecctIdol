using UnityEngine;

public partial class ClickEffectScript : MonoBehaviour
{

    /// --------関数一覧-------- ///
    /// -------public関数------- ///



    /// -------public関数------- ///
    /// -----protected関数------ ///



    /// -----protected関数------ ///
    /// ------private関数------- ///

    private void Update()
    {
        SummonEffect(); // クリックした場所にエフェクトを呼び出す
    }

    /// <summary>
    /// クリックした場所にエフェクトを呼び出す関数
    /// </summary>
    private void SummonEffect()
    {
        if (Input.GetMouseButtonDown(0) && clickEffect) // 左クリックしたらかつclickEffectがnullでなければ
        {
            // クリックした場所にエフェクトを呼び出す
            var mousePosition = Input.mousePosition; // マウスのポジションを代入
            mousePosition.z = 3f; // z座標に3fを代入し画面に出す様にする
            GameObject clone = Instantiate(clickEffect, // なにを召喚するか
                                           Camera.main.ScreenToWorldPoint(mousePosition), // 画面のどこに召喚するか
                                           Quaternion.identity); // ローテーションはどうするのか
            Destroy(clone, destroyDeleteTime); // destroyDeleteTime秒後にエフェクトを破壊する
        }
    }

    /// ------private関数------- ///
    /// --------関数一覧-------- ///
}
public partial class ClickEffectScript
{
    /// --------変数一覧-------- ///
    /// -------public変数------- ///



    /// -------public変数------- ///
    /// -----protected変数------ ///



    /// -----protected変数------ ///
    /// ------private変数------- ///

    [SerializeField] private float destroyDeleteTime = 1.0f; // エフェクトを消すまでの時間変数

    [SerializeField] private GameObject clickEffect; // エフェクト変数


    /// ------private変数------- ///
    /// -------プロパティ------- ///



    /// -------プロパティ------- ///
    /// --------変数一覧-------- ///
}