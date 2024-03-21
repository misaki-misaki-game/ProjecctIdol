using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data.Common;

//�������DiamondMesh�X�N���v�g�����̂܂܃R�s�y��������


// �����I�ɃR���|�[�l���g��ǉ� MeshFilter,MeshRenderer��ǉ�
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class RhythmDiamondMesh : MonoBehaviour
{
    public float[] vertexOffset = new float[4]; // ���_�̃I�t�Z�b�g
    public float maxVerticalVertex = 3f; // ��Ɖ��̒��_�̍ő�ʒu
    public float maxHorizontalVertex = 2f; // �E�ƍ��̒��_�̍ő�ʒu
    public float defaultVerticalVertex = 0f; // ��Ɖ��̒��_�̌��݈ʒu
    public float defaulttHorizontalVertex = 0f; // �E�ƍ��̒��_�̌��݈ʒu
    public UiManager uiManager; // ScoreDirector�ϐ�  ��SoreDirector����UiMana
    float[] correctionRate = new float[4]; // �e�X�R�A�̃O���t�̏���␳�� [0,1,2,3]...[��,�E,��,��] 
    bool isSetUp = false; // �Z�b�g�A�b�v���o�������ǂ���

    

    private void FixedUpdate()
    {
        if (isSetUp) // �^�Ȃ�
        {
            CreateGraph(); // �O���t���쐬����
        }
    }
    public void SetUp() // �_�C�A�����h�`�̃��b�V�����Z�b�g�A�b�v����֐�
    {
        CreateDiamond(defaultVerticalVertex, defaulttHorizontalVertex);
        Vector3[] vectices = GetComponent<MeshFilter>().mesh.vertices; // ���b�V���̒��_���W���擾
        string[] colorPoint = { uiManager.blueRank, uiManager.redRank, uiManager.whiteRank, uiManager.yellowRank };
        SetRankCorrectionRate(colorPoint, vectices);
        isSetUp = true; // CreateGraph���Ăяo�����߂�true�ɕύX
    }

    string[] src = { "Tokyo", "Osaka", "Nagoya" };

    void CreateGraph() // �e�p�����[�^����,�E,��,���̏��ŃO���t�ɂ���֐�
    {
        Vector3[] vectices = GetComponent<MeshFilter>().mesh.vertices; // ���b�V���̒��_���W���擾

        // ���_�̈ʒu��ύX
        if (vectices[0].y <= maxVerticalVertex * correctionRate[0])
        {
            vectices[0].y += vertexOffset[0]; // ��
        }
        else if (vectices[1].x <= maxHorizontalVertex * correctionRate[1])
        {
            vectices[1].x += vertexOffset[1]; // �E
        }
        else if (vectices[2].y >= -maxVerticalVertex * correctionRate[2])
        {
            vectices[2].y -= vertexOffset[2]; // ��
        }
        else if (vectices[3].x >= -maxHorizontalVertex * correctionRate[3])
        {
            vectices[3].x -= vertexOffset[3]; // ��
        }

        // �ύX�������_���W�����b�V���ɃZ�b�g
        GetComponent<MeshFilter>().mesh.vertices = vectices;
    }
    void SetRankCorrectionRate(string[] colorPoint, Vector3[] vectices) // �O���t��L�΂���������߂�֐�
    {
        RankChange(colorPoint);

        // �e�p�����[�^�̏�����v�Z���A1f�ō��W�ړ�������l��vertexOffset�z��ɑ��(60f�ŏ���܂ō��W���ړ�������悤�Ɍv�Z)
        vertexOffset[0] = ((maxVerticalVertex * correctionRate[0]) - defaultVerticalVertex) / 60f;
        vertexOffset[1] = ((maxHorizontalVertex * correctionRate[1]) - defaulttHorizontalVertex) / 60f;
        vertexOffset[2] = ((maxVerticalVertex * correctionRate[2]) - defaultVerticalVertex) / 60f;
        vertexOffset[3] = ((maxHorizontalVertex * correctionRate[3]) - defaulttHorizontalVertex) / 60f;
    }

    void RankChange(string[] colorPoint)
    {
        // �e�p�����[�^�̃����N�ɂ���Ċ|������rank�z��ɑ��
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


    void CreateDiamond(float verticalVertex, float horizontalVertex) // �_�C�A�����h�`�𐶐�����֐�
    {
        Mesh mesh = new Mesh(); // ���b�V���C���X�^���X�𐶐�
        GetComponent<MeshFilter>().mesh = mesh; // �����������b�V���C���X�^���X����
        mesh.vertices = new Vector3[] // ���_�̍��W����
        {
            new Vector3(0f, verticalVertex, 0f),   // ��
            new Vector3(horizontalVertex, 0f, 0f), // �E
            new Vector3(0f, -verticalVertex, 0f),  // ��
            new Vector3(-horizontalVertex, 0f, 0f) // ��
        };
        mesh.uv = new Vector2[] // UV����
            {
                new Vector2(0f, verticalVertex),    // ��
                new Vector2(horizontalVertex, 0f),  // �E
                new Vector2(0f, -verticalVertex),   // ��
                new Vector2( -horizontalVertex,0f)  // ��
            };
        mesh.triangles = new int[] { 0, 1, 2, 2, 3, 0 }; // �O�p�`�̏���
        mesh.RecalculateNormals(); // �O�p�`�ƒ��_���烁�b�V���̖@�����Čv�Z
    }
}
