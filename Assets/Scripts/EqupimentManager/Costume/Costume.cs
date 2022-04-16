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

    [SerializeField]
    private TypeSprite mSpriteList;
    #endregion
    #region method
    public EquipmentManager.CostumeSprite GetSpriteList(EquipmentManager.SpriteType _key)
    {
        return mSpriteList[_key];
    }
    public void AddSpriteList(EquipmentManager.SpriteType _key, EquipmentManager.CostumeSprite _sprite)
    {
        mSpriteList.Add(_key, _sprite);
    }
    #endregion
}


