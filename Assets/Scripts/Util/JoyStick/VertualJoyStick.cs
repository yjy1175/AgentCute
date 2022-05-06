using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VertualJoyStick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private Image bgImg;
    private Image controllerImg;
    private Vector3 inputVector;
    [SerializeField]
    private int type; // 좌측 패드 와 우측 패드 구분
    [SerializeField]
    private bool isMove;
    private void Start()
    {
        bgImg = GetComponent<Image>();
        controllerImg = transform.GetChild(0).GetComponent<Image>();
    }

    private void Update()
    {
//        if (isMove && Time.timeScale == 1)
//        {
//#if UNITY_EDITOR
//            if (Input.GetMouseButtonDown(0))
//            {
//                if (Input.mousePosition.x < Screen.width / 2 && Input.mousePosition.y < Screen.height / 2)
//                    transform.position = Input.mousePosition - new Vector3(GetComponent<RectTransform>().sizeDelta.x / 2, GetComponent<RectTransform>().sizeDelta.x / 2, 0);
//            }
//#else
//            if (Input.touchCount > 0)
//            {
//                Touch touch = Input.GetTouch(0);
//                if(touch.phase == TouchPhase.Began)
//                    if(touch.position.x < Screen.width / 2 && touch.position.y < Screen.height / 2)
//                        transform.position = touch.position - new Vector2(GetComponent<RectTransform>().sizeDelta.x / 2, GetComponent<RectTransform>().sizeDelta.x / 2);
//            }
//#endif
//        }
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(bgImg.rectTransform, eventData.position, eventData.pressEventCamera, out pos))
        {
            pos.x = (pos.x / bgImg.rectTransform.sizeDelta.x);
            pos.y = (pos.y / bgImg.rectTransform.sizeDelta.y); 

            inputVector = new Vector3(pos.x * 2 + type, pos.y * 2 - 1);
            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

            controllerImg.rectTransform.anchoredPosition = new Vector3(
                inputVector.x * (bgImg.rectTransform.sizeDelta.x / 3),
                inputVector.y * (bgImg.rectTransform.sizeDelta.y / 3));
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inputVector = Vector3.zero;
        controllerImg.rectTransform.anchoredPosition = Vector3.zero;
    }

    public float GetHorizontalValue()
    {
        return inputVector.x;
    }

    public float GetVerticalValue()
    {
        return inputVector.y;
    }
}
