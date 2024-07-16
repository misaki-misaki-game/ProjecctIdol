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
    [SerializeField, Header("アイ玉02")] TextMeshProUGUI perfectCountsText;

    public int aidamaLevel;

    private void FixedUpdate()
    {
        AidamaLevel();
        AidamaIncrease();
        AidamaImgChange();
        //アイ玉02
        perfectCountsText.text=uiManager.perfectCounts.ToString();
    }

    /// <summary>
    /// アイ玉レベルに合わせて画面上に表示させる画像を変更する
    /// </summary>
    public void AidamaImgChange()
    {
        // すべてのGameObject配列を非表示にする
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
    /// 指定したGameObject配列のすべての要素を指定したアクティブ状態に設定する
    /// </summary>
    private void SetActiveAll(GameObject[] gameObjects, bool isActive)
    {
        foreach (GameObject obj in gameObjects)
        {
            obj.SetActive(isActive);
        }
    }

    /// <summary>
    /// アイ玉のレベルに合わせてスコアの上昇率を計算する
    /// </summary>
    public void AidamaIncrease()
    {
        switch (aidamaLevel)
        {
            case 0:
                //100%上昇
                break;
            case 1:
                //120%上昇
                break;
            case 2:
                //140%上昇
                break;
            case 3:
                //160%上昇
                break;
            case 4:
                //180%上昇
                break;
            case 5:
                //200%上昇
                break;
        }
    }

    /// <summary>
    /// パーフェクトの獲得数によってアイ玉のレベルを変更する
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
