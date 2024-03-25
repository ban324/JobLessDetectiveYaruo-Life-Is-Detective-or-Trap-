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

            }else 
            {
                if(TextManager.instance.GetCurrentEvent().evtType == TalkEventType.PointOut)
                {

                    TextManager.instance.StopAllCoroutines();
                    TextManager.instance.StartTalk(TextManager.instance.currentComment.texts[TextManager.instance.commentIdx]);
                }else
                {
                    TextManager.instance.StopAllCoroutines();
                    TextManager.instance.TryOpenTalk(CommentDatabase.instance.GetComment(TextManager.instance.currentComment.wrongPointoutIdx));
                }
            }
            var arr = FindObjectsOfType<TestamentItem>();
            foreach(var v in arr)
            {
                v.particle.Stop();
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