using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using Unity.VisualScripting;
using System.Linq.Expressions;

public class ButtonScript : MonoBehaviour
{
    bool checkTopRight = false; // �E��̃V�O�i���𒲂ׂ邩�ǂ���
    bool checkTopLeft = false; // ����̃V�O�i���𒲂ׂ邩�ǂ��� 
    bool checkBottomRight = false; // �E���̃V�O�i���𒲂ׂ邩�ǂ���
    bool checkBottomLeft = false; // �����̃V�O�i���𒲂ׂ邩�ǂ���
    int centerSignalTP = 0; // 10�̈� ��
    int centerSignalDP = 0; // 1�̈� �s
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
                    CheckDetonation(clickedGameObject); // 
                }
                else // ����ȊO�̏ꍇ
                {
                    if (!CheckChainSignal(clickedGameObject)) return; // �`�F�C���m�F�֐����Ăяo�� �`�F�C�����Ă��Ȃ���΃��^�[������
                }
                SEAudioSource.Play(); // SE��炷
                clickedGameObject.GetComponent<SignalScript>().BreakSignal(isChain,chain); // �u���C�N�֐����Ăяo��
                ResurrectionSignal(); // state��NOTHING�̃V�O�i���S�Ă�setSignalPoint��1���Z����
                ScoreDirector.GetScore(chain, isChain, state); // �Q�b�g�X�R�A�֐����Ăяo��
                ShowGetScore(); // �Q�b�g�����X�R�A��\������
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
    /// �N���b�N���ꂽ�I�u�W�F�N�g�̎΂�4�����ɂ���V�O�i����j�󂷂�֐�
    /// </summary>
    /// <param name="gameObject">�N���b�N�����I�u�W�F�N�g</param>
    /// <returns></returns>
    private bool CheckDetonation(GameObject gameObject)
    {
        centerSignalTP = default; // 10�̈� ��
        centerSignalDP = default; // 1�̈� �s
        centerSignalSS = null; // �N���b�N�����I�u�W�F�N�g��SignalScript�i�[�p
        comparisonSignalSS = null; // �N���b�N���Ă��Ȃ��I�u�W�F�N�g��SignalScript�i�[�p

        // �N���b�N�����I�u�W�F�N�g���z����I�u�W�F�N�g�̂ǂ�Ȃ̂���T��
        SearchSignal(ref centerSignalTP, ref centerSignalDP);
        // �N���b�N�����I�u�W�F�N�g��SignalScript���i�[
        centerSignalSS = signals[centerSignalTP * 10 + centerSignalDP].GetComponent<SignalScript>();
        if (centerSignalDP < 4) // �N���b�N�����I�u�W�F�N�g�̍s���E�[�łȂ���� 
        {
            // �N���b�N�����I�u�W�F�N�g��1�E��(centerSignalDP��������0�̏ꍇ�͉E��)�̃I�u�W�F�N�g���i�[
            detonationObjects.Add(signals[centerSignalTP * 10 + centerSignalDP + 1]);
            if (centerSignalDP % 2 == 0) checkBottomRight = true; // �E���𒲂ׂ邽�߂�true�ɂ���
            else checkTopRight = true; // �E��𒲂ׂ邽�߂�true�ɂ���
        }
        if (centerSignalDP > 0) // �N���b�N�����I�u�W�F�N�g�̍s�����[�łȂ����
        {
            // �N���b�N�����I�u�W�F�N�g��1����(centerSignalDP��������0�̏ꍇ�͍���)�̃I�u�W�F�N�g���i�[
            detonationObjects.Add(signals[centerSignalTP * 10 + centerSignalDP - 1]);
            if (centerSignalDP % 2 == 0) checkBottomLeft = true; // �����𒲂ׂ邽�߂�true�ɂ���
            else checkTopLeft = true; // ����𒲂ׂ邽�߂�true�ɂ���
        }
        if (centerSignalTP > 0 && centerSignalDP < 4 && centerSignalDP % 2 == 0) // �N���b�N�����I�u�W�F�N�g�̗񂪍ŏ�i���s���E�[�����܂肪0�Ȃ��
        {
            // �N���b�N�����I�u�W�F�N�g��1�E��̃I�u�W�F�N�g���i�[
            detonationObjects.Add(signals[(centerSignalTP - 1) * 10 + centerSignalDP + 1]);
            checkTopRight = true; // �E��𒲂ׂ邽�߂�true�ɂ���
        }
        if (centerSignalTP > 0 && centerSignalDP > 0 && centerSignalDP % 2 == 0) // �N���b�N�����I�u�W�F�N�g�̗񂪍ŏ�i���s�����[�����܂肪0�Ȃ��
        {
            // �N���b�N�����I�u�W�F�N�g��1����̃I�u�W�F�N�g���i�[
            detonationObjects.Add(signals[(centerSignalTP - 1) * 10 + centerSignalDP - 1]);
            checkTopLeft = true; // ����𒲂ׂ邽�߂�true�ɂ���
        }
        if (centerSignalTP < 5 && centerSignalDP < 4 && centerSignalDP % 2 > 0) // �N���b�N�����I�u�W�F�N�g�̗񂪍ŉ��i���s���E�[�����܂肪0�𒴉߂���Ȃ��
        {
            // �N���b�N�����I�u�W�F�N�g��1�E����SignalScript���i�[
            detonationObjects.Add(signals[(centerSignalTP + 1) * 10 + centerSignalDP + 1]);
            checkBottomRight = true; // �E���𒲂ׂ邽�߂�true�ɂ���
        }
        if (centerSignalTP < 5 && centerSignalDP > 0 && centerSignalDP % 2 > 0) // �N���b�N�����I�u�W�F�N�g�̗񂪍ŉ��i���s�����[�����܂肪0�𒴉߂���Ȃ��
        {
            // �N���b�N�����I�u�W�F�N�g��1������SignalScript���i�[
            detonationObjects.Add(signals[(centerSignalTP + 1) * 10 + centerSignalDP - 1]);
            checkBottomLeft = true; // �����𒲂ׂ邽�߂�true�ɂ���
        }
        if (chain > 0) // �`�F�C������0��葽���Ȃ�
        {
            isChain = true; // isChain��^�ɂ���
            // �����Ń`�F�C�������킩�� //
        }
        // �ċN�֐�(gameObject detonationObjects[detonationObjects.cont-1])

        state = centerSignalSS.state; // �������{�^����state��������
        // �`�F�C�����Ă�����true��Ԃ�
        return isChain == true;
    }
    private void RecursiveCheckTopRight (GameObject gameObject, bool topRight)
    {
        if (topRight == false) return; 
        centerSignalTP = default; // 10�̈� ��
        centerSignalDP = default; // 1�̈� �s
        // �N���b�N�����I�u�W�F�N�g���z����I�u�W�F�N�g�̂ǂ�Ȃ̂���T��
        SearchSignal(ref centerSignalTP, ref centerSignalDP);
        if (centerSignalDP < 4 && centerSignalDP % 2 > 0) // �N���b�N�����I�u�W�F�N�g�̍s���E�[�łȂ���� 
        {
            // �N���b�N�����I�u�W�F�N�g��1�E��(centerSignalDP��������0�̏ꍇ�͉E��)�̃I�u�W�F�N�g���i�[
            detonationObjects.Add(signals[centerSignalTP * 10 + centerSignalDP + 1]);
            RecursiveCheckTopRight(detonationObjects[detonationObjects.Count - 1], topRight);
        }
        else if (centerSignalTP > 0 && centerSignalDP < 4 && centerSignalDP % 2 == 0) // �N���b�N�����I�u�W�F�N�g�̗񂪍ŏ�i���s���E�[�����܂肪0�Ȃ��
        {
            // �N���b�N�����I�u�W�F�N�g��1�E��̃I�u�W�F�N�g���i�[
            detonationObjects.Add(signals[(centerSignalTP - 1) * 10 + centerSignalDP + 1]);
            RecursiveCheckTopRight(detonationObjects[detonationObjects.Count - 1], topRight);
        }
        else topRight = false;
    }
    private void RecursiveCheckTopLeft(GameObject gameObject, bool topLeft)
    {
        if (topLeft == false) return;
        centerSignalTP = default; // 10�̈� ��
        centerSignalDP = default; // 1�̈� �s
        // �N���b�N�����I�u�W�F�N�g���z����I�u�W�F�N�g�̂ǂ�Ȃ̂���T��
        SearchSignal(ref centerSignalTP, ref centerSignalDP);
        if (centerSignalDP > 0 && centerSignalDP % 2 > 0) // �N���b�N�����I�u�W�F�N�g�̍s�����[�łȂ����
        {
            // �N���b�N�����I�u�W�F�N�g��1����(centerSignalDP��������0�̏ꍇ�͍���)�̃I�u�W�F�N�g���i�[
            detonationObjects.Add(signals[centerSignalTP * 10 + centerSignalDP - 1]);
            RecursiveCheckTopLeft(gameObject, topLeft);
        }
        else if (centerSignalTP > 0 && centerSignalDP > 0 && centerSignalDP % 2 == 0) // �N���b�N�����I�u�W�F�N�g�̗񂪍ŏ�i���s�����[�����܂肪0�Ȃ��
        {
            // �N���b�N�����I�u�W�F�N�g��1����̃I�u�W�F�N�g���i�[
            detonationObjects.Add(signals[(centerSignalTP - 1) * 10 + centerSignalDP - 1]);
            RecursiveCheckTopLeft(gameObject, topLeft);
        }
        else topLeft = false;
    }
    private void RecursiveCheckBottomRight(GameObject gameObject, bool bottomRight)
    {
        if (bottomRight == false) return;
        centerSignalTP = default; // 10�̈� ��
        centerSignalDP = default; // 1�̈� �s
        // �N���b�N�����I�u�W�F�N�g���z����I�u�W�F�N�g�̂ǂ�Ȃ̂���T��
        SearchSignal(ref centerSignalTP, ref centerSignalDP);
        if (centerSignalDP < 4 && centerSignalDP % 2 == 0) // �N���b�N�����I�u�W�F�N�g�̍s���E�[�łȂ���� 
        {
            // �N���b�N�����I�u�W�F�N�g��1�E��(centerSignalDP��������0�̏ꍇ�͉E��)�̃I�u�W�F�N�g���i�[
            detonationObjects.Add(signals[centerSignalTP * 10 + centerSignalDP + 1]);
            RecursiveCheckBottomRight(gameObject, bottomRight);
        }
        else if (centerSignalTP < 5 && centerSignalDP < 4 && centerSignalDP % 2 > 0) // �N���b�N�����I�u�W�F�N�g�̗񂪍ŉ��i���s���E�[�����܂肪0�𒴉߂���Ȃ��
        {
            // �N���b�N�����I�u�W�F�N�g��1�E����SignalScript���i�[
            detonationObjects.Add(signals[(centerSignalTP + 1) * 10 + centerSignalDP + 1]);
            RecursiveCheckBottomRight(gameObject, bottomRight);
        }
        else bottomRight = false;
    }
    private void RecursiveCheckBottomLeft(GameObject gameObject, bool bottomLeft)
    {
        if (bottomLeft == false) return;
        centerSignalTP = default; // 10�̈� ��
        centerSignalDP = default; // 1�̈� �s
        // �N���b�N�����I�u�W�F�N�g���z����I�u�W�F�N�g�̂ǂ�Ȃ̂���T��
        SearchSignal(ref centerSignalTP, ref centerSignalDP);
        if (centerSignalDP > 0 && centerSignalDP % 2 == 0) // �N���b�N�����I�u�W�F�N�g�̍s�����[�łȂ����
        {
            // �N���b�N�����I�u�W�F�N�g��1����(centerSignalDP��������0�̏ꍇ�͍���)�̃I�u�W�F�N�g���i�[
            detonationObjects.Add(signals[centerSignalTP * 10 + centerSignalDP - 1]);
            RecursiveCheckBottomLeft(gameObject, bottomLeft);
        }
        if (centerSignalTP < 5 && centerSignalDP > 0 && centerSignalDP % 2 > 0) // �N���b�N�����I�u�W�F�N�g�̗񂪍ŉ��i���s�����[�����܂肪0�𒴉߂���Ȃ��
        {
            // �N���b�N�����I�u�W�F�N�g��1������SignalScript���i�[
            detonationObjects.Add(signals[(centerSignalTP + 1) * 10 + centerSignalDP - 1]);
            RecursiveCheckBottomLeft(gameObject, bottomLeft);
        }
        else bottomLeft = false;
    }
    private bool CheckChainSignal(GameObject gameObject) // �N���b�N���ꂽ�I�u�W�F�N�g��6�������`�F�b�N���A�`�F�C�����m�F����֐�
    {
        centerSignalTP = default; // 10�̈� ��
        centerSignalDP = default; // 1�̈� �s
        centerSignalSS = null; // �N���b�N�����I�u�W�F�N�g��SignalScript�i�[�p
        comparisonSignalSS = null; // �N���b�N���Ă��Ȃ��I�u�W�F�N�g��SignalScript�i�[�p

        // �N���b�N�����I�u�W�F�N�g���z����I�u�W�F�N�g�̂ǂ�Ȃ̂���T��
        SearchSignal(ref centerSignalTP, ref centerSignalDP);
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
    private void SearchSignal(ref int centerSignalTP, ref int centerSignalDP) // �N���b�N�����I�u�W�F�N�g���z����I�u�W�F�N�g�̂ǂ�Ȃ̂���T���֐� ref...�������K�{
    {
        for (int j = 0; j < 6; j++) // ����`�F�b�N
        {
            for (int i = 0; i < 5; i++) // �s���`�F�b�N
            {
                if (clickedGameObject == signals[j * 10 + i]) // �N���b�N�����I�u�W�F�N�g�Ɣz����̃I�u�W�F�N�g�������Ȃ�
                {
                    centerSignalTP = j; // ����i�[
                    centerSignalDP = i; // �s���i�[
                }
            }
        }
    }
    private void CheckSameSignal(SignalScript centerSignalSS, SignalScript comparisonSignalSS)
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
    private void ShowGetScore() // �Q�b�g�����X�R�A��\������֐�
    {
        scoreTextObject = clickedGameObject.transform.GetChild(1).gameObject; // �N���b�N�����{�^���̃e�L�X�g���擾
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
                    GameObject child = signals[j * 10 + i].transform.GetChild(3).gameObject; // �q�I�u�W�F�N�g(�G�t�F�N�g)������
                    Destroy(child.gameObject); // �q�I�u�W�F�N�g��j�󂷂�
                    int childCount = signals[j * 10 + i].transform.childCount; // �q�I�u�W�F�N�g�̌�����
                    if (childCount > 5) // �q�I�u�W�F�N�g�̌���5�𒴉߂��Ă���ꍇ(�����ҋ@���̃G�t�F�N�g��������͎̂q�I�u�W�F�N�g��6���邽��)
                    {
                        Destroy(signals[j * 10 + i].transform.GetChild(3).gameObject); // �����ҋ@���G�t�F�N�g��j�󂷂�
                        Destroy(signals[j * 10 + i].transform.GetChild(5).gameObject); // �����ҋ@���G�t�F�N�g��j�󂷂�
                    }
                }
            }
            resetStock -= 1; // �c�胊�Z�b�g�񐔂�1���炷
        }
    }
}
