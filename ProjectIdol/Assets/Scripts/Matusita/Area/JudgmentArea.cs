using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class JudgmentArea : MonoBehaviour
{
    [SerializeField] float radius;
    [SerializeField] KeyCode keyCode = default;

    [SerializeField] UiManager uiManager;

    private void Update()
    {
        if(Input.GetKeyDown(keyCode))
        {
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
                //����@���̂Ƃ���Ȃ�
                if (distance < 3)
                {
                    uiManager.AddScore(50);
                    uiManager.AddCombo();
                    uiManager.AddStar(1);
                }
                else if (distance < 5)
                {
                    uiManager.AddScore(50);
                    uiManager.AddCombo();
                    uiManager.AddStar(1);
                }
                else
                {
                    uiManager.NoteMiss();
                }


                //�Ԃ��������̂�j�󂷂�
                Destroy(hit2D.collider.gameObject);
                hit2D.collider.gameObject.SetActive(false);
            }
        }
    }

    //�����c�[��
    void OnDrawGizmosSelected()
    {
        //����ꏊ��Ԃ��\��������
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, radius);
    }
}
