using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IMove : MonoBehaviour
{
    [SerializeField]
    protected float mSpeed;
    public float Speed
    {
        get { return mSpeed; }
    }
    [SerializeField]
    protected Vector3 mDir;
    [SerializeField]
    protected bool mMoveable = true;
    public bool MMoveable
    {
        get { return mMoveable; }
        set { mMoveable = value; }
    }
    // Start is called before the first frame update
    protected virtual void Start()
    {
        mDir = new Vector3();
        gameObject.GetComponent<IEventHandler>().registerMoveSpeedObserver(RegisterMoveSpeedObserver);
    }

    private void RegisterMoveSpeedObserver(float _moveSpeed, GameObject _obj)
    {
        mSpeed = _moveSpeed;
    }
    protected virtual void UpdateMove() { }

    public virtual void StopStiffTime(float _time)
    {
        if (mMoveable && gameObject.activeInHierarchy)
        {
            StartCoroutine(CoStopStiffTime(_time));
        }
    }
    /*
     * 이동시 경직이 걸리는 api입니다.
     *  _time : 경직되는 시간입니다.
     */
    public IEnumerator CoStopStiffTime(float _time)
    {
        if (mMoveable)
        {
            mMoveable = false;
            Debug.Log("경직 기다리는 중..");
            yield return new WaitForSeconds(_time);
            mMoveable = true;
        }
        else
            mMoveable = true;
    }
}
