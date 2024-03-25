using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour // MasterData��json�`���ɕς��ĕۑ��E�ǂݍ��݂���X�N���v�g
{
    [SerializeField] public MasterData data; // json�ϊ�����f�[�^�̃N���X 
    string filepath; // json�t�@�C���̃p�X
    string fileName = "HighScoreData.json"; // json�t�@�C����

    void Awake()
    {
        CheckSaveData(); // �J�n���Ƀt�@�C���`�F�b�N�A�ǂݍ���
    }
    /// <summary>
    /// �J�n���Ƀt�@�C���`�F�b�N�A�ǂݍ��݂���֐�
    /// </summary>
    private void CheckSaveData()
    {
        Debug.Log("�N�����[�h�J�n");
        data = new MasterData(); // data��MasterData�^����
#if UNITY_ANDROID
        // Path.Combine()���g�p���ăA�v���̉i���I�ȃf�[�^�ۑ��p�f�B���N�g���Ƀt�@�C���p�X���쐬���A������JSON�f�[�^����������
        // Application.persistentDataPath�́A�e�v���b�g�t�H�[���ŃA�v���P�[�V�����̉i���I�ȃf�[�^�ۑ��������
        filepath = Path.Combine(Application.persistentDataPath, fileName);
#elif UNITY_EDITOR || UNITY_STANDALONE
        filepath = Application.dataPath + "/" + fileName; // �p�X���擾
#endif
        if (!File.Exists(filepath)) // �t�@�C�����Ȃ��Ƃ�
        {
            Debug.Log("save�f�[�^����낤�Ƃ��Ă��܂�");
            Save(data); // �t�@�C���쐬
        }
        data = Load(filepath); // �t�@�C����ǂݍ����data�Ɋi�[
    }
    /// <summary>
    /// json�Ƃ��ăf�[�^��ۑ�����֐�
    /// </summary>
    /// <param name="data">�������݂���class</param>
    public void Save(MasterData data)
    {
        string json = JsonUtility.ToJson(data); // json�Ƃ��ĕϊ�
        StreamWriter writer = new StreamWriter(filepath, false); // �t�@�C���������ݎw��
        writer.WriteLine(json); // json�ϊ�����������������
        writer.Close(); // �t�@�C�������
        Debug.Log("�Z�[�u���Ă��܂�" + json);
    }
    /// <summary>
    /// json�f�[�^��ǂݍ��ފ֐�
    /// </summary>
    /// <param name="path">�ǂݍ��݂���json�f�[�^�̃p�X</param>
    /// <returns>�ǂݍ���json�f�[�^�̃N���X</returns>
    MasterData Load(string path)
    {
        if (File.Exists(path)) // json�f�[�^�������
        {
            StreamReader reader = new StreamReader(path); // �t�@�C���ǂݍ��ݎw��
            string json = reader.ReadToEnd(); // �t�@�C�����e�S�ēǂݍ���
            reader.Close(); // �t�@�C�������
            Debug.Log("���[�h���Ă��܂�" + json);
            return JsonUtility.FromJson<MasterData>(json); // json�t�@�C�����^�ɖ߂��ĕԂ�
        }
        else
        {
            Debug.LogError("�t�@�C����������܂���" + path);
            return null; // null��Ԃ�
        }
    }
    /// <summary>
    /// �f�[�^������������֐�
    /// </summary>
    public void ResetHighScore() 
    {
        Debug.Log("�n�C�X�R�A�̏��������s���܂�");
        data = new MasterData(); // data��MasterData�^����
        Save(data); // �Z�[�u����
    }
}

