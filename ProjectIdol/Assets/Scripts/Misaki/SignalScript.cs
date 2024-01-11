using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class SignalScript : MonoBehaviour
{
    
    SpriteRenderer sp; // �F�������邽�߂̕ϐ�

    public enum STATE // �X�e�[�^�X
    {
        NOTHING, // �V�O�i������ 0
        BLUE, // �V�O�i�� 1
        RED, // �ԃV�O�i�� 2
        WHITE, // ���V�O�i�� 3
        YELLOW // ���V�O�i�� 4
    }
    public STATE state; // state�ϐ�
    public int setSignalPoint = 0; // �ăZ�b�g���邽�߂̃|�C���g�𐔂���ϐ�
    [SerializeField] int needPoint = 3; // �V�O�i���ăZ�b�g�ɕK�v�ȃ|�C���g�ϐ�


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
        int rnd = Random.Range(1, 5); // 1�`4�͈̔͂Ń����_��
        state = (STATE)Enum.ToObject(typeof(STATE), rnd); // state�������_���Őݒ�
        switch(state)
        {
            // state�ɂ���ĐF��ύX
            case STATE.RED:
                sp.color = Color.red;
                break;
            case STATE.BLUE:
                sp.color = Color.blue;
                break;
            case STATE.YELLOW:
                sp.color = Color.yellow;
                break;
            case STATE.WHITE:
                sp.color = Color.white;
                break;
        }
    }
    public void BreakSignal(bool isChain) // �u���C�N�V�O�i���֐�
    {
        setSignalPoint = default; // setSignalPoint��defaultPoint��������
        if (isChain)
        {
            setSignalPoint -= 1; // �`�F�C�����Ă���ꍇ�̓V�O�i�����N���b�N�������_��AddSetSignalPoint���Ăяo����Ă��܂��̂ŁA
                                  // setSignalPoint��-1�ɂ��邱�ƂŒ���
        }
        state = STATE.NOTHING; // state��NOTHING�ɂ���
        sp.color = new Color32(55, 52, 52, 0); // �V�O�i���̐F�𓧖��ɂ���(�O���[)
        // StartCoroutine(DelayCoroutine()); // �f�B���C�R���[�`�����Ăяo�� �d�l�ύX�̂��߃R�����g��
    }

    public void AddSetSignalPoint() // SetSignalPoint�����Z����֐�
    {
        setSignalPoint += 1;
    }

    /*
    private IEnumerator DelayCoroutine() // �f�B���C�R���[�`���@�d�l�ύX�̂��߃R�����g��
    {
        // delayTime�̒lF(�����l20F)���҂�
        for (var i = 0; i < setSignalDelayTime; i++)
        {
            yield return null;
        }
        // delayTime�̒lF�ɐF��ύX
        SetSignal();
    }
    */
}
