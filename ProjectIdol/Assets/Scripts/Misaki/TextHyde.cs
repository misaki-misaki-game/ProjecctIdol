using TMPro;
using UnityEngine;

public class TextHyde : MonoBehaviour
{
    /// --------ŠÖ”ˆê——-------- ///
    /// -------publicŠÖ”------- ///

    public void TextBlank()
    {
        this.GetComponent<TextMeshPro>().text = string.Format(""); // ‹ó”’‚ğ‘ã“ü
        this.GetComponent<RectTransform>().position = Vector3.zero; // ‘S‚Ä0‚ğ‘ã“ü
    }

    /// -------publicŠÖ”------- ///
    /// -----protectedŠÖ”------ ///



    /// -----protectedŠÖ”------ ///
    /// ------privateŠÖ”------- ///



    /// ------privateŠÖ”------- ///
    /// --------ŠÖ”ˆê——-------- ///
}
