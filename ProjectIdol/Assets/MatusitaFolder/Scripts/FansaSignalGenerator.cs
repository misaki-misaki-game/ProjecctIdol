using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FansaSignalGenerator : MonoBehaviour
{
    public GameObject fansaSignalPrefab;
    public float spawnInterval = 17.0f;//17秒ごとに生成saseru 

    //拡大速度
    public float growthRate = 2;

    //最高サイズ
    public int maxScale = 12;


    void Start()
    {
        StartCoroutine(SpawnFanSignal());
    }

    void Update()
    {
        //オブジェクトのスケールを拡大する
        transform.localScale += new Vector3(growthRate, growthRate, growthRate) * Time.deltaTime;

        //オブジェクトが最大になったらデストロイ
        if (transform.localScale.x >= maxScale)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator SpawnFanSignal()
    {
        while(true)
        {
            //ファンサシグナルのインスタンスを生成する
            Instantiate(fansaSignalPrefab, transform.position, Quaternion.identity);

            //次の生成時間まで待機させる
            yield return new WaitForSeconds(spawnInterval);
        }
    }

}
