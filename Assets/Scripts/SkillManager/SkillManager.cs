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
    private Skill currentBaseSkill;
    public Skill CurrentBaseSkill
    {
        get { return currentBaseSkill; }
    }
    [SerializeField]
    private Skill currentGeneralSkill;
    public Skill CurrentGeneralSkill
    {
        get { return currentGeneralSkill; }
    }
    [SerializeField]
    private Skill currentUltimateSkill;
    public Skill CurrentUltimateSkill
    {
        get { return currentUltimateSkill; }
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
            item = skills[skillsData[i]["SkillNameE"].ToString()].GetComponent<Skill>();
            item.Spec.Type = skillsData[i]["SkillType"].ToString();
            item.Spec.TypeName = skillsData[i]["SkillTypeName"].ToString();
            item.Spec.SkillWeaponType = skillsData[i]["SkillWeaponType"].ToString();
            item.Spec.Name = skillsData[i]["SkillNameE"].ToString();
            item.Spec.Desc = skillsData[i]["SkillDesc"].ToString();
            //item.Spec.Rank = int.Parse(skillsData[i]["ProjectileSpawnTime"].ToString());
            item.Spec.SkillCount = int.Parse(skillsData[i]["SkillCount"].ToString());
            item.Spec.SkillClickCount = int.Parse(skillsData[i]["SkillClickCount"].ToString());
            item.Spec.SkillCoolTimeType = skillsData[i]["SkillCoolTimeType"].ToString();

            string[] projectiles = skillsData[i]["Projectiles"].ToString().Split('/');
            item.Spec.getProjectiles().Clear();
            for (int j = 0; j < projectiles.Length; j++)
                item.Spec.addProjectiles(projectiles[j]);

            string[] cooltimes = skillsData[i]["SkillCoolTime"].ToString().Split('/');
            item.Spec.getSkillCoolTime().Clear();
            for (int j = 0; j < cooltimes.Length; j++)
                item.Spec.addSkillCoolTime(int.Parse(cooltimes[j]));
            item.CurrentUseCount = 0;
            item.CurrentCoolTimeIndex = 0;
        }
    }
    #endregion
}
