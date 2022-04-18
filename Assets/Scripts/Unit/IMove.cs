using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IMove : MonoBehaviour
{
    [SerializeField]
    protected float mSpeed;
    [SerializeField]
    protected Vector3 mDir;
    // Start is called before the first frame update
    void Start()
    {
        mDir = new Vector3();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    protected virtual void UpdateMove() { }
    public float Speed
    {
        get { return mSpeed; }
        set { mSpeed = value; }
    }
}
