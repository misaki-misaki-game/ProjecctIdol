using UnityEngine;

// �����I�ɃR���|�[�l���g��ǉ� MeshFilter,MeshRenderer��ǉ�
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public partial class DiamondMesh : MonoBehaviour
{
    /// --------�֐��ꗗ-------- ///
    /// -------public�֐�------- ///

    /// <summary>
    /// �_�C�A�����h�`�̃��b�V�����Z�b�g�A�b�v����֐�
    /// </summary>
    public void SetUp()
    {
        CreateDiamond(defaultVerticalVertex, defaulttHorizontalVertex); // �_�C�������h�^�̐���
        Vector3[] vectices = GetComponent<MeshFilter>().mesh.vertices; // ���b�V���̒��_���W���擾
        string[] rank = scoreDirector.rank; // ScoreDirector��rank����
        SetRankCorrectionRate(rank, vectices); // �O���t��L�΂���������߂�
        isSetUp = true; // CreateGraph���Ăяo�����߂�true�ɕύX
    }

    /// -------public�֐�------- ///
    /// -----protected�֐�------ ///



    /// -----protected�֐�------ ///
    /// ------private�֐�------- ///

    private void FixedUpdate()
    {
        if (isSetUp) // �^�Ȃ�
        {
            CreateGraph(); // �O���t���쐬����
        }
    }

    /// <summary>
    /// �e�p�����[�^����,�E,��,���̏��ŃO���t�ɂ���֐�
    /// </summary>
    private void CreateGraph()
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

    /// <summary>
    /// �O���t��L�΂���������߂�֐�
    /// </summary>
    /// <param name="rank">�e���Ƀp�����[�^�̃����N</param>
    /// <param name="vectices"></param>
    private void SetRankCorrectionRate(string[] rank, Vector3[] vectices)
    {
        // �e�p�����[�^�̃����N�ɂ���Ċ|������rank�z��ɑ��
        for (int i = 0; i < 4; i++)
        {
            switch (rank[i])
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
        // �e�p�����[�^�̏�����v�Z���A1f�ō��W�ړ�������l��vertexOffset�z��ɑ��(60f�ŏ���܂ō��W���ړ�������悤�Ɍv�Z)
        vertexOffset[0] = ((maxVerticalVertex * correctionRate[0]) - defaultVerticalVertex) / 60f;
        vertexOffset[1] = ((maxHorizontalVertex * correctionRate[1]) - defaulttHorizontalVertex) / 60f;
        vertexOffset[2] = ((maxVerticalVertex * correctionRate[2]) - defaultVerticalVertex) / 60f;
        vertexOffset[3] = ((maxHorizontalVertex * correctionRate[3]) - defaulttHorizontalVertex) / 60f;
    }

    /// <summary>
    /// �_�C�A�����h�`�𐶐�����֐�
    /// </summary>
    /// <param name="verticalVertex"></param>
    /// <param name="horizontalVertex"></param>
    private void CreateDiamond(float verticalVertex, float horizontalVertex)
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

    /// ------private�֐�------- ///
    /// --------�֐��ꗗ-------- ///
}
public partial class DiamondMesh
{
    /// --------�ϐ��ꗗ-------- ///
    /// -------public�ϐ�------- ///



    /// -------public�ϐ�------- ///
    /// -----protected�ϐ�------ ///



    /// -----protected�ϐ�------ ///
    /// ------private�ϐ�------- ///

    private bool isSetUp = false; // �Z�b�g�A�b�v���o�������ǂ���

    private float[] correctionRate = new float[4]; // �e�X�R�A�̃O���t�̏���␳�� [0,1,2,3]...[��,�E,��,��]
                                                   // 
    [SerializeField] private float[] vertexOffset = new float[4]; // ���_�̃I�t�Z�b�g
    [SerializeField] private float maxVerticalVertex = 3f; // ��Ɖ��̒��_�̍ő�ʒu
    [SerializeField] private float maxHorizontalVertex = 2f; // �E�ƍ��̒��_�̍ő�ʒu
    [SerializeField] private float defaultVerticalVertex = 0f; // ��Ɖ��̒��_�̌��݈ʒu
    [SerializeField] private float defaulttHorizontalVertex = 0f; // �E�ƍ��̒��_�̌��݈ʒu

    [SerializeField] private ScoreDirector scoreDirector; // ScoreDirector�ϐ�

    /// ------private�ϐ�------- ///
    /// -------�v���p�e�B------- ///



    /// -------�v���p�e�B------- ///
    /// --------�ϐ��ꗗ-------- ///
}