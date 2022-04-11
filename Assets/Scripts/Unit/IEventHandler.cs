using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IEventHandler : MonoBehaviour
{

    public delegate void HpObserver(int _hp, GameObject _obj);
    public event HpObserver HpObserverEvent;


    public virtual void registerHpObserver(HpObserver _obs)
    {
        //HpObserverEvent는 null이여도 -연산에대해 에러가 발생하지 않음
        HpObserverEvent -= _obs;
        HpObserverEvent += _obs;
    }
    public virtual void UnRegisterHpObserver(HpObserver _obs)
    {
        HpObserverEvent -= _obs;
    }

    public virtual void ChangeHp(int _hp, GameObject _obj)
    {
        HpObserverEvent?.Invoke(_hp, _obj);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
