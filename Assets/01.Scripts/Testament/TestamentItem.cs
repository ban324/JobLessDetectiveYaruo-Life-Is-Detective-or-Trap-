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
        //Debug.Log("����");
        if(CommentDatabase.instance.GetComment(commentKey))
        {
            CommentSO com = CommentDatabase.instance.GetComment(commentKey);
            TextManager.instance.TryOpenTalk(com);
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
        Debug.Log("InspectionManager.instance.isOnCapture" + InspectionManager.instance.isOnCapture);
        if (InspectionManager.instance.isOnCapture )
        {
            if(isAlreadyInpected)
            {
                Debug.Log("��ƼŬ Ŵ");
                var main = GetComponentInChildren<ParticleSystem>().main;
                Color c = Color.red;
                c.a /= 2;
                main.startColor = c;
                GetComponentInChildren<ParticleSystem>().Play();
                //InspectionManager.instance.SetCursorEffect(true);
            }else
            {
                Debug.Log("��ƼŬ ��");
                var main = GetComponentInChildren<ParticleSystem>().main;
                Color c = Color.green;
                c.a /= 2;
                main.startColor = c;
                GetComponentInChildren<ParticleSystem>().Play();

            }
        }
    }
}
