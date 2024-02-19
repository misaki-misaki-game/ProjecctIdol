using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class JudgmentArea : MonoBehaviour
{
    [SerializeField] float radius;                      //�V�O�i���̔���̔��a��ݒ肷��

    [SerializeField] KeyCode keyCode;                   //Unity��Őݒ肵��KeyCode���g�p����

    [SerializeField] UiManager uiManager;               //UIManager���g�����߂̕ϐ�
    [SerializeField] GameObject textEffectPrefab;       //�V�O�i�����������Ƃ��ɕ\�����������e�L�X�g��ݒ肷�邽�߂̃Q�[���I�u�W�F�N�g

    private void Update()
    {
        if (Input.GetKeyDown(keyCode))
        {
            //Unity��Őݒ肳�ꂽKeyCode�������ꂽ�ꍇSignalJudgment()�����s�����
            SignalJudgment();
        }
    }

    void SignalJudgment()
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

            if (distance < 4)
            {
                //�����V�O�i�����������̂����a4�ȉ��Ȃ�
                uiManager.AddScore(50);                                                     //UIManager��AddScore���g�p���ăX�R�A��50���Z����
                uiManager.AddCombo();                                                       //UIManager��AddCombo���g�p���ăR���{��1���Z����
                SpawnTextEffect("Parfect", hit2D.transform.position, Color.yellow);         //�V�O�i�����������ꏊ�ɉ��F��Parfect�ƕ\������
            }
            else if (distance < 7)
            {
                //�����V�O�i�����������̂����a7�ȉ��Ȃ�
                uiManager.AddScore(25);                                                     //UIManager��AddScore���g�p���ăX�R�A��25���Z����
                uiManager.AddCombo();                                                       //UIManager��AddCombo���g�p���ăR���{��1���Z����
                SpawnTextEffect("Nomal", hit2D.transform.position, Color.red);              //�V�O�i�����������ꏊ�ɐԐF��Nomal�ƕ\������
            }
            else
            {
                //�����V�O�i�����������̂�����ȊO�Ȃ�
                uiManager.NoteMiss();                                                       //UIManager��NoteMiss���g�p���ăX�R�A��-25���Z����
                SpawnTextEffect("Miss", hit2D.transform.position, Color.blue);              //�V�O�i�����������ꏊ�ɐF��Miss�ƕ\������
            }

            //�Ԃ��������̂�j�󂷂�
            Destroy(hit2D.collider.gameObject);
            hit2D.collider.gameObject.SetActive(false);
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
