using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class DestroyArea : MonoBehaviour
{
    //プレイヤーが消せなかったシグナルを自動的に消して、ミス判定をするためのスクリプト

    [SerializeField] float radius;          //シグナルの判定の半径を設定する
    [SerializeField] UiManager uiManager;   //UIManagerを使うための変数

    private void Update()
    {
        RaycastHit2D[] hits2D = Physics2D.CircleCastAll(transform.position, radius, Vector3.zero);      //Rayを飛ばして判定する

        if (hits2D.Length == 0)
        {
            return;
        }

        RaycastHit2D hit2D = hits2D[0];
        if (hit2D)
        {
            //近さ表示  絶対値で考える Mathf.Abs()
            float distance = Mathf.Abs(hit2D.transform.position.y - transform.position.y);

            //ぶつかったものを破壊する
            Destroy(hit2D.collider.gameObject);
            hit2D.collider.gameObject.SetActive(false);

            uiManager.NoteMiss();   //UIManagerのNoteMissを使用してスコアに-25加算する
        }

    }

    //可視化ツール
    void OnDrawGizmosSelected()
    {
        //自動的に消す場所を青く表示させる
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, radius);
    }


}
