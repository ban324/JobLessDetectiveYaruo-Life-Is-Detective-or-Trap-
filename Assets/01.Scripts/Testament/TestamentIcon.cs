using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TestamentIcon : MonoBehaviour
{
    public Image image;
    Sequence seq;
    Vector3 originSize;
    public UnityEvent evt;
    public void Initiate(Sprite sprite)
    {
        Debug.LogWarning("¿Ã¥œº»∂Û¿Ã¡µ ");
        originSize = transform.localScale ;
        image.sprite = sprite;
        seq = DOTween.Sequence();
        seq.onKill += () =>
        {
            Debug.LogError("¡◊¿Ω");
        };
        seq.SetAutoKill(false).Append(transform.DOMove(new Vector3(0,10,transform.position.z), 0.4f)).Join(transform.DOScale(Vector3.one * 0.3f, 0.4f)).AppendCallback(() => { Debug.Log("¿Ã∫•∆Æ Ω««‡"); evt?.Invoke(); });
    }
    public void ReturnOriginalScale()
    {
        transform.localScale = originSize;
    }
    
    public void Proposal()
    {
        Debug.Log(seq.IsPlaying());
        if(!seq.IsPlaying())
        {
            Debug.Log(transform.position);
            Debug.Log(transform.localScale  );
            seq.Restart();
            Debug.Log("Ω√ƒˆΩ∫ »£√‚");
        }else
        {

        }
    }
}
