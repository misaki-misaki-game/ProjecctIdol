using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class NoteInstance : MonoBehaviour
{
    [SerializeField] GameObject l_prefabBlue;     //������p�̐�  ���쐼(S�L�[)�̕����Ɉړ�����
    [SerializeField] GameObject l_prefabRed;      //������p�̐�  �k�k��(F�L�[)�̕����Ɉړ�����
    [SerializeField] GameObject l_prefabWhite;    //������p�̔�  ��쐼(A�L�[)�̕����Ɉړ�����
    [SerializeField] GameObject l_prefabYellow;   //������p�̉�  ���k��(D�L�[)�̕����Ɉړ�����
    [SerializeField] GameObject r_prefabBlue;     //�E����p�̐�  �k�k��(J�L�[)�̕����Ɉړ�����
    [SerializeField] GameObject r_prefabRed;      //�E����p�̐�  ���쓌(L�L�[)�̕����Ɉړ�����
    [SerializeField] GameObject r_prefabWhite;    //�E����p�̔�  ���k��(K�L�[)�̕����Ɉړ�����
    [SerializeField] GameObject r_prefabYellow;   //�E����p�̉�  ��쓌(;�L�[)�̕����Ɉړ�����

    public void NoteEventCC()
    {
        //���K��C#���Ȃ������ɃV�O�i���𐶐�������
        Vector3 pos = new Vector3(0, -23, 0);                       //�~�̒��S�ɐ�������悤�ɐݒ肵�Ă���
        Instantiate(l_prefabWhite, pos, Quaternion.identity);       //���F�̃V�O�i�����쐼(A�L�[)�̕����Ɉړ�������
    }
    public void NoteEventC()
    {
        //���K��C���Ȃ������ɃV�O�i���𐶐�������
        Vector3 pos = new Vector3(0, -23, 0);                       //�~�̒��S�ɐ�������悤�ɐݒ肵�Ă���
        Instantiate(r_prefabYellow, pos, Quaternion.identity);      //���F�̃V�O�i�����쓌(;�L�[)�̕����Ɉړ�������
    }
    public void NoteEventDD()
    {
        //���K��D#���Ȃ������ɃV�O�i���𐶐�������
        Vector3 pos = new Vector3(0, -23, 0);                       //�~�̒��S�ɐ�������悤�ɐݒ肵�Ă���
        Instantiate(r_prefabYellow, pos, Quaternion.identity);      //���F�̃V�O�i���𓌖k��(K�L�[)�̕����Ɉړ�������
    }
    public void NoteEventD()
    {
        //���K��D���Ȃ������ɃV�O�i���𐶐�������
        Vector3 pos = new Vector3(0, -23, 0);                       //�~�̒��S�ɐ�������悤�ɐݒ肵�Ă���
        Instantiate(r_prefabRed, pos, Quaternion.identity);         //�ԐF�̃V�O�i���𓌓쓌(L�L�[)�̕����Ɉړ�������
    }
    public void NoteEventE()
    {
        //���K��E���Ȃ������ɃV�O�i���𐶐�������
        Vector3 pos = new Vector3(0, -23, 0);                       //�~�̒��S�ɐ�������悤�ɐݒ肵�Ă���
        Instantiate(l_prefabRed, pos, Quaternion.identity);         //�ԐF�̃V�O�i����k�k��(F�L�[)�̕����Ɉړ�������
    }
    public void NoteEventFF()
    {
        //���K��F#���Ȃ������ɃV�O�i���𐶐�������
        Vector3 pos = new Vector3(0, -23, 0);                       //�~�̒��S�ɐ�������悤�ɐݒ肵�Ă���
        Instantiate(l_prefabBlue, pos, Quaternion.identity);        //�F�̃V�O�i���𐼓쐼(S�L�[)�̕����Ɉړ�������
    }
    public void NoteEventF()
    {
        //���K��F���Ȃ������ɃV�O�i���𐶐�������
        Vector3 pos = new Vector3(0, -23, 0);                       //�~�̒��S�ɐ�������悤�ɐݒ肵�Ă���
        Instantiate(l_prefabWhite, pos, Quaternion.identity);       //���F�̃V�O�i�����쐼(A�L�[)�̕����Ɉړ�������
    }
    public void NoteEventGG()
    {
        //���K��G#���Ȃ������ɃV�O�i���𐶐�������
        Vector3 pos = new Vector3(0, -23, 0);                       //�~�̒��S�ɐ�������悤�ɐݒ肵�Ă���
        Instantiate(l_prefabYellow, pos, Quaternion.identity);      //���F�̃V�O�i���𐼖k��(D�L�[)�̕����Ɉړ�������
    }
    public void NoteEventG()
    {
        //���K��G���Ȃ������ɃV�O�i���𐶐�������
        Vector3 pos = new Vector3(0, -23, 0);                       //�~�̒��S�ɐ�������悤�ɐݒ肵�Ă���
        Instantiate(r_prefabRed, pos, Quaternion.identity);         //�ԐF�̃V�O�i���𓌓쓌(L�L�[)�̕����Ɉړ�������
    }
    public void NoteEventAA()
    {
        //���K��A#���Ȃ������ɃV�O�i���𐶐�������
        Vector3 pos = new Vector3(0, -23, 0);                       //�~�̒��S�ɐ�������悤�ɐݒ肵�Ă���
        Instantiate(l_prefabYellow, pos, Quaternion.identity);      //���F�F�̃V�O�i�����쐼�̕����Ɉړ�������
    }
    public void NoteEventA()
    {
        //���K��A���Ȃ������ɃV�O�i���𐶐�������
        Vector3 pos = new Vector3(0, -23, 0);                       //�~�̒��S�ɐ�������悤�ɐݒ肵�Ă���
        Instantiate(r_prefabBlue, pos, Quaternion.identity);        //�F�̃V�O�i����k�k��(J�L�[)�̕����Ɉړ�������
    }
    public void NoteEventB()
    {
        //���K��B���Ȃ������ɃV�O�i���𐶐�������
        Vector3 pos = new Vector3(0, -23, 0);                       //�~�̒��S�ɐ�������悤�ɐݒ肵�Ă���
        Instantiate(r_prefabYellow, pos, Quaternion.identity);      //���F�̃V�O�i���𓌓쓌(;�L�[)�̕����Ɉړ�������
    }
}

