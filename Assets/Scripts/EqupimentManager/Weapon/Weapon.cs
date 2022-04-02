using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Weapon : MonoBehaviour
{
    #region variable
    private WeaponSpec spec = new WeaponSpec();
    public WeaponSpec Spec
    {
        get { return spec; }
        set { spec = value; }
    }
    private Skill currentGeneralSkill;
    public Skill CurrentGeneralSkill
    {
        get { return currentGeneralSkill; }
    }
    private Skill currentUltimateSkill;
    public Skill CurrentUltimateSkill
    {
        get { return currentUltimateSkill; }
    }
    private bool isLocked;
    public bool IsLocked
    {
        get { return isLocked; }
        set { isLocked = value; }
    }
    #endregion
    #region method
    public void changeSkill(string _projectileType, int type)
    {
        Skill newSkill = type == 0 ?
            Spec.getGeneralSkill().Find(item => item.ProjectileType == _projectileType) :
            Spec.getUltimateSkill().Find(item => item.ProjectileType == _projectileType);
        // 해당 스킬이 해금이 되어있는지 확인
        if (newSkill.IsLocked)
        {
            // 장착 스킬 변경 type 0 - 일반 스킬 1 - 궁극기 스킬
            switch (type)
            {
                case 0:
                    currentGeneralSkill = newSkill;
                    break;
                case 1:
                    currentUltimateSkill = newSkill;
                    break;
            }
            // 스킬 완료 문구 출력
        }
        else
        {
            // 스킬 해금이 안되있다는 문구 출력
        }
    }
    #endregion
}


