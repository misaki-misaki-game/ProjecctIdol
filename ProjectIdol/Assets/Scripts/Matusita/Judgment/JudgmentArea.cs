using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class JudgmentArea : MonoBehaviour
{
    [SerializeField] float radius;                      //シグナルの判定の半径を設定する

    [SerializeField] KeyCode keyCode;                   //Unity上で設定したKeyCodeを使用する

    [SerializeField] UiManager uiManager;               //UIManagerを使うための変数
    [SerializeField] GameObject textEffectPrefab;       //シグナルを消せたときに表示させたいテキストを設定するためのゲームオブジェクト

    private void Update()
    {
        if (Input.GetKeyDown(keyCode))
        {
            //Unity上で設定されたKeyCodeが押された場合SignalJudgment()が実行される
            SignalJudgment();
        }
    }

    void SignalJudgment()
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

            if (distance < 4)
            {
                //もしシグナルを消したのが半径4以下なら
                uiManager.AddScore(50);                                                     //UIManagerのAddScoreを使用してスコアに50加算する
                uiManager.AddCombo();                                                       //UIManagerのAddComboを使用してコンボに1加算する
                SpawnTextEffect("Parfect", hit2D.transform.position, Color.yellow);         //シグナルを消した場所に黄色でParfectと表示する
            }
            else if (distance < 7)
            {
                //もしシグナルを消したのが半径7以下なら
                uiManager.AddScore(25);                                                     //UIManagerのAddScoreを使用してスコアに25加算する
                uiManager.AddCombo();                                                       //UIManagerのAddComboを使用してコンボに1加算する
                SpawnTextEffect("Nomal", hit2D.transform.position, Color.red);              //シグナルを消した場所に赤色でNomalと表示する
            }
            else
            {
                //もしシグナルを消したのがそれ以外なら
                uiManager.NoteMiss();                                                       //UIManagerのNoteMissを使用してスコアに-25加算する
                SpawnTextEffect("Miss", hit2D.transform.position, Color.blue);              //シグナルを消した場所に青色でMissと表示する
            }

            //ぶつかったものを破壊する
            Destroy(hit2D.collider.gameObject);
            hit2D.collider.gameObject.SetActive(false);
        }

    }

    void SpawnTextEffect(string message, Vector3 position, Color color)
    {
        //判定のエフェクトを生成する
        GameObject effect = Instantiate(textEffectPrefab, position + Vector3.up * 1.5f, Quaternion.identity);
        JudgmentEffect judgmentEffect = effect.GetComponent<JudgmentEffect>();
        judgmentEffect.SetText(message, color);
    }

    //可視化ツール
    void OnDrawGizmosSelected()
    {
        //判定場所を赤く表示させる
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, radius);
    }
}
