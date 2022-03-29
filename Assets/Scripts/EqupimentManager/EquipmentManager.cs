using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace YJY
{
    public class EquipmentManager : MonoBehaviour
    {
        // Start is called before the first frame update
        #region variable
        // key : 장비 분류 , value : 해당 장비 오브젝트(0: 무기, 1: 코스튬...)
        [SerializeField] private Dictionary<int, List<GameObject>> equipments = new Dictionary<int, List<GameObject>>();
        [SerializeField] private Dictionary<int, List<GameObject>> userCurrentEquip = new Dictionary<int, List<GameObject>>();
        // key : 몬스터 분류 , value : 해당 몬스터 장비 오브젝트
        [SerializeField] private Dictionary<int, GameObject> monsterCurrentEquip = new Dictionary<int, GameObject>();
        #endregion
        void Start()
        {

        }
        // Update is called once per frame
        void Update()
        {

        }
        // 무기 객체에 접근하여 무기 객체의 autoAttack()을 호출하는 함수
        public void autoAttack(float _dmg)
        {

        }
        // 무기 객체에 접근하여 무기 객체의 activeGeneralSkill()을 호출하는 함수
        public void generalSkillClicked()
        {

        }
        // 무기 객체에 접근하여 무기 객체의 activeUltimateSkill()을 호출하는 함수
        public void ultimateSkillClicked()
        {

        }
    }
}

