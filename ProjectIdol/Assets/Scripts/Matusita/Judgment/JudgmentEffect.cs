using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JudgmentEffect : MonoBehaviour
{
    //�����̕ύX
    [SerializeField] TextMeshProUGUI effectText;

    public void SetText(string message, Color color)
    {
        effectText.text = message;
        StartCoroutine(MoveUp());
        effectText.color = color;
        //switch (message)
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
