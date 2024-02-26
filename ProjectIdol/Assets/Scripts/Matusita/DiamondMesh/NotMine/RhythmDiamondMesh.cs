using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data.Common;

//岬さんのDiamondMeshスクリプトをそのままコピペしたもの


// 自動的にコンポーネントを追加 MeshFilter,MeshRendererを追加
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class RhythmDiamondMesh : MonoBehaviour
{
    public float[] vertexOffset = new float[4]; // 頂点のオフセット
    public float maxVerticalVertex = 3f; // 上と下の頂点の最大位置
    public float maxHorizontalVertex = 2f; // 右と左の頂点の最大位置
    public float defaultVerticalVertex = 0f; // 上と下の頂点の現在位置
    public float defaulttHorizontalVertex = 0f; // 右と左の頂点の現在位置
    public UiManager uiManager; // ScoreDirector変数  →SoreDirectorからUiMana
    float[] correctionRate = new float[4]; // 各スコアのグラフの上限補正率 [0,1,2,3]...[上,右,下,左] 
    bool isSetUp = false; // セットアップが出来たかどうか

    

    private void FixedUpdate()
    {
        if (isSetUp) // 真なら
        {
            CreateGraph(); // グラフを作成する
        }
    }
    public void SetUp() // ダイアモンド形のメッシュをセットアップする関数
    {
        CreateDiamond(defaultVerticalVertex, defaulttHorizontalVertex);
        Vector3[] vectices = GetComponent<MeshFilter>().mesh.vertices; // メッシュの頂点座標を取得
        string[] colorPoint = { uiManager.blueRank, uiManager.redRank, uiManager.whiteRank, uiManager.yellowRank };
        SetRankCorrectionRate(colorPoint, vectices);
        isSetUp = true; // CreateGraphを呼び出すためにtrueに変更
    }

    string[] src = { "Tokyo", "Osaka", "Nagoya" };

    void CreateGraph() // 各パラメータを上,右,下,左の順でグラフにする関数
    {
        Vector3[] vectices = GetComponent<MeshFilter>().mesh.vertices; // メッシュの頂点座標を取得

        // 頂点の位置を変更
        if (vectices[0].y <= maxVerticalVertex * correctionRate[0])
        {
            vectices[0].y += vertexOffset[0]; // 上
        }
        else if (vectices[1].x <= maxHorizontalVertex * correctionRate[1])
        {
            vectices[1].x += vertexOffset[1]; // 右
        }
        else if (vectices[2].y >= -maxVerticalVertex * correctionRate[2])
        {
            vectices[2].y -= vertexOffset[2]; // 下
        }
        else if (vectices[3].x >= -maxHorizontalVertex * correctionRate[3])
        {
            vectices[3].x -= vertexOffset[3]; // 左
        }

        // 変更した頂点座標をメッシュにセット
        GetComponent<MeshFilter>().mesh.vertices = vectices;
    }
    void SetRankCorrectionRate(string[] colorPoint, Vector3[] vectices) // グラフを伸ばす上限を決める関数
    {
        RankChange(colorPoint);

        // 各パラメータの上限を計算し、1fで座標移動させる値をvertexOffset配列に代入(60fで上限まで座標を移動させるように計算)
        vertexOffset[0] = ((maxVerticalVertex * correctionRate[0]) - defaultVerticalVertex) / 60f;
        vertexOffset[1] = ((maxHorizontalVertex * correctionRate[1]) - defaulttHorizontalVertex) / 60f;
        vertexOffset[2] = ((maxVerticalVertex * correctionRate[2]) - defaultVerticalVertex) / 60f;
        vertexOffset[3] = ((maxHorizontalVertex * correctionRate[3]) - defaulttHorizontalVertex) / 60f;
    }

    void RankChange(string[] colorPoint)
    {
        // 各パラメータのランクによって掛け率をrank配列に代入
        for (int i = 0; i < 4; i++)
        {
            switch (colorPoint[i])
            {
                case "D":
                    correctionRate[i] = 0.2f;
                    break;
                case "C":
                    correctionRate[i] = 0.4f;
                    break;
                case "B":
                    correctionRate[i] = 0.6f;
                    break;
                case "A":
                    correctionRate[i] = 0.8f;
                    break;
                case "S":
                    correctionRate[i] = 1f;
                    break;
            };
        }
    }


    void CreateDiamond(float verticalVertex, float horizontalVertex) // ダイアモンド形を生成する関数
    {
        Mesh mesh = new Mesh(); // メッシュインスタンスを生成
        GetComponent<MeshFilter>().mesh = mesh; // 生成したメッシュインスタンスを代入
        mesh.vertices = new Vector3[] // 頂点の座標を代入
        {
            new Vector3(0f, verticalVertex, 0f),   // 上
            new Vector3(horizontalVertex, 0f, 0f), // 右
            new Vector3(0f, -verticalVertex, 0f),  // 下
            new Vector3(-horizontalVertex, 0f, 0f) // 左
        };
        mesh.uv = new Vector2[] // UVを代入
            {
                new Vector2(0f, verticalVertex),    // 上
                new Vector2(horizontalVertex, 0f),  // 右
                new Vector2(0f, -verticalVertex),   // 下
                new Vector2( -horizontalVertex,0f)  // 左
            };
        mesh.triangles = new int[] { 0, 1, 2, 2, 3, 0 }; // 三角形の順序
        mesh.RecalculateNormals(); // 三角形と頂点からメッシュの法線を再計算
    }
}
