using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FansaSignalGenerator : MonoBehaviour
{
    public GameObject fansaSignalPrefab;
    public float spawnInterval = 17.0f;//17�b���Ƃɐ���saseru 

    //�g�呬�x
    public float growthRate = 2;

    //�ō��T�C�Y
    public int maxScale = 12;


    void Start()
    {
        StartCoroutine(SpawnFanSignal());
    }

    void Update()
    {
        //�I�u�W�F�N�g�̃X�P�[�����g�傷��
        transform.localScale += new Vector3(growthRate, growthRate, growthRate) * Time.deltaTime;

        //�I�u�W�F�N�g���ő�ɂȂ�����f�X�g���C
        if (transform.localScale.x >= maxScale)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator SpawnFanSignal()
    {
        while(true)
        {
            //�t�@���T�V�O�i���̃C���X�^���X�𐶐�����
            Instantiate(fansaSignalPrefab, transform.position, Quaternion.identity);

            //���̐������Ԃ܂őҋ@������
            yield return new WaitForSeconds(spawnInterval);
        }
    }

}
