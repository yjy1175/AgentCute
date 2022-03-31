using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace YJY
{
    public class ProjectileManager : MonoBehaviour
    {
        #region variable
        // key : s, c, l, r(타입 첫글자) value :  <key : 0~(타입 다음글자), value = 발사체 오브젝트>
        public PBL.StringIntGameObject allProjectiles;
        #endregion
        // Start is called before the first frame update
        void Start()
        {
            initAllProjectiles();
        }
        public void initAllProjectiles()
        {
            // 모든 발사체 오브젝트 초기화
            List<Dictionary<string, object>> projectilesData = PBL.CSVReader.Read("CSVFile\\Projectile");
            int i = 0;
            dynamic item;
            foreach (string key0 in allProjectiles.Keys)
            {
                for (int j = 0; j < allProjectiles[key0].Count; j++)
                {
                    switch (key0)
                    {
                        case "s":
                            item = allProjectiles[key0][j].GetComponent<Spawn>();
                            dataPaser(item, ref projectilesData, i + j);
                            break;
                        case "c":
                            item = allProjectiles[key0][j].GetComponent<Chase>();
                            dataPaser(item, ref projectilesData, i + j);
                            break;
                        case "l":
                            item = allProjectiles[key0][j].GetComponent<Launch>();
                            dataPaser(item, ref projectilesData, i + j);
                            break;
                        case "r":
                            item = allProjectiles[key0][j].GetComponent<Radia>();
                            dataPaser(item, ref projectilesData, i + j);
                            break;
                    }
                }
                i += allProjectiles[key0].Count;
            }
        }
        private void dataPaser(dynamic item, ref List<Dictionary<string, object>> projectilesData, int index)
        {
            item.spec.type = projectilesData[index]["ProjectileType"].ToString();
            item.spec.typeName = projectilesData[index]["ProjectileTypeName"].ToString();
            item.spec.equipName = projectilesData[index]["ProjectileName"].ToString();
            item.spec.equipDesc = projectilesData[index]["ProjectileDesc"].ToString();
            item.spec.equipRank = int.Parse(projectilesData[index]["ProjectileRank"].ToString());
            item.spec.Speed = float.Parse(projectilesData[index]["ProjectileSpeed"].ToString());
            item.spec.Count = int.Parse(projectilesData[index]["ProjectileCount"].ToString());
            item.spec.Angle = int.Parse(projectilesData[index]["ProjectileAngle"].ToString());
            item.spec.SpawnTime = int.Parse(projectilesData[index]["ProjectileSpawnTime"].ToString());
            item.spec.MaxPassCount = int.Parse(projectilesData[index]["ProjectileMaxPassCount"].ToString());
        }
    }
}

