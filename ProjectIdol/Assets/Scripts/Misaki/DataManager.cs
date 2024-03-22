using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour // MasterDataをjson形式に変えて保存・読み込みするスクリプト
{
    [SerializeField] public MasterData data; // json変換するデータのクラス 
    GameObject dataObj; // dataObject変数
    string filepath; // jsonファイルのパス
    string fileName = "HighScoreData.json"; // jsonファイル名

    void Awake()
    {
        CheckSaveData(); // 開始時にファイルチェック、読み込み
    }
    private void CheckSaveData() // 開始時にファイルチェック、読み込みする関数
    {
        Debug.Log("起動ロード開始");
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
            Debug.Log("saveデータを作ろうとしています");
            Save(data); // ファイル作成
        }
        data = Load(filepath); // ファイルを読み込んでdataに格納
    }
    public void Save(MasterData data) // jsonとしてデータを保存する関数
    {
        string json = JsonUtility.ToJson(data); // jsonとして変換
        StreamWriter writer = new StreamWriter(filepath, false); // ファイル書き込み指定
        writer.WriteLine(json); // json変換した情報を書き込み
        writer.Close(); // ファイルを閉じる
        Debug.Log("セーブしています" + json);
    }
    MasterData Load(string path) // jsonデータを読み込む関数
    {
        if (File.Exists(path)) // jsonデータがあれば
        {
            StreamReader reader = new StreamReader(path); // ファイル読み込み指定
            string json = reader.ReadToEnd(); // ファイル内容全て読み込み
            reader.Close(); // ファイルを閉じる
            Debug.Log("ロードしています" + json);
            return JsonUtility.FromJson<MasterData>(json); // jsonファイルを型に戻して返す
        }
        else
        {
            Debug.LogError("ファイルが見つかりません" + path);
            return null; // nullを返す
        }
    }
    public void ResetHighScore() // データを初期化する関数
    {
        Debug.Log("ハイスコアの初期化を行います");
        data = new MasterData(); // dataにMasterData型を代入
        Save(data); // セーブする
    }
    //void OnDestroy() // ゲーム終了時に保存
    //{
    //    Save(data);
    //}
}

