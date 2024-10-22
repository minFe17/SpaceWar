using UnityEngine;

public interface IFactory<T> : IFactory
{
    T MakeObject();
}