using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : SingleToneMaker<PlayerManager>
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject mPlayer;
    [SerializeField]
    private SpriteRenderer mWeaponSprite;

    // ÇÃ·¹ÀÌ¾îÀÇ ½ºÇÁ¶óÀÌÆ®(ÄÚ½ºÆ¬)
    [SerializeField]
    private List<SpriteRenderer> mCostumes;

    [SerializeField]
    private Text mTestCostumeRankText;

    private void Awake()
    {
        mPlayer = GameObject.Find("PlayerObject");
        InitPlayerBaseStat();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void InitPlayerBaseStat()
    {
        List<Dictionary<string, object>> playerBaseStatData = CSVReader.Read("CSVFile/PlayerBaseStat");
        for(int i = 0; i < playerBaseStatData.Count; i++)
        {
            mPlayer.GetComponent<IStatus>().Hp = int.Parse(playerBaseStatData[i]["PlayerBaseHP"].ToString());
            mPlayer.GetComponent<IStatus>().BaseDamage = int.Parse(playerBaseStatData[i]["PlayerBaseATK"].ToString());
            mPlayer.GetComponent<PlayerStatus>().Speed = float.Parse(playerBaseStatData[i]["PlayerBaseSPD"].ToString());
            mPlayer.GetComponent<PlayerStatus>().PlayerCritChance = float.Parse(playerBaseStatData[i]["PlayerBaseCritChance"].ToString());
            mPlayer.GetComponent<PlayerStatus>().PlayerCritDamage = float.Parse(playerBaseStatData[i]["PlayerBaseCritDamage"].ToString());
            mPlayer.GetComponent<PlayerStatus>().PlayerATKSPD = float.Parse(playerBaseStatData[i]["PlayerBaseATKSPD"].ToString());
        }
    }
    public SpriteRenderer getPlayerWeaponSprite()
    {
        return mWeaponSprite;
    }

    public SpriteRenderer GetPlayerCostumeSpriteRenderer(int _num)
    {
        return mCostumes[_num];
    }

    public void SettingGameStart()
    {
        mPlayer.GetComponent<PlayerAttack>().getProjectiles();
        MapManager.Instance.RandomMapSelect();
        string weaponType =
            mPlayer.GetComponent<PlayerStatus>()
            .PlayerCurrentWeapon.GetComponent<Weapon>().Spec.Type;
        weaponType = weaponType.Substring(0, 2);
        string costumeName = "";
        float chance = 0.5f;
        foreach(string cName in EquipmentManager.Instance.Costumes.Keys)
        {
            if (cName.Replace("cst", "").Contains(weaponType))
            {
                int random = Random.Range(0, 100);
                if( random < 50)
                {
                    costumeName = cName;
                    break;
                }
                else
                {
                    chance *= 0.5f;
                }
            }
        }
        if(costumeName == "")
        {
            costumeName = "cstbase";
            mTestCostumeRankText.text = Mathf.Round(chance * 10000f) * 0.01f + "%ÀÇ È®·ü·Î ±âº» ÄÚ½ºÆ¬ ´çÃ·!!";
        }
        else
        {
            mTestCostumeRankText.text = Mathf.Round(chance * 10000f) * 0.01f + "%ÀÇ È®·ü·Î " + costumeName.Substring(8, 1) + "´Ü°èÀÇ ÄÚ½ºÆ¬ ´çÃ·!!";
        }
        EquipmentManager.Instance.ChangeCostume(costumeName);
        
    }
}
