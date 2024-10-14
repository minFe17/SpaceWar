using UnityEngine;
using UnityEngine.UI;

public class PassiveButton : MonoBehaviour
{
    Image _image;
    IPassive _passive;

    public IPassive Passive { get => _passive; set => _passive = value; }

    public void Init()
    {
        _image = GetComponent<Image>();
        _passive.Init();
        _image.sprite = _passive.Image;
    }
}