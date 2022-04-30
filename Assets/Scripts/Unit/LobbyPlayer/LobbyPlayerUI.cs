using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LobbyPlayerUI : MonoBehaviour
{
    [SerializeField]
    private Text mGoldText;
    [SerializeField]
    private Text mDiamondText;
    [SerializeField]
    private Text mSteminaText;
    // Start is called before the first frame update
    void Awake()
    {
        GetComponent<LobbyPlayerEventHendler>().resgisterGoodsObserver(ResisterGoodsObserver);
        mGoldText = GameObject.Find("Gold/Diamond/Stemina").transform.GetChild(0).GetChild(2).GetComponent<Text>();
        mDiamondText = GameObject.Find("Gold/Diamond/Stemina").transform.GetChild(1).GetChild(2).GetComponent<Text>();
        mSteminaText = GameObject.Find("Gold/Diamond/Stemina").transform.GetChild(2).GetChild(2).GetComponent<Text>();
    }

    private void ResisterGoodsObserver(int _gold, int _diamond, int _stemina)
    {
        mGoldText.text = _gold.ToString();
        mDiamondText.text = _diamond.ToString();
        mSteminaText.text = _stemina.ToString();
    }
}
