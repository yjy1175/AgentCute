using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : Projectile
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
    [SerializeField]
    private bool isActive = false;
    [SerializeField]
    private float angle = 0;
    #endregion
    #region method
    protected override void destroySelf()
    {
        /*생성후 파괴되는 매서드*/
    }
    protected override void launchProjectile()
    {
        /*스폰 매서드*/
    }
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        launchProjectile();
    }
    // 발사 방향에 따라 발사체를 회전시키는 함수
    public float setAngle(Vector3 dir)
    {
        // 오일러각(0~360)
        return Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    }
    public override void setEnable(Vector3 _target, Vector3 _player, float _angle)
    {
        transform.localScale = new Vector3(AddScaleCoefficient, AddScaleCoefficient, AddScaleCoefficient);
        transform.position = _player + (_target - _player).normalized;
        target = _target;
        angle = setAngle(target - _player) + _angle;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        gameObject.SetActive(true);
        isActive = true;
    }

    public override void setDisable()
    {
        isActive = false;
    }
    #endregion
}
