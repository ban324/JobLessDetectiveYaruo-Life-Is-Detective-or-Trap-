using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ImpactEffect : MonoBehaviour
{
    public Sequence seq;
    public UnityEvent evt;
    public float inDuration = 0.1f;
    public float outDuration = 0.3f;
    public Image img;
    public Color color;
    public void Awake()
    {
        seq = DOTween.Sequence();
        img = GetComponent<Image>();
        Color c1, c2;
        c1 = color;
        c1.a = 1f;
        c2 = color;
        c2.a = 0;
        
        seq.SetAutoKill(false).
        Append(img.DOColor(c2,0)).
        AppendCallback(() => { img.enabled = true ; }).
        Append(img.DOColor(c1, inDuration)).
        AppendCallback(() => {  evt?.Invoke(); }).
        Append(img.DOColor(c2, outDuration)).
        AppendCallback(()=> { img.enabled = false; evt = new UnityEvent(); });
            
        img.enabled =false;
    }
    public void OnEffect()
    {
        if(!seq.IsPlaying())
        {

            seq.Restart();
        }
    }
}
