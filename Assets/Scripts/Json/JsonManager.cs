using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class JsonManager : MonoBehaviour
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

    List<string> _playerLevelDataFilePath = new List<string>();

    public bool IsWriting { get => _isWriting; set => _isWriting = value; }
    public String PlayerDataFilePath { get => _playerDataFilePath; }
    public String GameDataFilePath { get => _gameDataFilePath; }
    public String PassiveDataFilePath { get => _passiveDataFilePath; }
    public string SoundDataFilePath { get => _soundDataFilePath; }
    public List<string> PlayerLevelDataFilePath { get => _playerLevelDataFilePath; }

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
        CreateDataPath(out _playerDataFilePath, "SavePlayerDataFile.json");
        CreateDataPath(out _gameDataFilePath, "SaveGameDataFile.json");
        CreateDataPath(out _passiveDataFilePath, "PassiveDataFile.json");
        CreateDataPath(out _soundDataFilePath, "SoundDataFile.json");

        CreatePlayerLevelDataFilePath();
    }

    void CreateDataPath(out string path, string dataName)
    {
        _stringBuilder.Clear();
        _stringBuilder.Append(Application.persistentDataPath);
        _stringBuilder.Append(dataName);
        path = _stringBuilder.ToString();
    }

    void CreatePlayerLevelDataFilePath()
    {
        string path = null;
        CreateDataPath(out path, "SaveSoldierLevelDataFile.json");
        _playerLevelDataFilePath.Add(path);

        CreateDataPath(out path, "SaveWitchLevelDataFile.json");
        _playerLevelDataFilePath.Add(path);
    }

    void DestroyDataFile(string path)
    {
        if (CheckDataFile(path))
            File.Delete(path);
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

    public bool CheckDataFile(string path)
    {
        return File.Exists(path);
    }

    public void DestroyDataFiles()
    {
        DestroyDataFile(_playerDataFilePath);
        DestroyDataFile(_gameDataFilePath);
        DestroyDataFile(_passiveDataFilePath);
    }

    public async Task ReadPassiveData()
    {
        await _readData.ReadPassiveInfoData();
    }

    public void ReadDataFile()
    {
        _readData.ReadDataFile();
    }

    public void ReadSoundDataFile()
    {
        _readData.ReadSoundData();
    }

    public async Task ReadPlayerStatDataFile()
    {
        await _readData.ReadPlayerStatData();
    }

    public void WriteDataFile()
    {
        _writeData.WriteDataFile();
    }

    public void WriteSoundDataFile()
    {
        _writeData.WriteSound();
    }

    public void WrtiePlayerLevelDataFile()
    {
        _writeData.WritePlayerLevelData();
    }
}