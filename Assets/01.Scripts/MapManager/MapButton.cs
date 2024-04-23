using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MapButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    private Button button;
    public UnityEvent onPointerEnterEvent;
    public UnityEvent onPointerExitEvent;
    public void Initialize()
    {
        // 버튼 컴포넌트를 가져옵니다.
        button = GetComponent<Button>();

        // 버튼에 이벤트를 추가합니다.
        //button.onClick.AddListener(() => OnClick());
    }

    // 마우스 포인터가 버튼 위에 올라갔을 때 호출될 함수
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("마우스 포인터가 버튼 위에 올라갔습니다!");
        onPointerEnterEvent?.Invoke();
    }

    // 마우스 포인터가 버튼에서 벗어났을 때 호출될 함수
    public void OnPointerExit(PointerEventData eventData)
    {
        onPointerExitEvent?.Invoke();
        Debug.Log("마우스 포인터가 버튼에서 벗어났습니다!");
    }

    // 버튼이 클릭되었을 때 호출될 함수
    void OnClick()
    {
        Debug.Log("버튼이 클릭되었습니다!");
    }

}
