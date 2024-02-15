using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;
using static ScoreDirector;
using TMPro;

public class SignalScript : MonoBehaviour
{
    SpriteRenderer sp; // �摜��؂�ւ���

    public enum STATE // �X�e�[�^�X
    {
        NOTHING, // �V�O�i������ 0
        BLUE, // �V�O�i�� 1
        RED, // �ԃV�O�i�� 2
        WHITE, // ���V�O�i�� 3
        YELLOW // ���V�O�i�� 4
    }
    public STATE state; // state�ϐ�
    public enum Effect // �G�t�F�N�g�X�e�[�^�X
    {
        RESURRECTIONEFFECT, // �V�O�i������������Ƃ� 0
        NOTHINGEFFECT, // �V�O�i�������̂Ƃ� 1
        BREAKEFFECT // �V�O�i���������ꂽ�Ƃ� 2
    }
    public Effect effectState; // �G�t�F�N�g�ϐ�
    public int setSignalPoint = 0; // �ăZ�b�g���邽�߂̃|�C���g�𐔂���ϐ�
    public float destroyDeleteTime = 1.0f; // �G�t�F�N�g�������܂ł̎��ԕϐ�
    [SerializeField] int needPoint = 3; // �V�O�i���ăZ�b�g�ɕK�v�ȃ|�C���g�ϐ�
    [EnumIndex(typeof(Effect))]
    public GameObject[] effects = new GameObject[3]; // �G�t�F�N�g�z��
    [EnumIndex(typeof(STATE))]
    public Sprite[] signals = new Sprite[5]; // �V�O�i���摜�z��


    // public float setSignalDelayTime = 20; // �V�O�i���ď����܂ł̃t���[���ϐ� �d�l�ύX�̂��߃R�����g��

    // Start is called before the first frame update
    void Start()
    {
        // SpriteRenderer�i�[
        sp= GetComponent<SpriteRenderer>();
        // �V�O�i�����Z�b�g����
        SetSignal();
    }

    // Update is called once per frame
    void Update()
    {
        if(setSignalPoint == needPoint && state == STATE.NOTHING) SetSignal(); // NOTHING�̃V�O�i�����ăZ�b�g�܂ł̃|�C���g��K�v���������Ă�����V�O�i�����Z�b�g����
    }

    public void SetSignal() // �Z�b�g�V�O�i���֐�
    {
        EffectDestroy(Effect.NOTHINGEFFECT); // NOTHINGEFFECT���̃G�t�F�N�g������
        effectState = Effect.RESURRECTIONEFFECT; // �G�t�F�N�g�X�e�[�^�X�𕜊�����Ƃ��ɕύX
        PlayEffect(effectState); // �G�t�F�N�g���Ăяo��
        int rnd = Random.Range(1, 5); // 1�`4�͈̔͂Ń����_��
        state = (STATE)Enum.ToObject(typeof(STATE), rnd); // state�������_���Őݒ�
        switch (state)
        {
            // state�ɂ���ăC���[�W��ύX
            case STATE.RED:
                sp.sprite = signals[2]; // �ԃV�O�i����ݒ�
                //sp.color = Color.red;
                break;
            case STATE.BLUE:
                sp.sprite = signals[1]; // �V�O�i����ݒ�
                //sp.color = Color.blue;
                break;
            case STATE.YELLOW:
                sp.sprite = signals[4]; // ���V�O�i����ݒ�
                //sp.color = Color.yellow;
                break;
            case STATE.WHITE:
                sp.sprite = signals[3]; // ���V�O�i����ݒ�
                //sp.color = Color.white;
                break;
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
    public void BreakSignal(bool isChain) // �u���C�N�V�O�i���֐�
    {
        effectState = Effect.BREAKEFFECT; // �G�t�F�N�g�X�e�[�^�X�������ꂽ�Ƃ��ɕύX
        PlayEffect(effectState); // �G�t�F�N�g���Ăяo��
        setSignalPoint = default; // setSignalPoint��defaultPoint��������
        if (isChain)
        {
            setSignalPoint -= 1; // �`�F�C�����Ă���ꍇ�̓V�O�i�����N���b�N�������_��AddSetSignalPoint���Ăяo����Ă��܂��̂ŁA
                                 // setSignalPoint��-1�ɂ��邱�ƂŒ���
        }
        state = STATE.NOTHING; // state��NOTHING�ɂ���
        sp.sprite = null; // �V�O�i���摜��null�ɂ���
        effectState = Effect.NOTHINGEFFECT; // �G�t�F�N�g�X�e�[�^�X���Ȃɂ��Ȃ��Ƃ��ɕύX
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
            case Effect.BREAKEFFECT: // �V�O�i���������ꂽ�Ƃ��̃G�t�F�N�g���Ăяo��
                clone = Instantiate(effects[2], // �Ȃɂ��������邩
                                               this.transform.position, // ��ʂ̂ǂ��ɏ������邩
                                               Quaternion.identity); // ���[�e�[�V�����͂ǂ�����̂�
                clone.transform.parent = this.transform; // �V�O�i���̎q�ɂ���
                Destroy(clone, destroyDeleteTime); // destroyDeleteTime�b��ɃG�t�F�N�g��j�󂷂�
                break;
        }
    }
}
