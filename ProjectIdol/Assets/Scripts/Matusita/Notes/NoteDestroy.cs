using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteDestroy : MonoBehaviour
{
    //JudgmentArea judgmentArea;
    //MonoBehaviour
    //private UiManager uiManagerCS;
    //public void Start()
    //{
    //    uiManagerCS = GetComponent<UiManager>();
    //}

    //[SerializeField] GameObject textEffectPrefab;
    //float radius = 8;

    //public void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

    //        if (hit.collider != null)
    //        {
    //            Debug.Log("a");
    //            if (hit.collider.gameObject == gameObject)
    //            {
    //                SignalJudgment();
    //            }
    //        }
    //    }
    //}


    //public void SignalJudgment()
    //{
    //    RaycastHit2D[] hits2D = Physics2D.CircleCastAll(transform.position, radius, Vector3.zero);

    //    if (hits2D.Length == 0)
    //    {
    //        return;
    //    }

    //    RaycastHit2D hit2D = hits2D[0];
    //    if (hit2D)
    //    {
    //        //近さ表示  絶対値で考える Mathf.Abs()
    //        float distance = Mathf.Abs(hit2D.transform.position.y - transform.position.y);

    //        if (distance < 4)
    //        {
    //            //uiManagerCS.AddScore(3000);                                                                               //UIManagerのAddScoreを使用してスコアに50加算する
    //            //uiManagerCS.AddCombo();                                                                                   //UIManagerのAddComboを使用してコンボに1加算する
    //            //SpawnTextEffect("Parfect", hit2D.transform.position, Color.yellow, perfectEffectPrefab, perfectSE);     //シグナルを消した場所に黄色でParfectと表示し、Parfect専用のエフェクトを表示する
    //            //SpawnTextEffect("Parfect", hit2D.transform.position, Color.yellow);     //シグナルを消した場所に黄色でParfectと表示し、Parfect専用のエフェクトを表示する

    //            switch (hit2D.collider.gameObject.tag)
    //            {
    //                //ぶつかったオブジェクトのタグに応じてポイントを加算
    //                case "BlueNotes":
    //                    //uiManagerCS.AddBluePoint(2);      //ブルーシグナルの場合、2ポイント加算
    //                    break;
    //                case "RedNotes":
    //                    //uiManagerCS.AddRedPoint(2);       //レッドシグナルの場合、2ポイント加算
    //                    break;
    //                case "WhiteNotes":
    //                    //uiManagerCS.AddWhitePoint(2);     //ホワイトシグナルの場合、2ポイント加算
    //                    break;
    //                case "YellowNotes":
    //                    //uiManagerCS.AddYellowPoint(2);    //イエローシグナルの場合、2ポイント加算
    //                    break;
    //                default:
    //                    break;
    //            }
    //        }
    //        else if (distance < 7)
    //        {
    //            //uiManagerCS.AddScore(1500);                                                                               //UIManagerのAddScoreを使用してスコアに25加算する
    //            //uiManagerCS.AddCombo();                                                                                   //UIManagerのAddComboを使用してコンボに1加算する
    //            //SpawnTextEffect("Nomal", hit2D.transform.position, Color.red, nomalEffectPrefab, nomalSE);              //シグナルを消した場所に赤色でNomalと表示し、Nomal専用のエフェクトを表示する
    //            //SpawnTextEffect("Nomal", hit2D.transform.position, Color.red);              //シグナルを消した場所に赤色でNomalと表示し、Nomal専用のエフェクトを表示する

    //            switch (hit2D.collider.gameObject.tag)
    //            {
    //                //ぶつかったオブジェクトのタグに応じてポイントを加算
    //                case "BlueNotes":
    //                    //uiManagerCS.AddBluePoint(1);      //ブルーシグナルの場合、1ポイント加算
    //                    break;
    //                case "RedNotes":
    //                    //uiManagerCS.AddRedPoint(1);       //レッドシグナルの場合、1ポイント加算
    //                    break;
    //                case "WhiteNotes":
    //                    //uiManagerCS.AddWhitePoint(1);     //ホワイトシグナルの場合、1ポイント加算
    //                    break;
    //                case "YellowNotes":
    //                    //uiManagerCS.AddYellowPoint(1);    //イエローシグナルの場合、1ポイント加算
    //                    break;
    //                default:
    //                    break;
    //            }
    //        }
    //        else if (distance < 10)
    //        {
    //            //uiManagerCS.NoteMiss();                                                                                   //UIManagerのNoteMissを使用してスコアに-25加算する
    //            //SpawnTextEffect("Miss", hit2D.transform.position, Color.blue, null, null);                              //シグナルを消した場所に青色でMissと表示する、エフェクト、SEは無し
    //            //SpawnTextEffect("Miss", hit2D.transform.position, Color.blue);                              //シグナルを消した場所に青色でMissと表示する、エフェクト、SEは無し
    //        }

    //        //ぶつかったものを破壊する
    //        Destroy(hit2D.collider.gameObject);
    //        hit2D.collider.gameObject.SetActive(false);
    //    }
    //}

    ////public void SpawnTextEffect(string message, Vector3 position, Color color, GameObject effectPrefab, AudioClip se)
    //public void SpawnTextEffect(string message, Vector3 position, Color color)
    //{
    //    //シグナルを消した場所に判定のエフェクトを生成する  判定によって表示するテキストと色を設定する
    //    GameObject effect = Instantiate(textEffectPrefab, position + Vector3.up * 1.5f, Quaternion.identity);
    //    JudgmentEffect judgmentEffect = effect.GetComponent<JudgmentEffect>();
    //    judgmentEffect.SetText(message, color);

    //    //if (effectPrefab != null && se != null)
    //    //{
    //    //    //PerfectエフェクトとSE、NomalエフェクトとSEを表示する
    //    //    GameObject effectObject = Instantiate(effectPrefab, position, Quaternion.identity);
    //    //    AudioSource.PlayClipAtPoint(se, position);
    //    //    Destroy(effectObject, 1.0f);
    //    //}

    //}

    //カメラが見えなくなったら
        void OnBecameInvisible()
        {
            GameObject.Destroy(this.gameObject);
        }
    
}


