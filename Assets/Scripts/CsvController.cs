using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CsvController : MonoBehaviour
{
    // 싱글톤
    // 게임데이터 파일 쓰기 함수
    // 게임데이터 파일 읽기 함수
    List<stPassiveData> _passiveData = new List<stPassiveData>();

    public List<stPassiveData> PassiveData
    {
        get
        {
            if (_passiveData == null)
                ReadPassiveData();
            return _passiveData;
        }
    }

    string[] ReadFileData(string name)
    {
        TextAsset textFile = Resources.Load($"Datas/{name}") as TextAsset;

        using (StringReader stringReader = new StringReader(textFile.text))
        {
            string baseData = stringReader.ReadToEnd();
            return baseData.Split("\r\n");
        }
    }

    void ReadPassiveData()
    {
        var data = ReadFileData("PassiveData");
        if (data.Length < 2)
            return;
        for(int i=1;i<data.Length; i++)
        {
            var lineItem = data[i].Split(',');
            stPassiveData db;
            db.INDEX = int.Parse(lineItem[0]);
            db.PASSIVE = (EPassiveType)Enum.Parse(typeof(EPassiveType), lineItem[1]);
            _passiveData.Add(db);
        }
    }
}

public struct stPassiveData
{
    public int INDEX;
    public EPassiveType PASSIVE;
}

public enum EPassiveType
{
    None,
    HPUp,
    DamageUp,
    SpeedUp,
    BulletUp,
    UnlockBurstMode,
    UnlockAutoMode,
    GetMoneyUp,
    DoubleHP,
    HPUpByMoney,
    DamageUpByMoney,
    Vampirism,
    Max,
}
