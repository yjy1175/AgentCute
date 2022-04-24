using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class Stat
{
    [SerializeField]
    private LevelUpStatusManager.StatType mType;
    public LevelUpStatusManager.StatType Type
    {
        get { return mType; }
        set { mType = value; }
    }
    [SerializeField]
    private List<string> mWeaponTypes;
    public List<string> WeaponTypes
    {
        get { return mWeaponTypes; }
        set { mWeaponTypes = value; }
    }
    [SerializeField]
    private string mDesc;
    public string Desc
    {
        get { return mDesc; }
        set { mDesc = value; }
    }
    [SerializeField]
    private string mDescEng;
    public string DescEng
    {
        get { return mDescEng; }
        set { mDescEng = value; }
    }
    [SerializeField]
    private List<int> mStatInSlot;
    public List<int> StatInSlot
    {
        get { return mStatInSlot; }
        set { mStatInSlot = value; }
    }
    [SerializeField]
    private List<int> mStatChance;
    public List<int> StatChance
    {
        get { return mStatChance; }
        set { mStatChance = value; }
    }
    [SerializeField]
    private float mStatIncrease;
    public float StatIncrease
    {
        get { return mStatIncrease; }
        set { mStatIncrease = value; }
    }
    [SerializeField]
    private float mStatMax;
    public float StatMax
    {
        get { return mStatMax; }
        set { mStatMax = value; }
    }
    [SerializeField]
    private List<LevelUpStatusManager.StatType> mStatMaxAssign;
    public List<LevelUpStatusManager.StatType> StatMaxAssign
    {
        get { return mStatMaxAssign; }
        set { mStatMaxAssign = value; }
    }
    [SerializeField]
    private string mStatUnit;
    public string StatUnit
    {
        get { return mStatUnit; }
        set { mStatUnit = value; }
    }
    [SerializeField]
    private int mSelectCount;
    public int SelectCount
    {
        get { return mSelectCount; }
        set { mSelectCount = value; }
    }
    [SerializeField]
    private int mSelectMaxCount;
    public int SelectMaxCount
    {
        get { return mSelectMaxCount; }
        set { mSelectMaxCount = value; }
    }

    public void PlusSelectCount()
    {
        mSelectCount++;
        Debug.Log(mDesc + "À» 1È¸ Âï¾ú½À´Ï´Ù. ÃÑ " + mSelectCount + "È¸ / " + mSelectMaxCount + "È¸");
    }
}
