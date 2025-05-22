using System.Threading.Tasks;
using UnityEngine;
using Utils;

public class PassiveManager : MonoBehaviour
{
    // ╫л╠шео
    CsvManager _csvManager;
    PassiveDataList _dataList = DataSingleton<PassiveDataList>.Instance;

    async Task ReadData()
    {
        if (_csvManager == null)
            _csvManager = GenericSingleton<CsvManager>.Instance;
        await _csvManager.ReadPassiveData();
    }

    public async Task Init()
    {
        await ReadData();
    }

    public void RemovePassive(PassiveData passive)
    {
        _dataList.UIDataList.Remove(passive);
    }
}