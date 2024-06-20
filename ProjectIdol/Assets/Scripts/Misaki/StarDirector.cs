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
                gauge.fillAmount += chain / 100;
            }
            // �����l�Q�[�W��1�ɂȂ�����
            if (gauge.fillAmount == 1)
            {
                starState = StarState.StarMode; // �X�^�[���[�h�ɕύX
            }
        }
    }

    /// <summary>
    /// �����l���[�h���I������֐�
    /// </summary>
    public void StarEnd()
    {
        gauge.fillAmount = 0; // �}�C�i�X�ɂȂ�Ȃ��悤��0����

        // �ʏ탂�[�h�p�̔w�i�ɕύX
        for (int i = 0; i < backImageObjects.Length; i++)
        {
            backImageObjects[i].SetActive(false); // �X�e�[�W�w�i��ʏ탂�[�h�ɂ���
        }

        // �p�[�e�B�N�����~�߂�
        particle.Stop();
        swipeScript.GetParticleSystem.Stop();

        scoreDirector.GetStarScore(starUltChain, swipeScript.GetStarScore, swipeScript.GetStarState); // �����l���[�h�ł̃X�R�A���擾����
        swipeScript.InitializationScore(); // �X�R�A�����Z�b�g
        starCanvas.SetActive(false); // �����l���[�h���̃L�����p�X���\���ɂ���
        resetButton.interactable = true; // �{�^����������悤�ɂ���
        starState = StarState.NormalMode; // �ʏ탂�[�h�ɕύX
        starCount += 1; // �����l���[�h�����Z����
    }

    /// -------public�֐�------- ///
    /// -----protected�֐�------ ///



    /// -----protected�֐�------ ///
    /// ------private�֐�------- ///

    private void Start()
    {
        // �����ݒ�
        gauge.fillAmount = 0;
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
            // �p�[�e�B�N���𔭐�������
            particle.Play();
            starCanvas.SetActive(true); // �����l���[�h���̃L�����p�X��\������
            swipeScript.InitializationScore(); // �����l���[�h�̃X�R�A������������
            resetButton.interactable = false; // �{�^���������Ȃ��悤�ɂ���
            gauge.fillAmount -= Time.deltaTime / starTime; // �Q�[�W������������(10�b��)
        }
        else if (starState == StarState.StarMode && backImageObjects[0].activeSelf) // �X�^�[���[�h�̎�
        {
            gauge.fillAmount -= Time.deltaTime / starTime; // �Q�[�W������������(10�b��)
            if (gauge.fillAmount <= 0) // �Q�[�W���Ȃ��Ȃ�����
            {
                StarEnd(); // �����l���[�h���I������
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

    [SerializeField] private float starUltChain = 6; // �����l���[�h�̃A���e�B���b�g�X�R�A���̃`�F�C����

    [SerializeField] private GameObject starCanvas; // �����l���[�h���̃L�����p�X
    [SerializeField] private GameObject[] backImageObjects = new GameObject[2]; // �w�i�I�u�W�F�N�g�z��

    [SerializeField] private Button resetButton; // ���Z�b�g�{�^���ϐ�

    [SerializeField] private Image gauge; // �����l�Q�[�W

    [SerializeField] private SwipeScript swipeScript; // �X���C�v�X�N���v�g�ϐ�

    [SerializeField] private ScoreDirector scoreDirector; // �X�R�A�f�B���N�^�[�ϐ�

    [SerializeField] private ParticleSystem particle; // �p�[�e�B�N���V�X�e���ϐ�

    /// ------private�ϐ�------- ///
    /// -------�v���p�e�B------- ///



    /// -------�v���p�e�B------- ///
    /// --------�ϐ��ꗗ-------- ///
}