using Unity.VisualScripting;
using UnityEngine;

public class JudgmentArea : MonoBehaviour
{
    [SerializeField] float radius;                      //�V�O�i���̔���̔��a��ݒ肷��
    [SerializeField] UiManager uiManager;               //UIManager���g�����߂̕ϐ�
    [SerializeField] GameObject textEffectPrefab;       //�V�O�i�����������Ƃ��ɕ\�����������e�L�X�g��ݒ肷�邽�߂̃Q�[���I�u�W�F�N�g
    [SerializeField] GameObject perfectEffectPrefab;    //���肪Perfect�̏ꍇ�̃G�t�F�N�g�̃v���n�u
    [SerializeField] GameObject nomalEffectPrefab;      //���肪Nomal�̏ꍇ�̃G�t�F�N�g�̃v���n�u
    [SerializeField] float delay = 0.1f;                //�I�u�W�F�N�g���A�N�e�B�u�ɂ��鎞��
    [SerializeField] private AudioClip se;               //���肪Perfect�̏ꍇ��SE
    AudioSource audioSource;

    private void Start()
    {
        // AudioSource �R���|�[�l���g�̎擾
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null)
            {
                if (hit.collider.tag == "BlueNotes")
                {
                    //audioSource.PlayOneShot(se);
                    SignalJudgment(transform.position, hit.collider.transform.position);
                    Destroy(hit.collider.gameObject);
                }
                if (hit.collider.tag == "RedNotes")
                {
                    //audioSource.PlayOneShot(se);
                    SignalJudgment(transform.position, hit.collider.transform.position);
                    Destroy(hit.collider.gameObject);
                }
                if (hit.collider.tag == "WhiteNotes")
                {
                    //audioSource.PlayOneShot(se);
                    SignalJudgment(transform.position, hit.collider.transform.position);
                    Destroy(hit.collider.gameObject);
                }
                if (hit.collider.tag == "YellowNotes")
                {
                    //audioSource.PlayOneShot(se);
                    SignalJudgment(transform.position, hit.collider.transform.position);
                    Destroy(hit.collider.gameObject);
                }
            }
        }
    }

    void SignalJudgment(Vector3 lanePosition, Vector3 notePosition)
    {
        RaycastHit2D[] hits2D = Physics2D.CircleCastAll(lanePosition, radius, Vector3.zero);
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
                SpawnTextEffect("Parfect", notePosition, Color.yellow, perfectEffectPrefab);
                Debug.Log("perfect");

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
                SpawnTextEffect("Nomal", notePosition, Color.red, nomalEffectPrefab);              //�V�O�i�����������ꏊ�ɐԐF��Nomal�ƕ\�����ANomal��p�̃G�t�F�N�g��\������
                Debug.Log("nomal");

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
                SpawnTextEffect("Miss", notePosition, Color.blue, null);                              //�V�O�i�����������ꏊ�ɐF��Miss�ƕ\������A�G�t�F�N�g�ASE�͖���
                Debug.Log("miss");
            }

            ////�Ԃ��������̂�j�󂷂�
            //Destroy(hit2D.collider.gameObject);
            //hit2D.collider.gameObject.SetActive(false);
        }
    }

    void SpawnTextEffect(string message, Vector3 position, Color color, GameObject effectPrefab)
    {
        //�V�O�i�����������ꏊ�ɔ���̃G�t�F�N�g�𐶐�����  ����ɂ���ĕ\������e�L�X�g�ƐF��ݒ肷��
        GameObject effect = Instantiate(textEffectPrefab, position + Vector3.up * 1.5f, Quaternion.identity);
        JudgmentEffect judgmentEffect = effect.GetComponent<JudgmentEffect>();
        judgmentEffect.SetText(message, color);

        if (effectPrefab != null)
        {
            //Perfect�G�t�F�N�g,Nomal�G�t�F�N�g��\������
            GameObject effectObject = Instantiate(effectPrefab, position, Quaternion.identity);
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

