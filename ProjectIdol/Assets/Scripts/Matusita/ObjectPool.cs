using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    //オブジェクトプールとは？　（処理を軽くする） 
    //生成→表示　　　　に変更
    //破壊→非表示　　　に変更
    //あらかじめオブジェクトを複数生成してためておく必要がある:pool
    //使用する特に表示する       いらなくなったら非表示にする

    [SerializeField] GameObject prefabRed;
    [SerializeField] GameObject prefabYellow;
    [SerializeField] GameObject prefabBlue;
    [SerializeField] GameObject prefabWhite;
    List<GameObject> pool;

    public void CreatePool(int maxCount)
    {
        pool = new List<GameObject>();

        for (int i=0; i<maxCount; i++) 
        {
            //オブジェクト生成
            GameObject obj = Instantiate(prefabRed);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    //使用するときに場所を指定して表示する:poolの中から非表示のオブジェクトを探してくる
    public GameObject GetObj(Vector2 position)
    {
        //使ってないものを探してくる
        for(int i=0; i<pool.Count; i++)
        {
            if (pool[i].activeSelf == false)
            {
                GameObject obj = pool[i];
                obj.transform.position=position;
                obj.SetActive(true);
                return obj;
            }
        }

        //もしpoolの中の物を全部使っていたら新しく生成する
        GameObject newobj = Instantiate(prefabRed,position,Quaternion.identity);
        newobj.SetActive(false);
        pool.Add (newobj);
        return newobj;
    }

}
