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
    public string commentKey;
    public Button btn;
    public UnityEvent evt;
    public bool isAlreadyInpected;
    public ParticleSystem particle;
    
    private void Awake()
    {
        Initialize();
    }
    public void Initialize()
    {
        GetComponent<Button>().onClick.AddListener(this.ClickEvent);
        GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
        btn = GetComponent<Button>();

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
    public void ClickEvent()
    {
        //Debug.Log("´­¸²");
        if(CommentDatabase.instance.GetComment(commentKey))
        {
            CommentSO com = CommentDatabase.instance.GetComment(commentKey);
            TextManager.instance.TryOpenTalk(com);
            InspectionManager.instance.SetCursorEffect(false);
            InspectionManager.instance.SetColorNone();

            InspectionManager.instance.isOnCapture = false;
            isAlreadyInpected = true;
            evt?.Invoke();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(InspectionManager.instance.isOnCapture)
        {
            InspectionManager.instance.SetColorNone();
            //InspectionManager.instance.SetCursorEffect(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("InspectionManager.instance.isOnCapture" + InspectionManager.instance.isOnCapture);
        if (InspectionManager.instance.isOnCapture )
        {
            if(isAlreadyInpected)
            {
                InspectionManager.instance.SetColorWrong();
            }else
            {

                InspectionManager.instance.SetColorRight();

            }
        }
    }
}
