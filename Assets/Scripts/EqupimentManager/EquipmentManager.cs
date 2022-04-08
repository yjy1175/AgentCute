using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EquipmentManager : SingleToneMaker<EquipmentManager>
{
    // Start is called before the first frame update
    #region variable
    
    // key : 장비 분류(0: 무기, 1: 코스튬...) , value : <key : typename, value : 해당장비 오브젝트 
    [SerializeField]
    private StringGameObject weapons;
    [SerializeField]
    private StringGameObject costumes;
    [SerializeField]
    private Weapon playerCurrentWeapon;
    [SerializeField]
    private Costume playerCurrentCostume;
    // key : 몬스터 분류 , value : 해당 몬스터 장비 오브젝트
    public StringGameObject monsterCurrentEquip;
    #endregion
    void Start()
    {
        initAllEquips();
        //loadUserEquip();
    }
    #region method
    // 모든 장비류 오브젝트 데이터 파싱 후 값 저장하는 함수
    public void initAllEquips()
    {
        // Weapon
        {
            // Weapons 프리펩을 불러온다
            GameObject[] weaponsList = Resources.LoadAll<GameObject>("Prefabs\\Weapons");
            // Dic에 저장해둔다.
            foreach (GameObject weapon in weaponsList)
            {
                weapons.Add(weapon.name, weapon);
            }
            // csv파일을 읽어서 리스트형식으로 저장
            List<Dictionary<string, object>> weaponData = CSVReader.Read("CSVFile\\Weapon");
            Weapon item;
            for (int i = 0; i < weaponsList.Length; i++)
            {
                // 기본 무기 정보 추출 및 추가
                {
                    item = weapons[weaponData[i]["WeaponType"].ToString()].GetComponent<Weapon>();
                    item.Spec.Type = weaponData[i]["WeaponType"].ToString();
                    item.Spec.TypeName = weaponData[i]["WeaponTypeName"].ToString();
                    item.Spec.Name = weaponData[i]["WeaponName"].ToString();
                    item.Spec.Desc = weaponData[i]["WeaponDesc"].ToString();
                    //item.Spec.EquipRank = int.Parse(weaponData[i]["WeaponRank"].ToString());
                    item.Spec.WeaponDamage = int.Parse(weaponData[i]["WeaponATK"].ToString());
                    //item.Spec.WeaponAttackSpeed = float.Parse(weaponData[i]["WeaponAttackSpeed"].ToString());
                    //item.Spec.WeaponAttackRange = int.Parse(weaponData[i]["WeaponAttackRange"].ToString());
                    item.Spec.WeaponAddSpeed = float.Parse(weaponData[i]["WeaponSPD"].ToString());
                }
            }
        }

        // Costume
        //{
        //    
        //    // Costumes 프리펩을 불러온다
        //    GameObject[] costumesList = Resources.LoadAll<GameObject>("Prefabs\\Costumes");
        //    // Dic에 저장해둔다.
        //    foreach (GameObject costume in costumesList)
        //    {
        //        costumes.Add(costume.name, costume);
        //    }
        //    List<Dictionary<string, object>> costumeData = CSVReader.Read("CSVFile\\Costume");
        //    Costume item;
        //for (int i = 0; i < costumesList.Length; i++)
        //    {
        //        // 코스튬 정보 추출 및 추가
        //        {
        //            item = costumes[costumeData[i]["CostumeType"].ToString()].GetComponent<Costume>();
        //            item.Spec.TypeName = costumeData[i]["CostumeTypeName "].ToString();
        //            item.Spec.EquipName = costumeData[i]["CostumeName "].ToString();
        //            item.Spec.EquipDesc = costumeData[i]["CostumeDesc "].ToString();
        //            item.Spec.EquipRank = int.Parse(costumeData[i]["CostumeRank "].ToString());
        //            item.IsLocked = false;
        //        }
        //    }
        //}
    }
    // 착용중인 장비를 교체해주는 함수
    // name : 바꿀 장비의 typeName
    // type : 0 무기, 1 코스튬
    public void changeEquip(string name)
    {
        // Todo : playerCurrentWeapon의 게임오브젝트 해당 게임오브젝트로 변경
        Weapon cWeapon = weapons[name].GetComponent<Weapon>();
        // 해금이 되어있다면
        if (cWeapon.IsLocked)
        {
            playerCurrentWeapon = cWeapon;
            SpriteRenderer playerSprite = PlayerManager.Instance.getPlayerWeaponSprite();
            playerSprite.sprite = cWeapon.GetComponent<SpriteRenderer>().sprite;
        }
        // 해금이 안되어있다면
        else
        {
            // 경고문구 출력
        }
    }
    private void loadUserEquip()
    {
        // 유저의 데이터 로드 후 적용
    }
    public int getCurrentDamage()
    {
        return playerCurrentWeapon.Spec.WeaponDamage;
    }
    #endregion
}