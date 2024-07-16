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

    public void NoteEventCC()   //���K��C#���Ȃ������ɃV�O�i���𐶐�������
    {
        Debug.Log("CC");
        GameObject obj = SpawnPotision(yellowPrefab);
        AddAngle(obj);
    }
    public void NoteEventC()    //���K��C���Ȃ������ɃV�O�i���𐶐�������
    {
        Debug.Log("C");
        GameObject obj = SpawnPotision(yellowPrefab);
        AddAngle(obj);
    }
    public void NoteEventDD()   //���K��D#���Ȃ������ɃV�O�i���𐶐�������
    {
        Debug.Log("DD");
        GameObject obj = SpawnPotision(bluePrefab);
        AddAngle(obj);
    }
    public void NoteEventD()    //���K��D���Ȃ������ɃV�O�i���𐶐�������
    {
        Debug.Log("D");
        GameObject obj = SpawnPotision(yellowPrefab);
        AddAngle(obj);
    }
    public void NoteEventE()    //���K��E���Ȃ������ɃV�O�i���𐶐�������
    {
        Debug.Log("E");
        GameObject obj = SpawnPotision(yellowPrefab);
        AddAngle(obj);
    }
    public void NoteEventFF()   //���K��F#���Ȃ������ɃV�O�i���𐶐�������
    {
        Debug.Log("FF");
        GameObject obj = SpawnPotision(redPrefab);
        AddAngle(obj);
    }
    public void NoteEventF()    //���K��F���Ȃ������ɃV�O�i���𐶐�������
    {
        Debug.Log("F");
        GameObject obj = SpawnPotision(redPrefab);
        AddAngle(obj);
    }
    public void NoteEventGG()   //���K��G#���Ȃ������ɃV�O�i���𐶐�������
    {
        Debug.Log("GG");
        GameObject obj = SpawnPotision(redPrefab);
        AddAngle(obj);
    }
    public void NoteEventG()    //���K��G���Ȃ������ɃV�O�i���𐶐�������
    {
        Debug.Log("G");
        GameObject obj = SpawnPotision(bluePrefab);
        AddAngle(obj);
    }
    public void NoteEventA()    //���K��A���Ȃ������ɃV�O�i���𐶐�������
    {
        Debug.Log("A");
        GameObject obj = SpawnPotision(whitePrefab);
        AddAngle(obj);
    }
    public void NoteEventB()    //���K��B���Ȃ������ɃV�O�i���𐶐�������
    {
        Debug.Log("B");
        GameObject obj = SpawnPotision(whitePrefab);
        AddAngle(obj);
    }

    /// <summary>
    /// �V�O�i���𐶐��ʒu�ɐ�������
    /// </summary>
    /// <param name="spawnObj">�������������v���n�u������</param>
    public GameObject SpawnPotision(GameObject spawnObj)
    {
        Vector3 pos = new Vector3(0, -23, 0);
        return Instantiate(spawnObj, pos, Quaternion.identity);
    }

    /// <summary>
    /// �v���n�u�Ɉړ�������X�N���v�g��ǉ�����
    /// </summary>
    /// <param name="anglesObj">SpawnPosition()�Ŏg�p����V�O�i���v���n�u������</param>
    void AddAngle(GameObject anglesObj)
    {
        switch (Random.Range(1, 9))
        {
            case 1: //���[���P���ɃV�O�i�����ړ�������
                anglesObj.AddComponent<North_NorthEast>();
                break;

            case 2: //���[���Q���ɃV�O�i�����ړ�������
                anglesObj.AddComponent<East_NorthEast>();
                break;

            case 3: //���[���R���ɃV�O�i�����ړ�������
                anglesObj.AddComponent<East_SouthEast>();
                break;

            case 4: //���[���S���ɃV�O�i�����ړ�������
                anglesObj.AddComponent<South_SouthEast>();
                break;

            case 5: //���[���T���ɃV�O�i�����ړ�������
                anglesObj.AddComponent<North_NorthWest>();
                break;

            case 6: //���[���U���ɃV�O�i�����ړ�������
                anglesObj.AddComponent<West_NorthWest>();
                break;

            case 7: //���[���V���ɃV�O�i�����ړ�������
                anglesObj.AddComponent<West_SouthWest>();
                break;

            case 8: //���[���W���ɃV�O�i�����ړ�������
                anglesObj.AddComponent<South_SouthWest>();
                break;

        }
    }

}
