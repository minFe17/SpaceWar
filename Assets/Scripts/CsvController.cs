using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CsvController : MonoBehaviour
{
    // 싱글톤
    // 게임데이터 파일 쓰기 함수
    // 게임데이터 파일 읽기 함수

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