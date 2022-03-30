using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YJY
{
    public class WeaponSpec : EquipSpec
    {
        #region variable
        // 엑셀 속성 타입, 타입이름, 장비이름, 장비설명, 장비등급, 데미지....
        public override string type
        {
            get { return type; }
            set { type = value; }
        }
        public override string typeName
        {
            get { return typeName; }
            set { typeName = value; }
        }
        public override string equipName
        {
            get { return equipName; }
            set { equipName = value; }
        }
        public override string equipDesc
        {
            get { return equipDesc; }
            set { equipDesc = value; }
        }
        public override int equipRank
        {
            get { return equipRank; }
            set { equipRank = value; }
        }

        private float weaponDamage;
        public float WeaponDamage
        {
            get { return weaponDamage; }
            set { weaponDamage = value; }
        }

        private float weaponAttackSpeed;
        public float WeaponAttackSpeed
        {
            get { return weaponAttackSpeed; }
            set { weaponAttackSpeed = value; }
        }

        private int weaponAttackRange;
        public int WeaponAttackRange
        {
            get { return weaponAttackRange; }
            set { weaponAttackRange = value; }
        }

        private string attackProjectile;
        public string AttackProjectile
        {
            get { return attackProjectile; }
            set { attackProjectile = value; }
        }

        private List<Skill> generalSkillList = new List<Skill>();
        private List<Skill> ultimateSkillList = new List<Skill>();
        #endregion
        #region method
        public Skill getGeneralSkill(int index)
        {
            return generalSkillList[index];
        }
        public void addGeneralSkill(string _projectileType, int _coolTime, int _coefficient, bool _isLocked)
        {
            Skill newSkill = new Skill();
            newSkill.ProjectileType = _projectileType;
            newSkill.CoolTime = _coolTime;
            newSkill.Coefficient = _coefficient;
            newSkill.IsLocked = _isLocked;

            generalSkillList.Add(newSkill);
        }
        public Skill getUltimateSkill(int index)
        {
            return ultimateSkillList[index];
        }
        public void addUltimateSkill(string _projectileType, int _coolTime, int _coefficient, bool _isLocked)
        {
            Skill newSkill = new Skill();
            newSkill.ProjectileType = _projectileType;
            newSkill.CoolTime = _coolTime;
            newSkill.Coefficient = _coefficient;
            newSkill.IsLocked = _isLocked;

            ultimateSkillList.Add(newSkill);
        }
        #endregion
    }
}

