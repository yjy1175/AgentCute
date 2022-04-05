using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    #region variable
    // key : s, c, l, r(타입 첫글자) value :  <key : 0~(타입 다음글자), value = 발사체 오브젝트>
    public StringIntGameObject allProjectiles;

    #endregion
    private void Start()
    {
        initAllProjectiles();
    }
    private void Update()
    {
#if DEBUG
#endif
    }
    public void initAllProjectiles()
    {
        // 모든 발사체 오브젝트 초기화
        List<Dictionary<string, object>> projectilesData = CSVReader.Read("CSVFile\\Projectile");
        int i = 0;
        Projectile item;
        foreach (string key0 in allProjectiles.Keys)
        {
            for (int j = 0; j < allProjectiles[key0].Count; j++)
            {
                item = allProjectiles[key0][j].GetComponent<Projectile>();
                dataParser(item, ref projectilesData, i + j);
            }
            i += allProjectiles[key0].Count;
        }
    }
    private void dataParser(Projectile item, ref List<Dictionary<string, object>> projectilesData, int index)
    {
        item.Spec.Type = projectilesData[index]["ProjectileType"].ToString();
        item.Spec.TypeName = projectilesData[index]["ProjectileTypeName"].ToString();
        item.Spec.EquipName = projectilesData[index]["ProjectileName"].ToString();
        item.Spec.EquipDesc = projectilesData[index]["ProjectileDesc"].ToString();
        // item.Spec.EquipRank = int.Parse(projectilesData[index]["ProjectileRank"].ToString());
        item.Spec.Speed = float.Parse(projectilesData[index]["ProjectileSpeed"].ToString());
        item.Spec.Count = int.Parse(projectilesData[index]["ProjectileCount"].ToString());
        item.Spec.Angle = int.Parse(projectilesData[index]["ProjectileAngle"].ToString());
        item.Spec.SpawnTime = float.Parse(projectilesData[index]["ProjectileSpawnTime"].ToString());
        item.Spec.MaxPassCount = int.Parse(projectilesData[index]["ProjectileMaxPassCount"].ToString());
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


    // 임시 테스트용 입니다. 추후 삭제
    public void createUserDamageBox()
    {
        int damage = UnityEngine.Random.Range(10, 100);
        MessageBoxManager.Instance.createMessageBox(
            MessageBoxManager.BoxType.UserDamage, damage.ToString(), new Vector3(0, 0, 0));
    }
    // 임시 테스트용 입니다. 추후 삭제
    public void createMonsterDamageBox()
    {
        int damage = UnityEngine.Random.Range(10, 100);
        MessageBoxManager.Instance.createMessageBox(
            MessageBoxManager.BoxType.MonsterDamage, damage.ToString(), new Vector3(0, 0, 0));
    }
}

