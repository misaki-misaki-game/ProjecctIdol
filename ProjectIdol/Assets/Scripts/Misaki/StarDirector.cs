using UnityEngine;
using UnityEngine.UI;

public partial class StarDirector : MonoBehaviour
{
    /// --------�֐��ꗗ-------- ///
    /// -------public�֐�------- ///

    /// <summary>
    /// �Q�b�g�X�^�[�֐�
    /// </summary>
    /// <param name="chain">�������V�O�i���̃`�F�C����</param>
    public void GetStar(float chain)
    {
        if (starState == StarState.NormalMode) // �ʏ탂�[�h�̂Ƃ�
        {
            // �����l�Q�[�W�̑���������
            if (chain >= 1)
            {
                // �`�F�C���̒l���Q�[�W�𑝉�������
                Gauge.fillAmount += chain / 100;
            }
            // �����l�Q�[�W��1�ɂȂ�����
            if (Gauge.fillAmount == 1)
            {
                starState = StarState.StarMode; // �X�^�[���[�h�ɕύX
            }
        }
    }

    /// -------public�֐�------- ///
    /// -----protected�֐�------ ///



    /// -----protected�֐�------ ///
    /// ------private�֐�------- ///

    private void Start()
    {
        // �����ݒ�
        Gauge.fillAmount = 0;
    }

    private void FixedUpdate()
    {
        ShowStarMode(); // �X�^�[���[�h�̉��o���s��
    }

    /// <summary>
    /// �X�^�[���[�h�̉��o���s���֐�
    /// </summary>
    private void ShowStarMode()
    {
        if (starState == StarState.StarMode && !backImageObjects[0].activeSelf) // �X�^�[���[�h�̎�
        {
            // �����l���[�h�p�̔w�i�ɕύX
            for (int i = 0; i < backImageObjects.Length; i++)
            {
                backImageObjects[i].SetActive(true);
            }
            Gauge.fillAmount -= Time.deltaTime / starTime; // �Q�[�W������������(10�b��)
        }
        else if (starState == StarState.StarMode && backImageObjects[0].activeSelf) // �X�^�[���[�h�̎�
        {
            Gauge.fillAmount -= Time.deltaTime / starTime; // �Q�[�W������������(10�b��)
            if (Gauge.fillAmount <= 0) // �Q�[�W���Ȃ��Ȃ�����
            {
                // �����l���[�h�p�̔w�i�ɕύX
                for (int i = 0; i < backImageObjects.Length; i++)
                {
                    backImageObjects[i].SetActive(false); // �X�e�[�W�w�i��ʏ탂�[�h�ɂ���
                }
                starState = StarState.NormalMode; // �ʏ탂�[�h�ɕύX
                starCount += 1; // �����l���[�h�����Z����
            }
        }
    }

    /// ------private�֐�------- ///
    /// --------�֐��ꗗ-------- ///
}
public partial class StarDirector
{
    /// --------�ϐ��ꗗ-------- ///
    /// -------public�ϐ�------- ///

    public int starCount = 0; // �����l���[�h�ɂȂ�����
    public enum StarState
    {
        NormalMode, // �ʏ��� 0
        StarMode // �X�^�[���[�h 1
    }
    public StarState starState = StarState.NormalMode; // StarState�ϐ�

    /// -------public�ϐ�------- ///
    /// -----protected�ϐ�------ ///



    /// -----protected�ϐ�------ ///
    /// ------private�ϐ�------- ///

    [SerializeField] private float starTime = 10; // �����l���[�h�̐�������
    [SerializeField] private GameObject[] backImageObjects = new GameObject[2]; // �w�i�I�u�W�F�N�g�z��
    [SerializeField] private Image Gauge; // �����l�Q�[�W

    /// ------private�ϐ�------- ///
    /// -------�v���p�e�B------- ///



    /// -------�v���p�e�B------- ///
    /// --------�ϐ��ꗗ-------- ///
}