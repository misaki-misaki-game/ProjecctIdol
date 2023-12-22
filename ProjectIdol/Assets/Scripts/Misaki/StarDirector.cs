using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

public class StarDirector : MonoBehaviour
{
    public bool isStarMode; // �����l���[�h���ǂ���
    public Image Gauge; // �����l�Q�[�W
    public Animator animAi; // �A�C�̃A�j���[�V�����p�ϐ�

    // Start is called before the first frame update
    void Start()
    {
        // �����ݒ�
        Gauge.fillAmount = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isStarMode) // isStarMode���^�̂Ƃ�
        {
            animAi.SetBool("isStarMode", true); // �A�C�̃X�^�[���[�h�A�j���[�V�������X�^�[�g����
            Gauge.fillAmount -= Time.deltaTime / 10; // �Q�[�W������������(10�b��)
            if (Gauge.fillAmount <= 0) // �Q�[�W���Ȃ��Ȃ�����
            {
                animAi.SetBool("isStarMode", false); // �A�C�̃X�^�[���[�h�A�j���[�V�������I������
                isStarMode = false; // isStarMode���U�̂Ƃ�
            }
        }
    }

    public void GetStar(float chain) // �Q�b�g�X�^�[�֐�
    {
        if (!isStarMode) // isStarMode���U�̂Ƃ�
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
                // isStarMode��^�ɂ���
                isStarMode = true;
            }
        }
    }
}
