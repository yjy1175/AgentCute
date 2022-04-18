using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    #region variables

    [SerializeField]
    private SkillSpec spec = new SkillSpec();
    public SkillSpec Spec
    {
        get { return spec; }
        set { spec = value; }
    }
    [SerializeField]
    private int currentUseCount;
    public int CurrentUseCount
    {
        get { return currentUseCount; }
        set { currentUseCount = value; }
    }
    [SerializeField]
    private int currentCoolTimeIndex;
    public int CurrentCoolTimeIndex
    {
        get { return currentCoolTimeIndex; }
        set { currentCoolTimeIndex = value; }
    }
    [SerializeField]
    private float mSkillBuffValue;
    public float SkillBuffValue
    {
        set
        {
            mSkillBuffValue = value;
        }
        get
        {
            return mSkillBuffValue;
        }
    }
    [SerializeField]
    private ESkillBuffType mSkillBuffType;
    public ESkillBuffType SkillBuffType
    {
        set
        {
            mSkillBuffType = value;
        }
        get
        {
            return mSkillBuffType;
        }
    }
    public enum ESkillBuffType
    {
        PlayerSPD,
        PlayerPosition,
        PlayerWeaponSprite
    }

    private float mOriginValue;
    #endregion

    #region method
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuffOn(ESkillBuffType _type, float _value)
    {
       
    }

    public void BuffOff()
    {
        
    }
    #endregion
}
