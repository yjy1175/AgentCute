using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YJY
{
    public class WeaponSpec : EquipSpec
    {
        #region varialbe
        // 엑셀 속성 타입이름, 장비이름, 장비설명, 장비등급, 데미지....
        protected override string typeName
        {
            get { return typeName; }
            set { typeName = value; }
        }
        protected override string equipName
        {
            get { return equipName; }
            set { equipName = value; }
        }
        protected override string equipDesc
        {
            get { return equipDesc; }
            set { equipDesc = value; }
        }
        protected override int equipRank
        {
            get { return equipRank; }
            set { equipRank = value; }
        }
        private float equipDamage
        {
            get { return equipDamage; }
            set { equipDamage = value; }
        }
        #endregion

        #region method
        public void setData()
        {
            /* 여기서 무기 스펙 데이터 파싱후 초기화 해줍니다.*/
        }
        #endregion
    }
}

