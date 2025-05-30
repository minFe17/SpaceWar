using UnityEngine;

public class IceLanceParticle : MonoBehaviour
{
    [SerializeField] IceLance _parent;

    private void OnParticleCollision(GameObject other)
    {
        _parent.IsCollision(other);
    }
}