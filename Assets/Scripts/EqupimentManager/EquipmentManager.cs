using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class EquipmentManager : SingleToneMaker<EquipmentManager>
{
    // Start is called before the first frame update
    #region variable
    
    // key : 장비 분류(0: 무기, 1: 코스튬...) , value : <key : typename, value : 해당장비 오브젝트 
    [SerializeField]
    private StringGameObject weapons;
    [SerializeField]
    private StringGameObject costumes;

    
    // key : 몬스터 분류 , value : 해당 몬스터 장비 오브젝트
    public StringGameObject monsterCurrentEquip;

    public struct CostumeSprite
    {
        public CostumeSprite(Sprite _sprite, float _r, float _g, float _b)
        {
            sprite = _sprite;
            r = _r;
            g = _g;
            b = _b;
        }
        public Sprite sprite;
        public float r;
        public float g;
        public float b;
    }

    public enum SpriteType
    {
        CostumeHelmet,
        CostumeCloth,
        CostumeArmor,
        CostumePant,
        CostumeBack,
        Exit,
    }

    private const string NULL = "null";
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

                    item.IsLocked = true;
                }
            }
        }

        // Costume
        {

            // Costumes 프리펩을 불러온다
            GameObject[] costumesList = Resources.LoadAll<GameObject>("Prefabs\\Costumes");
            // Dic에 저장해둔다.
            foreach (GameObject costume in costumesList)
            {
                costumes.Add(costume.name, costume);
            }
            List<Dictionary<string, object>> costumeData = CSVReader.Read("CSVFile\\Costume");
            List<Dictionary<string, object>> costumeLoadData = CSVReader.Read("CSVFile\\CostumeLoad");
            Costume item;
            for (int i = 0; i < costumesList.Length; i++)
            {
                // 코스튬 정보 추출 및 추가
                {
                    item = costumes[costumeData[i]["CostumeLoad"].ToString()].GetComponent<Costume>();
                    item.Spec.TypeName = costumeData[i]["CostumeTypeName"].ToString();
                    item.Spec.Name = costumeData[i]["CostumeName"].ToString();
                    item.Spec.Desc = costumeData[i]["CostumeDesc"].ToString();
                    //item.Spec.Rank = int.Parse(costumeData[i]["CostumeRank "].ToString());
                    item.Spec.CoustumeHP = int.Parse(costumeData[i]["CostumeHP"].ToString());
                    item.Spec.CoustumeSpeed = int.Parse(costumeData[i]["CostumeSPD"].ToString());
                    item.IsLocked = false;
                }
            }
            for (int i = 0; i < costumesList.Length; i++)
            {
                // 코스튬 스프라이트 데이터 추출
                {
                    item = costumes[costumeLoadData[i]["CostumeLoadName"].ToString()].GetComponent<Costume>();
                    string path = costumeLoadData[i]["CostumeLoadPath"].ToString();
                    Sprite newSprite = null;
                    string[] rgb = new string[3];
                    // 각 스프라이트와 rgb정보를 저장
                    for(SpriteType j = SpriteType.CostumeHelmet; j < SpriteType.Exit; j++)
                    {
                        if (costumeLoadData[i][j.ToString()].ToString() != NULL)
                            newSprite = Resources.Load<Sprite>(path + costumeLoadData[i][j.ToString()].ToString());
                        if (costumeLoadData[i][j.ToString() +"RGB"].ToString() != NULL)
                            rgb = costumeLoadData[i][j.ToString() + "RGB"].ToString().Split('/');
                        if(newSprite != null)
                        {
                            CostumeSprite CSprite = new CostumeSprite(
                                newSprite, float.Parse(rgb[0]), float.Parse(rgb[1]), float.Parse(rgb[2]));
                            item.AddSpriteList(j, CSprite);
                        }
                    }
                }
            }
            
        }
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
            GameObject.Find("PlayerObject").GetComponent<PlayerStatus>().PlayerCurrentWeapon = cWeapon;
            SpriteRenderer playerSprite = PlayerManager.Instance.getPlayerWeaponSprite();
            playerSprite.sprite = cWeapon.GetComponent<SpriteRenderer>().sprite;
        }
        // 해금이 안되어있다면
        else
        {
            // 경고문구 출력
        }
    }
    public List<GameObject> FindWepaonList(string _type)
    {
        List<GameObject> newList = new List<GameObject>();
        foreach(string key in weapons.Keys)
        {
            string type = weapons[key].GetComponent<Weapon>().Spec.Type.Substring(0, 2);
            if (type == _type)
            {
                newList.Add(weapons[key]);
            }
        }
        return newList;
    }
    private void loadUserEquip()
    {
        // 유저의 데이터 로드 후 적용
    }
    #endregion
}