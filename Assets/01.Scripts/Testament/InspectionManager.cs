using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InspectionManager : MonoBehaviour
{
    public RectTransform markerTransform;
    public bool isOnCapture;
    public static InspectionManager instance;
    private void Awake()
    {
        instance = this;
    }
    void Update()
    {
        markerTransform.anchoredPosition = (Input.mousePosition);
        markerTransform.anchoredPosition = new Vector3(markerTransform.anchoredPosition.x, markerTransform.anchoredPosition.y, 0);
        if(Input.GetKeyDown(KeyCode.Q) )
        {
            isOnCapture = !isOnCapture;
            if(TextManager.instance.state == TalkState.none)
            {
                SetCursorEffect(isOnCapture);

            }
            var arr = FindObjectsOfType<TestamentItem>();
            foreach(var v in arr)
            {
                Debug.Log(v.name);
                v.particle.Stop();
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && !InventoryManager.instance.inventoryPanel.activeSelf)
        {
            Debug.Log("시도");
            if (TextManager.instance.state == TalkState.waitTalk || TextManager.instance.state == TalkState.onTalk)
            {
                Debug.Log("가능");
                if (TextManager.instance.GetCurrentEvent().evtType == TalkEventType.PointOut)
                {
                    Debug.Log("실행");
                    TextManager.instance.TryOpenTalk(CommentDatabase.instance.GetComment(TextManager.instance.GetCurrentEvent().target1Key));
                    InspectionManager.instance.isOnCapture = false;
                }else if(TextManager.instance.commentIdx != 0)
                {
                    TextManager.instance.TryOpenTalk(CommentDatabase.instance.GetComment(TextManager.instance.currentComment.wrongPointoutIdx));
                    InspectionManager.instance.isOnCapture = false;
                }
            }
        }
    }
    public void SetCursorEffect(bool val)
    {
        if(val)
        {
            //markerTransform.gameObject.SetActive(true);
            markerTransform.GetComponent<ParticleSystem>().Play();
        }else
        {
            //markerTransform.gameObject.SetActive(false);
            markerTransform.GetComponent<ParticleSystem>().Stop();

        }
    }
    public void SetCursorCollor(Color color)
    {
        markerTransform.GetComponent<ParticleSystem>().startColor = color;
    }
}