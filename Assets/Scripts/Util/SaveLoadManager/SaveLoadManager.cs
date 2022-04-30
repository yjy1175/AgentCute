using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveLoadManager : SingleToneMaker<SaveLoadManager>
{
    private string mLoadFileName;
    // Start is called before the first frame update
    void Awake()
    {
        mLoadFileName = "PlayerData.json";
        LoadFile();
    }

    private void LoadFile()
    {
        string path = Path.Combine(Application.dataPath, mLoadFileName);
        string fileStream = null;
        try
        {
            fileStream = File.ReadAllText(path);
        }
        catch (FileNotFoundException e)
        {
            CreateSaveFile();
            Debug.Log("오류 : " + e.Message);
            return;
        }
        
        fileStream = AES.Decrypt(fileStream, AES.key);

        LobbyPlayerInfo loadInfo = JsonUtility.FromJson<LobbyPlayerInfo>(fileStream);
        GameObject.Find("LobbyPlayer").GetComponent<LobbyPlayerData>().Info = loadInfo;
    }

    private void CreateSaveFile()
    {
        string path = Path.Combine(Application.dataPath, mLoadFileName);
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
        newInfo.TrainingHp = 0;
        newInfo.TrainingATK = 0;
        newInfo.TrainingAddDamage = 1f;
        newInfo.MoveSpeedRate = 1f;
        newInfo.CurrentWeaponName = "";
        newInfo.CurrentCostumeName = "";
        newInfo.Gold = 999999;
        newInfo.Diamond = 999999;
        newInfo.Stemina = 999;

        string fileStream = JsonUtility.ToJson(newInfo, true);
        fileStream = AES.Encrypt(fileStream, AES.key);

        File.WriteAllText(path, fileStream);

        LoadFile();
    }
}
