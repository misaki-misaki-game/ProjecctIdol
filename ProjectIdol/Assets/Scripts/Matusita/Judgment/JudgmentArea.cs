using UnityEngine;

public class JudgmentArea : MonoBehaviour
{
    [SerializeField] float radius;                      //�V�O�i���̔���̔��a��ݒ肷��
    [SerializeField] KeyCode keyCode;                   //Unity��Őݒ肵��KeyCode���g�p����
    [SerializeField] UiManager uiManager;               //UIManager���g�����߂̕ϐ�
    [SerializeField] GameObject textEffectPrefab;       //�V�O�i�����������Ƃ��ɕ\�����������e�L�X�g��ݒ肷�邽�߂̃Q�[���I�u�W�F�N�g
    [SerializeField] GameObject perfectEffectPrefab;    //���肪Perfect�̏ꍇ�̃G�t�F�N�g�̃v���n�u
    [SerializeField] GameObject nomalEffectPrefab;      //���肪Nomal�̏ꍇ�̃G�t�F�N�g�̃v���n�u
    [SerializeField] AudioClip perfectSE;               //���肪Perfect�̏ꍇ��SE
    [SerializeField] AudioClip nomalSE;                 //���肪Nomal�̏ꍇ��SE
    [SerializeField] float delay = 0.1f;                //�I�u�W�F�N�g���A�N�e�B�u�ɂ��鎞��

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            SignalJudgment();
        }
        //if (Input.GetKeyDown(keyCode))
        //{
        //    //Unity��Őݒ肳�ꂽKeyCode�������ꂽ�ꍇSignalJudgment()�����s�����
        //    SignalJudgment();
        //}
    }

    void SignalJudgment()
    {
        RaycastHit2D[] hits2D = Physics2D.CircleCastAll(transform.position, radius, Vector3.zero);

        if (hits2D.Length == 0)
        {
            return;
        }

        RaycastHit2D hit2D = hits2D[0];
        if (hit2D)
        {
            //�߂��\��  ��Βl�ōl���� Mathf.Abs()
            float distance = Mathf.Abs(hit2D.transform.position.y - transform.position.y);

            if (distance < 4)
            {
                //�����V�O�i�����������̂����a4�ȉ��Ȃ�
                uiManager.AddScore(3000);                                                                               //UIManager��AddScore���g�p���ăX�R�A��50���Z����
                uiManager.AddCombo();                                                                                   //UIManager��AddCombo���g�p���ăR���{��1���Z����
                SpawnTextEffect("Parfect", hit2D.transform.position, Color.yellow, perfectEffectPrefab, perfectSE);     //�V�O�i�����������ꏊ�ɉ��F��Parfect�ƕ\�����AParfect��p�̃G�t�F�N�g��\������

                switch (hit2D.collider.gameObject.tag)
                {
                //�Ԃ������I�u�W�F�N�g�̃^�O�ɉ����ă|�C���g�����Z
                    case "BlueNotes":
                        uiManager.AddBluePoint(2);      //�u���[�V�O�i���̏ꍇ�A2�|�C���g���Z
                        break;
                    case "RedNotes":
                        uiManager.AddRedPoint(2);       //���b�h�V�O�i���̏ꍇ�A2�|�C���g���Z
                        break;
                    case "WhiteNotes":
                        uiManager.AddWhitePoint(2);     //�z���C�g�V�O�i���̏ꍇ�A2�|�C���g���Z
                        break;
                    case "YellowNotes":
                        uiManager.AddYellowPoint(2);    //�C�G���[�V�O�i���̏ꍇ�A2�|�C���g���Z
                        break;
                    default:
                        break;
                }
            }
            else if (distance < 7)
            {
                //�����V�O�i�����������̂����a7�ȉ��Ȃ�
                uiManager.AddScore(1500);                                                                               //UIManager��AddScore���g�p���ăX�R�A��25���Z����
                uiManager.AddCombo();                                                                                   //UIManager��AddCombo���g�p���ăR���{��1���Z����
                SpawnTextEffect("Nomal", hit2D.transform.position, Color.red, nomalEffectPrefab, nomalSE);              //�V�O�i�����������ꏊ�ɐԐF��Nomal�ƕ\�����ANomal��p�̃G�t�F�N�g��\������

                switch (hit2D.collider.gameObject.tag)
                {
                //�Ԃ������I�u�W�F�N�g�̃^�O�ɉ����ă|�C���g�����Z
                    case "BlueNotes":
                        uiManager.AddBluePoint(1);      //�u���[�V�O�i���̏ꍇ�A1�|�C���g���Z
                        break;
                    case "RedNotes":
                        uiManager.AddRedPoint(1);       //���b�h�V�O�i���̏ꍇ�A1�|�C���g���Z
                        break;
                    case "WhiteNotes":
                        uiManager.AddWhitePoint(1);     //�z���C�g�V�O�i���̏ꍇ�A1�|�C���g���Z
                        break;
                    case "YellowNotes":
                        uiManager.AddYellowPoint(1);    //�C�G���[�V�O�i���̏ꍇ�A1�|�C���g���Z
                        break;
                    default:
                        break;
                }
            }
            else
            {
                //�����V�O�i�����������̂�����ȊO�Ȃ�
                uiManager.NoteMiss();                                                                                   //UIManager��NoteMiss���g�p���ăX�R�A��-25���Z����
                SpawnTextEffect("Miss", hit2D.transform.position, Color.blue, null, null);                              //�V�O�i�����������ꏊ�ɐF��Miss�ƕ\������A�G�t�F�N�g�ASE�͖���
            }


            //�Ԃ��������̂�j�󂷂�
            Destroy(hit2D.collider.gameObject);
            hit2D.collider.gameObject.SetActive(false);
        }
    }

    void SpawnTextEffect(string message, Vector3 position, Color color, GameObject effectPrefab, AudioClip se)
    {
        //�V�O�i�����������ꏊ�ɔ���̃G�t�F�N�g�𐶐�����  ����ɂ���ĕ\������e�L�X�g�ƐF��ݒ肷��
        GameObject effect = Instantiate(textEffectPrefab, position + Vector3.up * 1.5f, Quaternion.identity);
        JudgmentEffect judgmentEffect = effect.GetComponent<JudgmentEffect>();
        judgmentEffect.SetText(message, color);

        if (effectPrefab != null && se != null)
        {
            //Perfect�G�t�F�N�g��SE�ANomal�G�t�F�N�g��SE��\������
            GameObject effectObject = Instantiate(effectPrefab, position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(se, position);
            Destroy(effectObject, 1.0f);
        }
    }

    //�����c�[��
    void OnDrawGizmosSelected()
    {
        //����ꏊ��Ԃ��\��������
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, radius);
    }
}
