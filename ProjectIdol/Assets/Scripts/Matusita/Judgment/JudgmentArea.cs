using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;

public class JudgmentArea : MonoBehaviour
{
    [SerializeField] UiManager uiManager;
    [SerializeField] float radius;                      //�V�O�i���̔���̔��a��ݒ肷��
    [SerializeField] GameObject textEffectPrefab;       //�V�O�i�����������Ƃ��ɕ\�����������e�L�X�g��ݒ肷�邽�߂̃Q�[���I�u�W�F�N�g
    //[SerializeField] GameObject putEffectPrefab;
    [SerializeField] GameObject pereffectPerfab;
    [SerializeField] AudioSource SEaudio;
    [SerializeField] int perfectScorePoint = 3000;
    [SerializeField] int nomalScorePoint = 1500;
    [SerializeField] int perfectPoint = 2;
    [SerializeField] int nomalPoint = 1;

    private void Start()
    {
        // AudioSource �R���|�[�l���g�̎擾
        SEaudio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            //GameObject putEffect = Instantiate(putEffectPrefab, hit.collider.transform.position, Quaternion.identity);
            //StartCoroutine(effectOnToOff(putEffect));

            if (hit.collider != null)
            {
                // �G�t�F�N�g�v���n�u�𐶐�����
                GameObject perEffect = Instantiate(pereffectPerfab, hit.collider.transform.position, Quaternion.identity);
                StartCoroutine(effectOnToOff(perEffect));
                SEaudio.PlayOneShot(SEaudio.clip);

                if (hit.collider.tag == "BlueNotes"|| hit.collider.tag == "RedNotes"|| hit.collider.tag == "WhiteNotes"|| hit.collider.tag == "YellowNotes")
                {
                    SignalJudgment();
                    //SignalJudgment(transform.position, hit.collider.transform.position);
                    //SignalJudgment(hit.collider.transform.position);
                    //Destroy(hit.collider.gameObject);
                }
            }
        }
    }

    //void SignalJudgment(Vector3 lanePosition, Vector3 notePosition)
    //void SignalJudgment(Vector3 notePosition)
    void SignalJudgment()
    {
        //RaycastHit2D[] hits2D = Physics2D.CircleCastAll(lanePosition, radius, Vector3.zero);
        //RaycastHit2D[] Circlehits2D = Physics2D.CircleCastAll(transform.position, radius, Vector3.zero);
        //RaycastHit2D[] Circlehits2D = Physics2D.CircleCastAll(notePosition, radius, Vector3.zero);

        RaycastHit2D[] hits2D = Physics2D.CircleCastAll(transform.position, radius, Vector3.zero);


        //if (Circlehits2D.Length == 0)
        if (hits2D.Length == 0)
        {
            return;
        }

        RaycastHit2D hit2D = hits2D[0];

        if (hit2D)
        {
            //�߂��\��  ��Βl�ōl���� Mathf.Abs()
            float distance = Mathf.Abs(hit2D.transform.position.y - transform.position.y);
            Debug.Log("����:"+distance);
            if (distance < 4)
            {
                Debug.Log("���F�p�[�t�F�N�g");
                //�����V�O�i�����������̂����a4�ȉ��Ȃ�
                uiManager.AddScore(perfectScorePoint);      //�X�R�A��50���Z����
                uiManager.AddCombo();                       //�R���{��1���Z����
                uiManager.AddPerfectCounts();
                SpawnTextEffect("Perfect", hit2D.transform.position, Color.yellow);
                Debug.Log("perfect");

                switch (hit2D.collider.gameObject.tag)
                {
                    //�Ԃ������I�u�W�F�N�g�̃^�O�ɉ����ă|�C���g�����Z
                    case "BlueNotes":
                        uiManager.AddBluePoint(perfectPoint);
                        break;
                    case "RedNotes":
                        uiManager.AddRedPoint(perfectPoint);
                        break;
                    case "WhiteNotes":
                        uiManager.AddWhitePoint(perfectPoint);
                        break;
                    case "YellowNotes":
                        uiManager.AddYellowPoint(perfectPoint);
                        break;
                    default:
                        break;
                }
            }
            else if (distance < 5)
            {
                //�����V�O�i�����������̂����a7�ȉ��Ȃ�
                uiManager.AddScore(nomalScorePoint);    //�X�R�A��25���Z����
                uiManager.AddCombo();                   //�R���{��1���Z����
                SpawnTextEffect("Nomal",hit2D.transform.position, Color.red);
                Debug.Log("nomal");

                switch (hit2D.collider.gameObject.tag)
                {
                    //�Ԃ������I�u�W�F�N�g�̃^�O�ɉ����ă|�C���g�����Z
                    case "BlueNotes":
                        uiManager.AddBluePoint(nomalPoint);      //�u���[�V�O�i���̏ꍇ�A1�|�C���g���Z
                        break;
                    case "RedNotes":
                        uiManager.AddRedPoint(nomalPoint);       //���b�h�V�O�i���̏ꍇ�A1�|�C���g���Z
                        break;
                    case "WhiteNotes":
                        uiManager.AddWhitePoint(nomalPoint);     //�z���C�g�V�O�i���̏ꍇ�A1�|�C���g���Z
                        break;
                    case "YellowNotes":
                        uiManager.AddYellowPoint(nomalPoint);    //�C�G���[�V�O�i���̏ꍇ�A1�|�C���g���Z
                        break;
                    default:
                        break;
                }

            }
            else if (distance < 9)
            {
                //�����V�O�i�����������̂�����ȊO�Ȃ�
                uiManager.NoteMiss();                                   //�X�R�A��-25���Z����
                SpawnTextEffect("Miss",hit2D.transform.position, Color.blue);
                Debug.Log("miss");
            }

            ////�Ԃ��������̂�j�󂷂�
            Destroy(hit2D.collider.gameObject);
            hit2D.collider.gameObject.SetActive(false);
            Debug.Log("last");
        }
    }

    void SpawnTextEffect(string message, Vector3 position, Color color)
    {
        //�V�O�i�����������ꏊ�ɔ���̃G�t�F�N�g�𐶐�����  ����ɂ���ĕ\������e�L�X�g�ƐF��ݒ肷��
        GameObject effect = Instantiate(textEffectPrefab, position + Vector3.up * 1.5f, Quaternion.identity);
        JudgmentEffect judgmentEffect = effect.GetComponent<JudgmentEffect>();
        judgmentEffect.SetText(message, color);
    }

    //�����c�[��
    void OnDrawGizmosSelected()
    {
        //����ꏊ��Ԃ��\��������
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, radius);
    }
    IEnumerator effectOnToOff(GameObject effect)
    {
        yield return new WaitForSeconds(1f); // 1�b�҂�
        //effect.SetActive(false); // �G�t�F�N�g���A�N�e�B�u�ɂ���
        Destroy(effect);
    }

}

