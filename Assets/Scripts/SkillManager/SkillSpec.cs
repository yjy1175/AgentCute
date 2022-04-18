using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillSpec : EquipSpec
{
    #region variable
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
    private string skillWeaponType;
    public string SkillWeaponType
    {
        get { return skillWeaponType; }
        set { skillWeaponType = value; }
    }

    [SerializeField]
    private string skillName;
    public override string Name
    {
        get { return skillName; }
        set { skillName = value; }
    }

    [SerializeField]
    private string skillDesc;
    public override string Desc
    {
        get { return skillDesc; }
        set { skillDesc = value; }
    }

    [SerializeField]
    private int skillRank;
    public override int Rank
    {
        get { return skillRank; }
        set { skillRank = value; }
    }

    [SerializeField]
    private int skillCount;
    public int SkillCount
    {
        get { return skillCount; }
        set { skillCount = value; }
    }

    [SerializeField]
    private int skillClickCount;
    public int SkillClickCount
    {
        get { return skillClickCount; }
        set { skillClickCount = value; }
    }

    [SerializeField]
    private string skillCoolTimeType;
    public string SkillCoolTimeType
    {
        get { return skillCoolTimeType; }
        set { skillCoolTimeType = value; }
    }

    [SerializeField]
    private float[] mSkillRunTime = new float[2];
    public float[] MSkillRunTime
    {
        get { return mSkillRunTime; }
        set { mSkillRunTime = value; }
    }

    [SerializeField]
    private bool isLocked;
    public bool IsLocked
    {
        get { return isLocked; }
        set { isLocked = value; }
    }

    [SerializeField]
    private List<string> projectiles = new List<string>();
    [SerializeField]
    private List<float> skillCoolTime = new List<float>();
    #endregion
    #region method
    public List<string> getProjectiles()
    {
        return projectiles;
    }
    public void addProjectiles(string _projectile)
    {
        projectiles.Add(_projectile);
    }
    public List<float> getSkillCoolTime()
    {
        return skillCoolTime;
    }
    public void addSkillCoolTime(float _time)
    {
        skillCoolTime.Add(_time);
    }
    #endregion
}


