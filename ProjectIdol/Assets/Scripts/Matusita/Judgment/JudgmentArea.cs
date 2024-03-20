using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;

public class JudgmentArea : MonoBehaviour
{
    [SerializeField] float radius;                      //シグナルの判定の半径を設定する
    [SerializeField] UiManager uiManager;               //UIManagerを使うための変数
    [SerializeField] GameObject textEffectPrefab;       //シグナルを消せたときに表示させたいテキストを設定するためのゲームオブジェクト
    //[SerializeField] GameObject putEffectPrefab;    //判定がPerfectの場合のエフェクトのプレハブ
    [SerializeField] GameObject pereffectPerfab;      //判定がNomalの場合のエフェクトのプレハブ
    [SerializeField] float delay = 0.1f;                //オブジェクトを非アクティブにする時間
    [SerializeField] AudioSource SEaudio;
    [SerializeField] int perfectScorePoint = 3000;
    [SerializeField] int nomalScorePoint = 1500;
    [SerializeField] int perfectPoint = 2;
    [SerializeField] int nomalPoint = 1;

    private void Start()
    {
        // AudioSource コンポーネントの取得
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
                // エフェクトプレハブを生成する
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

        //RaycastHit2D hit2D = Circlehits2D[0];
        RaycastHit2D hit2D = hits2D[0];

        if (hit2D)
        {
            //近さ表示  絶対値で考える Mathf.Abs()
            float distance = Mathf.Abs(hit2D.transform.position.y - transform.position.y);
            //float distance = Mathf.Abs(hit2D.transform.position.x - transform.position.x);

            if (distance < 5)
            {
                Debug.Log("黄色パーフェクト");
                //もしシグナルを消したのが半径4以下なら
                uiManager.AddScore(perfectScorePoint);                                                                               //UIManagerのAddScoreを使用してスコアに50加算する
                uiManager.AddCombo();                                                                                   //UIManagerのAddComboを使用してコンボに1加算する
                //SpawnTextEffect("Parfect", notePosition, Color.yellow);
                SpawnTextEffect("Parfect", hit2D.transform.position, Color.yellow);
                Debug.Log("perfect");

                switch (hit2D.collider.gameObject.tag)
                {
                    //ぶつかったオブジェクトのタグに応じてポイントを加算
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
            else if (distance < 7)
            {
                //もしシグナルを消したのが半径7以下なら
                uiManager.AddScore(nomalScorePoint);                                                                               //UIManagerのAddScoreを使用してスコアに25加算する
                uiManager.AddCombo();                                                                                   //UIManagerのAddComboを使用してコンボに1加算する
                //SpawnTextEffect("Nomal", notePosition, Color.red);              //シグナルを消した場所に赤色でNomalと表示し、Nomal専用のエフェクトを表示する
                SpawnTextEffect("Nomal", hit2D.transform.position, Color.red);
                Debug.Log("nomal");

                switch (hit2D.collider.gameObject.tag)
                {
                    //ぶつかったオブジェクトのタグに応じてポイントを加算
                    case "BlueNotes":
                        uiManager.AddBluePoint(nomalPoint);      //ブルーシグナルの場合、1ポイント加算
                        break;
                    case "RedNotes":
                        uiManager.AddRedPoint(nomalPoint);       //レッドシグナルの場合、1ポイント加算
                        break;
                    case "WhiteNotes":
                        uiManager.AddWhitePoint(nomalPoint);     //ホワイトシグナルの場合、1ポイント加算
                        break;
                    case "YellowNotes":
                        uiManager.AddYellowPoint(nomalPoint);    //イエローシグナルの場合、1ポイント加算
                        break;
                    default:
                        break;
                }

            }
            else
            {
                //もしシグナルを消したのがそれ以外なら
                uiManager.NoteMiss();                                                                                   //UIManagerのNoteMissを使用してスコアに-25加算する
                //SpawnTextEffect("Miss", notePosition, Color.blue);                              //シグナルを消した場所に青色でMissと表示する、エフェクト、SEは無し
                SpawnTextEffect("Miss", hit2D.transform.position, Color.blue);
                Debug.Log("miss");
            }

            ////ぶつかったものを破壊する
            Destroy(hit2D.collider.gameObject);
            hit2D.collider.gameObject.SetActive(false);
        }
    }

    void SpawnTextEffect(string message, Vector3 position, Color color)
    {
        //シグナルを消した場所に判定のエフェクトを生成する  判定によって表示するテキストと色を設定する
        GameObject effect = Instantiate(textEffectPrefab, position + Vector3.up * 1.5f, Quaternion.identity);
        JudgmentEffect judgmentEffect = effect.GetComponent<JudgmentEffect>();
        judgmentEffect.SetText(message, color);
    }

    //可視化ツール
    void OnDrawGizmosSelected()
    {
        //判定場所を赤く表示させる
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, radius);
    }
    IEnumerator effectOnToOff(GameObject effect)
    {
        yield return new WaitForSeconds(1f); // 1秒待つ
        //effect.SetActive(false); // エフェクトを非アクティブにする
        Destroy(effect);
    }

}

