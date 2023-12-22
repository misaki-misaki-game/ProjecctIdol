using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteMiss : MonoBehaviour
{
    [SerializeField] float radius;

    private void Update()
    {
        //�m�[�c��2�d�Ȃ������ɏ�̃m�[�c�������遨���D��ɂ���
        //�����̃m�[�c�擾���Ĉ�ԉ�������
        RaycastHit2D[] hits2D = Physics2D.CircleCastAll(transform.position, radius, Vector3.zero);

        //��ԉ��̃m�[�c������

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
        }
    }

    //�����c�[��
    void OnDrawGizmosSelected()
    {
        //����ꏊ����\��������
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, radius);
    }

}
