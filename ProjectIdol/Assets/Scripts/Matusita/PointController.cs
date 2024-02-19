using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointController : MonoBehaviour
{
    [SerializeField] GameObject pointTextJ;         //北北東の方向にあるJテキスト
    [SerializeField] GameObject pointTextK;         //東北東の方向にあるKテキスト
    [SerializeField] GameObject pointTextL;         //東南東の方向にあるLテキスト
    [SerializeField] GameObject pointTextEquals;    //南南東の方向にある；(Equals)テキスト
    [SerializeField] GameObject pointTextF;         //北北西の方向にあるFテキスト
    [SerializeField] GameObject pointTextD;         //西北西の方向にあるDテキスト
    [SerializeField] GameObject pointTextS;         //西南西の方向にあるSテキスト
    [SerializeField] GameObject pointTextA;         //南南西の方向にあるAテキスト

    [SerializeField] float delay = 0.1f;            //オブジェクトを非アクティブにする時間

    private void Start()
    {
        pointTextJ      = transform.Find("Text_J").gameObject;          //子オブジェクトであるJテキストを見つけてくる
        pointTextK      = transform.Find("Text_K").gameObject;          //子オブジェクトであるKテキストを見つけてくる
        pointTextL      = transform.Find("Text_L").gameObject;          //子オブジェクトであるLテキストを見つけてくる
        pointTextEquals = transform.Find("Text_Equals").gameObject;     //子オブジェクトである;(Equals)テキストを見つけてくる
        pointTextF      = transform.Find("Text_F").gameObject;          //子オブジェクトであるFテキストを見つけてくる
        pointTextD      = transform.Find("Text_D").gameObject;          //子オブジェクトであるDテキストを見つけてくる
        pointTextS      = transform.Find("Text_S").gameObject;          //子オブジェクトであるSテキストを見つけてくる
        pointTextA      = transform.Find("Text_A").gameObject;          //子オブジェクトであるAテキストを見つけてくる
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            StartCoroutine(OffToOn_J());
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            StartCoroutine(OffToOn_K());
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            StartCoroutine(OffToOn_L());
        }
        if (Input.GetKeyDown(KeyCode.Equals))
        {
            StartCoroutine(OffToOn_Equals());
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(OffToOn_F());
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            StartCoroutine(OffToOn_D());
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine(OffToOn_S());
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(OffToOn_A());
        }
    }

    IEnumerator OffToOn_J()
    {
        //Jキーを押されたときに使用する
        pointTextJ.SetActive(false);                //Jテキストを非表示にする
        yield return new WaitForSeconds(delay);     //delayに設定した数値分だけ時間を空ける
        pointTextJ.SetActive(true);                 //Jテキストを表示させる
    }
    IEnumerator OffToOn_K()
    {
        //Kキーを押されたときに使用する
        pointTextK.SetActive(false);                //Kテキストを非表示にする
        yield return new WaitForSeconds(delay);     //delayに設定した数値分だけ時間を空ける
        pointTextK.SetActive(true);                 //Kテキストを表示させる
    }
    IEnumerator OffToOn_L()
    {
        //Lキーを押されたときに使用する
        pointTextL.SetActive(false);                //Lテキストを非表示にする
        yield return new WaitForSeconds(delay);     //delayに設定した数値分だけ時間を空ける
        pointTextL.SetActive(true);                 //Lテキストを表示させる
    }
    IEnumerator OffToOn_Equals()
    {
        //;(equals)キーを押されたときに使用する
        pointTextEquals.SetActive(false);           //;(Equals)テキストを非表示にする
        yield return new WaitForSeconds(delay);     //delayに設定した数値分だけ時間を空ける
        pointTextEquals.SetActive(true);            //;(Equals)テキストを表示させる
    }
    IEnumerator OffToOn_F()
    {
        //Fキーを押されたときに使用する
        pointTextF.SetActive(false);                //Fテキストを非表示にする
        yield return new WaitForSeconds(delay);     //delayに設定した数値分だけ時間を空ける
        pointTextF.SetActive(true);                 //Fテキストを表示させる
    }
    IEnumerator OffToOn_D()
    {
        //Dキーを押されたときに使用する
        pointTextD.SetActive(false);                //Dテキストを非表示にする
        yield return new WaitForSeconds(delay);     //delayに設定した数値分だけ時間を空ける
        pointTextD.SetActive(true);                 //Dテキストを表示させる
    }
    IEnumerator OffToOn_S()
    {
        //Sキーを押されたときに使用する   
        pointTextS.SetActive(false);                //Sテキストを非表示にする
        yield return new WaitForSeconds(delay);     //delayに設定した数値分だけ時間を空ける
        pointTextS.SetActive(true);                 //Sテキストを表示させる
    }
    IEnumerator OffToOn_A()
    {
        //Aキーを押されたときに使用する
        pointTextA.SetActive(false);                //Aテキストを非表示にする
        yield return new WaitForSeconds(delay);     //delayに設定した数値分だけ時間を空ける
        pointTextA.SetActive(true);                 //Aテキストを表示させる
    }
}