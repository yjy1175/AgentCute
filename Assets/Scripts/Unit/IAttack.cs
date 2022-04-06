using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAttack : MonoBehaviour
{
    //TO-Do value가 List<Projectile>로 변경 
    protected SkillDic TileDict;
    public GameObject firePosition;

    protected float mAutoAttackSpeed;
    protected float mAutoAttackCheckTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

      
    public void setTileDict(Skill _skill, List<Projectile> _projectiles)
    {
        TileDict.Add(_skill, _projectiles);
    }

    //TO-DO property 문법 set시 value가 Dictionanry일경우 어떻게 넣는지 확인하고 가능하면 set함수 교체
    /*
    public Dictionary<string, GameObject> propTileDict
    {
        set
        {
            Debug.Log("what value"+value);
            //TO-Do C#
        }
        get {
            return TileDict;
        }

    }
    */
}
