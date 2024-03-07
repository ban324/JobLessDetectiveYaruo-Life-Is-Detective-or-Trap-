using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TestamentItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TestamentSO SO;
    public CommentSO talkComment;
    public Button btn;
    public UnityEvent evt;
    public bool isAlreadyInpected;
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(this.Event);
        GetComponent<Image>().alphaHitTestMinimumThreshold = 0.5f;
        btn = GetComponent<Button>();
        evt.AddListener(() =>
        {
            InventoryManager.instance.AddTestament(SO);
            //Destroy(gameObject);
        });
    }
    private void Update()
    {
        if(InspectionManager.instance.isOnCapture)
        {
            btn.enabled= true;
        }else
        {
            btn.enabled= false;
        }
    }
    public void Event()
    {
        Debug.Log("´­¸²");
        if(talkComment)
        {
            TextManager.instance.currentComment = talkComment;
            TextManager.instance.commentIdx = 0;
            TextManager.instance.StartTalk(talkComment.texts[0]);
            InspectionManager.instance.SetCursorEffect(false);
            InspectionManager.instance.isOnCapture = false;
            isAlreadyInpected = true;
            evt?.Invoke();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(InspectionManager.instance.isOnCapture)
        {

            InspectionManager.instance.SetCursorEffect(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (InspectionManager.instance.isOnCapture )
        {
            if(isAlreadyInpected)
            {

                InspectionManager.instance.SetCursorEffect(true);
                InspectionManager.instance.SetCursorCollor(Color.red);
            }else
            {

                InspectionManager.instance.SetCursorEffect(true);
                InspectionManager.instance.SetCursorCollor(Color.green);
            }
        }
    }
}
