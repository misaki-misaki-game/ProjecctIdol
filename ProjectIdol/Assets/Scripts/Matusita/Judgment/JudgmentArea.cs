using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor.Tilemaps;

public class JudgmentArea : MonoBehaviour
{
    [SerializeField] float radius;
    [SerializeField] KeyCode keyCode;
    [SerializeField] UiManager uiManager;
    [SerializeField] GameObject textEffectPrefab;

    private void Update()
    {
        if (Input.GetKeyDown(keyCode))
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
                if (distance < 4)
                {
                    uiManager.AddScore(50);
                    uiManager.AddCombo();
                    SpawnTextEffect("Parfect", hit2D.transform.position, Color.yellow);
                }
                else if (distance < 7)
                {
                    uiManager.AddScore(25);
                    uiManager.AddCombo();
                    SpawnTextEffect("Nomal", hit2D.transform.position, Color.red);
                }
                else
                {
                    uiManager.NoteMiss();
                    SpawnTextEffect("Miss", hit2D.transform.position, Color.blue);
                }

                //�Ԃ��������̂�j�󂷂�
                Destroy(hit2D.collider.gameObject);
                hit2D.collider.gameObject.SetActive(false);
            }
        }
    }

    void SpawnTextEffect(string message, Vector3 position, Color color)
    {
        //����̃G�t�F�N�g�𐶐�����
        GameObject effect = Instantiate(textEffectPrefab, position + Vector3.up * 1.5f, Quaternion.identity);
        JudgmentEffect judgmentEffect = effect.GetComponent<JudgmentEffect>();
        judgmentEffect.SetText(message, color);
    }

    //�����c�[��
    void OnDrawGizmosSelected()
    {
        //����ꏊ��Ԃ��\��������
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, radius);
    }
}
