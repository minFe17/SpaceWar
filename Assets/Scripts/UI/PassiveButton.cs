using UnityEngine;
using UnityEngine.UI;

public class PassiveButton : MonoBehaviour
{
    Image _image;
    PassiveData _passive;

    public PassiveData Passive { get => _passive; set => _passive = value; }

    public void Init()
    {
        _image = GetComponent<Image>();
        _image.sprite = _passive.Image;
    }
}