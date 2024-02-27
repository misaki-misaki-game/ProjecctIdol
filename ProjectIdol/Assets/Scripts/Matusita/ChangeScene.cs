using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] GameObject parametersPanel;
    [SerializeField] GameObject parametorButton;
    [SerializeField] GameObject resultPanel;
    [SerializeField] GameObject pointObject;
    [SerializeField] GameObject scoreObject;
    [SerializeField] GameObject scrollObject;
    [SerializeField] UiManager uiManager;

    public void Change_Puzzle()
    {
        //パズルゲームに移動する
        SceneManager.LoadScene("PazulScene");
    }

    public void Change_Rhythm()
    {
        //リズムゲームに移動する
        SceneManager.LoadScene("RhythmGame");
    }

    public void Retry_Rhythm()
    {
        SceneManager.LoadScene("RhythmGame");
    }

    public void Change_Title()
    {
        //タイトルに移動する
        SceneManager.LoadScene("TitleScene");
    }

    public void ParametersToPoint()
    {
        //リザルト画面のパラメータ表示から各ポイントの評価表示に変更する
        parametersPanel.SetActive(false);
        parametorButton.SetActive(false);
        Debug.Log("各ポイントの評価表示");
        resultPanel.SetActive(true);
        pointObject.SetActive(true);
    }

    public void PointToScore()
    {
        //リザルト画面の各ポイントの評価表示からスコア表示に変更する
        resultPanel.SetActive(false);
        Debug.Log("スコア評価表示");
        //uiManager.ShowScore();
    }


    public void ScoreToScroll()
    {
        //リザルト画面のスコア表示からベストスコアのスクロール表示に変更する
        scoreObject.SetActive(false);
        Debug.Log("ベストスコアのスクロール表示");
        scrollObject.SetActive(true);
    }



}
