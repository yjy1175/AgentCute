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
        private void Start()
        {
            setInit();
        }
        public void setInit()
        {
            // Todo : 데이터 파싱 후 값 대입
            // Todo : 일반 스킬, 궁극기 스킬 추가 후 착용 스킬 각 첫번째 인덱스로 초기화 후 해당 스킬 isLocked = true
        }
        public void changeSkill(string _skill, int type)
        {
            // 장착 스킬 변경 type 0 - 일반 스킬 1 - 궁극기 스킬
        }
        #endregion
    }
}

