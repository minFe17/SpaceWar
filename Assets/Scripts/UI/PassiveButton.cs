using UnityEngine;
using UnityEngine.UI;

public class PassiveButton : MonoBehaviour
{
    [SerializeField] Image _image;
    PassiveBase _passive;
    public PassiveBase Passive { get => _passive; set => _passive = value; }

    public void Init()
    {
        _passive.Init();
        _image.sprite = _passive.Image;
    }
}
