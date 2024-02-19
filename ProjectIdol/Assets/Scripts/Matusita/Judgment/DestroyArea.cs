using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class DestroyArea : MonoBehaviour
{
    //�v���C���[�������Ȃ������V�O�i���������I�ɏ����āA�~�X��������邽�߂̃X�N���v�g

    [SerializeField] float radius;          //�V�O�i���̔���̔��a��ݒ肷��
    [SerializeField] UiManager uiManager;   //UIManager���g�����߂̕ϐ�

    private void Update()
    {
        RaycastHit2D[] hits2D = Physics2D.CircleCastAll(transform.position, radius, Vector3.zero);      //Ray���΂��Ĕ��肷��

        if (hits2D.Length == 0)
        {
            return;
        }

        RaycastHit2D hit2D = hits2D[0];
        if (hit2D)
        {
            //�߂��\��  ��Βl�ōl���� Mathf.Abs()
            float distance = Mathf.Abs(hit2D.transform.position.y - transform.position.y);

            //�Ԃ��������̂�j�󂷂�
            Destroy(hit2D.collider.gameObject);
            hit2D.collider.gameObject.SetActive(false);

            uiManager.NoteMiss();   //UIManager��NoteMiss���g�p���ăX�R�A��-25���Z����
        }

    }

    //�����c�[��
    void OnDrawGizmosSelected()
    {
        //�����I�ɏ����ꏊ����\��������
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, radius);
    }


}
