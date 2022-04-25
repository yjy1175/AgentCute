using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class WeaponSpec : EquipSpec
{
    #region variable
    // 엑셀 속성 타입, 타입이름, 장비이름, 장비설명, 장비등급, 데미지....
    [SerializeField]
    private string type;
    public override string Type
    {
        get { return type; }
        set { type = value; }
    }

    [SerializeField]
    private string typeName;
    public override string TypeName
    {
        get { return typeName; }
        set { typeName = value; }
    }

    [SerializeField]
    private string equipName;
    public override string EquipName
    {
        get { return equipName; }
        set { equipName = value; }
    }

    [SerializeField]
    private string equipDesc;
    public override string EquipDesc
    {
        get { return equipDesc; }
        set { equipDesc = value; }
    }

    [SerializeField]
    private int equipRank;
    public override int Rank
    {
        get { return equipRank; }
        set { equipRank = value; }
    }

    [SerializeField]
    private int weaponDamage;
    public int WeaponDamage
    {
        get { return weaponDamage; }
        set { weaponDamage = value; }
    }

    [SerializeField]
    private float weaponAttackSpeed;
    public float WeaponAttackSpeed
    {
        get { return weaponAttackSpeed; }
        set { weaponAttackSpeed = value; }
    }

    [SerializeField]
    private int weaponAttackRange;
    public int WeaponAttackRange
    {
        get { return weaponAttackRange; }
        set { weaponAttackRange = value; }
    }

    [SerializeField]
    private float weaponAddSpeed;
    public float WeaponAddSpeed
    {
        get { return weaponAddSpeed; }
        set { weaponAddSpeed = value; }
    }
    #endregion
    #region method
    #endregion
}

