using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointController : MonoBehaviour
{
    [SerializeField] GameObject pointTextJ;         //�k�k���̕����ɂ���J�e�L�X�g
    [SerializeField] GameObject pointTextK;         //���k���̕����ɂ���K�e�L�X�g
    [SerializeField] GameObject pointTextL;         //���쓌�̕����ɂ���L�e�L�X�g
    [SerializeField] GameObject pointTextEquals;    //��쓌�̕����ɂ���G(Equals)�e�L�X�g
    [SerializeField] GameObject pointTextF;         //�k�k���̕����ɂ���F�e�L�X�g
    [SerializeField] GameObject pointTextD;         //���k���̕����ɂ���D�e�L�X�g
    [SerializeField] GameObject pointTextS;         //���쐼�̕����ɂ���S�e�L�X�g
    [SerializeField] GameObject pointTextA;         //��쐼�̕����ɂ���A�e�L�X�g

    [SerializeField] float delay = 0.1f;            //�I�u�W�F�N�g���A�N�e�B�u�ɂ��鎞��

    private void Start()
    {
        pointTextJ      = transform.Find("Text_J").gameObject;          //�q�I�u�W�F�N�g�ł���J�e�L�X�g�������Ă���
        pointTextK      = transform.Find("Text_K").gameObject;          //�q�I�u�W�F�N�g�ł���K�e�L�X�g�������Ă���
        pointTextL      = transform.Find("Text_L").gameObject;          //�q�I�u�W�F�N�g�ł���L�e�L�X�g�������Ă���
        pointTextEquals = transform.Find("Text_Equals").gameObject;     //�q�I�u�W�F�N�g�ł���;(Equals)�e�L�X�g�������Ă���
        pointTextF      = transform.Find("Text_F").gameObject;          //�q�I�u�W�F�N�g�ł���F�e�L�X�g�������Ă���
        pointTextD      = transform.Find("Text_D").gameObject;          //�q�I�u�W�F�N�g�ł���D�e�L�X�g�������Ă���
        pointTextS      = transform.Find("Text_S").gameObject;          //�q�I�u�W�F�N�g�ł���S�e�L�X�g�������Ă���
        pointTextA      = transform.Find("Text_A").gameObject;          //�q�I�u�W�F�N�g�ł���A�e�L�X�g�������Ă���
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            StartCoroutine(OffToOn_J());
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            StartCoroutine(OffToOn_K());
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            StartCoroutine(OffToOn_L());
        }
        if (Input.GetKeyDown(KeyCode.Equals))
        {
            StartCoroutine(OffToOn_Equals());
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(OffToOn_F());
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            StartCoroutine(OffToOn_D());
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine(OffToOn_S());
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(OffToOn_A());
        }
    }

    IEnumerator OffToOn_J()
    {
        //J�L�[�������ꂽ�Ƃ��Ɏg�p����
        pointTextJ.SetActive(false);                //J�e�L�X�g���\���ɂ���
        yield return new WaitForSeconds(delay);     //delay�ɐݒ肵�����l���������Ԃ��󂯂�
        pointTextJ.SetActive(true);                 //J�e�L�X�g��\��������
    }
    IEnumerator OffToOn_K()
    {
        //K�L�[�������ꂽ�Ƃ��Ɏg�p����
        pointTextK.SetActive(false);                //K�e�L�X�g���\���ɂ���
        yield return new WaitForSeconds(delay);     //delay�ɐݒ肵�����l���������Ԃ��󂯂�
        pointTextK.SetActive(true);                 //K�e�L�X�g��\��������
    }
    IEnumerator OffToOn_L()
    {
        //L�L�[�������ꂽ�Ƃ��Ɏg�p����
        pointTextL.SetActive(false);                //L�e�L�X�g���\���ɂ���
        yield return new WaitForSeconds(delay);     //delay�ɐݒ肵�����l���������Ԃ��󂯂�
        pointTextL.SetActive(true);                 //L�e�L�X�g��\��������
    }
    IEnumerator OffToOn_Equals()
    {
        //;(equals)�L�[�������ꂽ�Ƃ��Ɏg�p����
        pointTextEquals.SetActive(false);           //;(Equals)�e�L�X�g���\���ɂ���
        yield return new WaitForSeconds(delay);     //delay�ɐݒ肵�����l���������Ԃ��󂯂�
        pointTextEquals.SetActive(true);            //;(Equals)�e�L�X�g��\��������
    }
    IEnumerator OffToOn_F()
    {
        //F�L�[�������ꂽ�Ƃ��Ɏg�p����
        pointTextF.SetActive(false);                //F�e�L�X�g���\���ɂ���
        yield return new WaitForSeconds(delay);     //delay�ɐݒ肵�����l���������Ԃ��󂯂�
        pointTextF.SetActive(true);                 //F�e�L�X�g��\��������
    }
    IEnumerator OffToOn_D()
    {
        //D�L�[�������ꂽ�Ƃ��Ɏg�p����
        pointTextD.SetActive(false);                //D�e�L�X�g���\���ɂ���
        yield return new WaitForSeconds(delay);     //delay�ɐݒ肵�����l���������Ԃ��󂯂�
        pointTextD.SetActive(true);                 //D�e�L�X�g��\��������
    }
    IEnumerator OffToOn_S()
    {
        //S�L�[�������ꂽ�Ƃ��Ɏg�p����   
        pointTextS.SetActive(false);                //S�e�L�X�g���\���ɂ���
        yield return new WaitForSeconds(delay);     //delay�ɐݒ肵�����l���������Ԃ��󂯂�
        pointTextS.SetActive(true);                 //S�e�L�X�g��\��������
    }
    IEnumerator OffToOn_A()
    {
        //A�L�[�������ꂽ�Ƃ��Ɏg�p����
        pointTextA.SetActive(false);                //A�e�L�X�g���\���ɂ���
        yield return new WaitForSeconds(delay);     //delay�ɐݒ肵�����l���������Ԃ��󂯂�
        pointTextA.SetActive(true);                 //A�e�L�X�g��\��������
    }
}