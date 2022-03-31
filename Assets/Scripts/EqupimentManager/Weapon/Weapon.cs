using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YJY
{
    public class Weapon : MonoBehaviour
    {
        #region variable
        public WeaponSpec spec = new WeaponSpec();
        private Skill currentGeneralSkill;
        private Skill currentUltimateSkill;
        private bool isLocked;
        public bool IsLocked
        {
            get { return isLocked; }
            set { isLocked = value; }
        }
        #endregion
        #region method
        public void changeSkill(string _skill, int type)
        {
            // 장착 스킬 변경 type 0 - 일반 스킬 1 - 궁극기 스킬
        }
        #endregion
    }
}

