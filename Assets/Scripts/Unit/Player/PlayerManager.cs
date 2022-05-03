using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : SingleToneMaker<PlayerManager>
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject mPlayer;
    public GameObject Player
    {
        get { return mPlayer; }
    }
    [SerializeField]
    private SpriteRenderer mWeaponSprite;

    // 플레이어의 스프라이트(코스튬)
    [SerializeField]
    private List<SpriteRenderer> mCostumes;

    [SerializeField]
    private Text mTestCostumeRankText;

    [SerializeField]
    private bool mIsGameStart;

    [SerializeField]
    public Dictionary<int, int> mLevelData;
    public bool IsGameStart
    {
        get { return mIsGameStart; }
        set { mIsGameStart = value; }
    }


    private void Awake()
    {
        mLevelData = new Dictionary<int, int>();
        InitPlayerLevelData();
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
            mPlayer.GetComponent<IStatus>().CriticalChance = float.Parse(playerBaseStatData[i]["PlayerBaseCritChance"].ToString());
            mPlayer.GetComponent<IStatus>().CriticalDamage = float.Parse(playerBaseStatData[i]["PlayerBaseCritDamage"].ToString());
            mPlayer.GetComponent<IStatus>().AttackSpeed = float.Parse(playerBaseStatData[i]["PlayerBaseATKSPD"].ToString());
        }
        WarInfo loadInfo = SaveLoadManager.Instance.LoadPlayerWarData();

        mPlayer.GetComponent<IStatus>().Hp = loadInfo.WarHp;
        mPlayer.GetComponent<IStatus>().MaxHP = loadInfo.WarHp;
        mPlayer.GetComponent<IStatus>().BaseDamage = loadInfo.WarDamage;
        mPlayer.GetComponent<IStatus>().MoveSpeed = loadInfo.WarMoveSpeed;
        mPlayer.GetComponent<PlayerStatus>().Diamond = loadInfo.WarDiamond;

        // 장비, 외형 입히기(코스튬은 입힐 필요 X)
        EquipmentManager.Instance.ChangeWeapon(loadInfo.WarWeaponName);
        EquipmentManager.Instance.ChangeCostume(loadInfo.WarCostumeShapeName);
        if (mPlayer.GetComponent<PlayerStatus>().PlayerCurrentWeapon.name.Substring(0, 2) == "sw" ||
            mPlayer.GetComponent<PlayerStatus>().PlayerCurrentWeapon.name.Substring(0, 2) == "sp")
        {
            mPlayer.GetComponent<IStatus>().AttackSpeed = 1f;
        }
        // 스킬 셋 UI로 불러오기
        UIManager.Instance.SkillSelectUILoad(loadInfo.WarWeaponName.Substring(0, 2));
    }

    private void InitPlayerLevelData()
    {
        List<Dictionary<string, object>> levelData = CSVReader.Read("CSVFile/LevelData");
        for (int i = 0; i < levelData.Count; i++)
        {
            mLevelData[int.Parse(levelData[i]["Level"].ToString())] = int.Parse(levelData[i]["Exp"].ToString());
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

        MusicManager.Instance.OnBackgroundMusic();

        IsGameStart = true;
    }

    public void ResurrectionPlayer()
    {
        mPlayer.GetComponent<PlayerStatus>().Resurrection();
    }
}
