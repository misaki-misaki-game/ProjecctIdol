using TMPro;
using UnityEngine;

public class TextHyde : MonoBehaviour
{
    public void TextBlank()
    {
        this.GetComponent<TextMeshPro>().text = string.Format(""); // �󔒂���
        this.GetComponent<RectTransform>().position = Vector3.zero; // �S��0����
    }
}
