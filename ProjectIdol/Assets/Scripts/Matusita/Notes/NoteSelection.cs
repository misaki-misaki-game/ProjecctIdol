using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSelection: MonoBehaviour
{
    public GameObject l_prefabBlue;     //������p�̐�
    public GameObject l_prefabRed;      //������p�̐�
    public GameObject l_prefabWhite;    //������p�̔�
    public GameObject l_prefabYellow;   //������p�̉�
    public GameObject r_prefabBlue;     //�E����p�̐�
    public GameObject r_prefabRed;      //�E����p�̐�
    public GameObject r_prefabWhite;    //�E����p�̔�
    public GameObject r_prefabYellow;   //�E����p�̉�

    void Update()
    {
        //30�t���[�����ƂɃV�[���Ƀv���n�u����
        if (Time.frameCount % 240 == 0)
        {
            int randNumber = Random.Range(0, 15);

            if (randNumber == 0)
            {
                //�����ʒu
                Vector3 pos = new Vector3(0, -23, 0);
                //�Ɛ𐶐�������
                Instantiate(l_prefabBlue, pos, Quaternion.identity);
                Instantiate(r_prefabBlue, pos, Quaternion.identity);
            }
            if (randNumber == 1)
            {
                //�����ʒu
                Vector3 pos = new Vector3(0, -23, 0);
                //�ƐԂ𐶐�������
                Instantiate(l_prefabBlue, pos, Quaternion.identity);
                Instantiate(r_prefabRed, pos, Quaternion.identity);
            }
            if (randNumber == 2)
            {
                //�����ʒu
                Vector3 pos = new Vector3(0, -23, 0);
                //�Ɣ��𐶐�������
                Instantiate(l_prefabBlue, pos, Quaternion.identity);
                Instantiate(r_prefabWhite, pos, Quaternion.identity);
            }
            if (randNumber == 3)
            {
                //�����ʒu
                Vector3 pos = new Vector3(0, -23, 0);
                //�Ɖ��𐶐�������
                Instantiate(l_prefabBlue, pos, Quaternion.identity);
                Instantiate(r_prefabYellow, pos, Quaternion.identity);
            }
            if (randNumber == 4)
            {
                //�����ʒu
                Vector3 pos = new Vector3(0, -23, 0);
                //�ԂƐ𐶐�������
                Instantiate(l_prefabRed, pos, Quaternion.identity);
                Instantiate(r_prefabBlue, pos, Quaternion.identity);
            }
            if (randNumber == 5)
            {
                //�����ʒu
                Vector3 pos = new Vector3(0, -23, 0);
                //�ԂƐԂ𐶐�������
                Instantiate(l_prefabRed, pos, Quaternion.identity);
                Instantiate(r_prefabRed, pos, Quaternion.identity);
            }
            if (randNumber == 6)
            {
                //�����ʒu
                Vector3 pos = new Vector3(0, -23, 0);
                //�ԂƔ��𐶐�������
                Instantiate(l_prefabRed, pos, Quaternion.identity);
                Instantiate(r_prefabWhite, pos, Quaternion.identity);
            }
            if (randNumber == 7)
            {
                //�����ʒu
                Vector3 pos = new Vector3(0, -23, 0);
                //�ԂƉ��𐶐�������
                Instantiate(l_prefabRed, pos, Quaternion.identity);
                Instantiate(r_prefabYellow, pos, Quaternion.identity);
            }
            if (randNumber == 8)
            {
                //�����ʒu
                Vector3 pos = new Vector3(0, -23, 0);
                //���Ɛ𐶐�������
                Instantiate(l_prefabWhite, pos, Quaternion.identity);
                Instantiate(r_prefabBlue, pos, Quaternion.identity);
            }
            if (randNumber == 9)
            {
                //�����ʒu
                Vector3 pos = new Vector3(0, -23, 0);
                //���ƐԂ𐶐�������
                Instantiate(l_prefabWhite, pos, Quaternion.identity);
                Instantiate(r_prefabRed, pos, Quaternion.identity);
            }
            if (randNumber == 10)
            {
                //�����ʒu
                Vector3 pos = new Vector3(0, -23, 0);
                //���Ɣ��𐶐�������
                Instantiate(l_prefabWhite, pos, Quaternion.identity);
                Instantiate(r_prefabWhite, pos, Quaternion.identity);
            }
            if (randNumber == 11)
            {
                //�����ʒu
                Vector3 pos = new Vector3(0, -23, 0);
                //���Ɖ��𐶐�������
                Instantiate(l_prefabWhite, pos, Quaternion.identity);
                Instantiate(r_prefabYellow, pos, Quaternion.identity);
            }
            if (randNumber == 12)
            {
                //�����ʒu
                Vector3 pos = new Vector3(0, -23, 0);
                //���Ɛ𐶐�������
                Instantiate(l_prefabYellow, pos, Quaternion.identity);
                Instantiate(r_prefabBlue, pos, Quaternion.identity);
            }
            if (randNumber == 13)
            {
                //�����ʒu
                Vector3 pos = new Vector3(0, -23, 0);
                //���ƐԂ𐶐�������
                Instantiate(l_prefabYellow, pos, Quaternion.identity);
                Instantiate(r_prefabRed, pos, Quaternion.identity);
            }
            if (randNumber == 14)
            {
                //�����ʒu
                Vector3 pos = new Vector3(0, -23, 0);
                //���Ɣ��𐶐�������
                Instantiate(l_prefabYellow, pos, Quaternion.identity);
                Instantiate(r_prefabWhite, pos, Quaternion.identity);
            }
            if (randNumber == 15)
            {
                //�����ʒu
                Vector3 pos = new Vector3(0, -23, 0);
                //���Ɖ��𐶐�������
                Instantiate(l_prefabYellow, pos, Quaternion.identity);
                Instantiate(r_prefabYellow, pos, Quaternion.identity);
            }

        }
    }
}
