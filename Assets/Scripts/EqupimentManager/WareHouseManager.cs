using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WareHouseManager : MonoBehaviour
{
    [SerializeField]
    private GameObject mEquipButtonPrefab;

    [Header("무기")]
    [SerializeField]
    private GameObject mWeaponPannel;
    [SerializeField]
    private GameObject mWeaponButtonImage;
    [SerializeField]
    private StringGameObject mWeaponButtonList;
    [SerializeField]
    private List<GameObject> mWContainer = new List<GameObject>();
    [SerializeField]
    private GameObject mWeaponInfoPannel;
    [SerializeField]
    private string mClickedWeaponType;
    [SerializeField]
    private GameObject mWeaponChangePannel;

    [Header("코스튬")]
    [SerializeField]
    private GameObject mCostumePannel;
    [SerializeField]
    private GameObject mCostumeButtonImage;
    [SerializeField]
    private StringGameObject mCostumeButtonList;
    [SerializeField]
    private List<GameObject> mCContainer = new List<GameObject>();
    [SerializeField]
    private GameObject mCostumeInfoPannel;
    [SerializeField]
    private string mClickedCostumeType;
    [SerializeField]
    private GameObject mCostumeChangePannel;
    [SerializeField]
    private GameObject mCostumeShapeChangePannel;
    // Start is called before the first frame update
    void Start()
    {
        initWeaponButtonList();
        initCostumeButtonList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenWeaponPannel()
    {
        mWeaponPannel.SetActive(true);
        mWeaponButtonImage.SetActive(true);
        mCostumePannel.SetActive(false);
        mCostumeButtonImage.SetActive(false);


    }
    public void OpenCostumePannel()
    {
        mWeaponPannel.SetActive(false);
        mWeaponButtonImage.SetActive(false);
        mCostumePannel.SetActive(true);
        mCostumeButtonImage.SetActive(true);
    }

    // 무기 데이터 로드
    private void initWeaponButtonList()
    {
        mWeaponButtonList.Clear();
        // 장비매니저에서 해당 타입의 장비를 받아와 버튼을 만들어 넣어준다.
        for (EquipmentManager.WeaponType type = EquipmentManager.WeaponType.sw; 
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
                    mEquipButtonPrefab, Vector3.zero, Quaternion.identity, mWContainer[(int)type].transform);
                newButton.GetComponent<RectTransform>().localScale = new Vector3(0.83f, 0.83f, 0.83f);
                newButton.GetComponent<EquipButton>().UnlockImage.SetActive(newList[i].GetComponent<Weapon>().IsLocked);
                newButton.GetComponent<EquipButton>().Image.GetComponent<Image>().sprite = newSprite;

                mWeaponButtonList.Add(weaponType, newButton);
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
    // 무기 장착
    public void ClickChangeWeapon()
    {
        CloseWeaponInfoPannel();
        string costumeType = GameObject.Find("LobbyPlayer").GetComponent<LobbyPlayerData>().Info.CurrentCostumeName;
        if (EquipmentManager.Instance.CheckUnlockWeapon(mClickedWeaponType))
        {
            // 현재 장착한 코스튬 정보 확인
            if(costumeType == "" || costumeType.Substring(3, costumeType.Length - 5).Contains(mClickedWeaponType.Substring(0, 2)))
            {
                // 장착 판넬 띄우기
                string currentWeapon = GameObject.Find("LobbyPlayer").GetComponent<LobbyPlayerData>().Info.CurrentWeaponName;
                if (currentWeapon != "")
                {
                    GameObject curWeapon = EquipmentManager.Instance.FindWeapon(currentWeapon);
                    mWeaponChangePannel.transform.GetChild(3).GetChild(1).GetChild(0).GetComponent<Text>().text
                        = curWeapon.GetComponent<Weapon>().Spec.EquipName;
                    mWeaponChangePannel.transform.GetChild(3).GetChild(2).GetChild(0).GetComponent<Image>().sprite
                        = curWeapon.GetComponent<SpriteRenderer>().sprite;
                    mWeaponChangePannel.transform.GetChild(3).GetChild(3).GetChild(0).GetComponent<Text>().text
                        = "ATK : +" + curWeapon.GetComponent<Weapon>().Spec.WeaponDamage;
                    if (curWeapon.GetComponent<Weapon>().Spec.WeaponAddSpeed > 0)
                        mWeaponChangePannel.transform.GetChild(3).GetChild(4).GetChild(0).GetComponent<Text>().text
                            = "SPD : +" + (curWeapon.GetComponent<Weapon>().Spec.WeaponAddSpeed * 100).ToString() + "%";
                    else
                        mWeaponChangePannel.transform.GetChild(3).GetChild(4).GetChild(0).GetComponent<Text>().text = "";
                }

                GameObject newWeapon = EquipmentManager.Instance.FindWeapon(mClickedWeaponType);

                mWeaponChangePannel.transform.GetChild(4).GetChild(1).GetChild(0).GetComponent<Text>().text
                    = newWeapon.GetComponent<Weapon>().Spec.EquipName;
                mWeaponChangePannel.transform.GetChild(4).GetChild(2).GetChild(0).GetComponent<Image>().sprite
                    = newWeapon.GetComponent<SpriteRenderer>().sprite;
                mWeaponChangePannel.transform.GetChild(4).GetChild(3).GetChild(0).GetComponent<Text>().text
                    = "ATK : +" + newWeapon.GetComponent<Weapon>().Spec.WeaponDamage;
                if (newWeapon.GetComponent<Weapon>().Spec.WeaponAddSpeed > 0)
                    mWeaponChangePannel.transform.GetChild(4).GetChild(4).GetChild(0).GetComponent<Text>().text
                        = "SPD : +" + (newWeapon.GetComponent<Weapon>().Spec.WeaponAddSpeed * 100).ToString() + "%";
                else
                    mWeaponChangePannel.transform.GetChild(4).GetChild(4).GetChild(0).GetComponent<Text>().text = "";

                mWeaponChangePannel.SetActive(true);
            }
            else
            {
                // 경고 문구
                LobbyUIManager.Instance.OpenAlertEnterPannel("현재 장착한 코스튬으로는 착용 불가능한 무기입니다.");
            }
        }
        else
        {
            // 경고 문구
            LobbyUIManager.Instance.OpenAlertEnterPannel("해당 무기는 해금되어 있지 않습니다.");
        }
    }
    public void CloseWeaponChangePannel()
    {
        mWeaponChangePannel.SetActive(false);
    }
    public void ChangeWeapon()
    {
        string currentWeapon = GameObject.Find("LobbyPlayer").GetComponent<LobbyPlayerData>().Info.CurrentWeaponName;
        if (currentWeapon != "")
            mWeaponButtonList[currentWeapon].GetComponent<EquipButton>().EquipCheckImage.SetActive(false);
        EquipmentManager.Instance.ChangeWeaponLobbyPlayer(mClickedWeaponType);
        mWeaponButtonList[mClickedWeaponType].GetComponent<EquipButton>().EquipCheckImage.SetActive(true);
        CloseWeaponChangePannel();
    }
    // 무기 해금 정보 변경 시
    public void ChangeWeaponUnlock(string _name, bool _locked)
    {
        GameObject btn = mWeaponButtonList[_name];
        btn.GetComponent<EquipButton>().UnlockImage.SetActive(_locked);
    }

    // 코스튬 데이터 로드
    private void initCostumeButtonList()
    {
        mCostumeButtonList.Clear();
        for (EquipmentManager.CostumeTpye type = EquipmentManager.CostumeTpye.swsp;
            type < EquipmentManager.CostumeTpye.Exit; type++)
        {
            List<GameObject> newList = EquipmentManager.Instance.FindCostumeList(type.ToString());
            for (int i = 0; i < newList.Count; i++)
            {
                string costumeName = newList[i].name;
                EquipmentManager.CostumeTpye thisType = type;
                Sprite newSprite = newList[i].GetComponent<SpriteRenderer>().sprite;
                string equipName = newList[i].GetComponent<Costume>().Spec.EquipName;
                string desc = newList[i].GetComponent<Costume>().Spec.EquipDesc;
                string rank = newList[i].GetComponent<Costume>().Spec.EquipRankDesc;
                string source = newList[i].GetComponent<Costume>().Spec.EquipSource;
                int addHp = newList[i].GetComponent<Costume>().GetBuffValue(Costume.CostumeBuffType.PlayerHP);
                int addSPD = newList[i].GetComponent<Costume>().GetBuffValue(Costume.CostumeBuffType.PlayerSPD);
                GameObject newButton = Instantiate(
                mEquipButtonPrefab, Vector3.zero, Quaternion.identity, mCContainer[(int)type].transform);
                newButton.GetComponent<EquipButton>().UnlockImage.SetActive(newList[i].GetComponent<Costume>().IsLocked);
                newButton.GetComponent<EquipButton>().Image.GetComponent<Image>().sprite = newSprite;
                switch (newList[i].GetComponent<Costume>().Spec.Rank)
                {
                    case 1:
                        newButton.GetComponent<EquipButton>().CommonRank.SetActive(true);
                        break;
                    case 2:
                        newButton.GetComponent<EquipButton>().AdvancedRank.SetActive(true);
                        break;
                    case 3:
                        newButton.GetComponent<EquipButton>().SuperiorRank.SetActive(true);
                        break;
                }

                mCostumeButtonList.Add(costumeName, newButton);
                newButton.GetComponent<Button>().onClick.AddListener(() => {
                    ClickCostumeButton(costumeName, thisType, newSprite, equipName, addHp, addSPD, desc, rank, source); });
            }
        }

        // 로드 시 실장착되고 있던 장비 표시
        string currentCostume = GameObject.Find("LobbyPlayer").GetComponent<LobbyPlayerData>().Info.CurrentCostumeName;
        if (currentCostume != "")
        {
            mCostumeButtonList[currentCostume].GetComponent<EquipButton>().EquipCheckImage.SetActive(true);
        }
        // 로드 시 외형 장착 장비 표시
        currentCostume = GameObject.Find("LobbyPlayer").GetComponent<LobbyPlayerData>().Info.CurrentCostumeShapeName;
        if(currentCostume != "")
        {
            mCostumeButtonList[currentCostume].GetComponent<EquipButton>().ShapeCheckImage.SetActive(true);
        }
    }
    private void ClickCostumeButton(string _typeName, EquipmentManager.CostumeTpye _type, Sprite _image, string _name, int _hp, int _addSpeed, string _desc, string _rank, string _source)
    {
        mClickedCostumeType = _typeName;
        mCostumeInfoPannel.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = _name;
        mCostumeInfoPannel.transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite = _image;

        if (_hp > 0)
            mCostumeInfoPannel.transform.GetChild(3).GetChild(0).GetComponent<Text>().text = "HP : +" + _hp.ToString();
        else
            mCostumeInfoPannel.transform.GetChild(3).GetChild(0).GetComponent<Text>().text = "";

        if(_addSpeed > 0)
            mCostumeInfoPannel.transform.GetChild(4).GetChild(0).GetComponent<Text>().text = "SPD : +" + _addSpeed.ToString() + "%";
        else
            mCostumeInfoPannel.transform.GetChild(4).GetChild(0).GetComponent<Text>().text = "";

        mCostumeInfoPannel.transform.GetChild(5).GetChild(1).GetComponent<Text>().text = _source;
        mCostumeInfoPannel.transform.GetChild(7).GetChild(0).GetComponent<Text>().text = _desc;

        // TODO : 랭크 출력해주기
        mCostumeInfoPannel.transform.GetChild(8).GetChild(0).GetComponent<Text>().text = _rank;

        switch (_type)
        {
            case EquipmentManager.CostumeTpye.swsp:
                mCostumeInfoPannel.transform.GetChild(8).GetChild(1).GetComponent<Text>().text = "소드 또는 스피어 무기에 장착 가능합니다.";
                break;
            case EquipmentManager.CostumeTpye.bgst:
                mCostumeInfoPannel.transform.GetChild(8).GetChild(1).GetComponent<Text>().text = "스태프 또는 석궁 무기에 장착 가능합니다.";
                break;
            case EquipmentManager.CostumeTpye.bgstswsp:
                mCostumeInfoPannel.transform.GetChild(8).GetChild(1).GetComponent<Text>().text = "모든 무기에 장착 가능합니다.";
                break;
        }
        
        mCostumeInfoPannel.SetActive(true);
    }
    public void CloseCostumeInfoPannel()
    {
        mCostumeInfoPannel.SetActive(false);
    }

    // 코스튬 장착
    public void ClickChangeCostume()
    {
        CloseCostumeInfoPannel();
        string weaponType = GameObject.Find("LobbyPlayer").GetComponent<LobbyPlayerData>().Info.CurrentWeaponName;
        if(weaponType == "")
        {
            LobbyUIManager.Instance.OpenAlertEnterPannel("무기가 장착되어있지 않습니다.");
            return;
        }
        weaponType = weaponType.Substring(0, 2);
        if (EquipmentManager.Instance.CheckUnlockCostume(mClickedCostumeType))
        {
            // 해당 무기와 맞는 코스튬
            if (mClickedCostumeType.Substring(3).Contains(weaponType))
            {
                string currentCostume = GameObject.Find("LobbyPlayer").GetComponent<LobbyPlayerData>().Info.CurrentCostumeName;
                if (currentCostume != "")
                {
                    GameObject curCostume = EquipmentManager.Instance.FindCostume(currentCostume);
                    mCostumeChangePannel.transform.GetChild(3).GetChild(1).GetChild(0).GetComponent<Text>().text
                        = curCostume.GetComponent<Costume>().Spec.EquipName;
                    mCostumeChangePannel.transform.GetChild(3).GetChild(2).GetChild(0).GetComponent<Image>().sprite
                        = curCostume.GetComponent<SpriteRenderer>().sprite;
                    if (curCostume.GetComponent<Costume>().GetBuffValue(Costume.CostumeBuffType.PlayerHP) > 0)
                        mCostumeChangePannel.transform.GetChild(3).GetChild(3).GetChild(0).GetComponent<Text>().text
                            = "HP : +" + curCostume.GetComponent<Costume>().GetBuffValue(Costume.CostumeBuffType.PlayerHP);
                    else
                        mCostumeChangePannel.transform.GetChild(3).GetChild(3).GetChild(0).GetComponent<Text>().text = "";
                    if (curCostume.GetComponent<Costume>().GetBuffValue(Costume.CostumeBuffType.PlayerSPD) > 0)
                        mCostumeChangePannel.transform.GetChild(3).GetChild(4).GetChild(0).GetComponent<Text>().text
                            = "SPD : +" + curCostume.GetComponent<Costume>().GetBuffValue(Costume.CostumeBuffType.PlayerSPD) + "%";
                    else
                        mCostumeChangePannel.transform.GetChild(3).GetChild(4).GetChild(0).GetComponent<Text>().text = "";
                }

                GameObject newCostume = EquipmentManager.Instance.FindCostume(mClickedCostumeType);

                mCostumeChangePannel.transform.GetChild(4).GetChild(1).GetChild(0).GetComponent<Text>().text
                         = newCostume.GetComponent<Costume>().Spec.EquipName;
                mCostumeChangePannel.transform.GetChild(4).GetChild(2).GetChild(0).GetComponent<Image>().sprite
                    = newCostume.GetComponent<SpriteRenderer>().sprite;
                if (newCostume.GetComponent<Costume>().GetBuffValue(Costume.CostumeBuffType.PlayerHP) > 0)
                    mCostumeChangePannel.transform.GetChild(4).GetChild(3).GetChild(0).GetComponent<Text>().text
                        = "HP : +" + newCostume.GetComponent<Costume>().GetBuffValue(Costume.CostumeBuffType.PlayerHP);
                else
                    mCostumeChangePannel.transform.GetChild(4).GetChild(3).GetChild(0).GetComponent<Text>().text = "";
                if (newCostume.GetComponent<Costume>().GetBuffValue(Costume.CostumeBuffType.PlayerSPD) > 0)
                    mCostumeChangePannel.transform.GetChild(4).GetChild(4).GetChild(0).GetComponent<Text>().text
                        = "SPD : +" + newCostume.GetComponent<Costume>().GetBuffValue(Costume.CostumeBuffType.PlayerSPD) + "%";
                else
                    mCostumeChangePannel.transform.GetChild(4).GetChild(4).GetChild(0).GetComponent<Text>().text = "";

                mCostumeChangePannel.SetActive(true);
            }
            // 무기와 맞지 않는 코스튬
            else
            {
                LobbyUIManager.Instance.OpenAlertEnterPannel("현재 장착된 무기로는 착용 불가능한 코스튬입니다.");
            }
        }
        // 해금되어있지 않음
        else
        {
            LobbyUIManager.Instance.OpenAlertEnterPannel("해당 코스튬은 해금되어 있지 않습니다.");
        }
    }
    public void CloseCostumeChangePannel()
    {
        mCostumeChangePannel.SetActive(false);
    }
    public void ChangeCostume()
    {
        // 새로운 코스튬 장착 시 외형 코스튬도 같이 변경해준다.
        string currentCostume = GameObject.Find("LobbyPlayer").GetComponent<LobbyPlayerData>().Info.CurrentCostumeName;
        string currentShape = GameObject.Find("LobbyPlayer").GetComponent<LobbyPlayerData>().Info.CurrentCostumeShapeName;
        if (currentCostume != "")
            mCostumeButtonList[currentCostume].GetComponent<EquipButton>().EquipCheckImage.SetActive(false);
        EquipmentManager.Instance.ChangeCostumeLobbyPlayer(mClickedCostumeType);
        mCostumeButtonList[mClickedCostumeType].GetComponent<EquipButton>().EquipCheckImage.SetActive(true);

        if(currentShape != "")
            mCostumeButtonList[currentShape].GetComponent<EquipButton>().ShapeCheckImage.SetActive(false);
        EquipmentManager.Instance.ChangeCostumeShapeLobbyPlayer(mClickedCostumeType);
        mCostumeButtonList[mClickedCostumeType].GetComponent<EquipButton>().ShapeCheckImage.SetActive(true);

        CloseCostumeChangePannel();
    }
    // 코스튬 외형 변경
    public void CloseCostumeShpaeChangePannel()
    {
        mCostumeShapeChangePannel.SetActive(false);
    }
    public void ClickChangeShapeCostume()
    {
        CloseCostumeInfoPannel();
        if (EquipmentManager.Instance.CheckUnlockCostume(mClickedCostumeType))
        {
            string currentCostume = GameObject.Find("LobbyPlayer").GetComponent<LobbyPlayerData>().Info.CurrentCostumeName;
            if (currentCostume != "")
            {
                GameObject curCostume = EquipmentManager.Instance.FindCostume(currentCostume);
                mCostumeShapeChangePannel.transform.GetChild(3).GetChild(1).GetChild(0).GetComponent<Text>().text
                    = curCostume.GetComponent<Costume>().Spec.EquipName;
                mCostumeShapeChangePannel.transform.GetChild(3).GetChild(2).GetChild(0).GetComponent<Image>().sprite
                    = curCostume.GetComponent<SpriteRenderer>().sprite;
            }

            GameObject newCostume = EquipmentManager.Instance.FindCostume(mClickedCostumeType);

            mCostumeShapeChangePannel.transform.GetChild(4).GetChild(1).GetChild(0).GetComponent<Text>().text
                     = newCostume.GetComponent<Costume>().Spec.EquipName;
            mCostumeShapeChangePannel.transform.GetChild(4).GetChild(2).GetChild(0).GetComponent<Image>().sprite
                = newCostume.GetComponent<SpriteRenderer>().sprite;

            mCostumeShapeChangePannel.SetActive(true);
        }
        // 해금 되어있지 않음
        else
        {
            LobbyUIManager.Instance.OpenAlertEnterPannel("해당 코스튬은 해금되어 있지 않습니다.");
        }
    }
    public void ChangeShapeCostume()
    {
        // 새로운 코스튬 장착 시 외형 코스튬도 같이 변경해준다.
        string currentShape = GameObject.Find("LobbyPlayer").GetComponent<LobbyPlayerData>().Info.CurrentCostumeShapeName;

        if (currentShape != "")
            mCostumeButtonList[currentShape].GetComponent<EquipButton>().ShapeCheckImage.SetActive(false);
        EquipmentManager.Instance.ChangeCostumeShapeLobbyPlayer(mClickedCostumeType);
        mCostumeButtonList[mClickedCostumeType].GetComponent<EquipButton>().ShapeCheckImage.SetActive(true);

        CloseCostumeShpaeChangePannel();
    }
    // 코스튬 해금 정보 변경 시
    public void ChangeCostumeUnlock(string _name, bool _locked)
    {
        GameObject btn = mCostumeButtonList[_name];
        btn.GetComponent<EquipButton>().UnlockImage.SetActive(_locked);
    }
}
