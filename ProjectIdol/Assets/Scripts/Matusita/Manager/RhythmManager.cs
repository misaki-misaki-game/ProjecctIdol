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
    [SerializeField] GameObject countDownPanal;                 //�Q�[���J�n�O�̃J�E���g�_�E����\�����邽�߂̃p�l��
    [SerializeField] TextMeshProUGUI countDownText;             //�Q�[���J�n�O�̃J�E���g�_�E�����邽�߂̃e�L�X�g
    [SerializeField] UiManager uiManager;                       //UIManager���g�����߂̕ϐ�
    [SerializeField] TextMeshProUGUI rankText;                  //�Q�[���I�����̃����N��\�����邽�߂̃e�L�X�g
    [SerializeField] GameObject resultPanel;                    //�Q�[���I�����̌��ʂ�\�����邽�߂̃p�l��
    [SerializeField] GameObject parametorButton;
    [SerializeField] GameObject noteController;                 //MIDI�̉������I�������Ƃ��Ɏg�p����Q�[���I�u�W�F�N�g
    [SerializeField] PlayableDirector playableDirector;         //�^�C�����C�����擾���čĐ�
    [SerializeField] AudioSource audioSource;
    [SerializeField] Animator charaAnimator;                    //�L�����N�^�[�A�j���[�V����

    [SerializeField] RhythmDiamondMesh rhythmDiamondMesh;
    [SerializeField] RhythmDiamondMesh diamondFrame;
    [SerializeField] RhythmDiamondMesh diamondGraph;
    [SerializeField] float bgmF = 0.3f;

    void Start()
    {
        StartCoroutine(GameMain());                             //�^�C�g����ʂ���ړ������Ƃ��ɃJ�E���g�_�E�����͂��߁A
    }

    IEnumerator GameMain()
    {
        countDownText.text = "      3";                         //�J�E���g�_�E���p�l���ɕ\������e�L�X�g��3�ɕύX����
                                                                //�󔒂̓J�E���g�_�E���p�l���ɕ\������Ƃ����Y��ɐ^�񒆂ɕ\�����邽�߂̂���
        yield return new WaitForSeconds(1);                     //1�b�J����
        countDownText.text = "      2";                         //�J�E���g�_�E���p�l���ɕ\������e�L�X�g��2�ɕύX����
        yield return new WaitForSeconds(1);                     //1�b�J����
        countDownText.text = "      1";                         //�J�E���g�_�E���p�l���ɕ\������e�L�X�g��1�ɕύX����
        yield return new WaitForSeconds(1);                     //1�b�J����
        countDownText.text = "  ���C�u\n" + "     �J�n!";        //�J�E���g�_�E���p�l���ɕ\������e�L�X�g�����C�u�J�n�I�ɕύX����
        yield return new WaitForSeconds(0.5f);                  //0.5�b�J����
        countDownText.text = " ";                               //�J�E���g�_�E���p�l���ɉ����\�����Ȃ��悤�ɂ��邽�߂ɋ󔒂�\��������
        countDownPanal.SetActive(false);                        //�J�E���g�_�E���p�l�����\���ɂ���
        charaAnimator.SetTrigger("isDanceStart");               //�L�����N�^�[�̃A�j���[�V�������X�^�[�g����
        yield return new WaitForSeconds(0.3f);                  //0.3�b�J����
        playableDirector.Play();                                //playableDirector��Play()���J�n������@���Q�[�̉����ƂȂ���̂��Đ�������

    }

    //�A�C�h���̉������I�������̂�������C�x���g
    public void EndEvent()
    {
        Debug.Log("game end");
        charaAnimator.SetTrigger("isDanceEnd");         // �L�����N�^�[�̃A�j���[�V�������I������
        noteController.SetActive(false);                //noteContoroller��SetActive��false�ɕύX���A�V�O�i���̏o������߂�����
        uiManager.ShowResult();                         //UIManager��ShowResult()���g�p���A�Q�[�����I�������Ƃ��ɏ������V�O�i���̗ʂ�\��������
        /// diamondFrame��diamondGraph�̃Z�b�g�A�b�v���s�� ///
        diamondFrame.SetUp();
        diamondGraph.SetUp();
        parametorButton.SetActive(true);
        resultPanel.SetActive(true);
        uiManager.PointRank();                               //UIManager��Rank()���g�p���A�Q�[�����I�������Ƃ��Ƀ����N�e�L�X�g��_���ɉ����ĕύX������
        //uiManager.ShowScore();                          //UIManager��ShowScore()���g�p���A�X�R�A�����Z���Ȃ���\�������遨������{�^������������\����������
    }



}
