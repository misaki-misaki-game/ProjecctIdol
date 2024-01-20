using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using Unity.VisualScripting;

public class ButtonScript : MonoBehaviour
{
    GameObject textObject;
    public TextMeshPro text;
    SignalScript.STATE state; //SignalScript��STATE�ϐ�
    public float chain = 0; // �`�F�C���ϐ�
    public bool isChain = false; // �`�F�C�����Ă��邩�ǂ���
    public bool gameStart = false; // �Q�[�����X�^�[�g���Ă��邩�ǂ���
    public int resetStock = 3; // ���Z�b�g��
    int centerSignalTP = 0; // 10�̈� ��
    int centerSignalDP = 0; // 1�̈� �s
    SignalScript centerSignalSS; // �N���b�N�����I�u�W�F�N�g��SignalScript�i�[�p
    SignalScript comparisonSignalSS; // �N���b�N���Ă��Ȃ��I�u�W�F�N�g��SignalScript�i�[�p
    public ScoreDirector ScoreDirector; // ScoreDirector�ϐ�
    public StarDirector StarDirector; // StarDirector�ϐ�
    public GameObject clickedGameObject; // �N���b�N�����I�u�W�F�N�g���i�[����ϐ�
    public GameObject[] signals; // �V�O�i���i�[�p
    public AudioSource SEAudioSource; // SE�p�I�[�f�B�I�\�[�X

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
                if (!CheckChainSignal(clickedGameObject)) return; // �`�F�C���m�F�֐����Ăяo�� �`�F�C�����Ă��Ȃ���΃��^�[������
                SEAudioSource.Play(); // SE��炷
                clickedGameObject.GetComponent<SignalScript>().BreakSignal(isChain); // �u���C�N�֐����Ăяo��
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
    private bool CheckChainSignal(GameObject gameObject) // �N���b�N���ꂽ�I�u�W�F�N�g��8�������`�F�b�N���A�`�F�C�����m�F����֐�
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
        textObject = clickedGameObject.transform.GetChild(1).gameObject; // �N���b�N�����{�^���̃e�L�X�g���擾
        text = textObject.GetComponent<TextMeshPro>(); // TextMEshPro����
        text.text = string.Format("+ {0:0}", ScoreDirector.score); // �Q�b�g�����X�R�A����
        textObject.GetComponent<Animator>().SetTrigger("GetScore"); // �A�j���[�V�������Đ�
    }
   public void AllButtonsReset() // �S�ẴV�O�i�������Z�b�g����
    {
        if (resetStock > 0)
        {
            for (int j = 0; j < 6; j++) // ����`�F�b�N
            {
                for (int i = 0; i < 5; i++) // �s���`�F�b�N
                {
                    signals[j * 10 + i].GetComponent<SignalScript>().BreakSignal(false); // �u���C�N�V�O�i�����Ăяo��
                    signals[j * 10 + i].GetComponent<SignalScript>().SetSignal(); // �Z�b�g�V�O�i�����Ăяo��
                    GameObject child = signals[j * 10 + i].transform.GetChild(3).gameObject; // �q�I�u�W�F�N�g(�G�t�F�N�g)������
                    Destroy(child); // �q�I�u�W�F�N�g��j�󂷂�
                }
            }
            resetStock -= 1; // �c�胊�Z�b�g�񐔂�1���炷
        }
    }
}
