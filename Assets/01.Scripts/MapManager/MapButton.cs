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
        // ��ư ������Ʈ�� �����ɴϴ�.
        button = GetComponent<Button>();

        // ��ư�� �̺�Ʈ�� �߰��մϴ�.
        //button.onClick.AddListener(() => OnClick());
    }

    // ���콺 �����Ͱ� ��ư ���� �ö��� �� ȣ��� �Լ�
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("���콺 �����Ͱ� ��ư ���� �ö󰬽��ϴ�!");
        onPointerEnterEvent?.Invoke();
    }

    // ���콺 �����Ͱ� ��ư���� ����� �� ȣ��� �Լ�
    public void OnPointerExit(PointerEventData eventData)
    {
        onPointerExitEvent?.Invoke();
        Debug.Log("���콺 �����Ͱ� ��ư���� ������ϴ�!");
    }

    // ��ư�� Ŭ���Ǿ��� �� ȣ��� �Լ�
    void OnClick()
    {
        Debug.Log("��ư�� Ŭ���Ǿ����ϴ�!");
    }

}
