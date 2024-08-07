using UnityEngine;
using UnityEngine.UI;
// フェードイン・アウト用のキャンパスにアタッチすること
// 上記のキャンパスはタグで「fade」としておくことを推奨
// キャンパスはプレハブ化し、ヒエラルキー上には存在させないこと
// スクリプト「SceneScript」と共に使用する
// 
public partial class FadeManager : MonoBehaviour
{

    /// --------関数一覧-------- ///
    /// -------public関数------- ///

    /// <summary>
    /// フェードイン関数
    /// </summary>
    public void fadeIn()
    {
        isFadeIn = true;   // フェードインフラグをtrueにする
        isFadeOut = false; // フェードアウトフラグをfalseにして誤発動を防ぐ
    }

    /// <summary>
    /// フェードアウト関数
    /// </summary>
    public void fadeOut()
    {
        isFadeOut = true; // フェードアウトフラグをtrueにする
        isFadeIn = false; // フェードインフラグをfalseにして誤発動を防ぐ
    }

    /// -------public関数------- ///
    /// -----protected関数------ ///



    /// -----protected関数------ ///
    /// ------private関数------- ///

    private void Start()
    {
        if (!isFadeInstance)           // FadeCanvas召喚フラグがfalseなら
        {
            DontDestroyOnLoad(this);   // Canvasを消えないようにする
            isFadeInstance = true;     // FadeCanvas召喚フラグがtrueにする
        }
        else                           // FadeCanvas召喚フラグがtrueなら
        {
            Destroy(this);             // 起動時以外は重複しないようにするため召喚したCanvasを消す
        }

    }

    private void Update()
    {
        if (isFadeIn)                                                                      // フェードインフラグがtrueなら
        {
            alpha -= Time.deltaTime / fadeSpeed;                                           // 徐々にalpha値を減らす fadeSpeedで速度調整可能
            if (alpha <= 0.0f)                                                             // 透明になったらフェードイン終了
            {
                isFadeIn = false;                                                          // フェードインフラグをfalseにする
                alpha = 0.0f;                                                              // alpha値がマイナスにならないように0を代入
            }
            this.GetComponentInChildren<Image>().color = new Color(0.0f, 0.0f, 0.0f, alpha); // 暗転用Imageのalphaに代入し徐々に透明にする
        }

        else if (isFadeOut)                                                                   // フェードアウトフラグがtrueなら
        {
            alpha += Time.deltaTime / fadeSpeed;                                             // 徐々にalpha値を増やす fadeSpeedで速度調整可能
            if (alpha >= 1.0f)                                                               // 真っ黒になったらフェードアウト終了
            {
                isFadeOut = false;                                                           // フェードアウトフラグをfalseにする
                isFadeIn = true;                                                             // フェードインフラグをtrueにする
                alpha = 1.0f;                                                                // alpha値が1以上にならないように1を代入
            }

            this.GetComponentInChildren<Image>().color = new Color(0.0f, 0.0f, 0.0f, alpha); // 暗転用Imageのalphaに代入し徐々に黒にする
        }
    }

    /// ------private関数------- ///
    /// --------関数一覧-------- ///
}
public partial class FadeManager
{
    /// --------変数一覧-------- ///
    /// -------public変数------- ///

    public static bool isFadeInstance = false; // FadeCanvas召喚フラグ
    public bool isFadeIn = false;              // フェードインするフラグ
    public bool isFadeOut = false;             // フェードアウトするフラグ
    public float alpha = 0.0f;                 // 透過率
    public float fadeSpeed = 0.2f;             // フェードにかかる時間

    /// -------public変数------- ///
    /// -----protected変数------ ///



    /// -----protected変数------ ///
    /// ------private変数------- ///



    /// ------private変数------- ///
    /// -------プロパティ------- ///



    /// -------プロパティ------- ///
    /// --------変数一覧-------- ///
}