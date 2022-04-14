using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : SingleToneMaker<PlayerManager>
{
    // Start is called before the first frame update
    [SerializeField]
    private SpriteRenderer mWeaponSprite;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public SpriteRenderer getPlayerWeaponSprite()
    {
        return mWeaponSprite;
    }

    public void SettingGameStart()
    {
        GameObject.Find("PlayerObject").GetComponent<PlayerAttack>().getProjectiles();
    }
}
