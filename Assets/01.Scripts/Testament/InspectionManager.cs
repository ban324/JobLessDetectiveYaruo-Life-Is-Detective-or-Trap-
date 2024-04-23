using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InspectionManager : MonoBehaviour
{
    public RectTransform markerTransform;
    public Image markerImg;
    public bool isOnCapture;
    public static InspectionManager instance;
    public PointOutEffect effect;
    public Sequence seq1;
    public Sequence seq2;
    private void Awake()
    {
        instance = this;
        markerImg = markerTransform.GetComponent<Image>();  
        seq1 = DOTween.Sequence();
        seq1.SetAutoKill(false).AppendCallback(() => { markerImg.gameObject.SetActive(true); }).Append(markerImg.DOFade(1f, 0.2f)).AppendCallback(() =>
        {
            isOnCapture = true;
        });
        seq2 = DOTween.Sequence();
        seq2.SetAutoKill(false).AppendCallback(() => { markerImg.gameObject.SetActive(false); }).Append(markerImg.DOFade(0f, 0.2f)).AppendCallback(() =>
        {
            isOnCapture = false;
        });
        markerImg.gameObject.SetActive(false);
    }
    void Update()
    {
        markerTransform.anchoredPosition = (Input.mousePosition);
        markerTransform.anchoredPosition = new Vector3(markerTransform.anchoredPosition.x, markerTransform.anchoredPosition.y, 0);
        if(Input.GetKeyDown(KeyCode.Q) )
        {
            if(TextManager.instance.state == TalkState.none && !InventoryManager.instance.inventoryPanel.activeSelf)
            {
                SetCursorEffect(!isOnCapture);
                //isOnCapture = true;
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
                    effect.evt.AddListener(() =>
                    {
                        TextManager.instance.TryOpenTalk(CommentDatabase.instance.GetComment(TextManager.instance.GetCurrentEvent().target1Key));
                        effect.evt = new UnityEngine.Events.UnityEvent();
                    });
                    effect.Proposal();
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
        if(!seq1.IsPlaying() && !seq2.IsPlaying())
        {
            Debug.Log(val);
            if (val)
            {
                seq1.Restart();
            }
            else
            {
                //markerTransform.gameObject.SetActive(false);
                seq2.Restart();

            }
        }
    }
    public void SetCursorCollor(Color color)
    {
        markerTransform.GetComponent<ParticleSystem>().startColor = color;
    }
}