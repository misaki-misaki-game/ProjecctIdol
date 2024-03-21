using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static SignalScript;

public class JudgmentEffect : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI effectText;        //色や表示する文字を変更するテキストオブジェクト
    [SerializeField] AudioSource audioSource;

    public void SetText(string message, Color color)
    {
        //JudmentAreaスクリプトのSpawnTextEffectで使用している
        effectText.text = message;      //messageに表示したい文字を入れることで参照先で表示される文字を変更することができる
        StartCoroutine(MoveUp());       //MoveUp()を使用する
        effectText.color = color;       //colorに表示したい色を入れることで参照先で表示される色を変更することができる

        //switch (message)              //もしswitch文でするときに切り替えれるように残している
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
