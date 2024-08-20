using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PointOutEffect : MonoBehaviour
{
    // Start is called before the first frame update
    public Sequence seq;
    public UnityEvent evt;
    void Start()
    {
        Tween t1 = transform.DOScaleY(1.2f, 0.2f);
        var vec = Camera.main.WorldToViewportPoint(Vector3.zero);
        Tween t2 = transform.DOMove(new Vector3(vec.x,vec.y, transform.position.z), 0.1f);
     
        Tween t3 = transform.DOScaleY(1f, 0.1f);
        Tween t4 = transform.DOScaleX(1f, 0.1f);
        seq = DOTween.Sequence().SetAutoKill(false).AppendInterval(0.3f).Append(t1).AppendInterval(0.01f).Append(t3).Join(t4).AppendInterval(0.1f).Append(transform.DOScale(new Vector3(transform.localScale.x, 0), 0.2f)).AppendCallback(() => { evt?.Invoke(); });
        
    }
    public void Proposal()
    {
        if(!seq.IsPlaying())
        {
            seq.Restart();
        }
    }
    private void Update()
    {
        //if(Input.GetKeyDown(KeyCode.K))
        //{
        //    Proposal();
        //}
    }
}
