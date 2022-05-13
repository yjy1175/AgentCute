using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TrainingManager : SingleToneMaker<TrainingManager>
{
    [Serializable]
    public struct Training
    {
        public TrainingType mType;
        public string mName;
        public string mDesc;
        public int mLevel;
        public float mIncrease;
        public float mMax;
        public int mCostMin;
        public int mCostIncrease;
        public TrainingType mPriorType;
        public Sprite mIcon;

        public int mCurrentCost;
        public float mCurrentValue;
        public string mUnit;
    }
    public enum TrainingType
    {
        None,
        PlayerHP,
        PlayerATK,
        PlayerDamage,
        PlayerDamageZ,
        PlayerMagnet,
        PlayerGold,
        PlayerRevive,
        PlayerShield,
        PlayerDodge,
        Exit,
    }

    [SerializeField]
    private TrainingSet mTrainingSet;
    public TrainingSet TrainingSet
    {
        get { return mTrainingSet; }
        set { mTrainingSet = value; }
    }

    [SerializeField]
    private TrainingButtonSet mTrainingButtonSet;
    public TrainingButtonSet TrainingButtonSet
    {
        get => mTrainingButtonSet;
        set
        {
            mTrainingButtonSet = value;
        }
    }

    [SerializeField]
    private GameObject mTrainingPannel;
    [SerializeField]
    private GameObject mContainer;
    [SerializeField]
    private GameObject mPrefab;

    [SerializeField]
    private TrainingType mCurrentSelectType;
    public TrainingType CurrentSelectType => mCurrentSelectType;

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

        for(TrainingType type = TrainingType.PlayerHP; type < TrainingType.Exit; type++)
        {
            Training newTraining = new Training();
            newTraining.mType = (TrainingType)Enum.Parse(typeof(TrainingType), trainingData[(int)type - 1]["StatusType"].ToString());
            newTraining.mName = trainingData[(int)type - 1]["StatusName"].ToString();
            newTraining.mDesc = trainingData[(int)type - 1]["StatusDesc"].ToString();
            newTraining.mIncrease = float.Parse(trainingData[(int)type - 1]["StatusIncrease"].ToString());
            newTraining.mMax = float.Parse(trainingData[(int)type - 1]["StatusMax"].ToString());
            newTraining.mCostMin = int.Parse(trainingData[(int)type - 1]["StatusCostMin"].ToString());
            newTraining.mCostIncrease = int.Parse(trainingData[(int)type - 1]["StatusCostIncrease"].ToString());
            newTraining.mUnit = trainingData[(int)type - 1]["StatusUnit"].ToString();
            newTraining.mPriorType = (TrainingType)Enum.Parse(typeof(TrainingType), trainingData[(int)type - 1]["StatusPriorType"].ToString());
            newTraining.mIcon = Resources.Load<Sprite>(trainingData[(int)type - 1]["StatusIcon"].ToString()); 

            newTraining.mLevel = playerSaveData[(int)type - 1];
            newTraining.mCurrentValue = newTraining.mLevel * newTraining.mIncrease;
            newTraining.mCurrentCost = newTraining.mCostMin + newTraining.mLevel * newTraining.mCostIncrease;

            mTrainingSet.Add(type, newTraining);

            // 훈련 패널에 Item추가
            GameObject newButton = Instantiate(mPrefab, mContainer.transform);
            // Item에 클릭리스너 추가
            TrainingType newType = type;
            newButton.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => {
                ClickStatus(newType);
            });
            // 버튼 셋에 추가
            mTrainingButtonSet.Add(type, newButton);
        }
    }
    // 각 스텟 버튼 리스너
    private void ClickStatus(TrainingType _type)
    {
        // 현재 선택중인 타입 변경
        mCurrentSelectType = _type;
        // 선택 표시
        for(TrainingType type = TrainingType.PlayerHP; type < TrainingType.Exit; type++)
        {
            if(type == _type)
                mTrainingButtonSet[type].transform.GetChild(0).GetChild(4).gameObject.SetActive(true);
            else
                mTrainingButtonSet[type].transform.GetChild(0).GetChild(4).gameObject.SetActive(false);
        }
        // 정보창에 정보 표출
        mTrainingPannel.transform.GetChild(3).GetChild(0).GetComponent<Image>().sprite =
            mTrainingSet[_type].mIcon;
        mTrainingPannel.transform.GetChild(3).GetChild(1).GetComponent<Text>().text =
            mTrainingSet[_type].mName;
        mTrainingPannel.transform.GetChild(3).GetChild(2).GetComponent<Text>().text =
            mTrainingSet[_type].mDesc;
        mTrainingPannel.transform.GetChild(3).gameObject.SetActive(true);
    }
    // 스텟 리스트 UI 업데이트
    public void UpdateStstus()
    {
        for (TrainingType type = TrainingType.PlayerHP; type < TrainingType.Exit; type++)
        {
            Training training = mTrainingSet[type];
            GameObject button = mTrainingButtonSet[type];
            // 훈련 가능한지 판단
            if (PossibleForLevelUp(type))
            {
                // 해당 훈련 아이콘 표시
                button.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = training.mIcon;
                // 해당 훈련이 Max이면 레벨수치 Max로 표기
                if (training.mMax == training.mCurrentValue)
                {
                    button.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = "MAX";
                    button.transform.GetChild(0).GetChild(1).GetComponent<Text>().color = Color.red;
                }
                else
                {
                    button.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = "Lv. " + training.mLevel.ToString();
                    button.transform.GetChild(0).GetChild(1).GetComponent<Text>().color = Color.yellow;
                }

                // 네임 및 수치 표기
                button.transform.GetChild(0).GetChild(2).GetComponent<Text>().text = training.mName;
                button.transform.GetChild(0).GetChild(3).GetComponent<Text>().text = "+ " + training.mCurrentValue + training.mUnit;
            }
            else
            {
                // 잠금 표기
                button.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = mTrainingSet[training.mPriorType].mName;
                button.transform.GetChild(1).gameObject.SetActive(true);
            }
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
            case TrainingType.PlayerHP:
                playerInfo.TrainingHp += (int)newTraining.mIncrease;
                break;
            case TrainingType.PlayerATK:
                playerInfo.TrainingATK += (int)newTraining.mIncrease;
                break;
            case TrainingType.PlayerDamage:
                playerInfo.TrainingAddDamage += newTraining.mIncrease * 0.01f;
                break;
            case TrainingType.PlayerDamageZ:
                playerInfo.TrainingAddDamage += newTraining.mIncrease * 0.01f;
                break;
            case TrainingType.PlayerMagnet:
                playerInfo.TrainingMagnetPower += (int)newTraining.mIncrease;
                break;
            case TrainingType.PlayerRevive:
                playerInfo.TrainingRevive += (int)newTraining.mIncrease;
                break;
            case TrainingType.PlayerGold:
                playerInfo.TrainingGoldPower += newTraining.mIncrease * 0.01f;
                break;
            case TrainingType.PlayerShield:
                playerInfo.TrainingShieldTime += newTraining.mIncrease;
                break;
            case TrainingType.PlayerDodge:
                playerInfo.TrainingDodgeTime += (int)newTraining.mIncrease;
                break;
        }
        mTrainingSet.Add(_type, newTraining);
    }

    // 다음레벨 데이터 리턴하는 API
    // 다음 레벨 value리턴
    public float NextLevelValue(TrainingType _type)
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
