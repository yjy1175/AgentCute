using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageBox : MonoBehaviour
{
    /* 어디서나 불러올수 있게 public
    * 조절 값
    *폰트 크기, 폰트 종류, 폰트 색상, 나타나거나 사라지는 타입, 보여질 텍스트
    */
    #region variables
    // 데미지박스에 표기될 텍스트 컴포넌트
    [SerializeField]
    private TextMesh textCom;
    public TextMesh TextCom
    {
        get { return textCom; }
        set { textCom = value; }
    }
    // 텍스트 컬러
    [SerializeField]
    private Color alpha;
    public Color Alpha
    {
        get { return alpha; }
        set {
            alpha = value; 
        }
    }
    // 텍스트의 움직임 속도
    [SerializeField]
    private float moveSpeed;
    public float MoveSpeed
    {
        get { return moveSpeed; }
        set { moveSpeed = value; }
    }
    // 텍스트의 알파값 감소 속도
    [SerializeField]
    private float alphaSpeed;
    public float AlphaSpeed
    {
        get { return alphaSpeed; }
        set { alphaSpeed = value; }
    }
    // 텍스트의 파괴 속도
    [SerializeField]
    private float destroyTime;
    public float DestroyTime
    {
        get { return destroyTime; }
        set { destroyTime = value; }
    }
    // 텍스트의 크기
    [SerializeField]
    private int fontSize;
    public int FontSize
    {
        get { return fontSize; }
        set { fontSize = value; }
    }
    // 텍스트박스 동작 실행 변수
    [SerializeField]
    private bool isStart = false;
    public bool IsStart
    {
        get { return isStart; }
        set { isStart = value; }
    }

    private Color newAlpha;
    // 초기 생성 위치
    #endregion
    #region method
    private void LateUpdate()
    {
        // isStart가 true가 되면 실행됩니다.
        if (isStart)
        {
            transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0));

            newAlpha.a = Mathf.Lerp(newAlpha.a, 0, alphaSpeed * Time.deltaTime);
            textCom.color = newAlpha;

        }
    }
    public void setEnable(string _msg, Vector3 _pos)
    {
        newAlpha = alpha;
        textCom.text = _msg;
        transform.position = _pos;
        gameObject.SetActive(true);
        isStart = true;
    }

    public void setDisable()
    {
        isStart = false;
        textCom.color = alpha;
        gameObject.SetActive(false);
    }
    #endregion
}
