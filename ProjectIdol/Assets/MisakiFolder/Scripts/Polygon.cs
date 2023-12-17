using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// SkinnedMeshRenderer 各頂点が動く
// MeshRenderer 頂点は動かない

// 自動的にコンポーネントを追加 MeshFilter,MeshRendererを追加
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Polygon : MonoBehaviour
{
    [SerializeField] private int vertexNum = 5; // 頂点数

    private void Awake()
    {
        Mesh mesh = new Mesh(); // メッシュインスタンスを生成
        GetComponent<MeshFilter>().mesh = mesh; // MeshFilterのmeshに先ほど生成したメッシュインスタンスを代入

        Vector3[] vertices = new Vector3[vertexNum + 1]; // 頂点の座標を頂点数+1、生成する
        int[] triangles = new int[vertexNum * 3]; // 三角形を頂点数分、生成する

        vertices[0] = Vector3.zero; // 頂点[0]はオブジェクトの(0,0,0)の位置に設定
        float angleStep = Mathf.PI * 2.0f / vertexNum; // 2π/頂点数で
        for (int i = 0; i < vertexNum; i++)
        {
            vertices[i + 1] = new Vector3(Mathf.Sin(i * angleStep), Mathf.Cos(i * angleStep), 0);
        }
        for (int i = 0; i < vertexNum; i++)
        {
            triangles[i * 3] = 0;
            triangles[i * 3 + 1] = i + 1;
            triangles[i * 3 + 2] = i + 2 != vertexNum + 1 ? i + 2 : 1;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
}