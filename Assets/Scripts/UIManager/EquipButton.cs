using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EquipButton : MonoBehaviour
{
    [SerializeField]
    private GameObject mUnlockImage;
    public GameObject UnlockImage
    {
        get { return mUnlockImage; }
        set { mUnlockImage = value; }
    }
    [SerializeField]
    private GameObject mImage;
    public GameObject Image
    {
        get { return mImage; }
        set { mImage = value; }
    }
    [SerializeField]
    private GameObject mEquipCheckImage;
    public GameObject EquipCheckImage
    {
        get { return mEquipCheckImage; }
        set { mEquipCheckImage = value; }
    }
    [SerializeField]
    private GameObject mShapeCheckImage;
    public GameObject ShapeCheckImage
    {
        get { return mShapeCheckImage; }
        set { mShapeCheckImage = value; }
    }
    [SerializeField]
    private GameObject mCommonRank;
    public GameObject CommonRank
    {
        get { return mCommonRank; }
        set { mCommonRank = value; }
    }
    [SerializeField]
    private GameObject mAdvancedRank;
    public GameObject AdvancedRank
    {
        get { return mAdvancedRank; }
        set { mAdvancedRank = value; }
    }
    [SerializeField]
    private GameObject mSuperiorRank;
    public GameObject SuperiorRank
    {
        get { return mSuperiorRank; }
        set { mSuperiorRank = value; }
    }
}
