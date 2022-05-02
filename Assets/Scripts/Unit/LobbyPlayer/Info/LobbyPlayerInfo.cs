using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class LobbyPlayerInfo
{
    #region variable
    #region BASE_STAT
    // ±âº» ½ºÅÝ
    [SerializeField]
    private int mBaseHp;
    public int BaseHp
    {
        get { return mBaseHp; }
        set { mBaseHp = value; }
    }

    [SerializeField]
    private int mBaseATK;
    public int BaseATK
    {
        get { return mBaseATK; }
        set { mBaseATK = value; }
    }

    [SerializeField]
    private float mBaseCriticalChance;
    public float BaseCriticalChance
    {
        get { return mBaseCriticalChance; }
        set { mBaseCriticalChance = value; }
    }

    [SerializeField]
    private float mBaseCriticalDamage;
    public float BaseCriticalDamage
    {
        get { return mBaseCriticalDamage; }
        set { mBaseCriticalDamage = value; }
    }

    [SerializeField]
    private float mBaseSPD;
    public float BaseSPD
    {
        get { return mBaseSPD; }
        set { mBaseSPD = value; }
    }

    [SerializeField]
    private float mBaseATKSPD;
    public float BaseATKSPD
    {
        get { return mBaseATKSPD; }
        set { mBaseATKSPD = value; }
    }
    #endregion
    #region TRAINNING_STAT
    [SerializeField]
    private int mTrainingHp;
    public int TrainingHp
    {
        get { return mTrainingHp; }
        set { mTrainingHp = value; }
    }
    [SerializeField]
    private int mTrainingATK;
    public int TrainingATK
    {
        get { return mTrainingATK; }
        set { mTrainingATK = value; }
    }
    [SerializeField]
    private float mTrainingAddDamage;
    public float TrainingAddDamage
    {
        get { return mTrainingAddDamage; }
        set { mTrainingAddDamage = value; }
    }
    #endregion
    #region EQUIP_STAT
    [SerializeField]
    private float mMoveSpeedRate;
    public float MoveSpeedRate
    {
        get { return mMoveSpeedRate; }
        set { mMoveSpeedRate = value; }
    }
    #endregion
    #region EQUIP
    [SerializeField]
    private string mCurrentWeaponName;
    public string CurrentWeaponName
    {
        get { return mCurrentWeaponName; }
        set 
        { 
            mCurrentWeaponName = value;
            SaveLoadManager.Instance.SavePlayerInfoFile(this);
        }
    }
    [SerializeField]
    private string mCurrentCostumeName;
    public string CurrentCostumeName
    {
        get { return mCurrentCostumeName; }
        set 
        { 
            mCurrentCostumeName = value;
            SaveLoadManager.Instance.SavePlayerInfoFile(this);
        }
    }
    [SerializeField]
    private string mCurrentCostumeShapeName;
    public string CurrentCostumeShapeName
    {
        get { return mCurrentCostumeShapeName; }
        set
        {
            mCurrentCostumeShapeName = value;
            SaveLoadManager.Instance.SavePlayerInfoFile(this);
        }
    }
    #endregion
    #region GOODS
    [SerializeField]
    private int mGold;
    public int Gold
    {
        get { return mGold; }
        set 
        { 
            mGold = value;
            SaveLoadManager.Instance.SavePlayerInfoFile(this);
        }
    }
    [SerializeField]
    private int mDiamond;
    public int Diamond
    {
        get { return mDiamond; }
        set
        {
            mDiamond = value;
            SaveLoadManager.Instance.SavePlayerInfoFile(this);
        }
    }
    [SerializeField]
    private int mStemina;
    public int Stemina
    {
        get { return mStemina; }
        set
        { 
            mStemina = value;
            SaveLoadManager.Instance.SavePlayerInfoFile(this);
        }
    }
    #endregion
    #endregion
}
