using UnityEngine;

public class VendingMachineResult : MonoBehaviour
{
    Animator _animator;

    float _time;

    public void Init()
    {
        _animator = GetComponent<Animator>();
        _animator.SetBool("isShowResultText", true);
    }

    void Update()
    {
        Checktime();
    }

    void Checktime()
    {
        if (_time < 1f)
            _time += Time.deltaTime;
        else
            _animator.SetBool("isShowResultText", false);
    }
}
