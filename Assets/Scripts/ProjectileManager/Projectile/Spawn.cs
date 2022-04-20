using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
    private float angle = 0;
    [SerializeField]
    private string mSpawnType;
    public string MSpawnType
    {
        get { return mSpawnType; }
        set { mSpawnType = value; }
    }
    public enum SpawnType
    {
        General, // 일반형
        SelfSpawn, // 자기주위 스폰형
        RandomSpawn, // 랜덤스폰형
        ShortWide, // 짧은와이드형
        LongWide, // 긴와이드형
    }
    [SerializeField]
    private VertualJoyStick mUJoySitick;
    [SerializeField]
    private Vector3 mPlayer;
    [SerializeField]
    private Vector3 newPos = Vector3.zero;
    [SerializeField]
    private Vector3 mJoyStickPos = Vector3.zero;
    [SerializeField]
    float scale;
    #endregion
    #region method
    protected override void destroySelf()
    {
        /*생성후 파괴되는 매서드*/
    }
    protected override void launchProjectile()
    {
        /*스폰 매서드*/
        // 방향키에 따라 위치가 바뀌는 경우
        if (isActive && (SpawnType)Enum.Parse(typeof(SpawnType), mSpawnType) == SpawnType.LongWide)
        {
            mPlayer = GameObject.Find("fire").transform.position;
            mUJoySitick = GameObject.Find("Canvas").transform.Find("UltimateSkillJoyStick").GetComponent<VertualJoyStick>();
            Vector3 joystickPos = new Vector3(mUJoySitick.GetHorizontalValue(), mUJoySitick.GetVerticalValue(), 0);
            // 터치패드 입력이 있을 경우
            if (joystickPos != Vector3.zero)
                mJoyStickPos = joystickPos;
            // 터치패드 입력이 없을 경우
            else
                mJoyStickPos = mJoyStickPos == Vector3.zero ? Vector3.right : mJoyStickPos;
            newPos = mJoyStickPos + mPlayer;
            angle = setAngle(newPos - mPlayer);
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.position = mPlayer + (newPos - mPlayer).normalized * (GetComponent<SpriteRenderer>().size.x * (1.5f + scale));
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        launchProjectile();
    }
    public float setAngle(Vector3 dir)
    {
        // 오일러각(0~360)
        return Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    }
    // 스폰형 타겟방향 단위벡터만큼 떨어져서 스폰
    // 자기주위 스폰
    // 랜덤 스폰형(player주변의 일정한 크기의 원에서 랜덤하게 스폰)
    // 일반형인지 랜덤형인지 구분할 변수가 필요(projectileSpec에서 랜덤형 구분)
    public override void setEnable(Vector3 _target, Vector3 _player, float _angle)
    {
        transform.localScale = new Vector3(AddScaleCoefficient, AddScaleCoefficient, AddScaleCoefficient);
        target = _target;
        mPlayer = _player;
        scale = AddScaleCoefficient - 1;
        // 랜덤형인 경우
        switch ((SpawnType)Enum.Parse(typeof(SpawnType), mSpawnType))
        {
            case SpawnType.General:
                transform.position = mPlayer + (target - mPlayer).normalized * (GetComponent<SpriteRenderer>().size.x * (0.3f + scale));
                angle = setAngle(target - mPlayer) + _angle;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                break;
            case SpawnType.SelfSpawn:
                transform.position = mPlayer;
                break;
            case SpawnType.RandomSpawn:
                float rH = UnityEngine.Random.Range(-3, 4);
                float rV = UnityEngine.Random.Range(-3, 4);
                Vector3 ranPos = new Vector3(rH, rV, 0);
                transform.position = mPlayer + ranPos;
                break;
            case SpawnType.ShortWide:
                // 추후에 정확한 계산공식 구해서 적용
                transform.position = mPlayer + (target - mPlayer).normalized * (GetComponent<SpriteRenderer>().size.x * (0.7f + scale));
                angle = setAngle(target - mPlayer) + _angle;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                break;
        }
        gameObject.SetActive(true);
        isActive = true;
    }

    public override void setDisable()
    {
        mJoyStickPos = Vector3.zero;
        newPos = Vector3.zero;
        isActive = false;
    }
    #endregion
}
