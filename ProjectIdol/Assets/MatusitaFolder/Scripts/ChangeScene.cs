using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
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
    public void Change_Title()
    {
        //タイトルに移動する
        SceneManager.LoadScene("TitleScene");
    }
}
