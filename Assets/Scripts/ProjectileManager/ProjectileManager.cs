using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    #region variable
    // key : s, c, l, r(타입 첫글자) value :  <key : 0~(타입 다음글자), value = 발사체 오브젝트>
    public StringIntGameObject allProjectiles;

    //private float time = 3;
    //private float tTime;
    #endregion
    private void Start()
    {
        initAllProjectiles();
    }
    private void Update()
    {
#if DEBUG
        //tTime += Time.deltaTime;
        //if(time < tTime)
        //{
        //    tTime = 0;
        //    foreach (string key0 in allProjectiles.Keys)
        //    {
        //        for (int i = 0; i < allProjectiles[key0].Count; i++)
        //        {
        //            Instantiate(allProjectiles[key0][i], transform.position, Quaternion.identity, transform.root);
        //            Debug.Log("발사체 생성");
        //        }
        //    }
        //}
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
}

