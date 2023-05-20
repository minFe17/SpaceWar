using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CsvController : MonoBehaviour
{
    // �̱���
    // ���ӵ����� ���� ���� �Լ�
    // ���ӵ����� ���� �б� �Լ�

    string[] ReadFileData(string name)
    {
        TextAsset textFile = Resources.Load($"Datas/{name}") as TextAsset;

        using (StringReader stringReader = new StringReader(textFile.text))
        {
            string baseData = stringReader.ReadToEnd();
            return baseData.Split("\r\n");
        }
    }
}