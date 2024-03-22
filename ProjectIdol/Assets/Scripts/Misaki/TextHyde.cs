using TMPro;
using UnityEngine;

public class TextHyde : MonoBehaviour
{
    public void TextBlank()
    {
        this.GetComponent<TextMeshPro>().text = string.Format(""); // ‹ó”’‚ð‘ã“ü
        this.GetComponent<RectTransform>().position = Vector3.zero; // ‘S‚Ä0‚ð‘ã“ü
    }
}
