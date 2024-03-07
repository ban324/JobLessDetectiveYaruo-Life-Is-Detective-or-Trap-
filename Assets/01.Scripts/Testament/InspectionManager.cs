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
        if(Input.GetKeyDown(KeyCode.Q))
        {
            isOnCapture = !isOnCapture;
            if(!isOnCapture)SetCursorEffect(false);
        }
    }
    public void SetCursorEffect(bool val)
    {
        if(val)
        {
            markerTransform.GetComponent<ParticleSystem>().Play();
        }else
        {
            markerTransform.GetComponent<ParticleSystem>().Stop();

        }
    }
    public void SetCursorCollor(Color color)
    {
        markerTransform.GetComponent<ParticleSystem>().startColor = color;
    }
}