using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class LevelUpStatusManager : SingleToneMaker<LevelUpStatusManager>
{
    [Serializable]
    public enum StatType
    {
        Null,
        AutoAttack,
        CriticalChance,
        AutoAttackSPD,
        MoveSPD,
        AutoAttackTimesMelee,
        AutoAttackTimesRange,
        AutoAttackRange,
        AutoAttackStiff,
        AutoLaunchSpread,
        AutoLaunchThrough,
        RecoverHP
    }
    [SerializeField]
    private List<Stat> AllStatList = new List<Stat>();

    [SerializeField]
    private List<StatType> Slot1List = new List<StatType>();
    [SerializeField]
    private List<StatType> Slot2List = new List<StatType>();
    [SerializeField]
    private List<StatType> Slot3List = new List<StatType>();

    [SerializeField]
    private StatType mSlot1Select;
    [SerializeField]
    private StatType mSlot2Select;
    [SerializeField]
    private StatType mSlot3Select;
    // Start is called before the first frame update
    void Start()
    {
        InitStatData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitStatData()
    {
        List<Dictionary<string, object>> statusData = CSVReader.Read("CSVFile/LevelUpStatus");

        for(int i = 0; i < statusData.Count; i++)
        {
            Stat newStat = new Stat();
            string[] tmp;
            newStat.Type = (StatType)Enum.Parse(typeof(StatType), statusData[i]["StatType"].ToString());

            tmp = statusData[i]["StatWeaponType"].ToString().Split('/');
            newStat.WeaponTypes = new List<string>();
            for (int j = 0; j < tmp.Length; j++)
                newStat.WeaponTypes.Add(tmp[j]);
            tmp = null;

            newStat.Desc = statusData[i]["StatDesc"].ToString();
            newStat.DescEng = statusData[i]["StatDescENG"].ToString();

            tmp = statusData[i]["StatInSlot"].ToString().Split('/');
            newStat.StatInSlot = new List<int>();
            for (int j = 0; j < tmp.Length; j++)
                newStat.StatInSlot.Add(int.Parse(tmp[j]));
            tmp = null;

            tmp = statusData[i]["StatChance"].ToString().Split('/');
            newStat.StatChance = new List<int>();
            for (int j = 0; j < tmp.Length; j++)
                newStat.StatChance.Add(int.Parse(tmp[j]));
            tmp = null;

            newStat.StatIncrease = float.Parse(statusData[i]["StatIncrease"].ToString());
            if (float.Parse(statusData[i]["StatMax"].ToString()) < 0)
                newStat.StatMax = int.MaxValue;
            else
                newStat.StatMax = float.Parse(statusData[i]["StatMax"].ToString());

            tmp = statusData[i]["StatMaxAssign"].ToString().Split('/');
            newStat.StatMaxAssign = new List<StatType>();
            for (int j = 0; j < tmp.Length; j++)
                newStat.StatMaxAssign.Add((StatType)Enum.Parse(typeof(StatType), tmp[j]));
            tmp = null;

            newStat.StatUnit = statusData[i]["StatUnit"].ToString();
            newStat.SelectCount = 0;
            newStat.SelectMaxCount = (int)(newStat.StatMax / newStat.StatIncrease);

            newStat.StatImage = Resources.Load<Sprite>("UI/WarUI/WarIcon/" + newStat.Type.ToString());

            AllStatList.Add(newStat);
        }
    }

    // 각 무기와 슬롯정보에 맞게 슬롯에 넣어준다.
    // _weaponType : 무기의 타입
    public  void SetSlots(string _weaponType)
    {
        //string wpType = _currentWeapon.Spec.Type.Substring(0,2);
        for(int i = 0; i < AllStatList.Count; i++)
        {
            for(int j = 0; j < AllStatList[i].WeaponTypes.Count; j++)
            {
                // 해당타입의 무기의 스텟이 있다면 슬롯에 맞게 넣어준다
                if (AllStatList[i].WeaponTypes[j] == _weaponType && AllStatList[i].Type != StatType.RecoverHP)
                {
                    for(int k = 0; k < AllStatList[i].StatInSlot.Count; k++)
                    {
                        switch (AllStatList[i].StatInSlot[k])
                        {
                            case 1:
                                for(int idx = 0; idx < AllStatList[i].StatChance[k]; idx++)
                                    Slot1List.Add(AllStatList[i].Type);
                                break;
                            case 2:
                                for (int idx = 0; idx < AllStatList[i].StatChance[k]; idx++)
                                    Slot2List.Add(AllStatList[i].Type);
                                break;
                            case 3:
                                for (int idx = 0; idx < AllStatList[i].StatChance[k]; idx++)
                                    Slot3List.Add(AllStatList[i].Type);
                                break;
                        }
                    }
                }
            }
        }

    }

    // 레벨업시 랜덤으로 스텟 뽑아주는 api
    public string SelectStatus(int _num)
    {
        string desc = "";
        switch (_num)
        {
            case 1:
                desc = SelectSlotList(Slot1List, _num);
                break;
            case 2:
                desc = SelectSlotList(Slot2List, _num);
                break;
            case 3:
                desc = SelectSlotList(Slot3List, _num);
                break;
        }

        return desc;
    }

    private string SelectSlotList(List<StatType> _list, int _num)
    {
        string desc = "";
        while (true)
        {
            Stat selectStat;
            if (_list.Count == 0)
            {
                selectStat = AllStatList.Find((item) => item.Type == StatType.RecoverHP);
                switch (_num)
                {
                    case 1:
                        mSlot1Select = selectStat.Type;
                        break;
                    case 2:
                        mSlot2Select = selectStat.Type;
                        break;
                    case 3:
                        mSlot3Select = selectStat.Type;
                        break;
                }
                return desc = selectStat.Desc + selectStat.StatIncrease.ToString() + selectStat.StatUnit;
            }
            int ran = UnityEngine.Random.Range(0, _list.Count);
            selectStat = AllStatList.Find((item) => item.Type == _list[ran]);
            // 선택된 스텟이 맥스이면?
            if (selectStat.SelectCount == selectStat.SelectMaxCount)
            {
                // 우선 해당 스텟을 삭제
                Slot1List.RemoveAll((type) => type == selectStat.Type);
                Slot2List.RemoveAll((type) => type == selectStat.Type);
                Slot3List.RemoveAll((type) => type == selectStat.Type);
                continue;
            }
            else
            {
                switch (_num)
                {
                    case 1:
                        mSlot1Select = selectStat.Type;
                        break;
                    case 2:
                        mSlot2Select = selectStat.Type;
                        break;
                    case 3:
                        mSlot3Select = selectStat.Type;
                        break;
                }
                switch (selectStat.StatUnit)
                {
                    case "%":
                        desc = selectStat.Desc + (selectStat.StatIncrease * 100).ToString() + selectStat.StatUnit;
                        break;
                    default:
                        desc = selectStat.Desc + selectStat.StatIncrease.ToString() + selectStat.StatUnit;
                        break;
                }
                return desc;
            }
        }
    }

    public Sprite SelectSlotImage(int _num)
    {
        StatType selectType = StatType.Null;
        Sprite newSprite = null;
        switch (_num)
        {
            case 1:
                selectType = mSlot1Select;
                break;
            case 2:
                selectType = mSlot2Select;
                break;
            case 3:
                selectType = mSlot3Select;
                break;
        }
        for (int i = 0; i < AllStatList.Count; i++)
        {
            if (AllStatList[i].Type == selectType)
            {
                newSprite = AllStatList[i].StatImage;
            }
        }
        return newSprite;
    }

    // 해당 스텟 선택 시 IStatus의 api를 호출
    public void SelectToStat(int _type)
    {
        StatType selectType = StatType.Null;
        switch (_type)
        {
            case 1:
                selectType = mSlot1Select;
                break;
            case 2:
                selectType = mSlot2Select;
                break;
            case 3:
                selectType = mSlot3Select;
                break;
        }
        for(int i = 0; i < AllStatList.Count; i++)
        {
            if(AllStatList[i].Type == selectType)
            {
                AllStatList[i].PlusSelectCount();
                // IStatus의 api호출
                GameObject.Find("PlayerObject").GetComponent<IStatus>().LevelUpStatus(AllStatList[i]);
                break;
            }
        }
    }
}
