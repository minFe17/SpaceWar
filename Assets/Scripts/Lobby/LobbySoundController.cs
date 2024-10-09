using Utils;

public class LobbySoundController : SoundController
{
    void Start()
    {
        GenericSingleton<SoundManager>.Instance.SoundController = this;
    }
}