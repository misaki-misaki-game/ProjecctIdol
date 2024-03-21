using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static SignalScript;

public class JudgmentEffect : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI effectText;        //�F��\�����镶����ύX����e�L�X�g�I�u�W�F�N�g
    [SerializeField] AudioSource audioSource;

    public void SetText(string message, Color color)
    {
        //JudmentArea�X�N���v�g��SpawnTextEffect�Ŏg�p���Ă���
        effectText.text = message;      //message�ɕ\�����������������邱�ƂŎQ�Ɛ�ŕ\������镶����ύX���邱�Ƃ��ł���
        StartCoroutine(MoveUp());       //MoveUp()���g�p����
        effectText.color = color;       //color�ɕ\���������F�����邱�ƂŎQ�Ɛ�ŕ\�������F��ύX���邱�Ƃ��ł���

        //switch (message)              //����switch���ł���Ƃ��ɐ؂�ւ����悤�Ɏc���Ă���
        //{
        //    //�p�[�t�F�N�g���o����F�����F�ɕύX����
        //    case "Parfect":
        //        effectText.color = Color.yellow;
        //        break;
        //    //�m�[�}�����o����F��ԐF�ɕύX����
        //    case "Nomal":
        //        effectText.color = Color.red;
        //        break;
        //    //�~�X���o����F��F�ɕύX����
        //    case "Miss":
        //        effectText.color = Color.blue;
        //        break;
        //}
    }

    //���b�Z�[�W���o�Ă����Ƃ��ɏ�����ɏオ��
    IEnumerator MoveUp()
    {
        for (int i = 0; i < 20; i++)
        {
            yield return new WaitForSeconds(0.01f);
            transform.Translate(0, 0.05f, 0);
        }
        Destroy(gameObject);
    }
}
