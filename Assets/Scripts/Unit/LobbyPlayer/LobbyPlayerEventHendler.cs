using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyPlayerEventHendler : MonoBehaviour
{
    public delegate void MoveSpeedObserver(float _speed);
    public event MoveSpeedObserver MoveSpeedObserverEvent;

    public delegate void GoodsObserver(int _gold, int _diamond, int _stamina);
    public event GoodsObserver GoodsObserverEvent;

    public void resgisterMoveSpeedObsever(MoveSpeedObserver _obs)
    {
        MoveSpeedObserverEvent -= _obs;
        MoveSpeedObserverEvent += _obs;
    }
    public void UnResisterMoveSpeedObserver(MoveSpeedObserver _obs)
    {
        MoveSpeedObserverEvent -= _obs;
    }
    public void ChangeMoveSpeed(float _speed)
    {
        MoveSpeedObserverEvent?.Invoke(_speed);
    }

    public void resgisterGoodsObserver(GoodsObserver _obs)
    {
        GoodsObserverEvent -= _obs;
        GoodsObserverEvent += _obs;
    }
    public void UnResisterGoodsObserver(GoodsObserver _obs)
    {
        GoodsObserverEvent -= _obs;
    }
    public void ChangeGoods(int _gold, int _diamond, int _stamina)
    {
        GoodsObserverEvent?.Invoke(_gold, _diamond, _stamina);
    }
}
