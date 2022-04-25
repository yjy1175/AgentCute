using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MonsterUI : IUI
{
    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnEnable()
    {
        mStatusObject = GameObject.Find("MonsterStatusObject");
        mHpBar = mStatusObject.transform.Find("HpBar");
        gameObject.GetComponent<MonsterEventHandler>().registerHpObserver(RegisterHpObserver);
        mHpBar.gameObject.SetActive(true);
    }
    private void OnDisable()
    {
        mHpBar.gameObject.SetActive(false);
    }

    private void RegisterHpObserver(int _hp, GameObject _obj)
    {
        int hp = gameObject.GetComponent<MonsterStatus>().Hp;
        int maxHp = gameObject.GetComponent<MonsterStatus>().MaxHP;
        mHpBar.transform.Find("Hp").GetComponent<Image>().fillAmount = ((float)hp / (float)maxHp);
        mHpBar.transform.Find("HpText").GetComponent<Text>().text = _hp.ToString();
    }
}
