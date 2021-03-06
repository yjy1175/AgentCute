using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class SkillManager : SingleToneMaker<SkillManager>
{
    #region variables
    // skill들을 담을 dic
    [SerializeField]
    public StringGameObject skills;

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
            item.Spec.SkillLaunchType = skillsData[i]["SkillLaunchType"].ToString();
            item.Spec.SkillWeaponType = skillsData[i]["SkillWeaponType"].ToString();
            item.Spec.EquipName = skillsData[i]["SkillName"].ToString();
            item.Spec.EquipDesc = skillsData[i]["SkillDesc"].ToString();
            //item.Spec.Rank = int.Parse(skillsData[i]["ProjectileSpawnTime"].ToString());
            item.Spec.SkillCount = int.Parse(skillsData[i]["SkillCount"].ToString());
            item.Spec.SkillCountTime = float.Parse(skillsData[i]["SkillCountTime"].ToString());
            item.Spec.SkillPriority = int.Parse(skillsData[i]["SkillPriority"].ToString());
            item.Spec.SkillClickCount = int.Parse(skillsData[i]["SkillClickCount"].ToString());
            item.Spec.SkillCoolTimeType = skillsData[i]["SkillCoolTimeType"].ToString();
            item.Spec.SkillMotionStartTime = float.Parse(skillsData[i]["SkillMotionStartTime"].ToString());
            item.Spec.SkillMotionRemainTime = float.Parse(skillsData[i]["SkillMotionRemainTime"].ToString());
            item.Spec.SkillStopTime = float.Parse(skillsData[i]["SkillStopTime"].ToString());
            item.Spec.SKillRushSpeed = float.Parse(skillsData[i]["SKillRushSpeed"].ToString());
            string[] tmp = skillsData[i]["SkillRunTime"].ToString().Split('/');
            float[] skillRunTimeList = new float[2];
            for (int k = 0; k < tmp.Length; k++)
                skillRunTimeList[k] = float.Parse(tmp[k]);
            item.Spec.SkillRunTime = skillRunTimeList;
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

            if(skillsData[i]["SkillBuff"].ToString() != "0")
            {
                tmp = skillsData[i]["SkillBuff"].ToString().Split('/');
                item.SkillBuffType = (Skill.ESkillBuffType)Enum.Parse(typeof(Skill.ESkillBuffType), tmp[0]);
                item.SkillBuffValue = float.Parse(tmp[1]);
            }
            else
            {
                item.SkillBuffType = Skill.ESkillBuffType.None;
                item.SkillBuffValue = -1;
            }

            item.Spec.IsLocked = false;

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

    public GameObject FindDodgeSkill(string _type)
    {
        GameObject newSkill = null;
        foreach (string key in skills.Keys)
        {
            if (skills[key].GetComponent<Skill>().Spec.SkillWeaponType == _type)
            {
                if (skills[key].GetComponent<Skill>().Spec.Type == "D")
                    newSkill = skills[key];
            }
        }
        return newSkill;
    }

    public List<GameObject> FindMonsterSkill(string _type)
    {
        List<GameObject> newList = new List<GameObject>();
        foreach (string key in skills.Keys)
        {
            if (skills[key].GetComponent<Skill>().Spec.SkillWeaponType == _type)
            {
                if (skills[key].GetComponent<Skill>().Spec.Type == "M")
                    newList.Add(skills[key]);
            }
        }
        return newList;
    }

    public GameObject FindSkill(string _name)
    {
        return skills[_name];
    }

    #endregion
}
