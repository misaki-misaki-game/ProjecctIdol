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


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            //Jキーが押された場合に
            StartCoroutine(OffToOn(pointTextJ));      //OffToOn()を使用して、一瞬表示を消す
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            //Kキーが押された場合に
            StartCoroutine(OffToOn(pointTextK));      //OffToOn()を使用して、一瞬表示を消す
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            //Lキーが押された場合に
            StartCoroutine(OffToOn(pointTextL));      //OffToOn()を使用して、一瞬表示を消す
        }
        if (Input.GetKeyDown(KeyCode.Equals))
        {
            //;(Equals)キーが押された場合に
            StartCoroutine(OffToOn(pointTextEquals)); //OffToOn()を使用して、一瞬表示を消す
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            //Fキーが押された場合に
            StartCoroutine(OffToOn(pointTextF));      //OffToOn()を使用して、一瞬表示を消す
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            //Dキーが押された場合に
            StartCoroutine(OffToOn(pointTextD));      //OffToOn()を使用して、一瞬表示を消す
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            //Sキーが押された場合に
            StartCoroutine(OffToOn(pointTextS));      //OffToOn()を使用して、一瞬表示を消す
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            //Aキーが押された場合に
            StartCoroutine(OffToOn(pointTextA));      //OffToOn()を使用して、一瞬表示を消す
        }
    }

    IEnumerator OffToOn(GameObject obj)
    {
        obj.SetActive(false);
        yield return new WaitForSeconds(delay);
        obj.SetActive(true);
    }
}