using UnityEngine;
using Utils;

public abstract class EventRoom : MonoBehaviour
{
    [SerializeField] protected EEventRoomType _eventRoomType;
    protected bool _inPlayer;
    protected string _message;

    public abstract void OnEnter();
    public abstract void Event();

    public virtual void Init()
    {
        _inPlayer = false;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OnEnter();
            GenericSingleton<UIManager>.Instance.InfoKey.SetActive(true);
            GenericSingleton<UIManager>.Instance.InfoMessage.text = _message;
            _inPlayer = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GenericSingleton<UIManager>.Instance.InfoKey.SetActive(false);
            _inPlayer = false;
        }
    }
}