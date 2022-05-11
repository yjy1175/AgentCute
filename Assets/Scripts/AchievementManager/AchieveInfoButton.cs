using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchieveInfoButton : MonoBehaviour
{
    public GameObject mDesc;
    public GameObject mRewardIcon;
    public GameObject mRewardButton;
    public GameObject mComplete;
    public GameObject mInactive;
    public GameObject mUnactive;

    public void Unactive()
    {
        mDesc.transform.GetChild(1).gameObject.SetActive(true);
        mRewardIcon.transform.GetChild(1).gameObject.SetActive(true);
        mUnactive.SetActive(true);
    }

    public void Inactive()
    {
        mDesc.transform.GetChild(1).gameObject.SetActive(false);
        mRewardIcon.transform.GetChild(1).gameObject.SetActive(false);
        mUnactive.SetActive(false);

        mInactive.SetActive(true);
    }

    public void WaitForComplete()
    {
        mDesc.transform.GetChild(1).gameObject.SetActive(false);
        mRewardIcon.transform.GetChild(1).gameObject.SetActive(false);
        mUnactive.SetActive(false);

        mInactive.SetActive(false);

        mRewardButton.SetActive(true);
    }

    public void Complete()
    {
        mDesc.transform.GetChild(1).gameObject.SetActive(false);
        mRewardIcon.transform.GetChild(1).gameObject.SetActive(false);
        mUnactive.SetActive(false);

        mInactive.SetActive(false);

        mRewardButton.SetActive(false);
        mComplete.SetActive(true);
    }
}
