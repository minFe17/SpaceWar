using System.Threading.Tasks;
using UnityEngine;

public class CsvManager : MonoBehaviour
{
    // �̱���
    ReadData _readData = new ReadData();

    public async Task ReadPassiveData()
    {
        await _readData.ReadPassiveInfoData();
    }
}