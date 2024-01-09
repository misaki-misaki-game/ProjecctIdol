using System.Collections;
using TMPro;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public GameObject startPanel;
    public GameObject countDownPanel;
    [SerializeField] TextMeshProUGUI countDownText = default;

    void Start()
    {
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        countDownText.text = "    3";
        yield return new WaitForSeconds(1);
        countDownText.text = "    2";
        yield return new WaitForSeconds(1);
        countDownText.text = "    1";
        yield return new WaitForSeconds(1);
        countDownText.text = "ライブ\n" + "    開始！";
        yield return new WaitForSeconds(0.5f);
    }
}