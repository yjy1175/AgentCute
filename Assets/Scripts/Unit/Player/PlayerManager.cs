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

    // 플레이어의 스프라이트(코스튬)
    [SerializeField]
    private List<SpriteRenderer> mCostumes;

    [SerializeField]
    private Text mTestCostumeRankText;

   void Start()
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
            mPlayer.GetComponent<IStatus>().MoveSpeed = float.Parse(playerBaseStatData[i]["PlayerBaseSPD"].ToString());
            mPlayer.GetComponent<IStatus>().CriticalChance = float.Parse(playerBaseStatData[i]["PlayerBaseCritChance"].ToString());
            mPlayer.GetComponent<IStatus>().CriticalDamage = float.Parse(playerBaseStatData[i]["PlayerBaseCritDamage"].ToString());
            mPlayer.GetComponent<IStatus>().AttackSpeed = float.Parse(playerBaseStatData[i]["PlayerBaseATKSPD"].ToString());
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
            costumeName = "cstcommon01";
            mTestCostumeRankText.text = Mathf.Round(chance * 10000f) * 0.01f + "%의 확률로 맨오브스틸 당첨!!";
        }
        else
        {
            mTestCostumeRankText.text = Mathf.Round(chance * 10000f) * 0.01f + "%의 확률로 " + costumeName.Substring(8, 1) + "단계의 코스튬 당첨!!";
        }
        EquipmentManager.Instance.ChangeCostume(costumeName);

        LevelUpStatusManager.Instance.SetSlots(weaponType);
    }
}
