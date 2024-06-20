using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public partial class Ranking : MonoBehaviour
{

    /// --------�֐��ꗗ-------- ///
    /// -------public�֐�------- ///

    /// <summary>
    /// �����N�C�����Ă��邩�̊֐�
    /// </summary>
    public void CheckRankin(float[] ranking, float currentScore)
    {
        if (ranking[9] < currentScore) // 10�ʂ��l���X�R�A���傫���ꍇ
        {
            ranking[9] = currentScore; // 10�ʂ��㏑������
            UpdateRanking(ranking); // �����L���O�̐������s��
            int element = SearchDescending(ranking, currentScore);
            // �����N�C�������X�R�A���ʂ�animator��isCurrentScore�ϐ���^�ɂ���
            nodeAnimator[element].SetBool("isCurrentScore", true);
            // �X�N���[���̖ڕW�l��element�ɂ���Đݒ肷��
            if (element < 4) // 4�����̏ꍇ
            {
                scrollTargetValue = 1f;
            }
            else if (element == 4) // 4�̂Ƃ�
            {
                scrollTargetValue = 0.75f;
                isScroll = true; // �X�N���[����������
            }
            else if (element == 5) // 5�̂Ƃ�
            {
                scrollTargetValue = 0.5f;
                isScroll = true; // �X�N���[����������
            }
            else if (element == 6) // 6�̂Ƃ�
            {
                scrollTargetValue = 0.25f;
                isScroll = true; // �X�N���[����������
            }
            else // 7�ȏ�̂Ƃ�
            {
                scrollTargetValue = 0f;
                isScroll = true; // �X�N���[����������
            }
        }
        WriteRanking(ranking); // �����L���O��\��
    }

    /// <param name="array">�Ώۂ̔z��</param>
    /// <param name="left">�\�[�g�͈͂̍ŏ��̃C���f�b�N�X</param>
    /// <param name="right">�\�[�g�͈͂̍Ō�̃C���f�b�N�X</param>
    public static void QuickSortAscending<T>(T[] array, int left, int right) where T : IComparable<T>
    {
        // �͈͂�������Ȃ珈���𔲂���
        if (left >= right) return;

        // �s�|�b�g��I��(�͈͂̐擪�E�^�񒆁E�����̒����l���g�p)
        T pivot = MedianAscending(array[left], array[(left + right) / 2], array[right]);

        int i = left;
        int j = right;

        while (i <= j)
        {
            // array[i] < pivot �܂ō�����T��
            while (i < right && array[i].CompareTo(pivot) < 0) i++;
            // array[i] >= pivot �܂ŉE����T��
            while (j > left && array[j].CompareTo(pivot) >= 0) j--;

            if (i > j) break;
            Swap<T>(ref array[i], ref array[j]);

            // �������s�����v�f�̎��̗v�f�ɃC���f�b�N�X��i�߂�
            i++;
            j--;
        }
        // �z��̒�����荶���̗v�f�ŃN�C�b�N�\�[�g
        QuickSortAscending(array, left, i - 1);
        // �z��̒������E���̗v�f�ŃN�C�b�N�\�[�g
        QuickSortAscending(array, i, right);
    }

    /// <summary>
    /// �N�C�b�N�\�[�g�i�~���j
    /// </summary>
    /// <param name="array">�Ώۂ̔z��</param>
    /// <param name="left">�\�[�g�͈͂̍ŏ��̃C���f�b�N�X</param>
    /// <param name="right">�\�[�g�͈͂̍Ō�̃C���f�b�N�X</param>
    public static void QuickSortDescending<T>(T[] array, int left, int right) where T : IComparable<T>
    {
        // �͈͂�������Ȃ珈���𔲂���
        if (left >= right) return;

        // �s�|�b�g��I��(�͈͂̐擪�E�^�񒆁E�����̒����l���g�p)
        T pivot = MedianDescending(array[left], array[(left + right) / 2], array[right]);

        int i = left;
        int j = right;

        while (i <= j)
        {
            // array[i] > pivot �܂ō�����T��
            while (i < right && array[i].CompareTo(pivot) > 0) i++;
            // array[i] <= pivot �܂ŉE����T��
            while (j > left && array[j].CompareTo(pivot) <= 0) j--;

            if (i > j) break;
            Swap<T>(ref array[i], ref array[j]);

            // �������s�����v�f�̎��̗v�f�ɃC���f�b�N�X��i�߂�
            i++;
            j--;
        }

        QuickSortDescending(array, left, i - 1);
        QuickSortDescending(array, i, right);
    }

    /// <summary>
    /// 2���T���֐������p
    /// </summary>
    /// <typeparam name="T">�C�ӂ̔z��</typeparam>
    /// <param name="arr">�\�[�g���ꂽ�z��</param>
    /// <param name="target">�T���������l</param>
    /// <returns></returns>
    public static int SearchAscending<T>(T[] arr, T target) where T : IComparable<T>
    {
        int left = 0;
        int right = arr.Length - 1;

        while (left <= right)
        {
            int mid = left + (right - left) / 2;

            // ���������̗v�f���^�[�Q�b�g�Ɠ������ꍇ
            if (arr[mid].CompareTo(target) == 0)
            {
                return mid;
            }
            // ���������̗v�f�����傫���ꍇ�A�E����T��
            else if (arr[mid].CompareTo(target) < 0)
            {
                left = mid + 1;
            }
            // ���������̗v�f�����������ꍇ�A������T��
            else
            {
                right = mid - 1;
            }
        }
        // �^�[�Q�b�g��������Ȃ��ꍇ
        return -1;
    }

    /// <summary>
    /// 2���T���֐��~���p
    /// </summary>
    /// <typeparam name="T">�C�ӂ̔z��</typeparam>
    /// <param name="arr">�\�[�g���ꂽ�z��</param>
    /// <param name="target">�T���������l</param>
    /// <returns></returns>
    public static int SearchDescending<T>(T[] arr, T target) where T : IComparable<T>
    {
        int left = 0;
        int right = arr.Length - 1;

        while (left <= right)
        {
            int mid = left + (right - left) / 2;

            // ���������̗v�f���^�[�Q�b�g�Ɠ������ꍇ
            if (arr[mid].CompareTo(target) == 0)
            {
                return mid;
            }
            // ���������̗v�f�����������ꍇ�A�E����T��
            else if (arr[mid].CompareTo(target) > 0)
            {
                left = mid + 1;
            }
            // ���������̗v�f�����傫���ꍇ�A������T��
            else
            {
                right = mid - 1;
            }
        }
        // �^�[�Q�b�g��������Ȃ��ꍇ
        return -1;
    }

    /// -------public�֐�------- ///
    /// -----protected�֐�------ ///



    /// -----protected�֐�------ ///
    /// ------private�֐�------- ///

    private void FixedUpdate()
    {
        ScrollAnim(scrollbar, scrollTargetValue); // �X�N���[���A�j���[�V�������Ăяo��
    }

    /// <summary>
    /// �X�N���[������֐�
    /// </summary>
    /// <param name="scrollbar">�Ώۂ̃X�N���[���o�[</param>
    /// <param name="scrollTarget">�X�N���[���̖ڕW�l</param>
    private void ScrollAnim(Scrollbar scrollbar, float scrollTarget)
    {
        if (!isScroll) return; // isScroll��false�Ȃ烊�^�[������
        if (scrollTarget < scrollbar.value)
        {
            scrollValue -= Time.deltaTime; // deltaTime�����Z����
            float progress = Mathf.Clamp01(scrollValue / scrollTime); // scrollValue / scrollTime�̊�������
            scrollbar.value -= Mathf.Lerp(0, scrollTarget, progress); // 0����scrollTargetValue�܂ł̒l�ɑ΂��Ċ���(progress)����
        }
        // showScore��totalScore�𒴂�����\�L���ꂵ�Ȃ��悤��totalScore��\������
        else
        {
            scrollbar.value = scrollTarget; // scrollTargetValue��������
            isScroll = false; // ScrollAnim���Ăяo���Ȃ��悤��false�ɂ���
        }
    }

    /// <summary>
    /// �����L���O�̐���������֐�
    /// </summary>
    private void UpdateRanking(float[] ranking)
    {
        // �N�C�b�N�\�[�g(�~��)���s��
        QuickSortDescending(ranking, 0, ranking.Length - 1);
        dataManager.Save(dataManager.data); // �Z�[�u����
    }

    /// <summary>
    /// �����L���O�̏������݊֐�
    /// </summary>
    private void WriteRanking(float[] ranking)
    {
        for (int i = 0; i < rankingScoreText.Length; i++)
        {
            rankingScoreText[i].text = string.Format("{0:0000000}", ranking[i]); // �e���ʂ������o��
        }
    }

    /// <summary>
    /// �����l�����߂�
    /// </summary>
    private static T MedianAscending<T>(T x, T y, T z) where T : IComparable<T>
    {
        // ���̕ϐ� > �E�̕ϐ� �Ȃ�1�ȏ�̐����l���Ԃ����
        if (x.CompareTo(y) > 0) Swap(ref x, ref y);
        if (x.CompareTo(z) > 0) Swap(ref x, ref z);
        if (y.CompareTo(z) > 0) Swap(ref y, ref z);
        return y;
    }

    /// <summary>
    /// �����l�����߂�i�~���j
    /// </summary>
    private static T MedianDescending<T>(T x, T y, T z) where T : IComparable<T>
    {
        // ���̕ϐ� < �E�̕ϐ� �Ȃ�-1�ȉ��̐����l���Ԃ����
        if (x.CompareTo(y) < 0) Swap(ref x, ref y);
        if (x.CompareTo(z) < 0) Swap(ref x, ref z);
        if (y.CompareTo(z) < 0) Swap(ref y, ref z);
        return y;
    }

    /// <summary>
    /// �Q�Ƃ����ւ���(�l�^���ƕϐ��̃R�s�[�ɂȂ��Ă��܂�����)
    /// </summary>
    private static void Swap<T>(ref T x, ref T y) where T : IComparable<T>
    {
        var tmp = x;
        x = y;
        y = tmp;
    }

    /// ------private�֐�------- ///
    /// --------�֐��ꗗ-------- ///
}
public partial class Ranking
{
    /// --------�ϐ��ꗗ-------- ///
    /// -------public�ϐ�------- ///



    /// -------public�ϐ�------- ///
    /// -----protected�ϐ�------ ///



    /// -----protected�ϐ�------ ///
    /// ------private�ϐ�------- ///

    private bool isScroll = false; // �X�N���[�����邩�ǂ���

    private float scrollValue = 1f; // �X�N���[���̌��ݒl
    private float scrollTargetValue = 0f; // �X�N���[���̖ڕW�l

    [SerializeField] private float scrollTime = 1f; // �X�N���[���Ɋ|���鎞��

    [SerializeField] private DataManager dataManager; // DataManager�ϐ�

    [SerializeField] private TextMeshProUGUI[] rankingScoreText = new TextMeshProUGUI[10]; // �����L���O�X�R�A�̔z��

    [SerializeField] private Animator[] nodeAnimator = new Animator[10]; // �m�[�h��Animator�z��

    [SerializeField] private Scrollbar scrollbar; // Scrollbar�ϐ�

    /// ------private�ϐ�------- ///
    /// -------�v���p�e�B------- ///



    /// -------�v���p�e�B------- ///
    /// --------�ϐ��ꗗ-------- ///
}