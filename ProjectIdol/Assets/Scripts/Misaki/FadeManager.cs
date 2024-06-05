using UnityEngine;
using UnityEngine.UI;
// �t�F�[�h�C���E�A�E�g�p�̃L�����p�X�ɃA�^�b�`���邱��
// ��L�̃L�����p�X�̓^�O�Łufade�v�Ƃ��Ă������Ƃ𐄏�
// �L�����p�X�̓v���n�u�����A�q�G�����L�[��ɂ͑��݂����Ȃ�����
// �X�N���v�g�uSceneScript�v�Ƌ��Ɏg�p����
// 
public partial class FadeManager : MonoBehaviour
{

    /// --------�֐��ꗗ-------- ///
    /// -------public�֐�------- ///

    /// <summary>
    /// �t�F�[�h�C���֐�
    /// </summary>
    public void fadeIn()
    {
        isFadeIn = true;   // �t�F�[�h�C���t���O��true�ɂ���
        isFadeOut = false; // �t�F�[�h�A�E�g�t���O��false�ɂ��Č딭����h��
    }

    /// <summary>
    /// �t�F�[�h�A�E�g�֐�
    /// </summary>
    public void fadeOut()
    {
        isFadeOut = true; // �t�F�[�h�A�E�g�t���O��true�ɂ���
        isFadeIn = false; // �t�F�[�h�C���t���O��false�ɂ��Č딭����h��
    }

    /// -------public�֐�------- ///
    /// -----protected�֐�------ ///



    /// -----protected�֐�------ ///
    /// ------private�֐�------- ///

    private void Start()
    {
        if (!isFadeInstance)           // FadeCanvas�����t���O��false�Ȃ�
        {
            DontDestroyOnLoad(this);   // Canvas�������Ȃ��悤�ɂ���
            isFadeInstance = true;     // FadeCanvas�����t���O��true�ɂ���
        }
        else                           // FadeCanvas�����t���O��true�Ȃ�
        {
            Destroy(this);             // �N�����ȊO�͏d�����Ȃ��悤�ɂ��邽�ߏ�������Canvas������
        }

    }

    private void Update()
    {
        if (isFadeIn)                                                                      // �t�F�[�h�C���t���O��true�Ȃ�
        {
            alpha -= Time.deltaTime / fadeSpeed;                                           // ���X��alpha�l�����炷 fadeSpeed�ő��x�����\
            if (alpha <= 0.0f)                                                             // �����ɂȂ�����t�F�[�h�C���I��
            {
                isFadeIn = false;                                                          // �t�F�[�h�C���t���O��false�ɂ���
                alpha = 0.0f;                                                              // alpha�l���}�C�i�X�ɂȂ�Ȃ��悤��0����
            }
            this.GetComponentInChildren<Image>().color = new Color(0.0f, 0.0f, 0.0f, alpha); // �Ó]�pImage��alpha�ɑ�������X�ɓ����ɂ���
        }

        else if (isFadeOut)                                                                   // �t�F�[�h�A�E�g�t���O��true�Ȃ�
        {
            alpha += Time.deltaTime / fadeSpeed;                                             // ���X��alpha�l�𑝂₷ fadeSpeed�ő��x�����\
            if (alpha >= 1.0f)                                                               // �^�����ɂȂ�����t�F�[�h�A�E�g�I��
            {
                isFadeOut = false;                                                           // �t�F�[�h�A�E�g�t���O��false�ɂ���
                isFadeIn = true;                                                             // �t�F�[�h�C���t���O��true�ɂ���
                alpha = 1.0f;                                                                // alpha�l��1�ȏ�ɂȂ�Ȃ��悤��1����
            }

            this.GetComponentInChildren<Image>().color = new Color(0.0f, 0.0f, 0.0f, alpha); // �Ó]�pImage��alpha�ɑ�������X�ɍ��ɂ���
        }
    }

    /// ------private�֐�------- ///
    /// --------�֐��ꗗ-------- ///
}
public partial class FadeManager
{
    /// --------�ϐ��ꗗ-------- ///
    /// -------public�ϐ�------- ///

    public static bool isFadeInstance = false; // FadeCanvas�����t���O
    public bool isFadeIn = false;              // �t�F�[�h�C������t���O
    public bool isFadeOut = false;             // �t�F�[�h�A�E�g����t���O
    public float alpha = 0.0f;                 // ���ߗ�
    public float fadeSpeed = 0.2f;             // �t�F�[�h�ɂ����鎞��

    /// -------public�ϐ�------- ///
    /// -----protected�ϐ�------ ///



    /// -----protected�ϐ�------ ///
    /// ------private�ϐ�------- ///



    /// ------private�ϐ�------- ///
    /// -------�v���p�e�B------- ///



    /// -------�v���p�e�B------- ///
    /// --------�ϐ��ꗗ-------- ///
}