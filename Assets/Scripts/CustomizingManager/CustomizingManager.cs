using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class CustomizingManager : SingleToneMaker<CustomizingManager>
{
    [SerializeField]
    private Sprite[] mSkinList;
    [SerializeField]
    private Sprite[] mEyeList;
    [SerializeField]
    private Sprite[] mFaceHairList;
    [SerializeField]
    private Sprite[] mHairList;

    [SerializeField]
    private GameObject mSelectPannel;
    [SerializeField]
    private GameObject mContainer;
    [SerializeField]
    private GameObject mSpriteItem;
    [SerializeField]
    private GameObject mColorPannel;

    [SerializeField]
    PlayerSprite mPlayerSprite;


    [SerializeField]
    private SpriteType mCurrentSelectType;

    [SerializeField]
    private IntGameObject mCurrentButtonList;

    [SerializeField]
    private Texture2D tex;
    [SerializeField]
    private Color mNowColor;

    private const int SKIN_SPRITE_COUNT = 6;
    public enum SpriteType
    {
        Skin,
        Eye,
        FaceHair,
        Hair
    }

    private void Awake()
    {
        InitSpriteList();
    }
    private void InitSpriteList()
    {
        // 피부색 리스트
        mSkinList = Resources.LoadAll<Sprite>("SPUM/SPUM_Sprites/Packages/0_Human/Skin");
        // 눈 리스트
        mEyeList = Resources.LoadAll<Sprite>("SPUM/SPUM_Sprites/Packages/0_Human/Eye");
        // 헤어 리스트
        mHairList = Resources.LoadAll<Sprite>("SPUM/SPUM_Sprites/Packages/Ver300/0_Hair");
        // 수염 리스트
        mFaceHairList = Resources.LoadAll<Sprite>("SPUM/SPUM_Sprites/Packages/Ver300/1_FaceHair");
    }
    public void ClickSelectPannel(int _num)
    {
        mCurrentSelectType = (SpriteType)_num;
        switch (mCurrentSelectType)
        {
            case SpriteType.Skin:
                for (int i = 0; i < mSkinList.Length; i++)
                {
                    if (i % SKIN_SPRITE_COUNT != 5)
                        continue;
                    GameObject newButton = Instantiate(mSpriteItem, mContainer.transform);
                    newButton.transform.GetChild(0).GetComponent<Image>().sprite = mSkinList[i];
                    int num = i - 5;
                    newButton.GetComponent<Button>().onClick.AddListener(() =>
                    {
                        SelectSprite(num, mCurrentSelectType);
                    });

                    mCurrentButtonList.Add(num, newButton);
                }
                break;
            case SpriteType.Eye:
                for (int i = 0; i < mEyeList.Length; i++)
                {
                    if (i % 2 == 1)
                        continue;
                    GameObject newButton = Instantiate(mSpriteItem, mContainer.transform);
                    newButton.transform.GetChild(0).GetComponent<Image>().sprite = mEyeList[i];
                    int num = i;
                    newButton.GetComponent<Button>().onClick.AddListener(() =>
                    {
                        SelectSprite(num, mCurrentSelectType);
                    });

                    mCurrentButtonList.Add(num, newButton);
                }
                break;
            case SpriteType.FaceHair:
                for (int i = 0; i < mFaceHairList.Length; i++)
                {
                    GameObject newButton = Instantiate(mSpriteItem, mContainer.transform);
                    newButton.transform.GetChild(0).GetComponent<Image>().sprite = mFaceHairList[i];
                    int num = i;
                    newButton.GetComponent<Button>().onClick.AddListener(() =>
                    {
                        SelectSprite(num, mCurrentSelectType);
                    });

                    mCurrentButtonList.Add(num, newButton);
                }
                break;
            case SpriteType.Hair:
                for (int i = 0; i < mHairList.Length; i++)
                {
                    GameObject newButton = Instantiate(mSpriteItem, mContainer.transform);
                    newButton.transform.GetChild(0).GetComponent<Image>().sprite = mHairList[i];
                    int num = i;
                    newButton.GetComponent<Button>().onClick.AddListener(() =>
                    {
                        SelectSprite(num, mCurrentSelectType);
                    });

                    mCurrentButtonList.Add(num, newButton);
                }
                break;
        }
        mSelectPannel.SetActive(true);
    }
    private void SelectSprite(int _num, SpriteType _type)
    {
        foreach(int key in mCurrentButtonList.Keys)
        {
            mCurrentButtonList[key].transform.GetChild(1).gameObject.SetActive(false);
            if(key == _num)
                mCurrentButtonList[key].transform.GetChild(1).gameObject.SetActive(true);
        }
        switch (_type)
        {
            case SpriteType.Skin:
                List<Sprite> newList = new List<Sprite>();
                for (int i = _num; i < _num + SKIN_SPRITE_COUNT; i++)
                    newList.Add(mSkinList[i]);
                mPlayerSprite.SetSkin(newList);
                mPlayerSprite.SaveInfo[(int)_type] = _num;
                break;
            case SpriteType.Eye:
                mPlayerSprite.SetEye(mEyeList[_num], mEyeList[_num + 1]);
                mPlayerSprite.SaveInfo[(int)_type] = _num;
                break;
            case SpriteType.FaceHair:
                mPlayerSprite.SetFaceHair(mFaceHairList[_num]);
                mPlayerSprite.SaveInfo[(int)_type] = _num + 1;
                break;
            case SpriteType.Hair:
                mPlayerSprite.SetHair(mHairList[_num]);
                mPlayerSprite.SaveInfo[(int)_type] = _num + 1;
                break;
        }
    }

    public void CloseSelectPannel()
    {
        mSelectPannel.SetActive(false);
        Transform[] childrens = mContainer.GetComponentsInChildren<Transform>();
        foreach (Transform item in childrens)
        {
            if (item.name == mContainer.name)
                continue;
            Destroy(item.gameObject);
        }
        mCurrentButtonList.Clear();
    }
    public void OpenColorPicker(int _num)
    {
        mCurrentSelectType = (SpriteType)_num;
        mColorPannel.SetActive(true);
    }
    public void CloseColorPicker()
    {
        mColorPannel.SetActive(false);
    }
    public void PickColor()
    {
        tex = new Texture2D(1, 1);
        StartCoroutine(CaptureTempArea());
    }

    IEnumerator CaptureTempArea()
    {
        yield return new WaitForEndOfFrame();
#if ENABLE_INPUT_SYSTEM
        Vector2 pos = Mouse.current.position.ReadValue();
#elif ENABLE_LEGACY_INPUT_MANAGER
        Vector2 pos = EventSystem.current.currentInputModule.input.mousePosition;
#endif

        tex.ReadPixels(new Rect(pos.x, pos.y, 1, 1), 0, 0);
        tex.Apply();
        mNowColor = tex.GetPixel(0, 0);

        yield return new WaitForSecondsRealtime(0.1f);
        SetColor();
    }

    private void SetColor()
    {
        switch (mCurrentSelectType)
        {
            case SpriteType.Eye:
                mPlayerSprite.SetEyeColor(mNowColor);
                break;
            case SpriteType.FaceHair:
                mPlayerSprite.SetFaceHairColor(mNowColor);
                break;
            case SpriteType.Hair:
                mPlayerSprite.SetHairColor(mNowColor);
                break;
        }
        mPlayerSprite.SaveColorInfo[(int)mCurrentSelectType] = mNowColor;
    }

    public void SaveCostumeInfo()
    {
        PlayerPrefs.SetString("Info", "true");
        for (int i = 0; i < mPlayerSprite.SaveInfo.Length; i++)
            PlayerPrefs.SetInt(mPlayerSprite.SpriteKey + i, mPlayerSprite.SaveInfo[i]);

        for (int i = 0; i < mPlayerSprite.SaveColorInfo.Length; i++)
        {
            PlayerPrefs.SetFloat(mPlayerSprite.ColorKey + i + "r", mPlayerSprite.SaveColorInfo[i].r);
            PlayerPrefs.SetFloat(mPlayerSprite.ColorKey + i + "g", mPlayerSprite.SaveColorInfo[i].g);
            PlayerPrefs.SetFloat(mPlayerSprite.ColorKey + i + "b", mPlayerSprite.SaveColorInfo[i].b);
        }
        // 로비씬 로드
        SceneManager.LoadScene("LobbyScene");
    }

    public void SaveShowHelmet()
    {
        PlayerPrefs.SetString(mPlayerSprite.ShowKey, (!mPlayerSprite.ShowHelmet).ToString());
    }

    public void LoadCostumeInfo()
    {
        mPlayerSprite.InitSprite();
        // Sprite 불러오기
        for(int i = 0; i < mPlayerSprite.SaveInfo.Length; i++)
        {
            switch ((SpriteType)i)
            {
                case SpriteType.Skin:
                    List<Sprite> newList = new List<Sprite>();
                    for (int j = mPlayerSprite.SaveInfo[i]; j < mPlayerSprite.SaveInfo[i] + SKIN_SPRITE_COUNT; j++)
                        newList.Add(mSkinList[j]);
                    mPlayerSprite.SetSkin(newList);
                    break;
                case SpriteType.Eye:
                    mPlayerSprite.SetEye(mEyeList[mPlayerSprite.SaveInfo[i]], mEyeList[mPlayerSprite.SaveInfo[i] + 1]);
                    break;
                case SpriteType.FaceHair:
                    if(mPlayerSprite.SaveInfo[i] > 0)
                        mPlayerSprite.SetFaceHair(mFaceHairList[mPlayerSprite.SaveInfo[i] - 1]);
                    break;
                case SpriteType.Hair:
                    if (mPlayerSprite.SaveInfo[i] > 0)
                        mPlayerSprite.SetHair(mHairList[mPlayerSprite.SaveInfo[i] - 1]);
                    break;
            }
        }
        // Color 불러오기
        for(int i = 0; i< mPlayerSprite.SaveColorInfo.Length; i++)
        {
            switch ((SpriteType)i)
            {
                case SpriteType.Eye:
                    mPlayerSprite.SetEyeColor(mPlayerSprite.SaveColorInfo[i]);
                    break;
                case SpriteType.FaceHair:
                    mPlayerSprite.SetFaceHairColor(mPlayerSprite.SaveColorInfo[i]);
                    break;
                case SpriteType.Hair:
                    mPlayerSprite.SetHairColor(mPlayerSprite.SaveColorInfo[i]);
                    break;
            }
        }
    }
}
