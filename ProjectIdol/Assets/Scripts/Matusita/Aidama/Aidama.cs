using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using System.Drawing;
using UnityEngine.UI;

public class Aidama : MonoBehaviour
{
    [SerializeField] UiManager uiManager;
    [SerializeField] GameObject[] ai01_Levelbase;
    [SerializeField] GameObject[] ai01_Level01;
    [SerializeField] GameObject[] ai01_Level02;
    [SerializeField] GameObject[] ai01_Level03;
    [SerializeField] GameObject[] ai01_Level04;
    [SerializeField] GameObject[] ai01_LevelMax;
    [SerializeField, Header("�A�C��02")] TextMeshProUGUI perfectCountsText;

    public int aidamaLevel;

    private void FixedUpdate()
    {
        AidamaLevel();
        AidamaIncrease();
        AidamaImgChange();
        //�A�C��02
        perfectCountsText.text=uiManager.perfectCounts.ToString();
    }

    /// <summary>
    /// �A�C�ʃ��x���ɍ��킹�ĉ�ʏ�ɕ\��������摜��ύX����
    /// </summary>
    public void AidamaImgChange()
    {
        // ���ׂĂ�GameObject�z����\���ɂ���
        SetActiveAll(ai01_Levelbase, false);
        SetActiveAll(ai01_Level01, false);
        SetActiveAll(ai01_Level02, false);
        SetActiveAll(ai01_Level03, false);
        SetActiveAll(ai01_Level04, false);
        SetActiveAll(ai01_LevelMax, false);

        switch (aidamaLevel)
        {
            case 0:
                SetActiveAll(ai01_Levelbase, true);
                break;
            case 1:
                SetActiveAll(ai01_Level01, true);
                break;
            case 2:
                SetActiveAll(ai01_Level02, true);
                break;
            case 3:
                SetActiveAll(ai01_Level03, true);
                break;
            case 4:
                SetActiveAll(ai01_Level04, true);
                break;
            case 5:
                SetActiveAll(ai01_LevelMax, true);
                break;
        }
    }

    /// <summary>
    /// �w�肵��GameObject�z��̂��ׂĂ̗v�f���w�肵���A�N�e�B�u��Ԃɐݒ肷��
    /// </summary>
    private void SetActiveAll(GameObject[] gameObjects, bool isActive)
    {
        foreach (GameObject obj in gameObjects)
        {
            obj.SetActive(isActive);
        }
    }

    /// <summary>
    /// �A�C�ʂ̃��x���ɍ��킹�ăX�R�A�̏㏸�����v�Z����
    /// </summary>
    public void AidamaIncrease()
    {
        switch (aidamaLevel)
        {
            case 0:
                //100%�㏸
                break;
            case 1:
                //120%�㏸
                break;
            case 2:
                //140%�㏸
                break;
            case 3:
                //160%�㏸
                break;
            case 4:
                //180%�㏸
                break;
            case 5:
                //200%�㏸
                break;
        }
    }

    /// <summary>
    /// �p�[�t�F�N�g�̊l�����ɂ���ăA�C�ʂ̃��x����ύX����
    /// </summary>
    public void AidamaLevel()
    {
        if (uiManager.perfectCounts < 20)
        {
            aidamaLevel = 0;
        }
        else if (uiManager.perfectCounts < 50)
        {
            aidamaLevel = 1;
        }
        else if (uiManager.perfectCounts < 90)
        {
            aidamaLevel = 2;
        }
        else if (uiManager.perfectCounts < 120)
        {
            aidamaLevel = 3;
        }
        else if (uiManager.perfectCounts < 150)
        {
            aidamaLevel = 4;
        }
        else
        {
            aidamaLevel = 5;
        }
    }
}
