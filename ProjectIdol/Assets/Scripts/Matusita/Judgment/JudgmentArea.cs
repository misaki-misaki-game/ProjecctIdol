using Unity.VisualScripting;
using UnityEngine;

public class JudgmentArea : MonoBehaviour
{
    [SerializeField] float radius;                      //シグナルの判定の半径を設定する
    [SerializeField] UiManager uiManager;               //UIManagerを使うための変数
    [SerializeField] GameObject textEffectPrefab;       //シグナルを消せたときに表示させたいテキストを設定するためのゲームオブジェクト
    [SerializeField] GameObject perfectEffectPrefab;    //判定がPerfectの場合のエフェクトのプレハブ
    [SerializeField] GameObject nomalEffectPrefab;      //判定がNomalの場合のエフェクトのプレハブ
    [SerializeField] float delay = 0.1f;                //オブジェクトを非アクティブにする時間
    [SerializeField] private AudioClip se;               //判定がPerfectの場合のSE
    AudioSource audioSource;

    private void Start()
    {
        // AudioSource コンポーネントの取得
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
            //近さ表示  絶対値で考える Mathf.Abs()
            float distance = Mathf.Abs(hit2D.transform.position.y - transform.position.y);

            if (distance < 4)
            {
                //もしシグナルを消したのが半径4以下なら
                uiManager.AddScore(3000);                                                                               //UIManagerのAddScoreを使用してスコアに50加算する
                uiManager.AddCombo();                                                                                   //UIManagerのAddComboを使用してコンボに1加算する
                SpawnTextEffect("Parfect", notePosition, Color.yellow, perfectEffectPrefab);
                Debug.Log("perfect");

                switch (hit2D.collider.gameObject.tag)
                {
                    //ぶつかったオブジェクトのタグに応じてポイントを加算
                    case "BlueNotes":
                        uiManager.AddBluePoint(2);      //ブルーシグナルの場合、2ポイント加算
                        break;
                    case "RedNotes":
                        uiManager.AddRedPoint(2);       //レッドシグナルの場合、2ポイント加算
                        break;
                    case "WhiteNotes":
                        uiManager.AddWhitePoint(2);     //ホワイトシグナルの場合、2ポイント加算
                        break;
                    case "YellowNotes":
                        uiManager.AddYellowPoint(2);    //イエローシグナルの場合、2ポイント加算
                        break;
                    default:
                        break;
                }
            }
            else if (distance < 7)
            {
                //もしシグナルを消したのが半径7以下なら
                uiManager.AddScore(1500);                                                                               //UIManagerのAddScoreを使用してスコアに25加算する
                uiManager.AddCombo();                                                                                   //UIManagerのAddComboを使用してコンボに1加算する
                SpawnTextEffect("Nomal", notePosition, Color.red, nomalEffectPrefab);              //シグナルを消した場所に赤色でNomalと表示し、Nomal専用のエフェクトを表示する
                Debug.Log("nomal");

                switch (hit2D.collider.gameObject.tag)
                {
                    //ぶつかったオブジェクトのタグに応じてポイントを加算
                    case "BlueNotes":
                        uiManager.AddBluePoint(1);      //ブルーシグナルの場合、1ポイント加算
                        break;
                    case "RedNotes":
                        uiManager.AddRedPoint(1);       //レッドシグナルの場合、1ポイント加算
                        break;
                    case "WhiteNotes":
                        uiManager.AddWhitePoint(1);     //ホワイトシグナルの場合、1ポイント加算
                        break;
                    case "YellowNotes":
                        uiManager.AddYellowPoint(1);    //イエローシグナルの場合、1ポイント加算
                        break;
                    default:
                        break;
                }
            }
            else
            {
                //もしシグナルを消したのがそれ以外なら
                uiManager.NoteMiss();                                                                                   //UIManagerのNoteMissを使用してスコアに-25加算する
                SpawnTextEffect("Miss", notePosition, Color.blue, null);                              //シグナルを消した場所に青色でMissと表示する、エフェクト、SEは無し
                Debug.Log("miss");
            }

            ////ぶつかったものを破壊する
            //Destroy(hit2D.collider.gameObject);
            //hit2D.collider.gameObject.SetActive(false);
        }
    }

    void SpawnTextEffect(string message, Vector3 position, Color color, GameObject effectPrefab)
    {
        //シグナルを消した場所に判定のエフェクトを生成する  判定によって表示するテキストと色を設定する
        GameObject effect = Instantiate(textEffectPrefab, position + Vector3.up * 1.5f, Quaternion.identity);
        JudgmentEffect judgmentEffect = effect.GetComponent<JudgmentEffect>();
        judgmentEffect.SetText(message, color);

        if (effectPrefab != null)
        {
            //Perfectエフェクト,Nomalエフェクトを表示する
            GameObject effectObject = Instantiate(effectPrefab, position, Quaternion.identity);
            Destroy(effectObject, 1.0f);
        }
    }

    //可視化ツール
    void OnDrawGizmosSelected()
    {
        //判定場所を赤く表示させる
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, radius);
    }
}

