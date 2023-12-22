using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FansaSignal : MonoBehaviour
{
    public GameObject fansaSignalPrefab;
    public float spawnInterval = 17.0f;

    public float growthRate = 2;
    public int maxScale = 13;

    public GameObject miss_Text;
    public Animator MissAnimation;

    private List<GameObject> fansaSignals = new List<GameObject>();

    void Start()
    {
        StartCoroutine(SpawnFanSignal());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("W");
            // �������ꂽ�t�@���T�V�O�i���̃��X�g�𔽕��������܂��B
            for (int i = fansaSignals.Count - 1; i >= 0; i--)
            {
                Debug.Log("For��");
                GameObject fansaSignal = fansaSignals[i];

                // �X�P�[����12�ȉ��̃t�@���T�V�O�i����j�󂵂܂��B
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
        while (true)
        {
            GameObject newSignal = Instantiate(fansaSignalPrefab, transform.position, Quaternion.identity);
            fansaSignals.Add(newSignal);
            Debug.Log("Signal Spawned: " + fansaSignals.Count);
            StartCoroutine(ScaleObject(newSignal));
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    IEnumerator ScaleObject(GameObject target)
    {
        while (true)
        {
            // target���܂����݂��邩�ǂ������m�F���܂��B
            if (target == null)
            {
                yield break;
            }

            target.transform.localScale += new Vector3(growthRate, growthRate, growthRate) * Time.deltaTime;

            if (target.transform.localScale.x >= maxScale)
            {
                if (miss_Text != null && MissAnimation != null)
                {
                    miss_Text.SetActive(true);
                    MissAnimation.Play("Miss_Anim");
                    Debug.Log("MISS!!");
                    miss_Text.SetActive(false);
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