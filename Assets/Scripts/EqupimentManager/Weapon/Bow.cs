using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YJY
{
    public class Bow : Weapon
    {
        #region variable
        protected override Dictionary<int, GameObject> projectiles
        {
            get { return projectiles; }
            set { projectiles = new Dictionary<int, GameObject>(); }
        }
        protected override Skill generalSkill
        {
            get { return generalSkill; }
            set { generalSkill = value; }
        }
        protected override Skill ultimateSkill
        {
            get { return ultimateSkill; }
            set { ultimateSkill = value; }
        }
        protected override WeaponSpec spec
        {
            get { return spec; }
            set { spec = new WeaponSpec(); }
        }
        #endregion

        // Start is called before the first frame update
        void Start()
        {
            setInit();
        }

        // Update is called once per frame
        void Update()
        {

        }

        #region method
        protected override void activeGeneralSkill()
        {
            /* SkillManager를 통하여 해당 Skill 메서드 호출*/
        }

        protected override void activeUltimateSkill()
        {
            /* SkillManager를 통하여 해당 Skill 메서드 호출*/
        }

        protected override void autoAttack(GameObject _projectile, float _dmg, Transform _targetMob)
        {
            /* 여기서 자동공격 매서드 작성합니다.*/
        }

        protected override void setInit()
        {
            /* 여기서 데이터 파싱후 변수에 대입해줍니다.*/


            // 무기 스펙 데이터 파싱
            spec.setData();
        }
        #endregion
    }
}

