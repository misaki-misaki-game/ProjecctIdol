using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class DestroyArea : MonoBehaviour
{
    [SerializeField] float radius;
    [SerializeField] UiManager uiManager;

    private void Update()
    {
        RaycastHit2D[] hits2D = Physics2D.CircleCastAll(transform.position, radius, Vector3.zero);

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

            uiManager.NoteMiss();
        }

    }

    //可視化ツール
    void OnDrawGizmosSelected()
    {
        //判定場所を青く表示させる
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, radius);
    }


}
