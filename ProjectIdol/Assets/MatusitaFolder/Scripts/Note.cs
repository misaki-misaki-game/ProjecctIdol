using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public GameObject prefabBlue;
    public GameObject prefabRed;
    public GameObject prefabWhite;
    public GameObject prefabYellow;

    void Update()
    {
        //30�t���[�����ƂɃV�[���Ƀv���n�u����
        if (Time.frameCount % 240 == 0)
        {
            int randNumber = Random.Range(0, 4);

            if (randNumber == 0)
            {
                // �����ʒu
                Vector3 pos = new Vector3(0, -40, 0);
                // �v���n�u���w��ʒu�ɐ���
                Instantiate(prefabBlue, pos, Quaternion.identity);
            }
            if (randNumber == 1)
            {
                Vector3 pos = new Vector3(0, -40, 0);
                Instantiate(prefabRed, pos, Quaternion.identity);
            }
            if (randNumber == 2)
            {
                Vector3 pos = new Vector3(0, -40, 0);
                Instantiate(prefabWhite, pos, Quaternion.identity);
            }
            if (randNumber == 3)
            {
                Vector3 pos = new Vector3(0, -40, 0);
                Instantiate(prefabYellow, pos, Quaternion.identity);
            }
        }
    }
}
