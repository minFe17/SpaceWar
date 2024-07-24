using System.Threading.Tasks;
using UnityEngine;
using Utils;

public class AssetManager : MonoBehaviour
{
    // �̱���
    public async Task LoadAsset()
    {
        await LoadUIAsset();
        LoadCoinAsset();
    }

    void LoadCoinAsset()
    {
        GenericSingleton<CoinManager>.Instance.Init();
    }

    async Task LoadUIAsset()
    {
        await GenericSingleton<UIManager>.Instance.LoadAsset();
    }

    //async Task MapAsset()
    //{
    //    // ��
    //    // ��ֹ�, 
    //    // Enemy, 
    //}

    //async Task SoundAsset()
    //{
    //    // ����
    //}
}