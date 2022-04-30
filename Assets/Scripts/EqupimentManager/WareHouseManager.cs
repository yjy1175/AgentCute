using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WareHouseManager : MonoBehaviour
{
    [SerializeField]
    private StringGameObject mWeaponButtonList;
    [SerializeField]
    private List<GameObject> mWContainer = new List<GameObject>();
    [SerializeField]
    private GameObject mWeaponButtonPrefab;

    [SerializeField]
    private GameObject mWeaponInfoPannel;
    [SerializeField]
    private string mClickedWeaponType;
    // Start is called before the first frame update
    void Start()
    {
        initWeaponButtonList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void initWeaponButtonList()
    {
        // 장비매니저에서 해당 타입의 장비를 받아와 버튼을 만들어 넣어준다.
        for(EquipmentManager.WeaponType type = EquipmentManager.WeaponType.sw; 
            type < EquipmentManager.WeaponType.Exit; type++)
        {
            List<GameObject> newList = EquipmentManager.Instance.FindWepaonList(type.ToString());
            for (int i = 0; i < newList.Count; i++)
            {
                string weaponType = newList[i].GetComponent<Weapon>().Spec.Type;
                Sprite newSprite = newList[i].GetComponent<SpriteRenderer>().sprite;
                string equipName = newList[i].GetComponent<Weapon>().Spec.EquipName;
                int damage = newList[i].GetComponent<Weapon>().Spec.WeaponDamage;
                float addSpeed = newList[i].GetComponent<Weapon>().Spec.WeaponAddSpeed;
                string desc = newList[i].GetComponent<Weapon>().Spec.EquipDesc;
                GameObject newButton = Instantiate(
                    mWeaponButtonPrefab, Vector3.zero, Quaternion.identity, mWContainer[(int)type].transform);
                newButton.GetComponent<RectTransform>().localScale = new Vector3(0.83f, 0.83f, 0.83f);
                newButton.GetComponent<EquipButton>().UnlockImage.SetActive(newList[i].GetComponent<Weapon>().IsLocked);
                newButton.GetComponent<EquipButton>().Image.GetComponent<Image>().sprite = newSprite;

                mWeaponButtonList.Add(newList[i].GetComponent<Weapon>().Spec.Type, newButton);
                // 각 버튼에 리스너 달아주기
                newButton.GetComponent<Button>().onClick.AddListener(() => {ClickWeaponButton(weaponType, newSprite, equipName, damage, addSpeed, desc); } );
            }
        }

        // 로드 시 장착되고 있던 장비 표시
        string currentWeapon = GameObject.Find("LobbyPlayer").GetComponent<LobbyPlayerData>().Info.CurrentWeaponName;
        if(currentWeapon != "")
        {
            mWeaponButtonList[currentWeapon].GetComponent<EquipButton>().EquipCheckImage.SetActive(true);
        }
    }

    private void ClickWeaponButton(string _type, Sprite _image, string _name, int _ATK, float _addSpeed, string _desc)
    {
        mClickedWeaponType = _type;
        mWeaponInfoPannel.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = _name;
        mWeaponInfoPannel.transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite = _image;
        mWeaponInfoPannel.transform.GetChild(3).GetChild(0).GetComponent<Text>().text = "ATK : +" + _ATK.ToString();
        if (_addSpeed > 0)
            mWeaponInfoPannel.transform.GetChild(4).GetChild(0).GetComponent<Text>().text = "SPD : +" + (_addSpeed * 100).ToString() + "%";
        else
            mWeaponInfoPannel.transform.GetChild(4).GetChild(0).GetComponent<Text>().text = "";
        mWeaponInfoPannel.transform.GetChild(7).GetChild(0).GetComponent<Text>().text = _desc;
        mWeaponInfoPannel.SetActive(true);
    }

    public void CloseWeaponInfoPannel()
    {
        mWeaponInfoPannel.SetActive(false);
    }

    public void ClickChangeWeapon()
    {
        string currentWeapon = GameObject.Find("LobbyPlayer").GetComponent<LobbyPlayerData>().Info.CurrentWeaponName;
        if(currentWeapon != "")
            mWeaponButtonList[currentWeapon].GetComponent<EquipButton>().EquipCheckImage.SetActive(false);
        if (EquipmentManager.Instance.ChangeWeaponLobbyPlayer(mClickedWeaponType))
        {
            mWeaponButtonList[mClickedWeaponType].GetComponent<EquipButton>().EquipCheckImage.SetActive(true);
            CloseWeaponInfoPannel();
        }
        else
        {
            // 경고 문구
        }
    }
}
