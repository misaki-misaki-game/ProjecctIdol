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
        //�p�Y���Q�[���Ɉړ�����
        SceneManager.LoadScene("PazulScene");
    }

    public void Change_Rhythm()
    {
        //���Y���Q�[���Ɉړ�����
        SceneManager.LoadScene("RhythmGame");
    }

    public void Retry_Rhythm()
    {
        SceneManager.LoadScene("RhythmGame");
    }

    public void Change_Title()
    {
        //�^�C�g���Ɉړ�����
        SceneManager.LoadScene("TitleScene");
    }

    public void ParametersToPoint()
    {
        //���U���g��ʂ̃p�����[�^�\������e�|�C���g�̕]���\���ɕύX����
        parametersPanel.SetActive(false);
        parametorButton.SetActive(false);
        Debug.Log("�e�|�C���g�̕]���\��");
        resultPanel.SetActive(true);
        pointObject.SetActive(true);
    }

    public void PointToScore()
    {
        //���U���g��ʂ̊e�|�C���g�̕]���\������X�R�A�\���ɕύX����
        resultPanel.SetActive(false);
        Debug.Log("�X�R�A�]���\��");
        //uiManager.ShowScore();
    }


    public void ScoreToScroll()
    {
        //���U���g��ʂ̃X�R�A�\������x�X�g�X�R�A�̃X�N���[���\���ɕύX����
        scoreObject.SetActive(false);
        Debug.Log("�x�X�g�X�R�A�̃X�N���[���\��");
        scrollObject.SetActive(true);
    }



}
