using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WarInfo
{
    // 전투 씬에 보낼 게임 모드
    [SerializeField]
    private DeongunStartManager.GameMode mWarMode;
    public DeongunStartManager.GameMode WarMode
    {
        get { return mWarMode; }
        set { mWarMode = value; }
    }
    // 전투 씬에 보낼 종합 HP
    [SerializeField]
    private int mWarHp;
    public int WarHp
    {
        get { return mWarHp; }
        set { mWarHp = value; }
    }
    // 전투 씬에 보낼 종합 데미지
    [SerializeField]
    private int mWarDamage;
    public int WarDamage
    {
        get { return mWarDamage; }
        set { mWarDamage = value; }
    }
    // 전투 씬에 보낼 종합 이동 속도
    [SerializeField]
    private float mWarMoveSpeed;
    public float WarMoveSpeed
    {
        get { return mWarMoveSpeed; }
        set { mWarMoveSpeed = value; }
    }

    // 전투 씬에 보낼 다이아 수량
    [SerializeField]
    private int mWarDiamond;
    public int WarDiamond
    {
        get { return mWarDiamond; }
        set { mWarDiamond = value; }
    }


    // 전투 씬에 보낼 장착 무기 이름
    [SerializeField]
    private string mWarWeaponName;
    public string WarWeaponName
    {
        get { return mWarWeaponName; }
        set { mWarWeaponName = value; }
    }
    // 전투 씬에 보낼 장착 코스튬 이름
    [SerializeField]
    private string mWarCostumeName;
    public string WarCostumeName
    {
        get { return mWarCostumeName; }
        set { mWarCostumeName = value; }
    }
    // 전투 씬에 보낼 외형 코스튬 이름
    [SerializeField]
    private string mWarCostumeShapeName;
    public string WarCostumeShapeName
    {
        get { return mWarCostumeShapeName; }
        set { mWarCostumeShapeName = value; }
    }

    // 전투 씬에 보낼 던전 버프 스텟
    [SerializeField]
    private DeongunStartManager.DeongunBuffType mWarDeongunBuffType;
    public DeongunStartManager.DeongunBuffType WarDeongunBuffType
    {
        get { return mWarDeongunBuffType; }
        set { mWarDeongunBuffType = value; }
    }
    [SerializeField]
    private int mWarBuffHp;
    public int WarBuffHp
    {
        get { return mWarBuffHp; }
        set { mWarBuffHp = value; }
    }

    [SerializeField]
    private float mWarBuffSpeedRate;
    public float WarBuffSpeedRate
    {
        get { return mWarBuffSpeedRate; }
        set { mWarBuffSpeedRate = value; }
    }

    // 전투 씬에 보낼 스킬 해금 정보
    [SerializeField]
    private StringBoolean mWarSkillLock;
    public StringBoolean WarSkillLock
    {
        get { return mWarSkillLock; }
        set { mWarSkillLock = value; }
    }

    // 전투 씬에 보낼 모드 정보
    [SerializeField]
    private bool mIsBossRelay;
    public bool IsBossRelay
    {
        get { return mIsBossRelay; }
        set { mIsBossRelay = value; }
    }

    // 전투 씬에 보낼 귀여워지는 물약 사용 여부
    [SerializeField]
    private bool mIsWarCutePotion;
    public bool IsWarCutePotion
    {
        get { return mIsWarCutePotion; }
        set { mIsWarCutePotion = value; }
    }
}
