using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// SkinnedMeshRenderer �e���_������
// MeshRenderer ���_�͓����Ȃ�

// �����I�ɃR���|�[�l���g��ǉ� MeshFilter,MeshRenderer��ǉ�
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Polygon : MonoBehaviour
{
    [SerializeField] private int vertexNum = 5; // ���_��

    private void Awake()
    {
        Mesh mesh = new Mesh(); // ���b�V���C���X�^���X�𐶐�
        GetComponent<MeshFilter>().mesh = mesh; // MeshFilter��mesh�ɐ�قǐ����������b�V���C���X�^���X����

        Vector3[] vertices = new Vector3[vertexNum + 1]; // ���_�̍��W�𒸓_��+1�A��������
        int[] triangles = new int[vertexNum * 3]; // �O�p�`�𒸓_�����A��������

        vertices[0] = Vector3.zero; // ���_[0]�̓I�u�W�F�N�g��(0,0,0)�̈ʒu�ɐݒ�
        float angleStep = Mathf.PI * 2.0f / vertexNum; // 2��/���_����
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