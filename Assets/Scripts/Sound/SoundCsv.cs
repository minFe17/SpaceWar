using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using Utils;

public class SoundCsv : MonoBehaviour
{
    // �̱���
    string _soundDataFilePath;
    public string SoundDataFilePath
    {
        get
        {
            if (_soundDataFilePath == null)
            {
                string soundDataFIleName = "SoundDataFile.csv";
                _soundDataFilePath = Application.persistentDataPath + soundDataFIleName;
            }
            return _soundDataFilePath;
        }
    }

    public bool ReadSound()
    {
        if (File.Exists(SoundDataFilePath))
        {
            string source;
            using (StreamReader sr = new StreamReader(SoundDataFilePath))
            {
                string[] lines;
                source = sr.ReadToEnd();
                lines = Regex.Split(source, @"\r\n|\n\r|\n|\r");
                string[] header = Regex.Split(lines[0], ",");
                string[] value = Regex.Split(lines[1], ",");

                GenericSingleton<SoundManager>.Instance.BgmSound = float.Parse(value[0]);
                GenericSingleton<SoundManager>.Instance.SFXSound = float.Parse(value[1]);
            }
            return true;
        }
        return false;
    }

    public void WriteSound()
    {
        List<string[]> lists = new List<string[]>();

        string[] header = new string[] { "BGMVolume", "SFXVolume" };
        lists.Add(header);
        string[] value = new string[] { GenericSingleton<SoundManager>.Instance.BgmSound.ToString(), GenericSingleton<SoundManager>.Instance.SFXSound.ToString() };
        lists.Add(value);

        string delimiter = ",";
        string[][] outputs = lists.ToArray();

        StringBuilder stringBuilder = new StringBuilder();
        for (int i = 0; i < outputs.Length; i++)
        {
            stringBuilder.AppendLine(string.Join(delimiter, outputs[i]));
        }
        using (StreamWriter outStream = File.CreateText(SoundDataFilePath))
            outStream.Write(stringBuilder);
    }
}