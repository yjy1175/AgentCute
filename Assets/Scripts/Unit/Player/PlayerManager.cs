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

    [SerializeField]
    private bool mIsGameStart;
    public bool IsGameStart
    {
        get { return mIsGameStart; }
        set { mIsGameStart = value; }
    }

    void Start()
    {
        IsGameStart = false; 
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
        MapManager.Instance.MapSelect();
        SpawnManager.Instance.InitAllSpawnData();

        string weaponType =
            mPlayer.GetComponent<PlayerStatus>()
            .PlayerCurrentWeapon.GetComponent<Weapon>().Spec.Type;
        weaponType = weaponType.Substring(0, 2);
        LevelUpStatusManager.Instance.SetSlots(weaponType);

        IsGameStart = true;
    }
}
