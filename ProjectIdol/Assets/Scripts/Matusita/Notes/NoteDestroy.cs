using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteDestroy : MonoBehaviour
{
    //JudgmentArea judgmentArea;
    //MonoBehaviour
    //private UiManager uiManagerCS;
    //public void Start()
    //{
    //    uiManagerCS = GetComponent<UiManager>();
    //}

    //[SerializeField] GameObject textEffectPrefab;
    //float radius = 8;

    //public void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

    //        if (hit.collider != null)
    //        {
    //            Debug.Log("a");
    //            if (hit.collider.gameObject == gameObject)
    //            {
    //                SignalJudgment();
    //            }
    //        }
    //    }
    //}


    //public void SignalJudgment()
    //{
    //    RaycastHit2D[] hits2D = Physics2D.CircleCastAll(transform.position, radius, Vector3.zero);

    //    if (hits2D.Length == 0)
    //    {
    //        return;
    //    }

    //    RaycastHit2D hit2D = hits2D[0];
    //    if (hit2D)
    //    {
    //        //�߂��\��  ��Βl�ōl���� Mathf.Abs()
    //        float distance = Mathf.Abs(hit2D.transform.position.y - transform.position.y);

    //        if (distance < 4)
    //        {
    //            //uiManagerCS.AddScore(3000);                                                                               //UIManager��AddScore���g�p���ăX�R�A��50���Z����
    //            //uiManagerCS.AddCombo();                                                                                   //UIManager��AddCombo���g�p���ăR���{��1���Z����
    //            //SpawnTextEffect("Parfect", hit2D.transform.position, Color.yellow, perfectEffectPrefab, perfectSE);     //�V�O�i�����������ꏊ�ɉ��F��Parfect�ƕ\�����AParfect��p�̃G�t�F�N�g��\������
    //            //SpawnTextEffect("Parfect", hit2D.transform.position, Color.yellow);     //�V�O�i�����������ꏊ�ɉ��F��Parfect�ƕ\�����AParfect��p�̃G�t�F�N�g��\������

    //            switch (hit2D.collider.gameObject.tag)
    //            {
    //                //�Ԃ������I�u�W�F�N�g�̃^�O�ɉ����ă|�C���g�����Z
    //                case "BlueNotes":
    //                    //uiManagerCS.AddBluePoint(2);      //�u���[�V�O�i���̏ꍇ�A2�|�C���g���Z
    //                    break;
    //                case "RedNotes":
    //                    //uiManagerCS.AddRedPoint(2);       //���b�h�V�O�i���̏ꍇ�A2�|�C���g���Z
    //                    break;
    //                case "WhiteNotes":
    //                    //uiManagerCS.AddWhitePoint(2);     //�z���C�g�V�O�i���̏ꍇ�A2�|�C���g���Z
    //                    break;
    //                case "YellowNotes":
    //                    //uiManagerCS.AddYellowPoint(2);    //�C�G���[�V�O�i���̏ꍇ�A2�|�C���g���Z
    //                    break;
    //                default:
    //                    break;
    //            }
    //        }
    //        else if (distance < 7)
    //        {
    //            //uiManagerCS.AddScore(1500);                                                                               //UIManager��AddScore���g�p���ăX�R�A��25���Z����
    //            //uiManagerCS.AddCombo();                                                                                   //UIManager��AddCombo���g�p���ăR���{��1���Z����
    //            //SpawnTextEffect("Nomal", hit2D.transform.position, Color.red, nomalEffectPrefab, nomalSE);              //�V�O�i�����������ꏊ�ɐԐF��Nomal�ƕ\�����ANomal��p�̃G�t�F�N�g��\������
    //            //SpawnTextEffect("Nomal", hit2D.transform.position, Color.red);              //�V�O�i�����������ꏊ�ɐԐF��Nomal�ƕ\�����ANomal��p�̃G�t�F�N�g��\������

    //            switch (hit2D.collider.gameObject.tag)
    //            {
    //                //�Ԃ������I�u�W�F�N�g�̃^�O�ɉ����ă|�C���g�����Z
    //                case "BlueNotes":
    //                    //uiManagerCS.AddBluePoint(1);      //�u���[�V�O�i���̏ꍇ�A1�|�C���g���Z
    //                    break;
    //                case "RedNotes":
    //                    //uiManagerCS.AddRedPoint(1);       //���b�h�V�O�i���̏ꍇ�A1�|�C���g���Z
    //                    break;
    //                case "WhiteNotes":
    //                    //uiManagerCS.AddWhitePoint(1);     //�z���C�g�V�O�i���̏ꍇ�A1�|�C���g���Z
    //                    break;
    //                case "YellowNotes":
    //                    //uiManagerCS.AddYellowPoint(1);    //�C�G���[�V�O�i���̏ꍇ�A1�|�C���g���Z
    //                    break;
    //                default:
    //                    break;
    //            }
    //        }
    //        else if (distance < 10)
    //        {
    //            //uiManagerCS.NoteMiss();                                                                                   //UIManager��NoteMiss���g�p���ăX�R�A��-25���Z����
    //            //SpawnTextEffect("Miss", hit2D.transform.position, Color.blue, null, null);                              //�V�O�i�����������ꏊ�ɐF��Miss�ƕ\������A�G�t�F�N�g�ASE�͖���
    //            //SpawnTextEffect("Miss", hit2D.transform.position, Color.blue);                              //�V�O�i�����������ꏊ�ɐF��Miss�ƕ\������A�G�t�F�N�g�ASE�͖���
    //        }

    //        //�Ԃ��������̂�j�󂷂�
    //        Destroy(hit2D.collider.gameObject);
    //        hit2D.collider.gameObject.SetActive(false);
    //    }
    //}

    ////public void SpawnTextEffect(string message, Vector3 position, Color color, GameObject effectPrefab, AudioClip se)
    //public void SpawnTextEffect(string message, Vector3 position, Color color)
    //{
    //    //�V�O�i�����������ꏊ�ɔ���̃G�t�F�N�g�𐶐�����  ����ɂ���ĕ\������e�L�X�g�ƐF��ݒ肷��
    //    GameObject effect = Instantiate(textEffectPrefab, position + Vector3.up * 1.5f, Quaternion.identity);
    //    JudgmentEffect judgmentEffect = effect.GetComponent<JudgmentEffect>();
    //    judgmentEffect.SetText(message, color);

    //    //if (effectPrefab != null && se != null)
    //    //{
    //    //    //Perfect�G�t�F�N�g��SE�ANomal�G�t�F�N�g��SE��\������
    //    //    GameObject effectObject = Instantiate(effectPrefab, position, Quaternion.identity);
    //    //    AudioSource.PlayClipAtPoint(se, position);
    //    //    Destroy(effectObject, 1.0f);
    //    //}

    //}

    //�J�����������Ȃ��Ȃ�����
        void OnBecameInvisible()
        {
            GameObject.Destroy(this.gameObject);
        }
    
}


