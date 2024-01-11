using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ButtonScript : MonoBehaviour
{
    SignalScript.STATE state; //SignalScript��STATE�ϐ�
    public float chain; // �`�F�C���ϐ�
    public bool isChain; // �`�F�C�����Ă��邩�ǂ���
    public bool gameStart; // �Q�[�����X�^�[�g���Ă��邩�ǂ���
    public ScoreDirector ScoreDirector; // ScoreDirector�ϐ�
    public StarDirector StarDirector; // StarDirector�ϐ�
    public GameObject clickedGameObject; // �N���b�N�����I�u�W�F�N�g���i�[����ϐ�
    public GameObject[] signals; // �V�O�i���i�[�p

    // Update is called once per frame
    void Update()
    {
        // �V�O�i�����N���b�N�����Ƃ��̏���
        if (Input.GetMouseButtonDown(0) && gameStart) // ���N���b�N������
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // ray�ɃN���b�N�����|�W�V�������i�[
            RaycastHit2D hitSprite = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction); // hitSprite�Ƀ��C�����������I�u�W�F�N�g���i�[

            if (hitSprite == true) // ���C������������
            {
                chain = 0; // ������
                isChain = false; // ������
                clickedGameObject = hitSprite.transform.gameObject; // clickedGameObject�Ƀ��C�����������I�u�W�F�N�g���i�[
                if (CheakSignalState(clickedGameObject)) return; // �V�O�i���̐F���`�F�b�N����NOTHING�Ȃ烊�^�[������
                CheakChainSignal(clickedGameObject); // �`�F�C���m�F�֐����Ăяo��
                clickedGameObject.GetComponent<SignalScript>().BreakSignal(isChain); // �u���C�N�֐����Ăяo��
                ResurrectionSignal(); // state��NOTHING�̃V�O�i���S�Ă�setSignalPoint��1���Z����
                ScoreDirector.GetScore(chain, isChain, state); // �Q�b�g�X�R�A�֐����Ăяo��
                StarDirector.GetStar(chain); // �Q�b�g�X�^�[�֐����Ăяo��
                Debug.Log(state+" �F�V�O�i�����N���b�N");
            }
        }
    }
    
    private bool CheakSignalState(GameObject gameObject) // �V�O�i���̐F�`�F�b�N
    {
        // STATE��NOTHING�Ȃ�true��Ԃ�
        return gameObject.GetComponent<SignalScript>().state == SignalScript.STATE.NOTHING;

    }

    private void CheakChainSignal(GameObject gameObject) // �N���b�N���ꂽ�I�u�W�F�N�g��8�������`�F�b�N���A�`�F�C�����m�F����֐�
    {
        int centerSignalTP = 0; // 10�̈� ��
        int centerSignalDP = 0; // 1�̈� �s
        SignalScript centerSignalSS; // �N���b�N�����I�u�W�F�N�g��SignalScript�i�[�p
        SignalScript comparisonSignalSS; // �N���b�N���Ă��Ȃ��I�u�W�F�N�g��SignalScript�i�[�p

        // �N���b�N�����I�u�W�F�N�g���z����I�u�W�F�N�g�̂ǂ�Ȃ̂���T��
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
        // �N���b�N�����I�u�W�F�N�g��SignalScript���i�[
        centerSignalSS = signals[centerSignalTP * 10 + centerSignalDP].GetComponent<SignalScript>();
        if (centerSignalDP < 4) // �N���b�N�����I�u�W�F�N�g�̍s���E�[�łȂ���� 
        {
            // �N���b�N�����I�u�W�F�N�g��1�E��(centerSignalDP��������0�̏ꍇ�͉E��)��SignalScript���i�[
            comparisonSignalSS = signals[centerSignalTP * 10 + centerSignalDP + 1].GetComponent<SignalScript>();
            if (centerSignalSS.state == comparisonSignalSS.state) // state�������Ȃ�
            {
                chain++; // �`�F�C���J�E���g��1���₷
                comparisonSignalSS.BreakSignal(true);
            }
        }
        if (centerSignalDP > 0) // �N���b�N�����I�u�W�F�N�g�̍s�����[�łȂ����
        {
            // �N���b�N�����I�u�W�F�N�g��1����(centerSignalDP��������0�̏ꍇ�͍���)��SignalScript���i�[
            comparisonSignalSS = signals[centerSignalTP * 10 + centerSignalDP - 1].GetComponent<SignalScript>();
            if (centerSignalSS.state == comparisonSignalSS.state) // state�������Ȃ�
            {
                chain++; // �`�F�C���J�E���g��1���₷
                comparisonSignalSS.BreakSignal(true);
            }
        }
        if (centerSignalTP > 0) // �N���b�N�����I�u�W�F�N�g�̗񂪍ŏ�i�łȂ����
        {
            // �N���b�N�����I�u�W�F�N�g��1���SignalScript���i�[
            comparisonSignalSS = signals[(centerSignalTP - 1) * 10 + centerSignalDP].GetComponent<SignalScript>();
            if (centerSignalSS.state == comparisonSignalSS.state) // state�������Ȃ�
            {
                chain++; // �`�F�C���J�E���g��1���₷
                comparisonSignalSS.BreakSignal(true);
            }
        }
        if (centerSignalTP > 0 && centerSignalDP < 4 && centerSignalDP % 2 == 0) // �N���b�N�����I�u�W�F�N�g�̗񂪍ŏ�i���s���E�[�����܂肪0�Ȃ��
        {
            // �N���b�N�����I�u�W�F�N�g��1�E���SignalScript���i�[
            comparisonSignalSS = signals[(centerSignalTP - 1) * 10 + centerSignalDP + 1].GetComponent<SignalScript>();
            if (centerSignalSS.state == comparisonSignalSS.state) // state�������Ȃ�
            {
                chain++; // �`�F�C���J�E���g��1���₷
                comparisonSignalSS.BreakSignal(true);
            }
        }
        if (centerSignalTP > 0 && centerSignalDP > 0 && centerSignalDP % 2 == 0) // �N���b�N�����I�u�W�F�N�g�̗񂪍ŏ�i���s�����[�����܂肪0�Ȃ��
        {
            // �N���b�N�����I�u�W�F�N�g��1�����SignalScript���i�[
            comparisonSignalSS = signals[(centerSignalTP - 1) * 10 + centerSignalDP - 1].GetComponent<SignalScript>();
            if (centerSignalSS.state == comparisonSignalSS.state) // state�������Ȃ�
            {
                chain++; // �`�F�C���J�E���g��1���₷
                comparisonSignalSS.BreakSignal(true);
            }
        }
        if (centerSignalTP < 5) // �N���b�N�����I�u�W�F�N�g�̗񂪍ŉ��i�łȂ����
        {
            // �N���b�N�����I�u�W�F�N�g��1����SignalScript���i�[
            comparisonSignalSS = signals[(centerSignalTP + 1) * 10 + centerSignalDP].GetComponent<SignalScript>();
            if (centerSignalSS.state == comparisonSignalSS.state) // state�������Ȃ�
            {
                chain++; // �`�F�C���J�E���g��1���₷
                comparisonSignalSS.BreakSignal(true);
            }
        }
        if (centerSignalTP < 5 && centerSignalDP < 4 && centerSignalDP % 2 > 0) // �N���b�N�����I�u�W�F�N�g�̗񂪍ŉ��i���s���E�[�����܂肪0�𒴉߂���Ȃ��
        {
            // �N���b�N�����I�u�W�F�N�g��1�E����SignalScript���i�[
            comparisonSignalSS = signals[(centerSignalTP + 1) * 10 + centerSignalDP + 1].GetComponent<SignalScript>();
            if (centerSignalSS.state == comparisonSignalSS.state) // state�������Ȃ�
            {
                chain++; // �`�F�C���J�E���g��1���₷
                comparisonSignalSS.BreakSignal(true);
            }
        }
        if (centerSignalTP < 5 && centerSignalDP > 0 && centerSignalDP % 2 > 0) // �N���b�N�����I�u�W�F�N�g�̗񂪍ŉ��i���s�����[�����܂肪0�𒴉߂���Ȃ��
        {
            // �N���b�N�����I�u�W�F�N�g��1������SignalScript���i�[
            comparisonSignalSS = signals[(centerSignalTP + 1) * 10 + centerSignalDP - 1].GetComponent<SignalScript>();
            if (centerSignalSS.state == comparisonSignalSS.state) // state�������Ȃ�
            {
                chain++; // �`�F�C���J�E���g��1���₷
                comparisonSignalSS.BreakSignal(true);
            }
        }
        if (chain > 0) // �`�F�C������0��葽���Ȃ�
        {
            isChain = true; // isChain��^�ɂ���
        }
        state = centerSignalSS.state; // �������{�^����state��������
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
    public void ButtonsDestroy()
    {
        for (int j = 0; j < 6; j++) // ����`�F�b�N
        {
            for (int i = 0; i < 5; i++) // �s���`�F�b�N
            {
                Destroy(signals[j * 10 + i]);// �S�ẴV�O�i����j�󂷂�
            }
        }
    }
}
