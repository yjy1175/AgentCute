using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMagnet : MonoBehaviour
{
    [SerializeField]
    private float mMagnetPower = 3f;
    [SerializeField]
    private float mMagenetSpeed = 4f;
    void Start()
    {
        gameObject.GetComponent<PlayerEventHandler>().registerMagnetPowerbserver(RegisterMagentPowerObserver);
    }

    private void FixedUpdate()
    {
        if(Physics2D.OverlapCircle(transform.position, mMagnetPower, LayerMask.GetMask("Item"))) {
            Collider2D[] colArray =  Physics2D.OverlapCircleAll(transform.position, mMagnetPower, LayerMask.GetMask("Item"));
            foreach(Collider2D obj in colArray)
            {
                Vector3 dir = PlayerManager.Instance.Player.transform.position - obj.transform.position;

                obj.GetComponent<Transform>().Translate(mMagenetSpeed * dir * Time.deltaTime);
            }
        }
        
    }
    // Update is called once per frame


    public void RegisterMagentPowerObserver(float _magnetPower)
    {
        mMagnetPower = _magnetPower;
        Debug.Log("register는 정상작동");
    }
}
