using UnityEngine;

public partial class ClickEffectScript : MonoBehaviour
{

    /// --------�֐��ꗗ-------- ///
    /// -------public�֐�------- ///



    /// -------public�֐�------- ///
    /// -----protected�֐�------ ///



    /// -----protected�֐�------ ///
    /// ------private�֐�------- ///

    private void Update()
    {
        SummonEffect(); // �N���b�N�����ꏊ�ɃG�t�F�N�g���Ăяo��
    }

    /// <summary>
    /// �N���b�N�����ꏊ�ɃG�t�F�N�g���Ăяo���֐�
    /// </summary>
    private void SummonEffect()
    {
        if (Input.GetMouseButtonDown(0) && clickEffect) // ���N���b�N�����炩��clickEffect��null�łȂ����
        {
            // �N���b�N�����ꏊ�ɃG�t�F�N�g���Ăяo��
            var mousePosition = Input.mousePosition; // �}�E�X�̃|�W�V��������
            mousePosition.z = 3f; // z���W��3f��������ʂɏo���l�ɂ���
            GameObject clone = Instantiate(clickEffect, // �Ȃɂ��������邩
                                           Camera.main.ScreenToWorldPoint(mousePosition), // ��ʂ̂ǂ��ɏ������邩
                                           Quaternion.identity); // ���[�e�[�V�����͂ǂ�����̂�
            Destroy(clone, destroyDeleteTime); // destroyDeleteTime�b��ɃG�t�F�N�g��j�󂷂�
        }
    }

    /// ------private�֐�------- ///
    /// --------�֐��ꗗ-------- ///
}
public partial class ClickEffectScript
{
    /// --------�ϐ��ꗗ-------- ///
    /// -------public�ϐ�------- ///



    /// -------public�ϐ�------- ///
    /// -----protected�ϐ�------ ///



    /// -----protected�ϐ�------ ///
    /// ------private�ϐ�------- ///

    [SerializeField] private float destroyDeleteTime = 1.0f; // �G�t�F�N�g�������܂ł̎��ԕϐ�

    [SerializeField] private GameObject clickEffect; // �G�t�F�N�g�ϐ�


    /// ------private�ϐ�------- ///
    /// -------�v���p�e�B------- ///



    /// -------�v���p�e�B------- ///
    /// --------�ϐ��ꗗ-------- ///
}