using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStartCountDownScript : MonoBehaviour
{
    public TextMeshProUGUI gameStartText; // �Q�[���X�^�[�g���̃J�E���g�_�E���e�L�X�g�\���p
    Animator aniScd; // gameStartcd��animator�ϐ�
    public TimeDirector timeDirector; // timeDirector�ϐ�
    public GameObject gameStartcd; // �Q�[���X�^�[�g���̃J�E���g�_�E���ϐ�
    public ButtonScript buttonScript; // ButtonScript�ϐ�
    public Animator animAi; // �A�C�̃A�j���[�V�����p�ϐ�

    void Start()
    {
        aniScd = gameObject.GetComponent<Animator>(); // Animator���i�[
    }

    public void Count2()
    {
        gameStartText.text = string.Format("2"); // �e�L�X�g�̕�����2�ɂ���
        aniScd.SetBool("isCount3", true); // Animator��isCount3��ύX
    }
    public void Count1()
    {
        gameStartText.text = string.Format("1"); // �e�L�X�g�̕�����1�ɂ���
        aniScd.SetBool("isCount2", true); // Animator��isCount2��ύX
    }
    public void Count0()
    {
        gameStartText.text = string.Format("Start"); // �e�L�X�g�̕�����Start�ɂ���
        aniScd.SetBool("isCount1", true); // Animator��isCount1��ύX
    }
    public void GameStart()
    {
        timeDirector.gameStart = true; // �������Ԃ��X�^�[�g����
        buttonScript.gameStart = true; // �{�^���̃N���b�N��������
        animAi.SetTrigger("isDanceStart"); // �A�C�̃A�j���[�V�������X�^�[�g����
        gameStartcd.SetActive(false); // �J�E���g�L�����p�X���\���ɂ���
    }
}
