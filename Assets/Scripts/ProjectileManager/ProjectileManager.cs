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
        // Update is called once per frame
        void Update()
        {

        }
        public void initAllProjectiles()
        {
            // 모든 발사체 오브젝트 초기화
        }
    }
}

