using TMPro;
using UnityEngine;

public class TextHyde : MonoBehaviour
{
    /// --------関数一覧-------- ///
    /// -------public関数------- ///

    public void TextBlank()
    {
        this.GetComponent<TextMeshPro>().text = string.Format(""); // 空白を代入
        this.GetComponent<RectTransform>().position = Vector3.zero; // 全て0を代入
    }

    /// -------public関数------- ///
    /// -----protected関数------ ///



    /// -----protected関数------ ///
    /// ------private関数------- ///



    /// ------private関数------- ///
    /// --------関数一覧-------- ///
}
