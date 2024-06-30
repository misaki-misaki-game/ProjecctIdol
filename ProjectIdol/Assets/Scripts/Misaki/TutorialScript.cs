using UnityEngine;
using UnityEngine.UI;

namespace Misaki
{
    public partial class TutorialScript : MonoBehaviour
    {
        /// --------関数一覧-------- ///

        #region public関数
        /// -------public関数------- ///

        /// <summary>
        /// チュートリアルを開く関数
        /// </summary>
        public void OpenTuto()
        {
            // SEを鳴らし、開く
            audioSource.Play();
            gameObject.SetActive(true);
        }

        /// <summary>
        /// チュートリアルを閉じる関数
        /// </summary>
        public void CloseTuto()
        {
            // SEを鳴らし、初期化して閉じる
            InitializeTuto();
            audioSource.Play();
            gameObject.SetActive(false);
        }

        /// <summary>
        /// 次のページに進む関数
        /// </summary>
        public void OnNext()
        {
            page++;
            PageChange();
        }


        /// <summary>
        /// 前のページに戻る関数
        /// </summary>
        public void OnPrev()
        {
            page--;
            PageChange();
        }

        /// -------public関数------- ///
        #endregion

        #region protected関数
        /// -----protected関数------ ///



        /// -----protected関数------ ///
        #endregion

        #region private関数
        /// ------private関数------- ///

        private void Awake()
        {
            pageMax = sprites.Length - 1; // ページの最終ページ目を画像の枚数にする

            InitializeTuto(); // チュートリアルを初期化する
        }

        /// <summary>
        /// チュートリアルページ初期化関数
        /// </summary>
        private void InitializeTuto()
        {
            page = pageMin; // ページ数の初期化
            image.sprite = sprites[page]; // 現在の画像を0ページ目にする
        }

        /// <summary>
        /// ページを切り替える関数
        /// </summary>
        private void PageChange()
        {
            // ページ数が最終ページを超える、または、ページの初発ページを下回る場合はリターンする
            if (page > pageMax)
            {
                page = pageMax;
                return;
            }
            else if (page < pageMin)
            {
                page = pageMin;
                return;
            }
            // 最終ページの場合はNextボタンを非表示にしてCloseボタンを表示
            else if (page == pageMax)
            {
                next.SetActive(false);
                close.SetActive(true);
            }
            // 初発ページの場合はPrevボタンを非表示
            else if (page == pageMin)
            {
                prev.SetActive(false);
            }
            // その他の場合
            else
            {
                // nextとprevが非表示なら表示する
                // closeは最終ページのみ表示する
                if (next.activeSelf == false) next.SetActive(true);
                if (prev.activeSelf == false) prev.SetActive(true);
                if (close.activeSelf == true) close.SetActive(false);
            }
            // SEを鳴らし、画像を切り替える
            audioSource.PlayOneShot(se);
            image.sprite = sprites[page];
        }


        /// ------private関数------- ///
        #endregion

        /// --------関数一覧-------- ///
    }
    public partial class TutorialScript
    {
        /// --------変数一覧-------- ///

        #region public変数
        /// -------public変数------- ///



        /// -------public変数------- ///
        #endregion

        #region protected変数
        /// -----protected変数------ ///



        /// -----protected変数------ ///
        #endregion

        #region private変数
        /// ------private変数------- ///

        private int page; // ページ数変数
        private int pageMax; // 最終ページ
        private int pageMin = 0; // 初発ページ

        // チュートリアルでの各ボタン変数
        [SerializeField] private GameObject next;
        [SerializeField] private GameObject prev;
        [SerializeField] private GameObject close;

        [SerializeField] private Image image; // イメージ変数

        [SerializeField] private Sprite[] sprites = new Sprite[4]; // チュートリアル画像配列

        [SerializeField] private AudioSource audioSource; // オーディオソース

        [SerializeField] private AudioClip se; // ページ送りの 

        /// ------private変数------- ///
        #endregion

        #region プロパティ
        /// -------プロパティ------- ///
    
    
    
        /// -------プロパティ------- ///
        #endregion
    
        /// --------変数一覧-------- ///
    }
}