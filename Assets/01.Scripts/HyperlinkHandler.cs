using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HyperlinkHandler : MonoBehaviour, IPointerClickHandler
{
    public UnityEvent evt;
    private void Awake()
    {
        evt.AddListener(() =>
        {
            OpenURL();
        });
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if(InspectionManager.instance.isOnCapture )
        {
            Vector3 sc = FindObjectOfType<CanvasScaler>().referenceResolution;
            Debug.Log(eventData.position);
            Vector3 v = eventData.position;
            v.z = 0;
            TMP_Text tex = GetComponentInChildren<TMP_Text>();
            //Debug.Log(tex.textInfo.linkInfo.Length);
            foreach(var t in tex.textInfo.linkInfo)
            {
                Debug.Log(t.GetLinkText());
            }
            int i = TMP_TextUtilities.FindIntersectingLink(tex, v, Camera.main);
            if (i != -1)
            {
                if (TextManager.instance.currentComment.texts[TextManager.instance.commentIdx].evt.evtType == TalkEventType.PointOut)
                {
                    TalkEventManager.ininstance.InvokeEventToKey(TextManager.instance.currentComment.texts[TextManager.instance.commentIdx].evt.target1Key);
                }
            }
        }
    }

    void OpenURL()
    {
        // 원하는 URL을 여기에 추가합니다.
        Debug.Log("URL Opened");
    }
}
