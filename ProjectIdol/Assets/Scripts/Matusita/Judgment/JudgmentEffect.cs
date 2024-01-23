using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JudgmentEffect : MonoBehaviour
{
    //文字の変更
    [SerializeField] TextMeshProUGUI effectText;

    public void SetText(string message, Color color)
    {
        effectText.text = message;
        StartCoroutine(MoveUp());
        effectText.color = color;
        //switch (message)
        //{
        //    //パーフェクトが出たら色を黄色に変更する
        //    case "Parfect":
        //        effectText.color = Color.yellow;
        //        break;
        //    //ノーマルが出たら色を赤色に変更する
        //    case "Nomal":
        //        effectText.color = Color.red;
        //        break;
        //    //ミスが出たら色を青色に変更する
        //    case "Miss":
        //        effectText.color = Color.blue;
        //        break;
        //}
    }

    //メッセージが出てきたときに少し上に上がる
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
