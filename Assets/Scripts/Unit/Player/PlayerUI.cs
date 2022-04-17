using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerUI : IUI
{
    [SerializeField]
    private GameObject mLevelBar;
    [SerializeField]
    private GameObject mHpBar;
    [SerializeField]
    private GameObject mExpBar;
    [SerializeField]
    private Text mGoldText;

    void Awake()
    {
        gameObject.GetComponent<PlayerEventHandler>().registerLevelObserver(RegisterLevelObserver);
        gameObject.GetComponent<PlayerEventHandler>().registerHpObserver(RegisterHpObserver);
        gameObject.GetComponent<PlayerEventHandler>().registerExpObserver(RegisterExpObserver);
        gameObject.GetComponent<PlayerEventHandler>().registerGoldObserver(RegisterGoldObserver);
        mLevelBar = GameObject.Find("LevelBar");
        mHpBar = GameObject.Find("HpBar");
        mExpBar = GameObject.Find("ExpBar");
        mGoldText = GameObject.Find("Gold").transform.GetChild(2).GetComponent<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void RegisterGoldObserver(int _gold)
    {
        mGoldText.text = _gold + " gold";
    }
    private void RegisterLevelObserver(int _level)
    {

        mLevelBar.transform.Find("LevelText").GetComponent<Text>().text = "Lv." + _level;
        // 레벨업 시 스텟선택창 팝업하는 API 호출
        // TO-DO : UIManager에 이벤트 등록 하기
        if(_level != 1)
            UIManager.Instance.StatusSelectPannelOn();
    }

    private void RegisterExpObserver(int _exp)
    {
        int playerExp = gameObject.GetComponent<PlayerStatus>().PlayerExp;
        int playerExpMax = gameObject.GetComponent<PlayerStatus>().PlayerMaxExp;
        mExpBar.transform.Find("Exp").GetComponent<Image>().fillAmount = ((float)playerExp / (float)playerExpMax);
        mExpBar.transform.Find("ExpText").GetComponent<Text>().text = _exp.ToString()+"/"+gameObject.GetComponent<PlayerStatus>().PlayerMaxExp;
    }

    private void RegisterHpObserver(int _hp, GameObject _obj)
    {
        int hp = gameObject.GetComponent<PlayerStatus>().Hp;
        int maxHp = gameObject.GetComponent<PlayerStatus>().MaxHP;
        mHpBar.transform.Find("Hp").GetComponent<Image>().fillAmount = ((float)hp / (float)maxHp);
        mHpBar.transform.Find("HpText").GetComponent<Text>().text = _hp.ToString();

        // 플레이어 사망시 게임오버 UI 팝업 API호출
        // TO-DO : UIManager에 이벤트 등록 하기
        if (_hp <= 0)
            UIManager.Instance.GameOverPannelOn();
    }

}
