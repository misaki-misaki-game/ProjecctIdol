using TMPro;
using UnityEngine;

public class TextHyde : MonoBehaviour
{
    /// --------�֐��ꗗ-------- ///
    /// -------public�֐�------- ///

    public void TextBlank()
    {
        this.GetComponent<TextMeshPro>().text = string.Format(""); // �󔒂���
        this.GetComponent<RectTransform>().position = Vector3.zero; // �S��0����
    }

    /// -------public�֐�------- ///
    /// -----protected�֐�------ ///



    /// -----protected�֐�------ ///
    /// ------private�֐�------- ///



    /// ------private�֐�------- ///
    /// --------�֐��ꗗ-------- ///
}
