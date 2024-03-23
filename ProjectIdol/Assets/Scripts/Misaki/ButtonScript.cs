using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonScript : MonoBehaviour
{
    int centerSignalTP = 0; // 10�̈� ��(�ʏ�V�O�i����)
    int centerSignalDP = 0; // 1�̈� �s(�ʏ�V�O�i����)
    int detonationSignalTP = 0; // 10�̈� ��(�U����)
    int detonationSignalDP = 0; // 1�̈� �s(�U����)
    GameObject scoreTextObject; // �e�L�X�g�̃I�u�W�F�N�g�ϐ�
    SignalScript.STATE state; //SignalScript��STATE�ϐ�
    SignalScript centerSignalSS; // �N���b�N�����I�u�W�F�N�g��SignalScript�i�[�p
    SignalScript comparisonSignalSS; // �N���b�N���Ă��Ȃ��I�u�W�F�N�g��SignalScript�i�[�p
    public bool isChain = false; // �`�F�C�����Ă��邩�ǂ���
    public bool gameStart = false; // �Q�[�����X�^�[�g���Ă��邩�ǂ���
    public float chain = 0; // �`�F�C���ϐ�
    public int resetStock = 3; // ���Z�b�g��
    public AudioSource SEAudioSource; // SE�p�I�[�f�B�I�\�[�X
    public GameObject clickedGameObject; // �N���b�N�����I�u�W�F�N�g���i�[����ϐ�
    public GameObject[] signals; // �V�O�i���i�[�p
    public List<GameObject> specialSignals; // X���{���i�[�p
    public List<GameObject> detonationObjects; // �U�������V�O�i���i�[�p
    public ScoreDirector ScoreDirector; // ScoreDirector�ϐ�
    public StarDirector StarDirector; // StarDirector�ϐ�
    public TextMeshPro getScoreText; // �擾�����X�R�A��\������e�L�X�g�ϐ�

    // Update is called once per frame
    void Update()
    {
        SignalClick(); // �V�O�i�����N���b�N�����Ƃ��̏���
    }

    private void SignalClick() // �V�O�i�����N���b�N�����Ƃ��̊֐�
    {
        if (Input.GetMouseButtonDown(0) && gameStart) // ���N���b�N�����炩�Q�[�����X�^�[�g���Ă����
        {
            // �V�O�i�����N���b�N�ł������̏���
            RaycastHit2D hitSprite = CheckHit();
            if (hitSprite == true) // ���C������������
            {
                InitializationChain(); // �`�F�C���n�̕ϐ���������
                clickedGameObject = hitSprite.transform.gameObject; // clickedGameObject�Ƀ��C�����������I�u�W�F�N�g���i�[
                if (CheckSignalState(clickedGameObject)) return; // �V�O�i���̐F���`�F�b�N����NOTHING�Ȃ烊�^�[������
                if (clickedGameObject.GetComponent<SignalScript>().state == SignalScript.STATE.SPECIAL) // X���{���̏ꍇ
                {
                    CheckDetonation(clickedGameObject); // X���{���̗U���͈͂��m�F����
                    DetonationBreak(); // �U�������V�O�i����j�󂷂�
                }
                else // ����ȊO�̏ꍇ
                {
                    if (!CheckChainSignal(clickedGameObject)) return; // �`�F�C���m�F�֐����Ăяo�� �`�F�C�����Ă��Ȃ���΃��^�[������
                }
                SEAudioSource.Play(); // SE��炷
                clickedGameObject.GetComponent<SignalScript>().BreakSignal(isChain,chain); // �u���C�N�֐����Ăяo��
                ResurrectionSignal(); // state��NOTHING�̃V�O�i���S�Ă�setSignalPoint��1���Z����
                ScoreDirector.GetScore(chain, isChain, state); // �Q�b�g�X�R�A�֐����Ăяo��
                ShowGetScore(clickedGameObject); // �Q�b�g�����X�R�A��\������
                StarDirector.GetStar(chain); // �Q�b�g�X�^�[�֐����Ăяo��
                Debug.Log(state + " �F�V�O�i�����N���b�N");
            }
        }
    }
    private RaycastHit2D CheckHit() // �q�b�g���Ă��邩�̃`�F�b�N�֐�
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // ray�ɃN���b�N�����|�W�V�������i�[
        RaycastHit2D hitSprite = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction); // hitSprite�Ƀ��C�����������I�u�W�F�N�g���i�[
        return hitSprite; //  hitSprite�����^�[������
    }
    private void InitializationChain() // �`�F�C���n�̕ϐ�������������֐�
    {
        chain = default; // ������
        isChain = default; // ������
    }
    private bool CheckSignalState(GameObject gameObject) // �V�O�i���̐F�`�F�b�N
    {
        // STATE��NOTHING�Ȃ�true��Ԃ�
        return gameObject.GetComponent<SignalScript>().state == SignalScript.STATE.NOTHING;
    }
    /// <summary>
    /// �N���b�N���ꂽ�I�u�W�F�N�g�̎΂�4�����ɂ���S�ẴV�O�i���m�F������֐�
    /// </summary>
    /// <param name="gameObject">�N���b�N�����I�u�W�F�N�g</param>
    /// <returns></returns>
    private void CheckDetonation(GameObject gameObject)
    {
        centerSignalTP = default; // 10�̈� ��
        centerSignalDP = default; // 1�̈� �s
        centerSignalSS = null; // �N���b�N�����I�u�W�F�N�g��SignalScript�i�[�p
        comparisonSignalSS = null; // �N���b�N���Ă��Ȃ��I�u�W�F�N�g��SignalScript�i�[�p

        // �N���b�N�����I�u�W�F�N�g���z����I�u�W�F�N�g�̂ǂ�Ȃ̂���T��
        SearchSignal(gameObject, ref centerSignalTP, ref centerSignalDP);
        // �N���b�N�����I�u�W�F�N�g��SignalScript���i�[
        centerSignalSS = signals[centerSignalTP * 10 + centerSignalDP].GetComponent<SignalScript>();
        if (centerSignalDP < 4) // �N���b�N�����I�u�W�F�N�g�̍s���E�[�łȂ���� 
        {
            // �N���b�N�����I�u�W�F�N�g��1�E��(centerSignalDP��������0�̏ꍇ�͉E��)�̃I�u�W�F�N�g���i�[
            Debug.Log(centerSignalTP + "��" + centerSignalDP);
            detonationObjects.Add(signals[centerSignalTP * 10 + centerSignalDP + 1]);
            if (centerSignalDP % 2 == 0) // �E���̏ꍇ
            {
                // �E���ւ̒T�����J�n����
                RecursiveCheckBottomRight(detonationObjects[detonationObjects.Count - 1]);
            }
            else // �E��̏ꍇ
            {
                // �E��ւ̒T�����J�n����
                RecursiveCheckTopRight(detonationObjects[detonationObjects.Count - 1]);
            }
        }
        if (centerSignalDP > 0) // �N���b�N�����I�u�W�F�N�g�̍s�����[�łȂ����
        {
            // �N���b�N�����I�u�W�F�N�g��1����(centerSignalDP��������0�̏ꍇ�͍���)�̃I�u�W�F�N�g���i�[
            detonationObjects.Add(signals[centerSignalTP * 10 + centerSignalDP - 1]);
            if (centerSignalDP % 2 == 0) // �����̏ꍇ
            {
                // �����ւ̒T�����J�n����
                RecursiveCheckBottomLeft(detonationObjects[detonationObjects.Count - 1]);
            }
            else
            {
                // ����ւ̒T�����J�n����
                RecursiveCheckTopLeft(detonationObjects[detonationObjects.Count - 1]);
            }
        }
        if (centerSignalTP > 0 && centerSignalDP < 4 && centerSignalDP % 2 == 0) // �N���b�N�����I�u�W�F�N�g�̗񂪍ŏ�i���s���E�[�����܂肪0�Ȃ��
        {
            // �N���b�N�����I�u�W�F�N�g��1�E��̃I�u�W�F�N�g���i�[
            detonationObjects.Add(signals[(centerSignalTP - 1) * 10 + centerSignalDP + 1]);
            // �E��ւ̒T�����J�n����
            RecursiveCheckTopRight(detonationObjects[detonationObjects.Count - 1]);
        }
        if (centerSignalTP > 0 && centerSignalDP > 0 && centerSignalDP % 2 == 0) // �N���b�N�����I�u�W�F�N�g�̗񂪍ŏ�i���s�����[�����܂肪0�Ȃ��
        {
            // �N���b�N�����I�u�W�F�N�g��1����̃I�u�W�F�N�g���i�[
            detonationObjects.Add(signals[(centerSignalTP - 1) * 10 + centerSignalDP - 1]);
            // ����ւ̒T�����J�n����
            RecursiveCheckTopLeft(detonationObjects[detonationObjects.Count - 1]);
        }
        if (centerSignalTP < 5 && centerSignalDP < 4 && centerSignalDP % 2 > 0) // �N���b�N�����I�u�W�F�N�g�̗񂪍ŉ��i���s���E�[�����܂肪0�𒴉߂���Ȃ��
        {
            // �N���b�N�����I�u�W�F�N�g��1�E����SignalScript���i�[
            detonationObjects.Add(signals[(centerSignalTP + 1) * 10 + centerSignalDP + 1]);
            // �E���ւ̒T�����J�n����
            RecursiveCheckBottomRight(detonationObjects[detonationObjects.Count - 1]);
        }
        if (centerSignalTP < 5 && centerSignalDP > 0 && centerSignalDP % 2 > 0) // �N���b�N�����I�u�W�F�N�g�̗񂪍ŉ��i���s�����[�����܂肪0�𒴉߂���Ȃ��
        {
            // �N���b�N�����I�u�W�F�N�g��1������SignalScript���i�[
            detonationObjects.Add(signals[(centerSignalTP + 1) * 10 + centerSignalDP - 1]);
            // �����ւ̒T�����J�n����
            RecursiveCheckBottomLeft(detonationObjects[detonationObjects.Count - 1]);
        }
        state = centerSignalSS.state; // �������{�^����state��������
        isChain = true; // �`�F�C�����Ă���̂�true�ɂ���
    }
    /// <summary>
    /// �E��ւ̗U����T������ċA�֐�
    /// </summary>
    /// <param name="gameObject">��ƂȂ�I�u�W�F�N�g</param>
    private void RecursiveCheckTopRight (GameObject gameObject)
    {
        detonationSignalTP = default; // 10�̈� ��
        detonationSignalDP = default; // 1�̈� �s
        // �Ώۂ̃I�u�W�F�N�g���z����I�u�W�F�N�g�̂ǂ�Ȃ̂���T��
        SearchSignal(gameObject, ref detonationSignalTP, ref detonationSignalDP);
        if (detonationSignalDP < 4 && detonationSignalDP % 2 > 0) // �Ώۂ̃I�u�W�F�N�g�̍s���E�[�łȂ���� 
        {
            // �Ώۂ̃I�u�W�F�N�g��1�E��(centerSignalDP��������0�̏ꍇ�͉E��)�̃I�u�W�F�N�g���i�[
            detonationObjects.Add(signals[detonationSignalTP * 10 + detonationSignalDP + 1]);
            // �����𖞂����Ȃ��Ȃ�܂ŒT������
            RecursiveCheckTopRight(detonationObjects[detonationObjects.Count - 1]);
        }
        else if (detonationSignalTP > 0 && detonationSignalDP < 4 && detonationSignalDP % 2 == 0) // �Ώۂ̃I�u�W�F�N�g�̗񂪍ŏ�i���s���E�[�����܂肪0�Ȃ��
        {
            // �Ώۂ̃I�u�W�F�N�g��1�E��̃I�u�W�F�N�g���i�[
            detonationObjects.Add(signals[(detonationSignalTP - 1) * 10 + detonationSignalDP + 1]);
            // �����𖞂����Ȃ��Ȃ�܂ŒT������
            RecursiveCheckTopRight(detonationObjects[detonationObjects.Count - 1]);
        }
    }
    /// <summary>
    /// ����ւ̗U����T������ċA�֐�
    /// </summary>
    /// <param name="gameObject">��ƂȂ�I�u�W�F�N�g</param>
    private void RecursiveCheckTopLeft(GameObject gameObject)
    {
        detonationSignalTP = default; // 10�̈� ��
        detonationSignalDP = default; // 1�̈� �s
        // �N���b�N�����I�u�W�F�N�g���z����I�u�W�F�N�g�̂ǂ�Ȃ̂���T��
        SearchSignal(gameObject, ref detonationSignalTP, ref detonationSignalDP);
        if (detonationSignalDP > 0 && detonationSignalDP % 2 > 0) // �N���b�N�����I�u�W�F�N�g�̍s�����[�łȂ����
        {
            // �N���b�N�����I�u�W�F�N�g��1����(centerSignalDP��������0�̏ꍇ�͍���)�̃I�u�W�F�N�g���i�[
            detonationObjects.Add(signals[detonationSignalTP * 10 + detonationSignalDP - 1]);
            // �����𖞂����Ȃ��Ȃ�܂ŒT������
            RecursiveCheckTopLeft(detonationObjects[detonationObjects.Count - 1]);
        }
        else if (detonationSignalTP > 0 && detonationSignalDP > 0 && detonationSignalDP % 2 == 0) // �N���b�N�����I�u�W�F�N�g�̗񂪍ŏ�i���s�����[�����܂肪0�Ȃ��
        {
            // �N���b�N�����I�u�W�F�N�g��1����̃I�u�W�F�N�g���i�[
            detonationObjects.Add(signals[(detonationSignalTP - 1) * 10 + detonationSignalDP - 1]);
            // �����𖞂����Ȃ��Ȃ�܂ŒT������
            RecursiveCheckTopLeft(detonationObjects[detonationObjects.Count - 1]);
        }
    }
    /// <summary>
    /// �E���ւ̗U����T������ċA�֐�
    /// </summary>
    /// <param name="gameObject">��ƂȂ�I�u�W�F�N�g</param>
    private void RecursiveCheckBottomRight(GameObject gameObject)
    {
        detonationSignalTP = default; // 10�̈� ��
        detonationSignalDP = default; // 1�̈� �s
        // �N���b�N�����I�u�W�F�N�g���z����I�u�W�F�N�g�̂ǂ�Ȃ̂���T��
        SearchSignal(gameObject, ref detonationSignalTP, ref detonationSignalDP);
        if (detonationSignalDP < 4 && detonationSignalDP % 2 == 0) // �N���b�N�����I�u�W�F�N�g�̍s���E�[�łȂ���� 
        {
            // �N���b�N�����I�u�W�F�N�g��1�E��(centerSignalDP��������0�̏ꍇ�͉E��)�̃I�u�W�F�N�g���i�[
            detonationObjects.Add(signals[detonationSignalTP * 10 + detonationSignalDP + 1]);
            // �����𖞂����Ȃ��Ȃ�܂ŒT������
            RecursiveCheckBottomRight(detonationObjects[detonationObjects.Count - 1]);
        }
        else if (detonationSignalTP < 5 && detonationSignalDP < 4 && detonationSignalDP % 2 > 0) // �N���b�N�����I�u�W�F�N�g�̗񂪍ŉ��i���s���E�[�����܂肪0�𒴉߂���Ȃ��
        {
            // �N���b�N�����I�u�W�F�N�g��1�E����SignalScript���i�[
            detonationObjects.Add(signals[(detonationSignalTP + 1) * 10 + detonationSignalDP + 1]);
            // �����𖞂����Ȃ��Ȃ�܂ŒT������
            RecursiveCheckBottomRight(detonationObjects[detonationObjects.Count - 1]);
        }
    }
    /// <summary>
    /// �����ւ̗U����T������ċA�֐�
    /// </summary>
    /// <param name="gameObject">��ƂȂ�I�u�W�F�N�g</param>
    private void RecursiveCheckBottomLeft(GameObject gameObject)
    {
        detonationSignalTP = default; // 10�̈� ��
        detonationSignalDP = default; // 1�̈� �s
        // �N���b�N�����I�u�W�F�N�g���z����I�u�W�F�N�g�̂ǂ�Ȃ̂���T��
        SearchSignal(gameObject, ref detonationSignalTP, ref detonationSignalDP);
        if (detonationSignalDP > 0 && detonationSignalDP % 2 == 0) // �N���b�N�����I�u�W�F�N�g�̍s�����[�łȂ����
        {
            // �N���b�N�����I�u�W�F�N�g��1����(centerSignalDP��������0�̏ꍇ�͍���)�̃I�u�W�F�N�g���i�[
            detonationObjects.Add(signals[detonationSignalTP * 10 + detonationSignalDP - 1]);
            // �����𖞂����Ȃ��Ȃ�܂ŒT������
            RecursiveCheckBottomLeft(detonationObjects[detonationObjects.Count - 1]);
        }
        if (detonationSignalTP < 5 && detonationSignalDP > 0 && detonationSignalDP % 2 > 0) // �N���b�N�����I�u�W�F�N�g�̗񂪍ŉ��i���s�����[�����܂肪0�𒴉߂���Ȃ��
        {
            // �N���b�N�����I�u�W�F�N�g��1������SignalScript���i�[
            detonationObjects.Add(signals[(detonationSignalTP + 1) * 10 + detonationSignalDP - 1]);
            // �����𖞂����Ȃ��Ȃ�܂ŒT������
            RecursiveCheckBottomLeft(detonationObjects[detonationObjects.Count - 1]);
        }
    }
    /// <summary>
    /// �U�������V�O�i����j�󂷂�֐�
    /// </summary>
    private void DetonationBreak()
    {
        // �U�������V�O�i����state��detonationStates�ɑ������BreakSignal�֐����Ăяo��(NOTHING�ȊO)
        for (int i = 0; i < detonationObjects.Count; i++)
        {
            if (detonationObjects[i].GetComponent<SignalScript>().state != SignalScript.STATE.NOTHING)
            {
                chain += 1; // �`�F�C������1�����Z����
                ScoreDirector.detonationStates.Add(detonationObjects[i].GetComponent<SignalScript>().state);
                detonationObjects[i].GetComponent<SignalScript>().BreakSignal(false);
            }
        }
        detonationObjects = new List<GameObject>(); // ���g����ɂ���
    }
    private bool CheckChainSignal(GameObject gameObject) // �N���b�N���ꂽ�I�u�W�F�N�g��6�������`�F�b�N���A�`�F�C�����m�F����֐�
    {
        centerSignalTP = default; // 10�̈� ��
        centerSignalDP = default; // 1�̈� �s
        centerSignalSS = null; // �N���b�N�����I�u�W�F�N�g��SignalScript�i�[�p
        comparisonSignalSS = null; // �N���b�N���Ă��Ȃ��I�u�W�F�N�g��SignalScript�i�[�p

        // �N���b�N�����I�u�W�F�N�g���z����I�u�W�F�N�g�̂ǂ�Ȃ̂���T��
        SearchSignal(gameObject, ref centerSignalTP, ref centerSignalDP);
        // �N���b�N�����I�u�W�F�N�g��SignalScript���i�[
        centerSignalSS = signals[centerSignalTP * 10 + centerSignalDP].GetComponent<SignalScript>();

        if (centerSignalDP < 4) // �N���b�N�����I�u�W�F�N�g�̍s���E�[�łȂ���� 
        {
            // �N���b�N�����I�u�W�F�N�g��1�E��(centerSignalDP��������0�̏ꍇ�͉E��)��SignalScript���i�[
            comparisonSignalSS = signals[centerSignalTP * 10 + centerSignalDP + 1].GetComponent<SignalScript>();
            CheckSameSignal(centerSignalSS, comparisonSignalSS);
        }
        if (centerSignalDP > 0) // �N���b�N�����I�u�W�F�N�g�̍s�����[�łȂ����
        {
            // �N���b�N�����I�u�W�F�N�g��1����(centerSignalDP��������0�̏ꍇ�͍���)��SignalScript���i�[
            comparisonSignalSS = signals[centerSignalTP * 10 + centerSignalDP - 1].GetComponent<SignalScript>();
            CheckSameSignal(centerSignalSS, comparisonSignalSS);
        }
        if (centerSignalTP > 0) // �N���b�N�����I�u�W�F�N�g�̗񂪍ŏ�i�łȂ����
        {
            // �N���b�N�����I�u�W�F�N�g��1���SignalScript���i�[
            comparisonSignalSS = signals[(centerSignalTP - 1) * 10 + centerSignalDP].GetComponent<SignalScript>();
            CheckSameSignal(centerSignalSS, comparisonSignalSS);
        }
        if (centerSignalTP > 0 && centerSignalDP < 4 && centerSignalDP % 2 == 0) // �N���b�N�����I�u�W�F�N�g�̗񂪍ŏ�i���s���E�[�����܂肪0�Ȃ��
        {
            // �N���b�N�����I�u�W�F�N�g��1�E���SignalScript���i�[
            comparisonSignalSS = signals[(centerSignalTP - 1) * 10 + centerSignalDP + 1].GetComponent<SignalScript>();
            CheckSameSignal(centerSignalSS, comparisonSignalSS);
        }
        if (centerSignalTP > 0 && centerSignalDP > 0 && centerSignalDP % 2 == 0) // �N���b�N�����I�u�W�F�N�g�̗񂪍ŏ�i���s�����[�����܂肪0�Ȃ��
        {
            // �N���b�N�����I�u�W�F�N�g��1�����SignalScript���i�[
            comparisonSignalSS = signals[(centerSignalTP - 1) * 10 + centerSignalDP - 1].GetComponent<SignalScript>();
            CheckSameSignal(centerSignalSS, comparisonSignalSS);
        }
        if (centerSignalTP < 5) // �N���b�N�����I�u�W�F�N�g�̗񂪍ŉ��i�łȂ����
        {
            // �N���b�N�����I�u�W�F�N�g��1����SignalScript���i�[
            comparisonSignalSS = signals[(centerSignalTP + 1) * 10 + centerSignalDP].GetComponent<SignalScript>();
            CheckSameSignal(centerSignalSS, comparisonSignalSS);
        }
        if (centerSignalTP < 5 && centerSignalDP < 4 && centerSignalDP % 2 > 0) // �N���b�N�����I�u�W�F�N�g�̗񂪍ŉ��i���s���E�[�����܂肪0�𒴉߂���Ȃ��
        {
            // �N���b�N�����I�u�W�F�N�g��1�E����SignalScript���i�[
            comparisonSignalSS = signals[(centerSignalTP + 1) * 10 + centerSignalDP + 1].GetComponent<SignalScript>();
            CheckSameSignal(centerSignalSS, comparisonSignalSS);
        }
        if (centerSignalTP < 5 && centerSignalDP > 0 && centerSignalDP % 2 > 0) // �N���b�N�����I�u�W�F�N�g�̗񂪍ŉ��i���s�����[�����܂肪0�𒴉߂���Ȃ��
        {
            // �N���b�N�����I�u�W�F�N�g��1������SignalScript���i�[
            comparisonSignalSS = signals[(centerSignalTP + 1) * 10 + centerSignalDP - 1].GetComponent<SignalScript>();
            CheckSameSignal(centerSignalSS, comparisonSignalSS);
        }
        if (chain > 0) // �`�F�C������0��葽���Ȃ�
        {
            isChain = true; // isChain��^�ɂ���
            // �����Ń`�F�C�������킩�� //
        }
        state = centerSignalSS.state; // �������{�^����state��������
        // �`�F�C�����Ă�����true��Ԃ�
        return isChain == true;
    }
    private void SearchSignal(GameObject gameObject, ref int centerSignalTP, ref int centerSignalDP) // �Ώۂ̃I�u�W�F�N�g���z����I�u�W�F�N�g�̂ǂ�Ȃ̂���T���֐� ref...�������K�{
    {
        for (int j = 0; j < 6; j++) // ����`�F�b�N
        {
            for (int i = 0; i < 5; i++) // �s���`�F�b�N
            {
                if (gameObject == signals[j * 10 + i]) // �Ώۂ̃I�u�W�F�N�g�Ɣz����̃I�u�W�F�N�g�������Ȃ�
                {
                    centerSignalTP = j; // ����i�[
                    centerSignalDP = i; // �s���i�[
                }
            }
        }
    }
    private void CheckSameSignal(SignalScript centerSignalSS, SignalScript comparisonSignalSS) // ����state���ǂ������m�F����֐�
    {
        if (centerSignalSS.state == comparisonSignalSS.state) // state�������Ȃ�
        {
            chain++; // �`�F�C���J�E���g��1���₷
            comparisonSignalSS.BreakSignal(true);
        }
    }
    public void ButtonsDestroy() // ���U���g��ʎ��Ƀ{�^��������
    {
        for (int j = 0; j < 6; j++) // ����`�F�b�N
        {
            for (int i = 0; i < 5; i++) // �s���`�F�b�N
            {
                Destroy(signals[j * 10 + i]);// �S�ẴV�O�i����j�󂷂�
            }
        }
    }
    private void ResurrectionSignal() // state��NOTHING�̃V�O�i���S�Ă�setSignalPoint��1���Z����֐�
    {
        if (!isChain) return; // �`�F�C�����Ă��Ȃ��Ȃ烊�^�[������
        for (int j = 0; j < 6; j++) // ����`�F�b�N
        {
            for (int i = 0; i < 5; i++) // �s���`�F�b�N
            {
                if (signals[j * 10 + i].GetComponent<SignalScript>().state == SignalScript.STATE.NOTHING) // ����Signal��state��NOTHING�Ȃ�
                {
                    signals[j * 10 + i].GetComponent<SignalScript>().AddSetSignalPoint(); // ����Signal��AddSetSignalPoint���Ăяo��
                }
            }
        }
    }
    private void ShowGetScore(GameObject clickedObject) // �Q�b�g�����X�R�A��\������֐�
    {
        scoreTextObject = clickedObject.transform.GetChild(1).gameObject; // �N���b�N�����{�^���̃e�L�X�g���擾
        getScoreText = scoreTextObject.GetComponent<TextMeshPro>(); // TextMeshPro����
        getScoreText.text = string.Format("+ {0:0}", ScoreDirector.score); // �Q�b�g�����X�R�A����
        scoreTextObject.GetComponent<Animator>().SetTrigger("GetScore"); // �A�j���[�V�������Đ�
    }
    public void AllButtonsReset() // �S�ẴV�O�i�������Z�b�g����
    {
        if (resetStock > 0) // �c�胊�Z�b�g�񐔂�0�𒴉߂��Ă���Ȃ�
        {
            for (int j = 0; j < 6; j++) // ����`�F�b�N
            {
                for (int i = 0; i < 5; i++) // �s���`�F�b�N
                {
                    signals[j * 10 + i].GetComponent<SignalScript>().BreakSignal(false); // �u���C�N�V�O�i�����Ăяo��
                    signals[j * 10 + i].GetComponent<SignalScript>().SetSignal(); // �Z�b�g�V�O�i�����Ăяo��
                }
            }
            resetStock -= 1; // �c�胊�Z�b�g�񐔂�1���炷
        }
    }
    /// <summary>
    /// �{���̎������j�֐�
    /// </summary>
    /// <param name="special">����������{���I�u�W�F�N�g</param>
    public void BombTimeOver(GameObject special)
    {
        InitializationChain(); // �`�F�C���n�̕ϐ���������
        CheckDetonation(special); // X���{���̗U���͈͂��m�F����
        DetonationBreak(); // �U�������V�O�i����j�󂷂�
        SEAudioSource.Play(); // SE��炷
        special.GetComponent<SignalScript>().BreakSignal(isChain, chain); // �u���C�N�֐����Ăяo��
        ResurrectionSignal(); // state��NOTHING�̃V�O�i���S�Ă�setSignalPoint��1���Z����
        ScoreDirector.GetScore(chain, isChain, state); // �Q�b�g�X�R�A�֐����Ăяo��
        ShowGetScore(special); // �Q�b�g�����X�R�A��\������
        StarDirector.GetStar(chain); // �Q�b�g�X�^�[�֐����Ăяo��
        Debug.Log(special + "�������Ŕ������܂���");
    }
}
