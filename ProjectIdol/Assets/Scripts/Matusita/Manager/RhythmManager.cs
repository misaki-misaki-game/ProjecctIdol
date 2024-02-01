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
    //�J�E���g�_�E���̃p�l���ƃe�L�X�g
    [SerializeField] GameObject countDownPanal;
    [SerializeField] TextMeshProUGUI countDownText;

    [SerializeField] UiManager uiManager;
    [SerializeField] TextMeshProUGUI rankText;

    //���U���g�p�l��
    [SerializeField] GameObject resultPanel;

    [SerializeField] GameObject noteController;

    //�^�C�����C�����擾���čĐ�
    [SerializeField] PlayableDirector playableDirector;

    //�L�����N�^�[�A�j���[�V����
    [SerializeField] Animator charaAnimator;

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
        countDownText.text = "  ���C�u\n" + "     �J�n!";
        yield return new WaitForSeconds(0.5f);
        countDownText.text = " ";
        countDownPanal.SetActive(false);
        charaAnimator.SetTrigger("isDanceStart"); // �L�����N�^�[�̃A�j���[�V�������X�^�[�g����
        yield return new WaitForSeconds(0.3f);
        playableDirector.Play();
    }

    //�A�C�h���̉������I�������̂�������C�x���g
    public void EndEvent()
    {
        Debug.Log("game end");
        charaAnimator.SetTrigger("isDanceEnd"); // �L�����N�^�[�̃A�j���[�V�������I������
        //resultPanel.SetActive(true);
        uiManager.Rank();
        uiManager.ShowScore();
        noteController.SetActive(false);
    }
}
