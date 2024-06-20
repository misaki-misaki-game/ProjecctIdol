using UnityEngine;
using System.IO;

public partial class DataManager : MonoBehaviour // MasterDataをjson形式に変えて保存・読み込みするスクリプト
{

    /// --------関数一覧-------- ///
    /// -------public関数------- ///

    /// <summary>
    /// jsonとしてデータを保存する関数
    /// </summary>
    /// <param name="data">書き込みたいclass</param>
    public void Save(MasterData data)
    {
        string json = JsonUtility.ToJson(data); // jsonとして変換
        StreamWriter writer = new StreamWriter(filepath, false); // ファイル書き込み指定
        writer.WriteLine(json); // json変換した情報を書き込み
        writer.Close(); // ファイルを閉じる
        // Debug.Log("セーブしています" + json);
    }

    /// <summary>
    /// データを初期化する関数
    /// </summary>
    public void ResetHighScore()
    {
        // Debug.Log("ハイスコアの初期化を行います");
        data = new MasterData(); // dataにMasterData型を代入
        Save(data); // セーブする
    }

    /// -------public関数------- ///
    /// -----protected関数------ ///



    /// -----protected関数------ ///
    /// ------private関数------- ///

    private void Awake()
    {
        CheckSaveData(); // 開始時にファイルチェック、読み込み
    }

    /// <summary>
    /// 開始時にファイルチェック、読み込みする関数
    /// </summary>
    private void CheckSaveData()
    {
        // Debug.Log("起動ロード開始");
        data = new MasterData(); // dataにMasterData型を代入
#if UNITY_ANDROID
        // Path.Combine()を使用してアプリの永続的なデータ保存用ディレクトリにファイルパスを作成し、そこにJSONデータを書き込む
        // Application.persistentDataPathは、各プラットフォームでアプリケーションの永続的なデータ保存先を示す
        filepath = Path.Combine(Application.persistentDataPath, fileName);
#elif UNITY_EDITOR || UNITY_STANDALONE
        filepath = Application.dataPath + "/" + fileName; // パス名取得
#endif
        if (!File.Exists(filepath)) // ファイルがないとき
        {
            //Debug.Log("saveデータを作ろうとしています");
            Save(data); // ファイル作成
        }
        data = Load(filepath); // ファイルを読み込んでdataに格納
    }

    /// <summary>
    /// jsonデータを読み込む関数
    /// </summary>
    /// <param name="path">読み込みたいjsonデータのパス</param>
    /// <returns>読み込んだjsonデータのクラス</returns>
    private MasterData Load(string path)
    {
        if (File.Exists(path)) // jsonデータがあれば
        {
            StreamReader reader = new StreamReader(path); // ファイル読み込み指定
            string json = reader.ReadToEnd(); // ファイル内容全て読み込み
            reader.Close(); // ファイルを閉じる
            // Debug.Log("ロードしています" + json);
            return JsonUtility.FromJson<MasterData>(json); // jsonファイルを型に戻して返す
        }
        else
        {
            // Debug.LogError("ファイルが見つかりません" + path);
            return null; // nullを返す
        }
    }

    /// ------private関数------- ///
    /// --------関数一覧-------- ///
}
public partial class DataManager
{
    /// --------変数一覧-------- ///
    /// -------public変数------- ///

    [SerializeField] public MasterData data; // json変換するデータのクラス 

    /// -------public変数------- ///
    /// -----protected変数------ ///



    /// -----protected変数------ ///
    /// ------private変数------- ///

    private string filepath; // jsonファイルのパス

    private string fileName = "HighScoreData.json"; // jsonファイル名

    /// ------private変数------- ///
    /// -------プロパティ------- ///



    /// -------プロパティ------- ///
    /// --------変数一覧-------- ///
}