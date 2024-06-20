using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public partial class ButtonScript : MonoBehaviour
{
    /// --------�֐��ꗗ-------- ///
    /// -------public�֐�------- ///

    /// <summary>
    /// ���U���g��ʎ��Ƀ{�^���������֐�
    /// </summary>
    public void ButtonsDestroy()
    {
        for (int j = 0; j < column; j++) // ����`�F�b�N
        {
            for (int i = 0; i < row; i++) // �s���`�F�b�N
            {
                if (!signals[j * 10 + i]) continue; // null�Ȃ玟�̏����Ɉڍs����
                Destroy(signals[j * 10 + i]);// �S�ẴV�O�i����j�󂷂�
            }
        }
        resetButton.gameObject.SetActive(false); // ���Z�b�g�{�^�����\���ɂ���
    }

    /// <summary>
    /// �S�ẴV�O�i�������Z�b�g����֐�
    /// </summary>
    public void AllButtonsReset()
    {
        if (resetStock > 0) // �c�胊�Z�b�g�񐔂�0�𒴉߂��Ă���Ȃ�
        {
            for (int j = 0; j < column; j++) // ����`�F�b�N
            {
                for (int i = 0; i < row; i++) // �s���`�F�b�N
                {
                    SignalScript ss = signals[j * 10 + i].GetComponent<SignalScript>();
                    ss.BreakSignal(false); // �u���C�N�V�O�i�����Ăяo��
                    ss.SetSignal(); // �Z�b�g�V�O�i�����Ăяo��
                }
            }
            resetStock -= 1; // �c�胊�Z�b�g�񐔂�1���炷
            // resetStock�ɂ���ă��Z�b�g�{�^���摜��ύX����
            switch (resetStock)
            {
                case 0:
                    resetButton.sprite = resetImage[0];
                    resetButton.GetComponent<Button>().interactable = false;
                    break;
                case 1:
                    resetButton.sprite = resetImage[1];
                    break;
                case 2:
                    resetButton.sprite = resetImage[2];
                    break;
            }
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

    /// -------public�֐�------- ///
    /// -----protected�֐�------ ///



    /// -----protected�֐�------ ///
    /// ------private�֐�------- ///

    private void Start()
    {
        rightMost = (signals.Length % 10) - 1; // �E�[����
        bottom = signals.Length / 10; // �ŉ��i����
        row = signals.Length % 10; // �s������
        column = (signals.Length / 10) + 1; // �񐔂���
        resetButton.GetComponent <Button>().interactable = false; // �{�^���������Ȃ��悤�ɂ���
    }

    private void Update()
    {
        SignalClick(); // �V�O�i�����N���b�N�����Ƃ��̏���
    }

    /// <summary>
    /// �V�O�i�����N���b�N�����Ƃ��̊֐�
    /// </summary>
    private void SignalClick()
    {
        // ���N���b�N�����炩�Q�[�����X�^�[�g���Ă���΂��ʏ탂�[�h�Ȃ�
        if (Input.GetMouseButtonDown(0) && gameStart && StarDirector.starState == StarDirector.StarState.NormalMode) 
        {
            // �V�O�i�����N���b�N�ł������̏���
            RaycastHit2D hitSprite = CheckHit();
            if (hitSprite == true) // ���C������������
            {
                InitializationChain(); // �`�F�C���n�̕ϐ���������
                clickedGameObject = hitSprite.transform.gameObject; // clickedGameObject�Ƀ��C�����������I�u�W�F�N�g���i�[
                ProcessingSignal(clickedGameObject); // clickedGameObject����ɂ����V�O�i���̏������s��
            }
        }
    }

    /// <summary>
    /// �n���ꂽ�V�O�i������ɏ������s���֐�
    /// </summary>
    /// <param name="signal">�I�������V�O�i��</param>
    private void ProcessingSignal(GameObject signal)
    {
        SignalScript ss = signal.GetComponent<SignalScript>(); // SignalScript����
        if (CheckSignalState(signal)) return; // �V�O�i���̐F���`�F�b�N����NOTHING�Ȃ烊�^�[������
        if (ss.state == SignalScript.STATE.SPECIAL) // X���{���̏ꍇ
        {
            CheckDetonation(signal); // X���{���̗U���͈͂��m�F����
            DetonationBreak(); // �U�������V�O�i����j�󂷂�
        }
        else // ����ȊO�̏ꍇ
        {
            if (!CheckChainSignal(signal)) return; // �`�F�C���m�F�֐����Ăяo�� �`�F�C�����Ă��Ȃ���΃��^�[������
        }
        // SE��炷
        if (ss.state == SignalScript.STATE.SPECIAL) SEAudioSource.PlayOneShot(SEClips[1]);
        else SEAudioSource.PlayOneShot(SEClips[0]);

        ss.BreakSignal(isChain, chain); // �u���C�N�֐����Ăяo��
        ResurrectionSignal(); // state��NOTHING�̃V�O�i���S�Ă�setSignalPoint��1���Z����
        ScoreDirector.GetScore(chain, isChain, state); // �Q�b�g�X�R�A�֐����Ăяo��
        ShowGetScore(signal); // �Q�b�g�����X�R�A��\������
        StarDirector.GetStar(chain); // �Q�b�g�X�^�[�֐����Ăяo��
    }

    /// <summary>
    /// �q�b�g���Ă��邩�̃`�F�b�N�֐�
    /// </summary>
    /// <returns>�q�b�g����RaycastHit2D</returns>
    private RaycastHit2D CheckHit()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // ray�ɃN���b�N�����|�W�V�������i�[
        RaycastHit2D hitSprite = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction); // hitSprite�Ƀ��C�����������I�u�W�F�N�g���i�[
        return hitSprite; //  hitSprite�����^�[������
    }

    /// <summary>
    /// �`�F�C���n�̕ϐ�������������֐�
    /// </summary>
    private void InitializationChain()
    {
        chain = default; // ������
        isChain = default; // ������
    }

    /// <summary>
    /// �V�O�i���̐F�`�F�b�N
    /// </summary>
    /// <param name="gameObject"></param>
    /// <returns>STATE��NOTHING�Ȃ�true</returns>
    private bool CheckSignalState(GameObject gameObject)
    {
        // STATE��NOTHING�Ȃ�true��Ԃ�
        return gameObject.GetComponent<SignalScript>().state == SignalScript.STATE.NOTHING;
    }

    /// <summary>
    /// �N���b�N���ꂽ�I�u�W�F�N�g�̎΂�4�����ɂ���S�ẴV�O�i���m�F������֐�
    /// </summary>
    /// <param name="gameObject">�N���b�N�����I�u�W�F�N�g</param>
    private void CheckDetonation(GameObject gameObject)
    {

        // �n�߂̔����܂��͔�������V�O�i�����d�����Ă��Ȃ��Ȃ甚�������V�O�i���Ƃ��Ċi�[
        // �d�����Ă���Ώ����𒆎~
        if (explotionObjects == null || !explotionObjects.Contains(gameObject)) explotionObjects.Add(gameObject);
        else if (explotionObjects.Contains(gameObject)) return;

        centerSignalTP = default; // 10�̈� ��
        centerSignalDP = default; // 1�̈� �s
        centerSignalSS = null; // �N���b�N�����I�u�W�F�N�g��SignalScript�i�[�p
        comparisonSignalSS = null; // �N���b�N���Ă��Ȃ��I�u�W�F�N�g��SignalScript�i�[�p

        // �N���b�N�����I�u�W�F�N�g���z����I�u�W�F�N�g�̂ǂ�Ȃ̂���T��
        SearchSignal(gameObject, ref centerSignalTP, ref centerSignalDP);
        // �N���b�N�����I�u�W�F�N�g��SignalScript���i�[
        centerSignalSS = signals[centerSignalTP * 10 + centerSignalDP].GetComponent<SignalScript>();

        // �e�΂ߕ�����T���@���Ɋi�[�����I�u�W�F�N�g�͏������s��Ȃ� 
        // �N���b�N�����I�u�W�F�N�g�̍s���E�[�łȂ����
        if (centerSignalDP < rightMost && !detonationObjects.Contains(signals[centerSignalTP * 10 + centerSignalDP + 1])) 
        {
            // �N���b�N�����I�u�W�F�N�g��1�E��(centerSignalDP��������0�̏ꍇ�͉E��)�̃I�u�W�F�N�g���i�[
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
        // �N���b�N�����I�u�W�F�N�g�̍s�����[�łȂ����
        if (centerSignalDP > 0 && !detonationObjects.Contains(signals[centerSignalTP * 10 + centerSignalDP - 1]))
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
        // �N���b�N�����I�u�W�F�N�g�̗񂪍ŏ�i���s���E�[�����܂肪0�Ȃ��
        if (centerSignalTP > 0 && centerSignalDP < rightMost && centerSignalDP % 2 == 0 && !detonationObjects.Contains(signals[(centerSignalTP - 1) * 10 + centerSignalDP + 1]))
        {
            // �N���b�N�����I�u�W�F�N�g��1�E��̃I�u�W�F�N�g���i�[
            detonationObjects.Add(signals[(centerSignalTP - 1) * 10 + centerSignalDP + 1]);
            // �E��ւ̒T�����J�n����
            RecursiveCheckTopRight(detonationObjects[detonationObjects.Count - 1]);
        }
        // �N���b�N�����I�u�W�F�N�g�̗񂪍ŏ�i���s�����[�����܂肪0�Ȃ��
        if (centerSignalTP > 0 && centerSignalDP > 0 && centerSignalDP % 2 == 0 && !detonationObjects.Contains(signals[(centerSignalTP - 1) * 10 + centerSignalDP - 1]))
        {
            // �N���b�N�����I�u�W�F�N�g��1����̃I�u�W�F�N�g���i�[
            detonationObjects.Add(signals[(centerSignalTP - 1) * 10 + centerSignalDP - 1]);
            // ����ւ̒T�����J�n����
            RecursiveCheckTopLeft(detonationObjects[detonationObjects.Count - 1]);
        }
        // �N���b�N�����I�u�W�F�N�g�̗񂪍ŉ��i���s���E�[�����܂肪0�𒴉߂���Ȃ��
        if (centerSignalTP < bottom && centerSignalDP < rightMost && centerSignalDP % 2 > 0 && !detonationObjects.Contains(signals[(centerSignalTP + 1) * 10 + centerSignalDP + 1]))
        {
            // �N���b�N�����I�u�W�F�N�g��1�E����SignalScript���i�[
            detonationObjects.Add(signals[(centerSignalTP + 1) * 10 + centerSignalDP + 1]);
            // �E���ւ̒T�����J�n����
            RecursiveCheckBottomRight(detonationObjects[detonationObjects.Count - 1]);
        }
        // �N���b�N�����I�u�W�F�N�g�̗񂪍ŉ��i���s�����[�����܂肪0�𒴉߂���Ȃ��
        if (centerSignalTP < bottom && centerSignalDP > 0 && centerSignalDP % 2 > 0 && !detonationObjects.Contains(signals[(centerSignalTP + 1) * 10 + centerSignalDP - 1]))
        {
            // �N���b�N�����I�u�W�F�N�g��1������SignalScript���i�[
            detonationObjects.Add(signals[(centerSignalTP + 1) * 10 + centerSignalDP - 1]);
            // �����ւ̒T�����J�n����
            RecursiveCheckBottomLeft(detonationObjects[detonationObjects.Count - 1]);
        }
        // �U�������I�u�W�F�N�g�̒��Ƀ{��������΁A����𒆐S�ɔ����������s��
        for (int i = 0; i < detonationObjects.Count; i++)
        {
            if (detonationObjects[i].GetComponent<SignalScript>().state == SignalScript.STATE.SPECIAL)
            {
                CheckDetonation(detonationObjects[i]);
            }
        }
        state = centerSignalSS.state; // �������{�^����state��������
        isChain = true; // �`�F�C�����Ă���̂�true�ɂ���
    }

    /// <summary>
    /// �E��ւ̗U����T������ċA�֐�
    /// </summary>
    /// <param name="gameObject">��ƂȂ�I�u�W�F�N�g</param>
    private void RecursiveCheckTopRight(GameObject gameObject)
    {
        detonationSignalTP = default; // 10�̈� ��
        detonationSignalDP = default; // 1�̈� �s
        // �Ώۂ̃I�u�W�F�N�g���z����I�u�W�F�N�g�̂ǂ�Ȃ̂���T��
        SearchSignal(gameObject, ref detonationSignalTP, ref detonationSignalDP);
        if (detonationSignalDP < rightMost && detonationSignalDP % 2 > 0) // �Ώۂ̃I�u�W�F�N�g�̍s���E�[�łȂ���� 
        {
            // �Ώۂ̃I�u�W�F�N�g��1�E��(centerSignalDP��������0�̏ꍇ�͉E��)�̃I�u�W�F�N�g���i�[
            detonationObjects.Add(signals[detonationSignalTP * 10 + detonationSignalDP + 1]);
            // �����𖞂����Ȃ��Ȃ�܂ŒT������
            RecursiveCheckTopRight(detonationObjects[detonationObjects.Count - 1]);
        }
        else if (detonationSignalTP > 0 && detonationSignalDP < rightMost && detonationSignalDP % 2 == 0) // �Ώۂ̃I�u�W�F�N�g�̗񂪍ŏ�i���s���E�[�����܂肪0�Ȃ��
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
        if (detonationSignalDP < rightMost && detonationSignalDP % 2 == 0) // �N���b�N�����I�u�W�F�N�g�̍s���E�[�łȂ���� 
        {
            // �N���b�N�����I�u�W�F�N�g��1�E��(centerSignalDP��������0�̏ꍇ�͉E��)�̃I�u�W�F�N�g���i�[
            detonationObjects.Add(signals[detonationSignalTP * 10 + detonationSignalDP + 1]);
            // �����𖞂����Ȃ��Ȃ�܂ŒT������
            RecursiveCheckBottomRight(detonationObjects[detonationObjects.Count - 1]);
        }
        else if (detonationSignalTP < bottom && detonationSignalDP < rightMost && detonationSignalDP % 2 > 0) // �N���b�N�����I�u�W�F�N�g�̗񂪍ŉ��i���s���E�[�����܂肪0�𒴉߂���Ȃ��
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
        if (detonationSignalTP < bottom && detonationSignalDP > 0 && detonationSignalDP % 2 > 0) // �N���b�N�����I�u�W�F�N�g�̗񂪍ŉ��i���s�����[�����܂肪0�𒴉߂���Ȃ��
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
        // �U���͈͂̃V�O�i����setSignalSoint��+1���₵�Ă��� SignalClick()�ł����+1�����̂Ŏ���+2
        for (int i = 0; i < detonationObjects.Count; i++)
        {
            SignalScript ss = detonationObjects[i].GetComponent<SignalScript>();
            if (ss.state != SignalScript.STATE.NOTHING)
            {
                chain += 1; // �`�F�C������1�����Z����
                ScoreDirector.detonationStates.Add(ss.state);
                ss.BreakSignal(false);
            }
            ss.AddSetSignalPoint();
        }
        // ���g����ɂ���
        detonationObjects = new List<GameObject>();
        explotionObjects = new List<GameObject>();
    }

    /// <summary>
    /// �N���b�N���ꂽ�I�u�W�F�N�g��6�������`�F�b�N���A�`�F�C�����m�F����֐�
    /// </summary>
    /// <param name="gameObject">�N���b�N�����I�u�W�F�N�g</param>
    /// <returns>�`�F�C�����Ă��邩�ǂ���</returns>
    private bool CheckChainSignal(GameObject gameObject)
    {
        centerSignalTP = default; // 10�̈� ��
        centerSignalDP = default; // 1�̈� �s
        centerSignalSS = null; // �N���b�N�����I�u�W�F�N�g��SignalScript�i�[�p
        comparisonSignalSS = null; // �N���b�N���Ă��Ȃ��I�u�W�F�N�g��SignalScript�i�[�p

        // �N���b�N�����I�u�W�F�N�g���z����I�u�W�F�N�g�̂ǂ�Ȃ̂���T��
        SearchSignal(gameObject, ref centerSignalTP, ref centerSignalDP);
        // �N���b�N�����I�u�W�F�N�g��SignalScript���i�[
        centerSignalSS = signals[centerSignalTP * 10 + centerSignalDP].GetComponent<SignalScript>();

        if (centerSignalDP < rightMost) // �N���b�N�����I�u�W�F�N�g�̍s���E�[�łȂ���� 
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
        if (centerSignalTP > 0 && centerSignalDP < rightMost && centerSignalDP % 2 == 0) // �N���b�N�����I�u�W�F�N�g�̗񂪍ŏ�i���s���E�[�����܂肪0�Ȃ��
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
        if (centerSignalTP < bottom) // �N���b�N�����I�u�W�F�N�g�̗񂪍ŉ��i�łȂ����
        {
            // �N���b�N�����I�u�W�F�N�g��1����SignalScript���i�[
            comparisonSignalSS = signals[(centerSignalTP + 1) * 10 + centerSignalDP].GetComponent<SignalScript>();
            CheckSameSignal(centerSignalSS, comparisonSignalSS);
        }
        if (centerSignalTP < bottom && centerSignalDP < rightMost && centerSignalDP % 2 > 0) // �N���b�N�����I�u�W�F�N�g�̗񂪍ŉ��i���s���E�[�����܂肪0�𒴉߂���Ȃ��
        {
            // �N���b�N�����I�u�W�F�N�g��1�E����SignalScript���i�[
            comparisonSignalSS = signals[(centerSignalTP + 1) * 10 + centerSignalDP + 1].GetComponent<SignalScript>();
            CheckSameSignal(centerSignalSS, comparisonSignalSS);
        }
        if (centerSignalTP < bottom && centerSignalDP > 0 && centerSignalDP % 2 > 0) // �N���b�N�����I�u�W�F�N�g�̗񂪍ŉ��i���s�����[�����܂肪0�𒴉߂���Ȃ��
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

    /// <summary>
    ///  �Ώۂ̃I�u�W�F�N�g���z����I�u�W�F�N�g�̂ǂ�Ȃ̂���T���֐� ref...�������K�{
    /// </summary>
    /// <param name="gameObject">�N���b�N�����I�u�W�F�N�g</param>
    /// <param name="centerSignalTP">10�̈�</param>
    /// <param name="centerSignalDP">1�̈�</param>
    private void SearchSignal(GameObject gameObject, ref int centerSignalTP, ref int centerSignalDP)
    {
        for (int j = 0; j < column; j++) // ����`�F�b�N
        {
            for (int i = 0; i < row; i++) // �s���`�F�b�N
            {
                if (gameObject == signals[j * 10 + i]) // �Ώۂ̃I�u�W�F�N�g�Ɣz����̃I�u�W�F�N�g�������Ȃ�
                {
                    centerSignalTP = j; // ����i�[
                    centerSignalDP = i; // �s���i�[
                }
            }
        }
    }

    /// <summary>
    /// ����state���ǂ������m�F����֐�
    /// </summary>
    /// <param name="centerSignalSS">��r���̃I�u�W�F�N�g</param>
    /// <param name="comparisonSignalSS">��r��̃I�u�W�F�N�g</param>
    private void CheckSameSignal(SignalScript centerSignalSS, SignalScript comparisonSignalSS)
    {
        if (centerSignalSS.state == comparisonSignalSS.state) // state�������Ȃ�
        {
            chain++; // �`�F�C���J�E���g��1���₷
            comparisonSignalSS.BreakSignal(true);
        }
    }

    /// <summary>
    /// state��NOTHING�̃V�O�i���S�Ă�setSignalPoint��1���Z����֐�
    /// </summary>
    private void ResurrectionSignal()
    {
        if (!isChain) return; // �`�F�C�����Ă��Ȃ��Ȃ烊�^�[������
        for (int j = 0; j < column; j++) // ����`�F�b�N
        {
            for (int i = 0; i < row; i++) // �s���`�F�b�N
            {
                if (signals[j * 10 + i].GetComponent<SignalScript>().state == SignalScript.STATE.NOTHING) // ����Signal��state��NOTHING�Ȃ�
                {
                    signals[j * 10 + i].GetComponent<SignalScript>().AddSetSignalPoint(); // ����Signal��AddSetSignalPoint���Ăяo��
                }
            }
        }
    }

    /// <summary>
    /// �Q�b�g�����X�R�A��\������֐�
    /// </summary>
    /// <param name="clickedObject">�N���b�N�����I�u�W�F�N�g</param>
    private void ShowGetScore(GameObject clickedObject)
    {
        scoreTextObject = clickedObject.transform.GetChild(1).gameObject; // �N���b�N�����{�^���̃e�L�X�g���擾
        getScoreText = scoreTextObject.GetComponent<TextMeshPro>(); // TextMeshPro����
        getScoreText.text = string.Format("+ {0:0}", ScoreDirector.score); // �Q�b�g�����X�R�A����
        scoreTextObject.GetComponent<Animator>().SetTrigger("GetScore"); // �A�j���[�V�������Đ�
    }

    /// ------private�֐�------- ///
    /// --------�֐��ꗗ-------- ///
}
public partial class ButtonScript
{
    /// --------�ϐ��ꗗ-------- ///
    /// -------public�ϐ�------- ///

    public bool gameStart = false; // �Q�[�����X�^�[�g���Ă��邩�ǂ���

    public List<GameObject> specialSignals; // X���{���i�[�p

    public Image resetButton; // ���Z�b�g�{�^���ϐ�

    /// -------public�ϐ�------- ///
    /// -----protected�ϐ�------ ///



    /// -----protected�ϐ�------ ///
    /// ------private�ϐ�------- ///

    private bool isChain = false; // �`�F�C�����Ă��邩�ǂ���

    private int centerSignalTP = 0; // 10�̈� ��(�ʏ�V�O�i����)
    private int centerSignalDP = 0; // 1�̈� �s(�ʏ�V�O�i����)
    private int detonationSignalTP = 0; // 10�̈� ��(�U����)
    private int detonationSignalDP = 0; // 1�̈� �s(�U����)
    private int resetStock = 3; // ���Z�b�g��
    private int rightMost = 0; // �V�O�i���̕��т̉E�[
    private int bottom = 0; // �V�O�i���̕��т̍ŉ��i
    private int row = 0; // �s��
    private int column = 0; // ��

    private float chain = 0; // �`�F�C���ϐ�

    private GameObject scoreTextObject; // �e�L�X�g�̃I�u�W�F�N�g�ϐ�

    private SignalScript.STATE state; //SignalScript��STATE�ϐ�
    private SignalScript centerSignalSS; // �N���b�N�����I�u�W�F�N�g��SignalScript�i�[�p
    private SignalScript comparisonSignalSS; // �N���b�N���Ă��Ȃ��I�u�W�F�N�g��SignalScript�i�[�p

    [SerializeField] private AudioSource SEAudioSource; // SE�p�I�[�f�B�I�\�[�X

    [Header("[0]...�ʏ�V�O�i��SE,[1]...�{���V�O�i��")]
    [SerializeField] private AudioClip[] SEClips = new AudioClip[2];

    [SerializeField] private GameObject clickedGameObject; // �N���b�N�����I�u�W�F�N�g���i�[����ϐ�
    [SerializeField] private GameObject[] signals; // �V�O�i���i�[�p
    [SerializeField] private List<GameObject> detonationObjects; // �U�������V�O�i���i�[�p
    [SerializeField] private List<GameObject> explotionObjects; // ���������V�O�i���i�[�p

    [SerializeField] private ScoreDirector ScoreDirector; // ScoreDirector�ϐ�

    [SerializeField] private StarDirector StarDirector; // StarDirector�ϐ�

    [SerializeField] private TextMeshPro getScoreText; // �擾�����X�R�A��\������e�L�X�g�ϐ�

    [SerializeField] private Sprite[] resetImage = new Sprite[3]; // ���Z�b�g�{�^����image�ϐ�

    /// ------private�ϐ�------- ///
    /// -------�v���p�e�B------- ///



    /// -------�v���p�e�B------- ///
    /// --------�ϐ��ꗗ-------- ///
}