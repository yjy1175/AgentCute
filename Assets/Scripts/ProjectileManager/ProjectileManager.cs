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
#if DEBUG_TEST
            foreach(string key0 in allProjectiles.Keys)
            {
                for(int i = 0; i < allProjectiles[key0].Count; i++)
                {
                    Instantiate(allProjectiles[key0][i], transform.position, Quaternion.identity, transform.root);
                    Debug.Log("발사체 생성");
                }
            }
#endif
        }
        public void initAllProjectiles()
        {
            // 모든 발사체 오브젝트 초기화
            List<Dictionary<string, object>> projectilesData = PBL.CSVReader.Read("CSVFile\\Projectile");
            int i = 0;
            Spawn item;
            foreach (string key0 in allProjectiles.Keys)
            {
                for (int j = 0; j < allProjectiles[key0].Count; j++)
                {
                    switch (key0)
                    {
                        case "s":
                            item = allProjectiles[key0][j].GetComponent<Spawn>();
                            dataParser(item, ref projectilesData, i + j);
                            break;
                        case "c":
                            //item = allProjectiles[key0][j].GetComponent<Chase>();
                            //dataParser(item, ref projectilesData, i + j);
                            break;
                        case "l":
                            //item = allProjectiles[key0][j].GetComponent<Launch>();
                            //dataParser(item, ref projectilesData, i + j);
                            break;
                        case "r":
                            //item = allProjectiles[key0][j].GetComponent<Radia>();
                            //dataParser(item, ref projectilesData, i + j);
                            break;
                    }
                }
                i += allProjectiles[key0].Count;
            }
        }
        private void dataParser(Spawn item, ref List<Dictionary<string, object>> projectilesData, int index)
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
}

