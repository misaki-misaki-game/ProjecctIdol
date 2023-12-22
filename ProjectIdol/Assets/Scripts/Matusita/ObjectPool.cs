using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    //�I�u�W�F�N�g�v�[���Ƃ́H�@�i�������y������j 
    //�������\���@�@�@�@�ɕύX
    //�j�󁨔�\���@�@�@�ɕύX
    //���炩���߃I�u�W�F�N�g�𕡐��������Ă��߂Ă����K�v������:pool
    //�g�p������ɕ\������       ����Ȃ��Ȃ������\���ɂ���

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
            //�I�u�W�F�N�g����
            GameObject obj = Instantiate(prefabRed);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    //�g�p����Ƃ��ɏꏊ���w�肵�ĕ\������:pool�̒������\���̃I�u�W�F�N�g��T���Ă���
    public GameObject GetObj(Vector2 position)
    {
        //�g���ĂȂ����̂�T���Ă���
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

        //����pool�̒��̕���S���g���Ă�����V������������
        GameObject newobj = Instantiate(prefabRed,position,Quaternion.identity);
        newobj.SetActive(false);
        pool.Add (newobj);
        return newobj;
    }

}
