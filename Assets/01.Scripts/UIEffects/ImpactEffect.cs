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
    public Image img;
    public void Awake()
    {
        seq = DOTween.Sequence();
        img = GetComponent<Image>();
        Color c1, c2;
        c1 = Color.white;
        c1.a = 0.7f;
        c2 = Color.white;
        c2.a = 0;
        seq.SetAutoKill(false).AppendCallback(() => { img.gameObject.SetActive(true); }).Append(img.DOColor(c1, 0.1f)).Append(img.DOColor(c2, 0.3f)).AppendCallback(() => { evt?.Invoke(); }).AppendCallback(()=> { img.gameObject.SetActive(false); });
        img.gameObject.SetActive(false);
    }
    public void OnEffect()
    {
        if(!seq.IsPlaying())
        {
            seq.Restart();
        }
    }
}
