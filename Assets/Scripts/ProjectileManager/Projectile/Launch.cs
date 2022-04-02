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

    private bool isActive = false;
    #endregion
    #region method
    protected override void destroySelf()
    {
        /*생성후 파괴되는 매서드*/
        Destroy(gameObject, Spec.SpawnTime);
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
    public void setAngle()
    {
        // 오일러각(0~360)
        float angle = Quaternion.FromToRotation(
            new Vector3(1, 0, 0), target.normalized).eulerAngles.z;
        transform.Rotate(0, 0, angle);
    }
    // 비활성화
    public void setDisable()
    {
        gameObject.SetActive(false);
        isActive = false;
    }

    // 활성화
    /*
     * _target : 터치패드 입력방향 벡터 or 근거리 몬스터의 위치 벡터
     * _player : 현재 player의 위치 벡터
     */
    public void setEnable(Vector3 _target, Vector3 _player)
    {
        transform.position = _player;
        target = _target;
        setAngle();
        gameObject.SetActive(true);
        isActive = true;

    }
    void Start()
    {
#if DEBUG
        target = new Vector3(50, -10, 0);
#endif
        setDisable();
    }
    // Update is called once per frame
    void Update()
    {
        launchProjectile();
    }
    #endregion
}
