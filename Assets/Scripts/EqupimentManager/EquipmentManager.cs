using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YJY
{
    public class EquipmentManager : MonoBehaviour
    {
        // Start is called before the first frame update
        #region variable
        // key : 장비 분류(0: 무기, 1: 코스튬...) , value : <key : typename, value : 해당장비 오브젝트 
        public PBL.IntStringGameObject equipments;
        public PBL.IntGameObject userCurrentEquip;
        // key : 몬스터 분류 , value : 해당 몬스터 장비 오브젝트
        public PBL.IntGameObject monsterCurrentEquip;
        #endregion
        void Start()
        {
            //initAllEquips();
            //loadUserEquip();
#if DEBUG_TEST
            for(int i = 0; i < equipments.Count; i++)
            {
                foreach(GameObject item in equipments[i].Values)
                {
                    Instantiate(item, transform);
                }
            }
#endif
        }
        #region method
        // 모든 장비류 오브젝트 데이터 파싱 후 값 저장하는 함수
        public void initAllEquips()
        {
            
            // Weapon
            // csv파일을 읽어서 리스트형식으로 저장
            List<Dictionary<string, object>> weaponData = PBL.CSVReader.Read("CSVFile\\Weapon");
            for(int i = 0; i < weaponData.Count; i++)
            {
                Dictionary<string, GameObject> weapons = equipments[0];
                // 한줄씩 불러와서 오브젝트 대입
                foreach(GameObject item in weapons.Values)
                {
                    Weapon newWeapon = item.GetComponent<Weapon>();
                    newWeapon.Spec.Type = weaponData[i]["WeaponType"].ToString();
                    newWeapon.Spec.TypeName = weaponData[i]["WeaponTypeName"].ToString();
                    newWeapon.Spec.EquipName = weaponData[i]["WeaponName"].ToString();
                    newWeapon.Spec.EquipDesc = weaponData[i]["WeaponDesc"].ToString();
                    newWeapon.Spec.EquipRank = int.Parse(weaponData[i]["WeaponRank"].ToString());
                    newWeapon.Spec.WeaponDamage = float.Parse(weaponData[i]["WeaponDamage"].ToString());
                    newWeapon.Spec.WeaponAttackSpeed = float.Parse(weaponData[i]["WeaponAttackSpeed"].ToString());
                    newWeapon.Spec.WeaponAttackRange = int.Parse(weaponData[i]["WeaponAttackRange"].ToString());
                    newWeapon.Spec.AttackProjectile = weaponData[i]["AttackProjectile"].ToString();

                    string[] projectiles = weaponData[i]["GeneralSkillProjectile"].ToString().Split('/');
                    string[] coolTimes = weaponData[i]["GeneralSkillCoolTime"].ToString().Split('/');
                    string[] coefficients = weaponData[i]["GeneralSkillCoefficient"].ToString().Split('/');

                    for(int j = 0; j < projectiles.Length; j++)
                    {
                        newWeapon.Spec.addGeneralSkill(
                            projectiles[i], 
                            int.Parse(coolTimes[i]), 
                            int.Parse(coefficients[i]), 
                            j == 0 ? true : false);
                    }

                    projectiles = weaponData[i]["UltimateSkillProjectile"].ToString().Split('/');
                    coolTimes = weaponData[i]["UltimateSkillCoolTime"].ToString().Split('/');
                    coefficients = weaponData[i]["UltimateSkillCoefficient"].ToString().Split('/');

                    for (int j = 0; j < projectiles.Length; j++)
                    {
                        newWeapon.Spec.addUltimateSkill(
                            projectiles[i],
                            int.Parse(coolTimes[i]),
                            int.Parse(coefficients[i]),
                            j == 0 ? true : false);
                    }
                    // 각 무기별 기본 스킬 세팅
                    newWeapon.changeSkill(newWeapon.Spec.getGeneralSkill()[0].ProjectileType, 0);
                    newWeapon.changeSkill(newWeapon.Spec.getUltimateSkill()[0].ProjectileType, 1);
                }
            }
            // Costume
            List<Dictionary<string, object>> costumeData = PBL.CSVReader.Read("CSVFile\\Costume");
            for (int i = 0; i < costumeData.Count; i++)
            {
                Dictionary<string, GameObject> costumes = equipments[1];
                foreach(GameObject item in costumes.Values)
                {
                    Costume newCostume = item.GetComponent<Costume>();
                    newCostume.Spec.TypeName = costumeData[i]["CostumeTypeName "].ToString();
                    newCostume.Spec.EquipName = costumeData[i]["CostumeName "].ToString();
                    newCostume.Spec.EquipDesc = costumeData[i]["CostumeDesc "].ToString();
                    newCostume.Spec.EquipRank = int.Parse(costumeData[i]["CostumeRank "].ToString());
                    newCostume.IsLocked = false;
                }
            }
        }
        // 장착중인 무기 객체에 접근하여 장착중인 일반 스킬 사용하는 함수
        public void generalSkillClicked()
        {

        }
        // 장착중인 무기 객체에 접근하여 장착중인 궁극기 스킬 사용하는 함수
        public void ultimateSkillClicked()
        {

        }
        // 착용중인 장비를 교체해주는 함수
        // name : 바꿀 장비의 typeName
        // type : 0 무기, 1 코스튬
        public void changeEquip(string name, int type)
        {

        }
        private void loadUserEquip()
        {
            // 유저의 데이터 로드 후 적용
        }
#endregion
    }
}