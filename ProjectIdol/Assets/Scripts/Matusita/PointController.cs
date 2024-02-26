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


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            //J�L�[�������ꂽ�ꍇ��
            StartCoroutine(OffToOn(pointTextJ));      //OffToOn()���g�p���āA��u�\��������
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            //K�L�[�������ꂽ�ꍇ��
            StartCoroutine(OffToOn(pointTextK));      //OffToOn()���g�p���āA��u�\��������
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            //L�L�[�������ꂽ�ꍇ��
            StartCoroutine(OffToOn(pointTextL));      //OffToOn()���g�p���āA��u�\��������
        }
        if (Input.GetKeyDown(KeyCode.Equals))
        {
            //;(Equals)�L�[�������ꂽ�ꍇ��
            StartCoroutine(OffToOn(pointTextEquals)); //OffToOn()���g�p���āA��u�\��������
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            //F�L�[�������ꂽ�ꍇ��
            StartCoroutine(OffToOn(pointTextF));      //OffToOn()���g�p���āA��u�\��������
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            //D�L�[�������ꂽ�ꍇ��
            StartCoroutine(OffToOn(pointTextD));      //OffToOn()���g�p���āA��u�\��������
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            //S�L�[�������ꂽ�ꍇ��
            StartCoroutine(OffToOn(pointTextS));      //OffToOn()���g�p���āA��u�\��������
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            //A�L�[�������ꂽ�ꍇ��
            StartCoroutine(OffToOn(pointTextA));      //OffToOn()���g�p���āA��u�\��������
        }
    }

    IEnumerator OffToOn(GameObject obj)
    {
        obj.SetActive(false);
        yield return new WaitForSeconds(delay);
        obj.SetActive(true);
    }
}