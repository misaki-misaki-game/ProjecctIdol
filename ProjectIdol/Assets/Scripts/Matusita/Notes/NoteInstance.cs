using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//using static UnityEditor.PlayerSettings;

public class NoteInstance : MonoBehaviour
{
    [SerializeField] GameObject bluePrefab;
    [SerializeField] GameObject redPrefab;
    [SerializeField] GameObject whitePrefab;
    [SerializeField] GameObject yellowPrefab;

    public void NoteEventCC()   //音階のC#がなった時にシグナルを生成させる
    {
        Debug.Log("CC");
        GameObject obj = SpawnPotision(yellowPrefab);
        AddAngle(obj);
    }
    public void NoteEventC()    //音階のCがなった時にシグナルを生成させる
    {
        Debug.Log("C");
        GameObject obj = SpawnPotision(yellowPrefab);
        AddAngle(obj);
    }
    public void NoteEventDD()   //音階のD#がなった時にシグナルを生成させる
    {
        Debug.Log("DD");
        GameObject obj = SpawnPotision(bluePrefab);
        AddAngle(obj);
    }
    public void NoteEventD()    //音階のDがなった時にシグナルを生成させる
    {
        Debug.Log("D");
        GameObject obj = SpawnPotision(yellowPrefab);
        AddAngle(obj);
    }
    public void NoteEventE()    //音階のEがなった時にシグナルを生成させる
    {
        Debug.Log("E");
        GameObject obj = SpawnPotision(yellowPrefab);
        AddAngle(obj);
    }
    public void NoteEventFF()   //音階のF#がなった時にシグナルを生成させる
    {
        Debug.Log("FF");
        GameObject obj = SpawnPotision(redPrefab);
        AddAngle(obj);
    }
    public void NoteEventF()    //音階のFがなった時にシグナルを生成させる
    {
        Debug.Log("F");
        GameObject obj = SpawnPotision(redPrefab);
        AddAngle(obj);
    }
    public void NoteEventGG()   //音階のG#がなった時にシグナルを生成させる
    {
        Debug.Log("GG");
        GameObject obj = SpawnPotision(redPrefab);
        AddAngle(obj);
    }
    public void NoteEventG()    //音階のGがなった時にシグナルを生成させる
    {
        Debug.Log("G");
        GameObject obj = SpawnPotision(bluePrefab);
        AddAngle(obj);
    }
    public void NoteEventA()    //音階のAがなった時にシグナルを生成させる
    {
        Debug.Log("A");
        GameObject obj = SpawnPotision(whitePrefab);
        AddAngle(obj);
    }
    public void NoteEventB()    //音階のBがなった時にシグナルを生成させる
    {
        Debug.Log("B");
        GameObject obj = SpawnPotision(whitePrefab);
        AddAngle(obj);
    }

    /// <summary>
    /// シグナルを生成位置に生成する
    /// </summary>
    /// <param name="spawnObj">生成させたいプレハブを入れる</param>
    public GameObject SpawnPotision(GameObject spawnObj)
    {
        Vector3 pos = new Vector3(0, -23, 0);
        return Instantiate(spawnObj, pos, Quaternion.identity);
    }

    /// <summary>
    /// プレハブに移動させるスクリプトを追加する
    /// </summary>
    /// <param name="anglesObj">SpawnPosition()で使用するシグナルプレハブを入れる</param>
    void AddAngle(GameObject anglesObj)
    {
        switch (Random.Range(1, 9))
        {
            case 1: //レーン１側にシグナルを移動させる
                anglesObj.AddComponent<North_NorthEast>();
                break;

            case 2: //レーン２側にシグナルを移動させる
                anglesObj.AddComponent<East_NorthEast>();
                break;

            case 3: //レーン３側にシグナルを移動させる
                anglesObj.AddComponent<East_SouthEast>();
                break;

            case 4: //レーン４側にシグナルを移動させる
                anglesObj.AddComponent<South_SouthEast>();
                break;

            case 5: //レーン５側にシグナルを移動させる
                anglesObj.AddComponent<North_NorthWest>();
                break;

            case 6: //レーン６側にシグナルを移動させる
                anglesObj.AddComponent<West_NorthWest>();
                break;

            case 7: //レーン７側にシグナルを移動させる
                anglesObj.AddComponent<West_SouthWest>();
                break;

            case 8: //レーン８側にシグナルを移動させる
                anglesObj.AddComponent<South_SouthWest>();
                break;

        }
    }

}
