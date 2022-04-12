using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerUI : IUI
{
    [SerializeField]
    private GameObject mLevelBar;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<PlayerEventHandler>().registerLevelObserver(RegisterLevelObserver);
        mLevelBar = GameObject.Find("LevelBar");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void RegisterLevelObserver(int _level)
    {
        mLevelBar.transform.Find("LevelText").GetComponent<Text>().text = "Lv." + _level;
    }

}
