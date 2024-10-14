using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using Utils;

public class ReadData : MonoBehaviour
{
    AddressableManager _addressableManager;
    PassiveManager _passiveManager;

    PassiveData _passiveData;

    void BaseReadOnlyData(out string[] data, TextAsset readData)
    {
        using (StringReader stringReader = new StringReader(readData.text))
        {
            string baseData = stringReader.ReadToEnd();
            data = baseData.Split("\r\n");
        }
        if (data.Length < 2)
            data = null;
    }

    public async Task ReadPassiveInfoData()
    {
        if (_addressableManager == null)
            _addressableManager = GenericSingleton<AddressableManager>.Instance;
        if (_passiveManager == null)
            _passiveManager = GenericSingleton<PassiveManager>.Instance;

        TextAsset passiveDatas = await _addressableManager.GetAddressableAsset<TextAsset>("PassiveData");
        string[] data;
        BaseReadOnlyData(out data, passiveDatas);
        for (int i = 1; i < data.Length; i++)
        {
            var values = data[i].Split(",");
            if (values.Length == 0 || string.IsNullOrEmpty(values[0]))
                continue;

            _passiveData.Index = int.Parse(values[0]);
            _passiveData.Name = values[1];
            _passiveData.Info = values[2];
            _passiveData.ImageName = values[3];

            _passiveManager.Passive[i - 1].SetPassiveData(_passiveData);
        }
    }
}