using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class South_SouthEast : MonoBehaviour
{
    [SerializeField] int speed = 70;    //�V�O�i���̃X�s�[�h��ݒ肷��
    Vector3 direction;                  //�ړ�����������������ꍞ�ނ��߂̕ϐ�

    void Start()
    {
    //��쓌�̕����Ɉړ�����
        direction = new Vector3(2.8f, -6.5f, 0).normalized;
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

}
