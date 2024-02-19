using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FansaSignal : MonoBehaviour
{
    [SerializeField] GameObject fansaSignalPrefab;                      //�t�@���T�V�O�i���̃v���n�u
    [SerializeField] float spawnInterval = 17.0f;                       //�t�@���T�V�O�i���̐����Ԋu

    [SerializeField] float growthRate = 2;                              //�t�@���T�V�O�i���̐�����
    [SerializeField] int maxScale = 13;                                 //�t�@���T�V�O�i���̍ő�X�P�[��

    [SerializeField] GameObject miss_Text;                              //�t�@���T�V�O�i����j�󂷂�Ƃ��Ɏ��s�����Ƃ��Ɏg���~�X�e�L�X�g
    [SerializeField] Animator MissAnimation;                            //�t�@���T�V�O�i����j�󂷂�Ƃ��Ɏ��s�����Ƃ��Ɏg���A�j���[�V����

    private List<GameObject> fansaSignals = new List<GameObject>();     //�������ꂽ�t�@���T�V�O�i���̃��X�g

    void Start()
    {
        StartCoroutine(SpawnFanSignal());       //�t�@���T�V�O�i���̐����R���[�`�����J�n����
    }

    void Update()
    {
        //W�L�[���������ꍇ
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("W");
            //�������ꂽ�t�@���T�V�O�i���̃��X�g�𔽕�����������
            for (int i = fansaSignals.Count - 1; i >= 0; i--)
            {
                Debug.Log("For��");
                GameObject fansaSignal = fansaSignals[i];

                //�X�P�[����12�ȉ��̏ꍇ�t�@���T�V�O�i����j�󂷂�
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
        //�t�@���T�V�O�i���𐶐����邽�߂̃R���[�`��
        while (true)
        {
            //�t�@���T�V�O�i���𐶐����A���X�g�ɒǉ�����
            GameObject newSignal = Instantiate(fansaSignalPrefab, transform.position, Quaternion.identity);
            fansaSignals.Add(newSignal);
            Debug.Log("Signal Spawned: " + fansaSignals.Count);

            StartCoroutine(ScaleObject(newSignal));                 //�����������J�n����

            yield return new WaitForSeconds(spawnInterval);         //���̃t�@���T�V�O�i���̐����܂őҋ@���Ă��炤
        }
    }

    IEnumerator ScaleObject(GameObject target)
    {
        //�t�@���T�V�O�i���̐����������s�����߂̃R���[�`��
        while (true)
        {
            //target���܂����݂��邩�ǂ������m�F����
            if (target == null)
            {
                yield break;
            }

            //�t�@���T�V�O�i���𐬒�������
            target.transform.localScale += new Vector3(growthRate, growthRate, growthRate) * Time.deltaTime;

            //�t�@���T�V�O�i�����ő�X�P�[���ɂȂ����ꍇ�~�X�ɂȂ邽�ߔj�󂷂�
            if (target.transform.localScale.x >= maxScale)
            {
                if (miss_Text != null && MissAnimation != null)
                {
                    miss_Text.SetActive(true);                              //�~�X�e�L�X�g�𐶐�����
                    MissAnimation.Play("Miss_Anim");                        //�~�X�A�j���[�V�����𐶐�����
                    Debug.Log("MISS!!");
                    miss_Text.SetActive(false);                             //�~�X�e�L�X�g��j�󂷂�
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