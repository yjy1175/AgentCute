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

    void Awake()
    {
        gameObject.GetComponent<PlayerEventHandler>().registerLevelObserver(RegisterLevelObserver);
        gameObject.GetComponent<PlayerEventHandler>().registerHpObserver(RegisterHpObserver);
        gameObject.GetComponent<PlayerEventHandler>().registerExpObserver(RegisterExpObserver);
        mLevelBar = GameObject.Find("LevelBar");
        mHpBar = GameObject.Find("HpBar");
        mExpBar = GameObject.Find("ExpBar");
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void RegisterLevelObserver(int _level)
    {

        mLevelBar.transform.Find("LevelText").GetComponent<Text>().text = "Lv." + _level;
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
    }

}
