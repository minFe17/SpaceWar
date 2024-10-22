using System;

public interface IFactorys
{
    void AddFactorys<TEnum>(TEnum key, IFactory value) where TEnum : Enum;
    object MakeObject<TEnum>(TEnum key) where TEnum : Enum;
}