using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAttack : MonoBehaviour
{

    protected GameObject firstProjectile;


    //TO-Do value가 List<Projectile>로 변경 
    protected SkillDic TileDict;
    public GameObject firePosition;

    [SerializeField]
    protected float mAutoAttackSpeed;
    protected float mAutoAttackCheckTime;

    [SerializeField]
    private Skill currentBaseSkill;
    public Skill CurrentBaseSkill
    {
        get { return currentBaseSkill; }
        set
        {
            currentBaseSkill = value;
        }
    }


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

    /*
    * _angle : 추가 각도 설정입니다.
    * _name : 해당 발사체오브젝트의 name입니다.
    */
    protected IEnumerator LaunchCorutines(float _angle, string _name, Vector3 _targetPos, Vector3 _firePos, bool _notSingle)
    {
        GameObject obj = ObjectPoolManager.Instance.EnableGameObject(_name);
        if (_notSingle) firstProjectile = obj;
        float keepTime = obj.GetComponent<Projectile>().Spec.SpawnTime;
        setProjectileData(ref obj);
        obj.GetComponent<Projectile>().CurrentPassCount = 0;
        obj.GetComponent<Projectile>().setEnable(_targetPos, _firePos, _angle);

        yield return new WaitForSeconds(keepTime);
        // 간헐적 disable문제때문에 해당 오브젝트의 Active가 true일 시만 disable되게 했습니다
        // 스폰시간이 다되기 전 발사체가 몬스터와 부딪힐 때 disable이 한번 호출되는데 혹시나 다른 객체로
        // 접근해서 disable해서 간헐적 문제가 발생할 수 있을거라 추측해서 조건문 걸었습니다.
        // 추후에 다른곳에서의 문제가 발견되면 삭제할 예정
        if (obj.activeInHierarchy)
        {
            obj.GetComponent<Projectile>().setDisable();
            ObjectPoolManager.Instance.DisableGameObject(obj);
        }
    }


    // 발사체 데미지를 설정합니다.
    private void setProjectileData(ref GameObject obj)
    {
        obj.GetComponent<Projectile>().Damage = 
            (int)((gameObject.GetComponent<IStatus>().BaseDamage + gameObject.GetComponent<IStatus>().getCurrentWeponeDamage()) * obj.GetComponent<Projectile>().Spec.ProjectileDamage);
    }



}
