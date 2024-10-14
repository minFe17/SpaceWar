using UnityEngine;

public interface IPassive
{
    int Index { get; }
    string Name { get; }
    string Info { get; }
    Sprite Image { get; }

    void Init();
    void AddPassive();
}