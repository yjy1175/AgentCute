using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YJY
{
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
        public override int EquipRank
        {
            get { return equipRank; }
            set { equipRank = value; }
        }

        [SerializeField]
        private float weaponDamage;
        public float WeaponDamage
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
        private string attackProjectile;
        public string AttackProjectile
        {
            get { return attackProjectile; }
            set { attackProjectile = value; }
        }

        [SerializeField]
        private List<Skill> generalSkillList = new List<Skill>();
        [SerializeField]
        private List<Skill> ultimateSkillList = new List<Skill>();
        #endregion
        #region method
        public List<Skill> getGeneralSkill()
        {
            return generalSkillList;
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
        public List<Skill> getUltimateSkill()
        {
            return ultimateSkillList;
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

