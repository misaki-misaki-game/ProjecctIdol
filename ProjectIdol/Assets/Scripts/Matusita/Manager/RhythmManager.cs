using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class RhythmManager : MonoBehaviour
{
    [SerializeField] GameObject countDownPanal;             //ゲーム開始前のカウントダウンを表示するためのパネル
    [SerializeField] TextMeshProUGUI countDownText;         //ゲーム開始前のカウントダウンするためのテキスト

    [SerializeField] UiManager uiManager;                   //UIManagerを使うための変数
    [SerializeField] TextMeshProUGUI rankText;              //ゲーム終了時のランクを表示するためのテキスト

    [SerializeField] GameObject resultPanel;                //ゲーム終了時の結果を表示するためのパネル

    [SerializeField] GameObject noteController;             //MIDIの音源が終了したときに使用するゲームオブジェクト

    [SerializeField] PlayableDirector playableDirector;     //タイムラインを取得して再生

    [SerializeField] Animator charaAnimator;                //キャラクターアニメーション

    void Start()
    {
        StartCoroutine(GameMain());                         //タイトル画面から移動したときに
    }

    IEnumerator GameMain()
    {
        countDownText.text = "      3";                     //カウントダウンパネルに表示するテキストを3に変更する
                                                            //空白はカウントダウンパネルに表示するときに綺麗に真ん中に表示するための物
        yield return new WaitForSeconds(1);                 //1秒開ける
        countDownText.text = "      2";                     //カウントダウンパネルに表示するテキストを2に変更する
        yield return new WaitForSeconds(1);                 //1秒開ける
        countDownText.text = "      1";                     //カウントダウンパネルに表示するテキストを1に変更する
        yield return new WaitForSeconds(1);                 //1秒開ける
        countDownText.text = "  ライブ\n" + "     開始!";   //カウントダウンパネルに表示するテキストをライブ開始！に変更する
        yield return new WaitForSeconds(0.5f);              //0.5秒開ける
        countDownText.text = " ";                           //カウントダウンパネルに何も表示しないようにするために空白を表示させる
        countDownPanal.SetActive(false);                    //カウントダウンパネルを非表示にする
        charaAnimator.SetTrigger("isDanceStart");           //キャラクターのアニメーションをスタートする
        yield return new WaitForSeconds(0.3f);              //1秒開ける
        playableDirector.Play();                            //playableDirectorのPlay()を開始させる　音ゲーの音源となるものを再生させる
    }

    //アイドルの音源が終了したのが分かるイベント
    public void EndEvent()
    {
        Debug.Log("game end");
        charaAnimator.SetTrigger("isDanceEnd"); // キャラクターのアニメーションを終了する
        //resultPanel.SetActive(true);
        uiManager.Rank();                       //UIManagerのRank()を使用し、ゲームが終了したときにランクテキストを点数に応じて変更させる
        uiManager.ShowScore();                  //UIManagerのShowScore()を使用し、スコアを加算しながら表示させる
        noteController.SetActive(false);        //noteContorollerのSetActiveをfalseにし、シグナルの出現をやめさせる
    }
}
