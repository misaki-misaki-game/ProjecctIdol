                                            using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class West_SouthWest : MonoBehaviour
{
    [SerializeField] int speed = 70;    //�V�O�i���̃X�s�[�h��ݒ肷��
    Vector3 direction;                  //�ړ�����������������ꍞ�ނ��߂̕ϐ�

    void Start()
    {
    //���쐼�̕����Ɉړ�����
        direction = new Vector3(-6.5f, -2.8f, 0).normalized;
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

}
