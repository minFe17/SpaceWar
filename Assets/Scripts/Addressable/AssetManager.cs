using System.Threading.Tasks;
using UnityEngine;
using Utils;

public class AssetManager : MonoBehaviour
{
    // 싱글턴
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
    //    // 맵
    //    // 장애물, 
    //    // Enemy, 
    //}

    //async Task SoundAsset()
    //{
    //    // 사운드
    //}
}