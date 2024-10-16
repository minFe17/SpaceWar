using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class CsvManager : MonoBehaviour
{
    // ╫л╠шео
    ReadData _readData = new ReadData();
    WriteData _writeData = new WriteData();

    StringBuilder _stringBuilder;

    bool _isWriting;

    string _playerDataFilePath;
    string _gameDataFilePath;
    string _passiveDataFilePath;
    string _soundDataFilePath;

    public bool IsWriting { get => _isWriting; set => _isWriting = value; }
    public String PlayerDataFilePath { get => _playerDataFilePath; }
    public String GameDataFilePath { get => _gameDataFilePath; }
    public String PassiveDataFilePath { get => _passiveDataFilePath; }
    public string SoundDataFilePath { get => _soundDataFilePath; }

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        CreateDataPath();
        _readData.Init(this);
        _writeData.Init(this);
    }

    void CreateDataPath()
    {
        if (_stringBuilder == null)
            _stringBuilder = new StringBuilder();
        CreateDataPath(out _playerDataFilePath, "SavePlayerDataFile.csv");
        CreateDataPath(out _gameDataFilePath, "SaveGameDataFile.csv");
        CreateDataPath(out _passiveDataFilePath, "PassiveDataFile.csv");
        CreateDataPath(out _soundDataFilePath, "SoundDataFile.csv");
    }

    void CreateDataPath(out string path, string dataName)
    {
        _stringBuilder.Clear();
        _stringBuilder.Append(Application.persistentDataPath);
        _stringBuilder.Append(dataName);
        path = _stringBuilder.ToString();
    }

    public bool CheckDataFiles()
    {
        if (CheckDataFile(_playerDataFilePath) == false)
            return false;
        if (CheckDataFile(_gameDataFilePath) == false)
            return false;
        if (CheckDataFile(_passiveDataFilePath) == false)
            return false;

        return true;
    }

    public void DestroyDataFiles()
    {
        DestroyDataFile(_playerDataFilePath);
        DestroyDataFile(_gameDataFilePath);
        DestroyDataFile(_passiveDataFilePath);
    }

    public void ReadDataFile()
    {
        _readData.ReadDataFile();
    }

    public bool ReadSoundDataFile()
    {
        return _readData.ReadSoundData();
    }

    public void WriteDataFile()
    {
        _writeData.WriteDataFile();
    }

    public void WriteSoundDataFile()
    {
        _writeData.WriteSound();
    }

    public async Task ReadPassiveData()
    {
        await _readData.ReadPassiveInfoData();
    }

    public bool CheckDataFile(string path)
    {
        if (File.Exists(path) == false)
            return false;
        return true;
    }

    void DestroyDataFile(string path)
    {
        if (CheckDataFile(path))
            File.Delete(path);
    }
}