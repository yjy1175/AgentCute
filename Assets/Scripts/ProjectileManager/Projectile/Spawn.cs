using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Spawn : Projectile
{
    private bool DEBUG = false;

    #region variable
    [SerializeField]
    private ProjectileSpec spec = new ProjectileSpec();
    public override ProjectileSpec Spec
    {
        get { return spec; }
        set { spec = value; }
    }
    [SerializeField]
    private Vector3 mTarget;
    public override Vector3 Target
    {
        get { return mTarget; }
        set { mTarget = value; }
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
        ReverseSpawn, //목표 오브젝트에 따라 뒤집어지짐
        TargetSpawn, //타겟 위치에 스폰
        NormalizeSpawn,//firePosition에서 단위벡터만큼멀리서 회전하지 않고 스폰
    }
    [SerializeField]
    private VertualJoyStick mUJoySitick;
    [SerializeField]
    private Vector3 mPlayer;
    [SerializeField]
    private Vector3 newPos = Vector3.zero;
    [SerializeField]
    private Vector3 mJoyStickPos = Vector3.zero;
    #endregion
    #region method
    protected override void launchProjectile()
    {
        if (gameObject.layer == LayerMask.NameToLayer("projectile")) { 
            /*스폰 매서드*/
            // 방향키에 따라 위치가 바뀌는 경우
            if (mIsActive && (SpawnType)Enum.Parse(typeof(SpawnType), mSpawnType) == SpawnType.LongWide)
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
                transform.position = mPlayer;
            }
            if(mIsActive && (SpawnType)Enum.Parse(typeof(SpawnType), mSpawnType) == SpawnType.SelfSpawn)
            {
                mPlayer = GameObject.Find("fire").transform.position;
                transform.position = mPlayer;
            }
        }
    }
    // Update is called once per frame
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
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
    public override void setEnable(Vector3 _target, Vector3 _fire, float _angle)
    {
        if (DEBUG)
            Debug.Log("spawn형 spawnType: "+ mSpawnType.ToString()+", enable target: " + _target + ",fire: " + _fire + ",angle: " + _angle);

        mTarget = _target;
        mPlayer = _fire;
        // 랜덤형인 경우
        switch ((SpawnType)Enum.Parse(typeof(SpawnType), mSpawnType))
        {
            case SpawnType.General:
                transform.position = mPlayer;
                angle = setAngle(mTarget - mPlayer) + _angle;
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
                transform.position = mPlayer;
                angle = setAngle(mTarget - mPlayer) + _angle;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                break;
            case SpawnType.ReverseSpawn:
                transform.position = mPlayer;
                float sizeX = transform.localScale.x;
                float sizeY = transform.localScale.y;
                //TO-DO : monsterAttack에 의존성이 생기는 코드라 player도 사용하려면 수정이 필요
                if (CallerPos.x < PlayerManager.Instance.Player.transform.position.x)
                {
                    transform.localScale = new Vector3(sizeX, sizeY, 1f);
                }
                else
                {
                    transform.localScale = new Vector3(-sizeX, sizeY, 1f);
                }
                break;
            //TO-DO 단위벡터를 어디서 받는게 좋을까? 이것또한 받을수 있게 data설정이되어야 좋음
            case SpawnType.NormalizeSpawn:
                Vector3 dir = mTarget - mPlayer;
                dir.Normalize();
                transform.position = mPlayer + dir*2;
                break;
            case SpawnType.TargetSpawn:
                transform.position = mTarget;
                break;
        }
        if (DEBUG)
            Debug.Log("spawn object positon: " + transform.position);

        gameObject.SetActive(true);
        mIsActive = true;
    }

    public override void setDisable()
    {
        mJoyStickPos = Vector3.zero;
        newPos = Vector3.zero;
        mIsActive = false;
    }
    #endregion
}
