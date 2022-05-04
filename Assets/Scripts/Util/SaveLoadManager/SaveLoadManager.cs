using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;

public class SaveLoadManager : SingleToneMaker<SaveLoadManager>
{
    private string mLoadFileName;
    private string mWarFileName;
    private bool mIsCreate = false;
    // Start is called before the first frame update
    void Awake()
    {
        mLoadFileName = "PlayerData.json";
        mWarFileName = "WarData.json";
    }

    public LobbyPlayerInfo LoadBaseData()
    {
        string path = "";
        path = Path.Combine(Application.persistentDataPath, mLoadFileName);
        string fileStream = null;
        try
        {
            fileStream = File.ReadAllText(path);
        }
        catch
        {
            return CreateSaveFile();
        }
        
        fileStream = AES.Decrypt(fileStream, AES.key);

        LobbyPlayerInfo loadInfo = JsonUtility.FromJson<LobbyPlayerInfo>(fileStream);
        return loadInfo;
    }
    private LobbyPlayerInfo CreateSaveFile()
    {
        mIsCreate = true;
        string path = "";
        path = Path.Combine(Application.persistentDataPath, mLoadFileName);
        LobbyPlayerInfo newInfo = new LobbyPlayerInfo();
        // 초기값 세팅 : TODO -> 추후에 데이터로 초기값 받기
        List<Dictionary<string, object>> playerBaseStatData = CSVReader.Read("CSVFile/PlayerBaseStat");
        for (int i = 0; i < playerBaseStatData.Count; i++)
        {
            newInfo.BaseHp = int.Parse(playerBaseStatData[i]["PlayerBaseHP"].ToString());
            newInfo.BaseATK = int.Parse(playerBaseStatData[i]["PlayerBaseATK"].ToString());
            newInfo.BaseSPD = float.Parse(playerBaseStatData[i]["PlayerBaseSPD"].ToString());
            newInfo.BaseCriticalChance = float.Parse(playerBaseStatData[i]["PlayerBaseCritChance"].ToString());
            newInfo.BaseCriticalDamage = float.Parse(playerBaseStatData[i]["PlayerBaseCritDamage"].ToString());
            newInfo.BaseATKSPD = float.Parse(playerBaseStatData[i]["PlayerBaseATKSPD"].ToString());
        }
        newInfo.TrainingLevelList = new List<int>();
        newInfo.TrainingLevelList.Add(0);
        newInfo.TrainingLevelList.Add(0);
        newInfo.TrainingLevelList.Add(0);
        newInfo.TrainingLevelList.Add(0);
        newInfo.TrainingHp = 0;
        newInfo.TrainingATK = 0;
        newInfo.TrainingAddDamage = 1f;
        newInfo.MoveSpeedRate = 1f;
        newInfo.CurrentWeaponName = "";
        newInfo.CurrentCostumeName = "";
        newInfo.CurrentCostumeShapeName = "";
        newInfo.Gold = 10000;
        newInfo.Diamond = 10000;
        newInfo.Stemina = 999;
        List<Dictionary<string, object>> weponLockData = CSVReader.Read("CSVFile\\Weapon");
        newInfo.Weaponlock = new StringBoolean();
        for(int i = 0; i < weponLockData.Count; i++)
        {
            string weaponName = weponLockData[i]["WeaponType"].ToString();
            bool locked = Convert.ToBoolean(weponLockData[i]["Weaponlock"].ToString());
            newInfo.Weaponlock.Add(weaponName, locked);
        }
        List<Dictionary<string, object>> costumeLockData = CSVReader.Read("CSVFile\\Costume");
        newInfo.Costumelock = new StringBoolean();
        for (int i = 0; i < costumeLockData.Count; i++)
        {
            string costumeName = costumeLockData[i]["CostumeLoad"].ToString();
            bool locked = Convert.ToBoolean(costumeLockData[i]["CostumeLock"].ToString());
            newInfo.Costumelock.Add(costumeName, locked);
        }
        List<Dictionary<string, object>> skillLockData = CSVReader.Read("CSVFile\\Skill");
        newInfo.Skilllock = new StringBoolean();
        for (int i = 0; i < skillLockData.Count; i++)
        {
            string skillName = skillLockData[i]["SkillNameENG"].ToString();
            bool locked = Convert.ToBoolean(skillLockData[i]["SkillLock"].ToString());
            newInfo.Skilllock.Add(skillName, locked);
        }
        string fileStream = JsonUtility.ToJson(newInfo, true);
        fileStream = AES.Encrypt(fileStream, AES.key);

        File.WriteAllText(path, fileStream);
        mIsCreate = false;
        return newInfo;
    }
    
    public void SavePlayerInfoFile(LobbyPlayerInfo _info)
    {
        if (!mIsCreate)
        {
            string path = "";
            path = Path.Combine(Application.persistentDataPath, mLoadFileName);
            string fileStream = JsonUtility.ToJson(_info, true);
            fileStream = AES.Encrypt(fileStream, AES.key);
            File.WriteAllText(path, fileStream);
            Debug.Log("데이터 저장");
        }
    }

    // 전투씬에 보낼 데이터 저장
    public void SavePlayerWarData(WarInfo _info)
    {
        string path = "";
        path = Path.Combine(Application.persistentDataPath, mWarFileName);
        string fileStream = JsonUtility.ToJson(_info, true);
        fileStream = AES.Encrypt(fileStream, AES.key);
        File.WriteAllText(path, fileStream);
    }

    // 전투씬에서 로드할 데이터
    public WarInfo LoadPlayerWarData()
    {
        string path = "";
        path = Path.Combine(Application.persistentDataPath, mWarFileName);
        string fileStream = File.ReadAllText(path);

        fileStream = AES.Decrypt(fileStream, AES.key);

        WarInfo loadInfo = JsonUtility.FromJson<WarInfo>(fileStream);
        return loadInfo;
    }

    // 전투가 끝난 후 저장하는 데이터
    public void SaveWarEndData()
    {
        LobbyPlayerInfo info = LoadBaseData();

        // 전투가 끝난 후 변경사항 저장
        // 얻은 골드, 다이아...
        // TODO : 업적관리에 필요한 데이터 추가
        info.Diamond = PlayerManager.Instance.Player.GetComponent<PlayerStatus>().Diamond;
        info.Gold += PlayerManager.Instance.Player.GetComponent<PlayerStatus>().GainGold;

        SavePlayerInfoFile(info);
    }
}
