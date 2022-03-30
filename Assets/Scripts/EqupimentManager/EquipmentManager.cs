using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YJY
{
    public class EquipmentManager : MonoBehaviour
    {
        // Start is called before the first frame update
        #region variable
        // key : 장비 분류(0: 무기, 1: 코스튬...) , value : <key : typename, value : 해당장비 오브젝트 
        public PBL.IntStringGameObject equipments;
        public PBL.IntGameObject userCurrentEquip;
        // key : 몬스터 분류 , value : 해당 몬스터 장비 오브젝트
        public PBL.IntGameObject monsterCurrentEquip;
        #endregion
        void Start()
        {
            initAllEquips();
        }
        #region method
        // 모든 장비류 오브젝트 데이터 파싱 후 값 저장하는 함수
        public void initAllEquips()
        {

        }
        // 장착중인 무기 객체에 접근하여 장착중인 일반 스킬 사용하는 함수
        public void generalSkillClicked()
        {

        }
        // 장착중인 무기 객체에 접근하여 장착중인 궁극기 스킬 사용하는 함수
        public void ultimateSkillClicked()
        {

        }
        // 착용중인 장비를 교체해주는 함수
        // name : 바꿀 장비의 typeName
        // type : 0 무기, 1 코스튬
        public void changeEquip(string name, int type)
        {

        }
        #endregion
    }
}