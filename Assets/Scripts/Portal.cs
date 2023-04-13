using UnityEngine;
using Utils;

public class Portal : MonoBehaviour
{
    bool _inPlayer;

    private void Update()
    {
        NextStage();
    }

    void NextStage()
    {
        if (Input.GetKeyDown(KeyCode.F) && _inPlayer)
        {
            GenericSingleton<GameManager>.Instance.NextStage();
            _inPlayer = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GenericSingleton<UIManager>.Instance.PortalInfoKey.SetActive(true);
            _inPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GenericSingleton<UIManager>.Instance.PortalInfoKey.SetActive(false);
            _inPlayer = false;
        }
    }
}
