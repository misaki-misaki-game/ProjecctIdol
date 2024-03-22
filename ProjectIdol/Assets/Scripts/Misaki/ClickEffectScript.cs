using UnityEngine;

public class ClickEffectScript : MonoBehaviour
{
    public GameObject clickEffect; // �G�t�F�N�g�ϐ�
    public float destroyDeleteTime = 1.0f; // �G�t�F�N�g�������܂ł̎��ԕϐ�

    // Update is called once per frame
    void Update()
    {
        SummonEffect(); // �N���b�N�����ꏊ�ɃG�t�F�N�g���Ăяo��
    }

    private void SummonEffect() // �N���b�N�����ꏊ�ɃG�t�F�N�g���Ăяo���֐�
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
}
