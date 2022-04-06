using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Weapon : MonoBehaviour
{
    #region variable
    [SerializeField]
    private WeaponSpec spec = new WeaponSpec();
    public WeaponSpec Spec
    {
        get { return spec; }
        set { spec = value; }
    }
    [SerializeField]
    private bool isLocked;
    public bool IsLocked
    {
        get { return isLocked; }
        set { isLocked = value; }
    }
    #endregion
    #region method
    //public void changeSkill(string _skillName, Skill.SkillType _type)
    //{
    //    Skill newSkill = _type == Skill.SkillType.GENERAL ?
    //        Spec.getGeneralSkill().Find(item => item.SkillName == _skillName) :
    //        Spec.getUltimateSkill().Find(item => item.SkillName == _skillName);
    //    // 해당 스킬이 해금이 되어있는지 확인
    //    if (newSkill.IsLocked)
    //    {
    //        // 장착 스킬 변경 type 0 - 일반 스킬 1 - 궁극기 스킬
    //        switch (_type)
    //        {
    //            case Skill.SkillType.GENERAL:
    //                currentGeneralSkill = newSkill;
    //                break;
    //            case Skill.SkillType.ULTIMATE:
    //                currentUltimateSkill = newSkill;
    //                break;
    //        }
    //        // 스킬 완료 문구 출력
    //    }
    //    else
    //    {
    //        // 스킬 해금이 안되있다는 문구 출력
    //    }
    //}
    #endregion
}


