using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TrainingManager : SingleToneMaker<TrainingManager>
{
    [Serializable]
    public struct Training
    {
        public TrainingType mType;
        public string mDesc;
        public int mLevel;
        public int mIncrease;
        public int mMax;
        public int mCostMin;
        public int mCostIncrease;
        public TrainingType mPriorType;

        public int mCurrentCost;
        public int mCurrentValue;
        public string mUnit;
    }
    public enum TrainingType
    {
        None,
        PlayerHp,
        PlayerATK,
        PlayerDamage,
        PlayerDamageZ,
        Exit,
    }
    [SerializeField]
    private TrainingSet mTrainingSet;
    public TrainingSet TrainingSet
    {
        get { return mTrainingSet; }
        set { mTrainingSet = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        initTrainingSet();
    }

    // csv데이터와, 플레이어의 데이터를 통하여 초기화
    private void initTrainingSet()
    {
        List<Dictionary<string, object>> trainingData = CSVReader.Read("CSVFile/TrainingData");
        List<int> playerSaveData = GameObject.Find("LobbyPlayer").GetComponent<LobbyPlayerData>().Info.TrainingLevelList;

        for(TrainingType type = TrainingType.PlayerHp; type < TrainingType.Exit; type++)
        {
            Training newTraining = new Training();
            newTraining.mType = (TrainingType)Enum.Parse(typeof(TrainingType), trainingData[(int)type - 1]["StatusType"].ToString());
            newTraining.mDesc = trainingData[(int)type - 1]["StatusDesc"].ToString();
            newTraining.mIncrease = int.Parse(trainingData[(int)type - 1]["StatusIncrease"].ToString());
            newTraining.mMax = int.Parse(trainingData[(int)type - 1]["StatusMax"].ToString());
            newTraining.mCostMin = int.Parse(trainingData[(int)type - 1]["StatusCostMin"].ToString());
            newTraining.mCostIncrease = int.Parse(trainingData[(int)type - 1]["StatusCostIncrease"].ToString());
            newTraining.mUnit = trainingData[(int)type - 1]["StatusUnit"].ToString();
            newTraining.mPriorType = (TrainingType)Enum.Parse(typeof(TrainingType), trainingData[(int)type - 1]["StatusPriorType"].ToString());

            newTraining.mLevel = playerSaveData[(int)type - 1];
            newTraining.mCurrentValue = newTraining.mLevel * newTraining.mIncrease;
            newTraining.mCurrentCost = newTraining.mCostMin + newTraining.mLevel * newTraining.mCostIncrease;

            mTrainingSet.Add(type, newTraining);
        }
    }

    // 훈련 레벨업 시 데이터 수정
    public void LevelUpTraining(TrainingType _type)
    {
        Training newTraining = mTrainingSet[_type];
        mTrainingSet.Remove(_type);

        newTraining.mLevel++;
        newTraining.mCurrentValue = newTraining.mLevel * newTraining.mIncrease;
        newTraining.mCurrentCost = newTraining.mCostMin + newTraining.mLevel * newTraining.mCostIncrease;

        LobbyPlayerInfo playerInfo = GameObject.Find("LobbyPlayer").GetComponent<LobbyPlayerData>().Info;
        playerInfo.TrainingLevelList[(int)_type - 1] += 1;
        switch (_type)
        {
            case TrainingType.PlayerHp:
                playerInfo.TrainingHp += newTraining.mIncrease;
                break;
            case TrainingType.PlayerATK:
                playerInfo.TrainingATK += newTraining.mIncrease;
                break;
            case TrainingType.PlayerDamage:
                playerInfo.TrainingAddDamage += newTraining.mIncrease * 0.01f;
                break;
            case TrainingType.PlayerDamageZ:
                playerInfo.TrainingAddDamage += newTraining.mIncrease * 0.01f;
                break;
        }
        
        mTrainingSet.Add(_type, newTraining);
    }

    // 다음레벨 데이터 리턴하는 API
    // 다음 레벨 value리턴
    public int NextLevelValue(TrainingType _type)
    {
        Training newTraining = mTrainingSet[_type];

        return (newTraining.mLevel + 1) * newTraining.mIncrease;
    }
    // 다음 레벨 cost리턴
    public int NextLevelCost(TrainingType _type)
    {
        Training newTraining = mTrainingSet[_type];

        return newTraining.mCostMin + (newTraining.mLevel + 1) * newTraining.mCostIncrease;
    }
    // 현재 훈련을 진행할 수있는지 판단
    public bool PossibleForLevelUp(TrainingType _type)
    {
        Training newTraining = mTrainingSet[_type];

        if (newTraining.mPriorType == TrainingType.None)
            return true;
        else
        {
            if (mTrainingSet[newTraining.mPriorType].mCurrentValue == mTrainingSet[newTraining.mPriorType].mMax)
                return true;
            else
                return false;
        }
    }
}
