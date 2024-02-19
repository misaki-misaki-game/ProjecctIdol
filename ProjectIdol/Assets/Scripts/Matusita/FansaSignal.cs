using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FansaSignal : MonoBehaviour
{
    [SerializeField] GameObject fansaSignalPrefab;                      //ファンサシグナルのプレハブ
    [SerializeField] float spawnInterval = 17.0f;                       //ファンサシグナルの生成間隔

    [SerializeField] float growthRate = 2;                              //ファンサシグナルの成長率
    [SerializeField] int maxScale = 13;                                 //ファンサシグナルの最大スケール

    [SerializeField] GameObject miss_Text;                              //ファンサシグナルを破壊するときに失敗したときに使うミステキスト
    [SerializeField] Animator MissAnimation;                            //ファンサシグナルを破壊するときに失敗したときに使うアニメーション

    private List<GameObject> fansaSignals = new List<GameObject>();     //生成されたファンサシグナルのリスト

    void Start()
    {
        StartCoroutine(SpawnFanSignal());       //ファンサシグナルの生成コルーチンを開始する
    }

    void Update()
    {
        //Wキーを押した場合
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("W");
            //生成されたファンサシグナルのリストを反復処理させる
            for (int i = fansaSignals.Count - 1; i >= 0; i--)
            {
                Debug.Log("For文");
                GameObject fansaSignal = fansaSignals[i];

                //スケールが12以下の場合ファンサシグナルを破壊する
                if (fansaSignal.transform.localScale.x < 12)
                {
                    Debug.Log("Destroy");
                    Destroy(fansaSignal);
                    fansaSignals.RemoveAt(i);
                }
            }
        }
    }

    IEnumerator SpawnFanSignal()
    {
        //ファンサシグナルを生成するためのコルーチン
        while (true)
        {
            //ファンサシグナルを生成し、リストに追加する
            GameObject newSignal = Instantiate(fansaSignalPrefab, transform.position, Quaternion.identity);
            fansaSignals.Add(newSignal);
            Debug.Log("Signal Spawned: " + fansaSignals.Count);

            StartCoroutine(ScaleObject(newSignal));                 //成長処理を開始する

            yield return new WaitForSeconds(spawnInterval);         //次のファンサシグナルの生成まで待機してもらう
        }
    }

    IEnumerator ScaleObject(GameObject target)
    {
        //ファンサシグナルの成長処理を行うためのコルーチン
        while (true)
        {
            //targetがまだ存在するかどうかを確認する
            if (target == null)
            {
                yield break;
            }

            //ファンサシグナルを成長させる
            target.transform.localScale += new Vector3(growthRate, growthRate, growthRate) * Time.deltaTime;

            //ファンサシグナルが最大スケールになった場合ミスになるため破壊する
            if (target.transform.localScale.x >= maxScale)
            {
                if (miss_Text != null && MissAnimation != null)
                {
                    miss_Text.SetActive(true);                              //ミステキストを生成する
                    MissAnimation.Play("Miss_Anim");                        //ミスアニメーションを生成する
                    Debug.Log("MISS!!");
                    miss_Text.SetActive(false);                             //ミステキストを破壊する
                }
                fansaSignals.Remove(target);
                Destroy(target);
                Debug.Log("Signal Destroyed: " + fansaSignals.Count);
                break;
            }

            yield return null;
        }
    }
}