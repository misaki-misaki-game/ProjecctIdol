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
        //�����ʒu
        Vector3 pos = new Vector3(0, -23, 0);
        Instantiate(l_prefabWhite, pos, Quaternion.identity);
    }
    public void NoteEventC()
    {
        //�����ʒu
        Vector3 pos = new Vector3(0, -23, 0);
        Instantiate(r_prefabRed, pos, Quaternion.identity);
    }
    public void NoteEventDD()
    {
        //�����ʒu
        Vector3 pos = new Vector3(0, -23, 0);
        Instantiate(l_prefabRed, pos, Quaternion.identity);

    }
    public void NoteEventD()
    {
        //�����ʒu
        Vector3 pos = new Vector3(0, -23, 0);
        Instantiate(l_prefabWhite, pos, Quaternion.identity);
    }
    public void NoteEventE()
    {
        //�����ʒu
        Vector3 pos = new Vector3(0, -23, 0);
        Instantiate(l_prefabBlue, pos, Quaternion.identity);
    }
    public void NoteEventFF()
    {
        //�����ʒu
        Vector3 pos = new Vector3(0, -23, 0);
        Instantiate(l_prefabYellow, pos, Quaternion.identity);
    }
    public void NoteEventF()
    {
        //�����ʒu
        Vector3 pos = new Vector3(0, -23, 0);
        Instantiate(r_prefabBlue, pos, Quaternion.identity);
    }
    public void NoteEventGG()
    {
        //�����ʒu
        Vector3 pos = new Vector3(0, -23, 0);
        Instantiate(r_prefabWhite, pos, Quaternion.identity);
    }
    public void NoteEventG()
    {
        //�����ʒu
        Vector3 pos = new Vector3(0, -23, 0);
        Instantiate(r_prefabRed, pos, Quaternion.identity);
    }
    public void NoteEventAA()
    {
        //�����ʒu
        Vector3 pos = new Vector3(0, -23, 0);
        Instantiate(l_prefabYellow, pos, Quaternion.identity);
    }
    public void NoteEventA()
    {
        //�����ʒu
        Vector3 pos = new Vector3(0, -23, 0);
        Instantiate(r_prefabYellow, pos, Quaternion.identity);
    }
    public void NoteEventB()
    {
        //�����ʒu
        Vector3 pos = new Vector3(0, -23, 0);
        Instantiate(r_prefabBlue, pos, Quaternion.identity);
    }
}

