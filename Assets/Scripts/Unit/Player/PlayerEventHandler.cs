using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEventHandler : IEventHandler
{
    public delegate void LevelObserver(int _level);

    //ExpObserver 사용시 Player현재 경험치, 최대경험치가 바뀔경우 호출되므로 PlayerStatus의 경험치를 PlayerExp,PlayerMaxExp를 참조하여 사용할 것
    public delegate void ExpObserver();
    public delegate void GoldObserver(int _gold);

    public event LevelObserver LevelObserverEvent;
    public event ExpObserver ExpObserverEvent;
    public event GoldObserver GoldObserverEvent;



    //Level EventHandler
    public virtual void registerLevelObserver(LevelObserver _obs)
    {
        //HpObserverEvent는 null이여도 -연산에대해 에러가 발생하지 않음
        LevelObserverEvent -= _obs;
        LevelObserverEvent += _obs;
    }
    public virtual void UnRegisterLevelObserver(LevelObserver _obs)
    {
        LevelObserverEvent -= _obs;
    }

    public virtual void ChangeLevel(int _level)
    {
        LevelObserverEvent?.Invoke(_level);
    }



    //Exp EventHandler
    public virtual void registerExpObserver(ExpObserver _obs)
    {
        //HpObserverEvent는 null이여도 -연산에대해 에러가 발생하지 않음
        ExpObserverEvent -= _obs;
        ExpObserverEvent += _obs;
    }
    public virtual void UnRegisterExpObserver(ExpObserver _obs)
    {
        ExpObserverEvent -= _obs;
    }

    public virtual void ChangeExp()
    {
        ExpObserverEvent?.Invoke();
    }

    //Gold EventHadeler
    public virtual void registerGoldObserver(GoldObserver _obs)
    {
        GoldObserverEvent -= _obs;
        GoldObserverEvent += _obs;
    }
    public virtual void UnRegisterGoldObserver(GoldObserver _obs)
    {
        GoldObserverEvent -= _obs;
    }
    public virtual void ChangeGold(int _gold)
    {
        GoldObserverEvent?.Invoke(_gold);
    }

}
