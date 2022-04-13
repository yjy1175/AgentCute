using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : SingleToneMaker<ProjectileManager>
{
    #region variable
    // key : s, c, l, r(타입 첫글자) value :  <key : 0~(타입 다음글자), value = 발사체 오브젝트>
    public StringGameObject allProjectiles;

    #endregion
    private void Start()
    {
        initAllProjectiles();
    }
    private void Update()
    {

    }
    public void initAllProjectiles()
    {
        // Projectiles 프리펩을 불러온다
        GameObject[] projectilesList = Resources.LoadAll<GameObject>("Prefabs\\Projectiles");
        // Dic에 저장해둔다.
        foreach (GameObject projectile in projectilesList)
        {
            allProjectiles.Add(projectile.name, projectile);
        }
        // 모든 발사체 오브젝트 초기화
        List<Dictionary<string, object>> projectilesData = CSVReader.Read("CSVFile\\Projectile");
        Projectile item;
        for(int i = 0; i < projectilesList.Length; i++)
        {
            item = allProjectiles[projectilesData[i]["ProjectileType"].ToString()].GetComponent<Projectile>();
            item.Spec.Type = projectilesData[i]["ProjectileType"].ToString();
            item.Spec.Speed = float.Parse(projectilesData[i]["ProjectileSpeed"].ToString());
            item.Spec.ProjectileDamage = float.Parse(projectilesData[i]["ProjectileDamage"].ToString());
            item.Spec.Count = int.Parse(projectilesData[i]["ProjectileCount"].ToString());
            item.Spec.Angle = int.Parse(projectilesData[i]["ProjectileAngle"].ToString());
            item.Spec.SpawnTime = float.Parse(projectilesData[i]["ProjectileSpawnTime"].ToString());
            item.Spec.MaxPassCount = int.Parse(projectilesData[i]["ProjectileMaxPassCount"].ToString());

            // 스폰형인 경우 스폰 타입 지정
            if(item.Spec.Type[0] == 's')
            {
                item.GetComponent<Spawn>().MSpawnType = (Spawn.SpawnType)int.Parse(projectilesData[i]["ProjectileSpawnType"].ToString());
            }
        }
    }
    /*
     * 레벨업에 따른 발사체 추가시 필요한 api입니다.
     * 전체적으로 모든 발사체를 추가시킬때 필요
     * _num : 추가시킬 발사체 개수입니다. 
     */
    public void AddProjectilesCount(int _num)
    {
        if (_num < 0)
            if (Projectile.AddProjectilesCount <= 0)
                _num = 0;
        Projectile.AddProjectilesCount += _num;
    }

    /*
 * 레벨업에 따른 발사체 범위 증가시 필요한 api입니다.
 * 전체적으로 모든 발사체의 범위를 증가 시킬때 필요
 * _coefficient : 범위 증가 수치 입니다.
 */
    public void AddProjectilesScale(float _coefficient)
    {
        if (_coefficient < 0)
            if (Projectile.AddScaleCoefficient <= 1)
                _coefficient = 0;
        Projectile.AddScaleCoefficient += _coefficient;
    }
    /*
    * 레벨업에 따른 발사체 관통 마리수 추가 api입니다
    * 전체적으로 모든 발사체의 관통 마리수를 추가 시켜줍니다.
    * _passCount : 관통 마리수 추가 수치입니다.
    */
    public void AddPassCount(int _passCount)
    {
        if (_passCount < 0)
            if (Projectile.AddPassCount <= 0)
                _passCount = 0;
        Projectile.AddPassCount += _passCount;
    }
}

