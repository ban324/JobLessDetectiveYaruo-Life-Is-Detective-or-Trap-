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
    public ParticleSystem particle;
    
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(this.ClickEvent);
        GetComponent<Image>().alphaHitTestMinimumThreshold = 0.5f;
        btn = GetComponent<Button>();
        particle = GetComponentInChildren<ParticleSystem>();
    }
    private void Update()
    {
        if(InspectionManager.instance.isOnCapture)
        {
            btn.enabled= true;
        }else
        {
            btn.enabled= false;
            if(particle.isPlaying)
            {
                particle.Stop();
            }
        }
    }
    public void ClickEvent()
    {
        //Debug.Log("´­¸²");
        if(talkComment)
        {
            TextManager.instance.currentComment = talkComment;
            TextManager.instance.commentIdx = 0;
            TextManager.instance.StartTalk(talkComment.texts[0]);
            InspectionManager.instance.SetCursorEffect(false);
            GetComponentInChildren<ParticleSystem>().Stop();

            InspectionManager.instance.isOnCapture = false;
            isAlreadyInpected = true;
            evt?.Invoke();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(InspectionManager.instance.isOnCapture)
        {
            GetComponentInChildren<ParticleSystem>().Stop();
            //InspectionManager.instance.SetCursorEffect(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (InspectionManager.instance.isOnCapture )
        {
            if(isAlreadyInpected)
            {
                var main = GetComponentInChildren<ParticleSystem>().main;
                Color c = Color.red;
                c.a /= 2;
                main.startColor = c;
                GetComponentInChildren<ParticleSystem>().Play();
                //InspectionManager.instance.SetCursorEffect(true);
            }else
            {
                var main = GetComponentInChildren<ParticleSystem>().main;
                Color c = Color.green;
                c.a /= 2;
                main.startColor = c;
                GetComponentInChildren<ParticleSystem>().Play();

            }
        }
    }
}
