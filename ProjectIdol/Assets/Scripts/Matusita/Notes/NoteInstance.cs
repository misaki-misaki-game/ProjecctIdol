using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteInstance : MonoBehaviour
{
    [SerializeField] GameObject l_prefabBlue;     //������p�̐�
    [SerializeField] GameObject l_prefabRed;      //������p�̐�
    [SerializeField] GameObject l_prefabWhite;    //������p�̔�
    [SerializeField] GameObject l_prefabYellow;   //������p�̉�
    [SerializeField] GameObject r_prefabBlue;     //�E����p�̐�
    [SerializeField] GameObject r_prefabRed;      //�E����p�̐�
    [SerializeField] GameObject r_prefabWhite;    //�E����p�̔�
    [SerializeField] GameObject r_prefabYellow;   //�E����p�̉�


    public void NoteEventCC()
    {
        //���K��C#���Ȃ������ɃV�O�i���𐶐�������
        Vector3 pos = new Vector3(0, -23, 0);                   //�~�̒��S�ɐ�������悤�ɐݒ肵�Ă���
        Instantiate(l_prefabWhite, pos, Quaternion.identity);   //���F�̃V�O�i���𐼓쐼�̕����Ɉړ�������
    }
    public void NoteEventC()
    {
        //���K��C���Ȃ������ɃV�O�i���𐶐�������
        Vector3 pos = new Vector3(0, -23, 0);                   //�~�̒��S�ɐ�������悤�ɐݒ肵�Ă���
        Instantiate(r_prefabRed, pos, Quaternion.identity);     //�ԐF�̃V�O�i�����쓌�̕����Ɉړ�������
    }
    public void NoteEventDD()
    {
        //���K��D#���Ȃ������ɃV�O�i���𐶐�������
        Vector3 pos = new Vector3(0, -23, 0);                   //�~�̒��S�ɐ�������悤�ɐݒ肵�Ă���
        Instantiate(l_prefabRed, pos, Quaternion.identity);     //�ԐF�̃V�O�i���𐼖k���̕����Ɉړ�������
    }
    public void NoteEventD()
    {
        //���K��D���Ȃ������ɃV�O�i���𐶐�������
        Vector3 pos = new Vector3(0, -23, 0);                   //�~�̒��S�ɐ�������悤�ɐݒ肵�Ă���
        Instantiate(l_prefabWhite, pos, Quaternion.identity);   //���F�̃V�O�i���𐼓쐼�̕����Ɉړ�������
    }
    public void NoteEventE()
    {
        //���K��E���Ȃ������ɃV�O�i���𐶐�������
        Vector3 pos = new Vector3(0, -23, 0);                   //�~�̒��S�ɐ�������悤�ɐݒ肵�Ă���
        Instantiate(l_prefabBlue, pos, Quaternion.identity);    //�F�̃V�O�i����k�k���̕����Ɉړ�������
    }
    public void NoteEventFF()
    {
        //���K��F#���Ȃ������ɃV�O�i���𐶐�������
        Vector3 pos = new Vector3(0, -23, 0);                   //�~�̒��S�ɐ�������悤�ɐݒ肵�Ă���
        Instantiate(l_prefabYellow, pos, Quaternion.identity);  //���F�̃V�O�i���𐼓쐼�̕����Ɉړ�������
    }
    public void NoteEventF()
    {
        //���K��F���Ȃ������ɃV�O�i���𐶐�������
        Vector3 pos = new Vector3(0, -23, 0);                   //�~�̒��S�ɐ�������悤�ɐݒ肵�Ă���
        Instantiate(r_prefabBlue, pos, Quaternion.identity);    //�F�̃V�O�i���𓌓쓌�̕����Ɉړ�������
    }
    public void NoteEventGG()
    {
        //���K��G#���Ȃ������ɃV�O�i���𐶐�������
        Vector3 pos = new Vector3(0, -23, 0);                   //�~�̒��S�ɐ�������悤�ɐݒ肵�Ă���
        Instantiate(r_prefabWhite, pos, Quaternion.identity);   //���F�̃V�O�i���𓌖k���̕����Ɉړ�������
    }
    public void NoteEventG()
    {
        //���K��G���Ȃ������ɃV�O�i���𐶐�������
        Vector3 pos = new Vector3(0, -23, 0);                   //�~�̒��S�ɐ�������悤�ɐݒ肵�Ă���
        Instantiate(r_prefabRed, pos, Quaternion.identity);     //�ԐF�̃V�O�i�����쓌�̕����Ɉړ�������
    }
    public void NoteEventAA()
    {
        //���K��A#���Ȃ������ɃV�O�i���𐶐�������
        Vector3 pos = new Vector3(0, -23, 0);                   //�~�̒��S�ɐ�������悤�ɐݒ肵�Ă���
        Instantiate(l_prefabYellow, pos, Quaternion.identity);  //���F�F�̃V�O�i�����쐼�̕����Ɉړ�������
    }
    public void NoteEventA()
    {
        //���K��A���Ȃ������ɃV�O�i���𐶐�������
        Vector3 pos = new Vector3(0, -23, 0);                   //�~�̒��S�ɐ�������悤�ɐݒ肵�Ă���
        Instantiate(r_prefabYellow, pos, Quaternion.identity);  //���F�̃V�O�i����k�k���̕����Ɉړ�������
    }
    public void NoteEventB()
    {
        //���K��B���Ȃ������ɃV�O�i���𐶐�������
        Vector3 pos = new Vector3(0, -23, 0);                   //�~�̒��S�ɐ�������悤�ɐݒ肵�Ă���
        Instantiate(r_prefabBlue, pos, Quaternion.identity);    //�F�̃V�O�i���𓌓쓌�̕����Ɉړ�������
    }
}

