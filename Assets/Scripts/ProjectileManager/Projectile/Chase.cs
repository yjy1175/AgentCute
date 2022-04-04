using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : Projectile
{
    #region variable
    [SerializeField]
    private ProjectileSpec spec = new ProjectileSpec();
    public override ProjectileSpec Spec
    {
        get { return spec; }
        set { spec = value; }
    }

    [SerializeField]
    private Vector3 target;
    public override Vector3 Target
    {
        get { return target; }
        set { target = value; }
    }
    #endregion
    #region method
    protected override void destroySelf()
    {
        /*생성후 파괴되는 매서드*/
        if (Target == null)
            setDisable();
    }
    protected override void launchProjectile()
    {
        /*추적 매서드*/
    }
    // Start is called before the first frame update
    void Start()
    {
        destroySelf();
    }
    // Update is called once per frame
    void Update()
    {
        launchProjectile();
    }

    public override void setEnable(Vector3 _target, Vector3 _player, float _angle)
    {
        throw new System.NotImplementedException();
    }

    public override void setDisable()
    {
        throw new System.NotImplementedException();
    }
    #endregion
}

