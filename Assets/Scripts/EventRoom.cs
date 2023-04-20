using Unity.VisualScripting;
using UnityEngine;
using Utils;

public abstract class EventRoom : MonoBehaviour
{
    protected bool _inPlayer;
    protected string _message;
    public abstract void OnEnter();
    public abstract void Event();

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            OnEnter();
            GenericSingleton<UIManager>.Instance.InfoKey.SetActive(true);
            GenericSingleton<UIManager>.Instance.InfoMessage.text = _message;
            _inPlayer = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GenericSingleton<UIManager>.Instance.InfoKey.SetActive(false);
            _inPlayer = false;
        }
    }

}
