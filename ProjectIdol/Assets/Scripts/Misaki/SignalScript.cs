using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;
using static ScoreDirector;
using TMPro;

public class SignalScript : MonoBehaviour
{
    bool isBomb = false; // ���̃V�O�i�����{���ɂȂ邩�ǂ����̐^�U
    int bombMax = 3; // X���{���̌����
    SpriteRenderer sp; // �摜��؂�ւ���
    ButtonScript buttonScript; // ButtonScript�ϐ�

    public enum STATE // �X�e�[�^�X
    {
        NOTHING, // �V�O�i������ 0
        BLUE, // �V�O�i�� 1
        RED, // �ԃV�O�i�� 2
        WHITE, // ���V�O�i�� 3
        YELLOW, // ���V�O�i�� 4
        SPECIAL // X���{�� 5
    }
    public STATE state; // state�ϐ�
    public enum Effect // �G�t�F�N�g�X�e�[�^�X
    {
        RESURRECTIONEFFECT, // �V�O�i������������Ƃ� 0
        NOTHINGEFFECT, // �V�O�i�������̂Ƃ� 1
        BOMBSETEFFECT, // �V�O�i�����������{�����Z�b�g����Ƃ� 2
        BREAKEFFECT // �V�O�i���������ꂽ�Ƃ� 3
    }
    public Effect effectState; // �G�t�F�N�g�ϐ�
    public int setSignalPoint = 0; // �ăZ�b�g���邽�߂̃|�C���g�𐔂���ϐ�
    public float destroyDeleteTime = 1.0f; // �G�t�F�N�g�������܂ł̎��ԕϐ�
    public const float bombRequirement = 4; // �{�����Z�b�g��������ϐ�
    [SerializeField] int needPoint = 3; // �V�O�i���ăZ�b�g�ɕK�v�ȃ|�C���g�ϐ�
    [EnumIndex(typeof(Effect))]
    public GameObject[] effects = new GameObject[4]; // �G�t�F�N�g�z��
    [EnumIndex(typeof(STATE))]
    public Sprite[] signals = new Sprite[6]; // �V�O�i���摜�z��

    // Start is called before the first frame update
    void Start()
    {
        // SpriteRenderer�i�[
        sp= GetComponent<SpriteRenderer>();
        // ButtonScript�i�[
        buttonScript = GameObject.FindGameObjectWithTag("GameDirector").GetComponent<ButtonScript>();
        // �V�O�i�����Z�b�g����
        SetSignal();
    }

    // Update is called once per frame
    void Update()
    {
        if(setSignalPoint == needPoint && state == STATE.NOTHING) SetSignal(isBomb); // NOTHING�̃V�O�i�����ăZ�b�g�܂ł̃|�C���g��K�v���������Ă�����V�O�i�����Z�b�g����
    }

    /// <summary>
    /// �Z�b�g�V�O�i���֐�
    /// </summary>
    /// <param name="isBomb">�{���𐶐����邩�ǂ���</param>
    public void SetSignal(bool isBomb = false)
    {
        EffectDestroy(Effect.NOTHINGEFFECT); // NOTHINGEFFECT���̃G�t�F�N�g������
        EffectDestroy(Effect.BOMBSETEFFECT); // BOMBSETEFFECT���̃G�t�F�N�g������
        effectState = Effect.RESURRECTIONEFFECT; // �G�t�F�N�g�X�e�[�^�X�𕜊�����Ƃ��ɕύX
        PlayEffect(effectState); // �G�t�F�N�g���Ăяo��
        if (!isBomb) // �{������������Ȃ��ꍇ
        {
            int rnd = Random.Range(1, 5); // 1�`4�͈̔͂Ń����_��
            state = (STATE)Enum.ToObject(typeof(STATE), rnd); // state�������_���Őݒ�
            switch (state)
            {
                // state�ɂ���ăC���[�W��ύX
                case STATE.RED:
                    sp.sprite = signals[2]; // �ԃV�O�i����ݒ�
                    break;
                case STATE.BLUE:
                    sp.sprite = signals[1]; // �V�O�i����ݒ�
                    break;
                case STATE.YELLOW:
                    sp.sprite = signals[4]; // ���V�O�i����ݒ�
                    break;
                case STATE.WHITE:
                    sp.sprite = signals[3]; // ���V�O�i����ݒ�
                    break;
            }
        }
        else // �{�������������ꍇ
        {
            state = STATE.SPECIAL; // STATE��X���{���ɐݒ�
            buttonScript.specialSignals.Add(this.gameObject); // �{�����i�[����
            sp.sprite = signals[5]; // X���{����ݒ�
            // �{���̌������𒴂��Ă�����
            if (buttonScript.specialSignals.Count > bombMax ) 
            {
                // �����Ƃ��Â��{�����������j����
                buttonScript.BombTimeOver(buttonScript.specialSignals[0]);
            }
        }
    }
    private void EffectDestroy(Effect effectCondition) // �������Ɠ����G�t�F�N�g�X�e�[�^�X�Ȃ�q�I�u�W�F�N�g(�G�t�F�N�g)��j�󂷂�֐�
    {
        if (effectState == effectCondition) // �G�t�F�N�g�X�e�[�^�X���������Ɠ����Ȃ�
        {
            GameObject child = transform.GetChild(2).gameObject; // �q�I�u�W�F�N�g(�G�t�F�N�g)������
            Destroy(child); // �q�I�u�W�F�N�g��j�󂷂�
        }
    }
    public void BreakSignal(bool isChain, float chain = 0) // �u���C�N�V�O�i���֐�
    {
        if (state == STATE.NOTHING) return; // state��NOTHING�����^�[������
        if (state == STATE.SPECIAL) // state��SPECIAL�Ȃ�
        {
            // X���{�����X�g���玩������菜��
            buttonScript.specialSignals.Remove(this.gameObject);
        }
        setSignalPoint = default; // setSignalPoint��defaultPoint��������
        if (isChain)
        {
            setSignalPoint -= 1; // �`�F�C�����Ă���ꍇ�̓V�O�i�����N���b�N�������_��AddSetSignalPoint���Ăяo����Ă��܂��̂ŁA
                                 // setSignalPoint��-1�ɂ��邱�ƂŒ���
        }
        effectState = Effect.BREAKEFFECT; // �G�t�F�N�g�X�e�[�^�X�������ꂽ�Ƃ��ɕύX
        PlayEffect(effectState); // �G�t�F�N�g���Ăяo��
        if (chain >= bombRequirement && state != STATE.SPECIAL) // �`�F�C�������{�����Z�b�g���������葽���ꍇ ���� state��SPECIAL�ȊO�̏ꍇ
        {
            effectState = Effect.BOMBSETEFFECT; // �G�t�F�N�g�X�e�[�^�X���V�O�i�����������{�����Z�b�g����Ƃ��ɕύX
            isBomb = true; // �{���ɂ��邽�߂�true�ɂ���
        }
        else // ����ȊO
        {
            effectState = Effect.NOTHINGEFFECT; // �G�t�F�N�g�X�e�[�^�X���Ȃɂ��Ȃ��Ƃ��ɕύX
            isBomb = false; // �{���ɂ��Ȃ����߂�true�ɂ���
        }
        state = STATE.NOTHING; // state��NOTHING�ɂ���
        sp.sprite = null; // �V�O�i���摜��null�ɂ���
        PlayEffect(effectState); // �G�t�F�N�g���Ăяo��
    }
    public void AddSetSignalPoint() // SetSignalPoint�����Z����֐�
    {
        setSignalPoint += 1;
    }
    private void PlayEffect(Effect effectState) // �G�t�F�N�g���Đ�����֐�
    {
        GameObject clone = null; // GameObject�𐶐�
        switch (effectState)
        {
            case Effect.RESURRECTIONEFFECT: // �V�O�i�������������Ƃ��̃G�t�F�N�g���Ăяo��
                clone = Instantiate(effects[0], // �Ȃɂ��������邩
                                               this.transform.position, // ��ʂ̂ǂ��ɏ������邩
                                               Quaternion.identity); // ���[�e�[�V�����͂ǂ�����̂�
                clone.transform.parent = this.transform; // �V�O�i���̎q�ɂ���
                Destroy(clone, destroyDeleteTime); // destroyDeleteTime�b��ɃG�t�F�N�g��j�󂷂�
                break;
            case Effect.NOTHINGEFFECT: // �V�O�i�����Ȃ��Ƃ��̃G�t�F�N�g���Ăяo��(��������܂ŃG�t�F�N�g�𔭐�����������)
                clone = Instantiate(effects[1], // �Ȃɂ��������邩
                                               this.transform.position, // ��ʂ̂ǂ��ɏ������邩
                                               Quaternion.identity); // ���[�e�[�V�����͂ǂ�����̂�
                clone.transform.parent = this.transform; // �V�O�i���̎q�ɂ���
                break;
            case Effect.BOMBSETEFFECT: // �V�O�i�����������{�����Z�b�g����Ƃ��̃G�t�F�N�g���Ăяo��(��������܂ŃG�t�F�N�g�𔭐�����������)
                clone = Instantiate(effects[2], // �Ȃɂ��������邩
                                               this.transform.position, // ��ʂ̂ǂ��ɏ������邩
                                               Quaternion.identity); // ���[�e�[�V�����͂ǂ�����̂�
                clone.transform.parent = this.transform; // �V�O�i���̎q�ɂ���
                break;
            case Effect.BREAKEFFECT: // �V�O�i���������ꂽ�Ƃ��̃G�t�F�N�g���Ăяo��
                clone = Instantiate(effects[3], // �Ȃɂ��������邩
                                               this.transform.position, // ��ʂ̂ǂ��ɏ������邩
                                               Quaternion.identity); // ���[�e�[�V�����͂ǂ�����̂�
                clone.transform.parent = this.transform; // �V�O�i���̎q�ɂ���
                Destroy(clone, destroyDeleteTime); // destroyDeleteTime�b��ɃG�t�F�N�g��j�󂷂�
                break;
        }
    }
}
