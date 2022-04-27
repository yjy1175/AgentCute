using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpStatusEventHandler : MonoBehaviour
{
    public delegate void IntObserver(int _value, LevelUpStatusManager.StatType _type);
    public event IntObserver IntObserverEvent;

    public void registerIntObserver(IntObserver _obs)
    {
        //HpObserverEvent는 null이여도 -연산에대해 에러가 발생하지 않음
        IntObserverEvent -= _obs;
        IntObserverEvent += _obs;
    }
    public void UnRegisterIntObserver(IntObserver _obs)
    {
        IntObserverEvent -= _obs;
    }
    public void ChangeValue(int _value, LevelUpStatusManager.StatType _type)
    {
        IntObserverEvent?.Invoke(_value, _type);
    }
}
