using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkillManager : SingleToneMaker<SkillManager>
{
    #region variables
    // skill들을 담을 dic
    public StringGameObject skills;

    [SerializeField]
    private Image mGeneralSkillImg;
    [SerializeField]
    private Image mUltimateSkillImg;
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
    [SerializeField]
    private Skill currentGeneralSkill;
    public Skill CurrentGeneralSkill
    {
        get { return currentGeneralSkill; }
        set 
        { 
            currentGeneralSkill = value;
            mGeneralSkillImg.sprite = ProjectileManager.
                Instance.allProjectiles[currentGeneralSkill.Spec.getProjectiles()[0]].
                GetComponent<SpriteRenderer>().sprite;
        }
    }
    [SerializeField]
    private Skill currentUltimateSkill;
    public Skill CurrentUltimateSkill
    {
        get { return currentUltimateSkill; }
        set 
        { 
            currentUltimateSkill = value;
            mUltimateSkillImg.sprite = ProjectileManager.
                Instance.allProjectiles[currentUltimateSkill.Spec.getProjectiles()[0]].
                GetComponent<SpriteRenderer>().sprite;
        }
    }
    #endregion

    #region method
    void Start()
    {
        initAllSkills();
    }
    private void initAllSkills()
    {
        // Skill
        // Skill 프리펩을 불러온다
        GameObject[] skillList = Resources.LoadAll<GameObject>("Prefabs\\Skills");
        // Dic에 저장해둔다.
        foreach (GameObject skill in skillList)
        {
            skills.Add(skill.name, skill);
        }
        // Skill데이터를 불러온다
        List<Dictionary<string, object>> skillsData = CSVReader.Read("CSVFile\\Skill");
        Skill item;
        for (int i = 0; i < skillList.Length; i++)
        {
            item = skills[skillsData[i]["SkillNameENG"].ToString()].GetComponent<Skill>();
            item.Spec.Type = skillsData[i]["SkillType"].ToString();
            item.Spec.TypeName = skillsData[i]["SkillTypeName"].ToString();
            item.Spec.SkillWeaponType = skillsData[i]["SkillWeaponType"].ToString();
            item.Spec.Name = skillsData[i]["SkillName"].ToString();
            item.Spec.Desc = skillsData[i]["SkillDesc"].ToString();
            //item.Spec.Rank = int.Parse(skillsData[i]["ProjectileSpawnTime"].ToString());
            item.Spec.SkillCount = int.Parse(skillsData[i]["SkillCount"].ToString());
            item.Spec.SkillClickCount = int.Parse(skillsData[i]["SkillClickCount"].ToString());
            item.Spec.SkillCoolTimeType = skillsData[i]["SkillCoolTimeType"].ToString();
            item.Spec.MSkillRunTime = float.Parse(skillsData[i]["SkillRunTime"].ToString());
            string[] projectiles = skillsData[i]["Projectiles"].ToString().Split('/');
            item.Spec.getProjectiles().Clear();
            for (int j = 0; j < projectiles.Length; j++)
                item.Spec.addProjectiles(projectiles[j]);

            string[] cooltimes = skillsData[i]["SkillCoolTime"].ToString().Split('/');
            item.Spec.getSkillCoolTime().Clear();
            for (int j = 0; j < cooltimes.Length; j++)
                item.Spec.addSkillCoolTime(float.Parse(cooltimes[j]));
            item.CurrentUseCount = 0;
            item.CurrentCoolTimeIndex = 0;
        }
    }

    public List<GameObject> FindGeneralSkill(string _type)
    {
        List<GameObject> newList = new List<GameObject>();
        foreach (string key in skills.Keys)
        {
            if (skills[key].GetComponent<Skill>().Spec.SkillWeaponType == _type)
            {
                if(skills[key].GetComponent<Skill>().Spec.Type == "G")
                    newList.Add(skills[key]);
            }
        }
        return newList;
    }

    public List<GameObject> FindUltimateSkill(string _type)
    {
        List<GameObject> newList = new List<GameObject>();
        foreach (string key in skills.Keys)
        {
            if (skills[key].GetComponent<Skill>().Spec.SkillWeaponType == _type)
            {
                if (skills[key].GetComponent<Skill>().Spec.Type == "U")
                    newList.Add(skills[key]);
            }
        }
        return newList;
    }

    public GameObject FindBaseSkill(string _type)
    {
        GameObject newSkill = null;
        foreach (string key in skills.Keys)
        {
            if (skills[key].GetComponent<Skill>().Spec.SkillWeaponType == _type)
            {
                if (skills[key].GetComponent<Skill>().Spec.Type == "B")
                    newSkill = skills[key];
            }
        }
        return newSkill;
    }
    #endregion
}
