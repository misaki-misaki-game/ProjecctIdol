using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

public class RhythmManager : MonoBehaviour
{
    //カウントダウンのパネルとテキスト
    [SerializeField] GameObject countDownPanal;
    [SerializeField] TextMeshProUGUI countDownText;

    //リザルトのパネル
    [SerializeField] GameObject resultPanel;

    //タイムラインを取得して再生
    [SerializeField] PlayableDirector playableDirector;


    void Start()
    {
        StartCoroutine(GameMain());
    }

    IEnumerator GameMain()
    {
        countDownText.text = "      3";
        yield return new WaitForSeconds(1);
        countDownText.text = "      2";
        yield return new WaitForSeconds(1);
        countDownText.text = "      1";
        yield return new WaitForSeconds(1);
        countDownText.text = "  ライブ\n" + "     開始!";
        yield return new WaitForSeconds(0.5f);
        countDownText.text = " ";
        countDownPanal.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        playableDirector.Play();
    }

    //アイドルの音源が終了したのが分かるイベント
    public void EndEvent()
    {
        Debug.Log("game end");
        resultPanel.SetActive(true);
    }
}
