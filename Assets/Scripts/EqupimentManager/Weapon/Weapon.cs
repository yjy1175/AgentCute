using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YJY
{
    public abstract class Weapon : MonoBehaviour
    {
        #region variable
        // key : value
        // 0 : 일반 공격
        // 1 : 일반 스킬
        // 2 : 궁극기
        protected abstract Dictionary<int, GameObject> projectiles
        {
            get;
            set;
        }
        protected abstract Skill generalSkill
        {
            get;
            set;
        }
        protected abstract Skill ultimateSkill
        {
            get;
            set;
        }
        protected abstract WeaponSpec spec
        {
            get;
            set;
        }
        #endregion
        #region method
        protected abstract void setInit();
        protected abstract void autoAttack(GameObject _projectile, float _dmg, Transform _targetMob);
        protected abstract void activeGeneralSkill();
        protected abstract void activeUltimateSkill();
        #endregion
    }
}

