using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountDownController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI countdownText;
    public GameObject countDownPanel;

    void Start()
    {
        StartCoroutine(CountDown());
    }

    IEnumerator CountDown()
    {
        Debug.Log("3");
        yield return new WaitForSeconds(1);
        Debug.Log("2");
        yield return new WaitForSeconds(1);
        Debug.Log("1");
        yield return new WaitForSeconds(1);
        Debug.Log("ライブ開始！");
        yield return new WaitForSeconds(0.2f);
        countDownPanel.SetActive(false);
    }
}
