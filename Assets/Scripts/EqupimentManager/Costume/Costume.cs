using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Costume : MonoBehaviour
{
    // Start is called before the first frame update
    #region varialbe
    [SerializeField]
    private CostumeSpec spec = new CostumeSpec();
    public CostumeSpec Spec
    {
        get { return spec; }
        set { spec = value; }
    }
    [SerializeField]
    private bool isLocked;
    public bool IsLocked
    {
        get { return isLocked; }
        set { isLocked = value; }
    }

    public enum CostumeBuffType
    {
        PlayerSPD,
        PlayerHP,
        Exit
    }

    [SerializeField]
    private CstBuffTypeValue mBuffDic;

    [SerializeField]
    private TypeSprite mSpriteList;
    #endregion
    #region method
    // 스텟버프 딕셔너리 관련 매서드
    public CstBuffTypeValue GetBuffDic()
    {
        return mBuffDic;
    }
    public int GetBuffValue(CostumeBuffType _key)
    {
        return mBuffDic[_key];
    }
    public void SetBuffDic(CostumeBuffType _key, int value)
    {
        if (mBuffDic.ContainsKey(_key))
            mBuffDic.Remove(_key);
        mBuffDic.Add(_key, value);
    }


    // 스프라이트 딕셔너리 관련 매서드
    public TypeSprite GetSpriteDic()
    {
        return mSpriteList;
    }
    public EquipmentManager.CostumeSprite GetSpriteList(EquipmentManager.SpriteType _key)
    {
        return mSpriteList[_key];
    }
    public void AddSpriteList(EquipmentManager.SpriteType _key, EquipmentManager.CostumeSprite _sprite)
    {
        if (mSpriteList.ContainsKey(_key))
            mSpriteList.Remove(_key);
        mSpriteList.Add(_key, _sprite);
    }
    #endregion
}


