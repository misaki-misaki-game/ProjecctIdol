using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void Change_Puzzle()
    {
        //�p�Y���Q�[���Ɉړ�����
        SceneManager.LoadScene("PazulScene");
    }
    public void Change_Rhythm()
    {
        //���Y���Q�[���Ɉړ�����
        SceneManager.LoadScene("RhythmGame");
    }
    public void Change_Title()
    {
        //�^�C�g���Ɉړ�����
        SceneManager.LoadScene("TitleScene");
    }
}
