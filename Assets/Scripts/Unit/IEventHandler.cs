using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IEventHandler : MonoBehaviour
{
    /*
     * TO-DO : 이벤트핸들러를 제너릭하게 짤수있는 방법이 있어보인다 delegate를 각 이벤트에대해 일일히 만들어주는 방식이라 중복되는 코드가 너무많다.
     *         해결방법을 찾아보자./
     * 참고 https://docs.microsoft.com/ko-kr/dotnet/csharp/programming-guide/events/how-to-publish-events-that-conform-to-net-framework-guidelines
     */


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
