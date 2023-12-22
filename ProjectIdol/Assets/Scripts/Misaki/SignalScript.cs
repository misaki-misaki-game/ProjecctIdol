using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class SignalScript : MonoBehaviour
{
    // �F�������邽�߂̕ϐ�
    SpriteRenderer sp;
    public enum STATE // �X�e�[�^�X
    {
        NOTHING, // �V�O�i������ 0
        BLUE, // �V�O�i�� 1
        RED, // �ԃV�O�i�� 2
        WHITE, // ���V�O�i�� 3
        YELLOW // ���V�O�i�� 4
    }
    public float setSignalDelayTime = 20; // �V�O�i���ď����܂ł̃t���[���ϐ�
    public STATE state; // state�ϐ�

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
    public void BreakSignal() // �u���C�N�V�O�i���֐�
    {
        state= STATE.NOTHING; // state��NOTHING�ɂ���
        sp.color = new Color32(55, 52, 52, 255); // �V�O�i���̐F�������F�ɂ���(�O���[)
        StartCoroutine(DelayCoroutine()); // �f�B���C�R���[�`�����Ăяo��
    }
    private IEnumerator DelayCoroutine() // �f�B���C�R���[�`��
    {
        // delayTime�̒lF(�����l20F)���҂�
        for (var i = 0; i < setSignalDelayTime; i++)
        {
            yield return null;
        }
        // delayTime�̒lF�ɐF��ύX
        SetSignal();
    }
}
