using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launch : Projectile
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
    private float angle = 0;
    #endregion
    #region method
    protected override void destroySelf()
    {
        /*생성후 파괴되는 매서드*/
    }
    protected override void launchProjectile()
    {
        if (isActive)
        {
            // Rotate로 회전 시켰기 때문에 단위벡터의 방향은 고정
            transform.Translate(Vector2.right * Time.deltaTime * spec.Speed);
        }
    }
    // 발사 방향에 따라 발사체를 회전시키는 함수
    public float setAngle(Vector3 dir)
    {
        // 오일러각(0~360)
        return Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    }
    // 비활성화
    public override void setDisable()
    {
        isActive = false;
        myPos = transform.position;
    }

    // 활성화
    /*
     * _target : 터치패드 입력방향 벡터 or 근거리 몬스터의 위치 벡터
     * _player : 현재 player의 위치 벡터(발사체의 시작위치)
     */
    public override void setEnable(Vector3 _target, Vector3 _player, float _angle)
    {
        float scale = GameObject.Find("PlayerObject").GetComponent<IAttack>().ProjectileScale + 1f;
         transform.localScale = new Vector3(scale, scale, scale);
         transform.position = _player;
         target = _target;
         angle = setAngle(target - _player) + _angle;
         transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
         gameObject.SetActive(true);
         isActive = true;
    }
    void Start()
    {
#if DEBUG
#endif
    }
    void FixedUpdate()
    {
        launchProjectile();
    }
    #endregion
}
