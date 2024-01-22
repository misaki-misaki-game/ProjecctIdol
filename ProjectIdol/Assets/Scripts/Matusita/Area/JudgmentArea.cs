using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class JudgmentArea : MonoBehaviour
{
    [SerializeField] float radius;
    [SerializeField] KeyCode keyCode = default;

    [SerializeField] UiManager uiManager;

    private void Update()
    {
        if(Input.GetKeyDown(keyCode))
        {
            RaycastHit2D[] hits2D = Physics2D.CircleCastAll(transform.position, radius, Vector3.zero);

            //一番下のノーツを消す

            if (hits2D.Length == 0)
            {
                return;
            }

            RaycastHit2D hit2D = hits2D[0];
            if (hit2D)
            {
                //近さ表示  絶対値で考える Mathf.Abs()
                float distance = Mathf.Abs(hit2D.transform.position.y - transform.position.y);
                //判定　今のところない
                if (distance < 3)
                {
                    uiManager.AddScore(50);
                    uiManager.AddCombo();
                    uiManager.AddStar(1);
                }
                else if (distance < 5)
                {
                    uiManager.AddScore(50);
                    uiManager.AddCombo();
                    uiManager.AddStar(1);
                }
                else
                {
                    uiManager.NoteMiss();
                }


                //ぶつかったものを破壊する
                Destroy(hit2D.collider.gameObject);
                hit2D.collider.gameObject.SetActive(false);
            }
        }
    }

    //可視化ツール
    void OnDrawGizmosSelected()
    {
        //判定場所を赤く表示させる
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, radius);
    }
}
