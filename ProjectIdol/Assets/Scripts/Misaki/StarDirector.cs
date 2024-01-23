using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;
using static SignalScript;

public class StarDirector : MonoBehaviour
{
    public int starCount = 0; // �����l���[�h�ɂȂ�����
    public enum StarState
    {
        NormalMode, // �ʏ��� 0
        StarMode // �X�^�[���[�h 1
    }
    public enum BackImageState
    {
        StageNormal, // �ʏ�X�e�[�W�w�i 0
        StageStar, // �X�^�[�X�e�[�W�w�i 1
        ArenaNormal, // �ʏ�A���[�i�w�i 2
        ArenaStar // �X�^�[�A���[�i�w�i 3
    }
    public StarState starState = StarState.NormalMode; // StarState�ϐ�
    public Image Gauge; // �����l�Q�[�W
    public Animator animAi; // �A�C�̃A�j���[�V�����p�ϐ�
    [EnumIndex(typeof(BackImageState))]
    public Sprite[] backImages = new Sprite[4]; // �w�i�摜�z��
    public GameObject[] backImageObjects = new GameObject[2]; // �w�i�I�u�W�F�N�g�z��

    // Start is called before the first frame update
    void Start()
    {
        // �����ݒ�
        Gauge.fillAmount = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ShowStarMode(); // �X�^�[���[�h�̉��o���s��
    }
    private void ShowStarMode() // �X�^�[���[�h�̉��o���s���֐�
    {
        if (starState == StarState.StarMode) // �X�^�[���[�h�̎�
        {
            animAi.SetBool("isStarMode", true); // �A�C�̃X�^�[���[�h�A�j���[�V�������X�^�[�g����
            backImageObjects[0].GetComponent<Image>().sprite = backImages[1]; // �X�e�[�W�w�i���X�^�[���[�h�ɂ���
            backImageObjects[1].GetComponent<Image>().sprite = backImages[3]; // �ϋq�w�i���X�^�[���[�h�ɂ���
            Gauge.fillAmount -= Time.deltaTime / 10; // �Q�[�W������������(10�b��)
            if (Gauge.fillAmount <= 0) // �Q�[�W���Ȃ��Ȃ�����
            {
                backImageObjects[0].GetComponent<Image>().sprite = backImages[0]; // �X�e�[�W�w�i��ʏ탂�[�h�ɂ���
                backImageObjects[1].GetComponent<Image>().sprite = backImages[2]; // �ϋq�w�i��ʏ탂�[�h�ɂ���
                animAi.SetBool("isStarMode", false); // �A�C�̃X�^�[���[�h�A�j���[�V�������I������
                starState = StarState.NormalMode; // �ʏ탂�[�h�ɕύX
                starCount += 1; // �����l���[�h�����Z����
            }
        }
    }
    public void GetStar(float chain) // �Q�b�g�X�^�[�֐�
    {
        if (starState == StarState.NormalMode) // �ʏ탂�[�h�̂Ƃ�
        {
            // �����l�Q�[�W�̑���������
            if (chain >= 1)
            {
                // �`�F�C���̒l���Q�[�W�𑝉�������
                Gauge.fillAmount += chain / 100;
            }
            // �����l�Q�[�W��1�ɂȂ�����
            if (Gauge.fillAmount == 1)
            {
                starState = StarState.StarMode; // �X�^�[���[�h�ɕύX
            }
        }
    }
}
