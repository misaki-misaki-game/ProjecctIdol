using TMPro;
using UnityEngine;

public class TextHyde : MonoBehaviour
{
    public void TextBlank()
    {
        this.GetComponent<TextMeshPro>().text = string.Format(""); // 空白を代入
        this.GetComponent<RectTransform>().position = Vector3.zero; // 全て0を代入
    }
}
