using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class BlurUIEffect : MonoBehaviour
{
    public Material mat;
    public Sequence seq;
    public Sequence seq2;
    private float v;
    void Start()
    {
        mat = GetComponent<Image>().material;
        seq = DOTween.Sequence();
        seq.SetAutoKill(false).Append(mat.DOFloat(1f, "_Radius", 0.3f)).Append(mat.DOFloat(0, "_Radius", 0.1f)).AppendCallback(() =>
        {
                    });
        seq2 = DOTween.Sequence();
        seq2.SetAutoKill(false).Append(mat.DOFloat(11f, "_Radius", 0.3f)).Append(mat.DOFloat(10, "_Radius", 0.1f)).AppendCallback(() =>
        {
            Debug.LogError("블러긑1");
        });
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            Debug.LogError("블ㄹ러!");

            
            seq2.Restart();
        }
    }
    public void Play(bool b)
    {
        if(!seq.IsPlaying())
        {
            if(b)
            {
                seq2.Restart();
            }else
            {
                seq.Restart();
            }
        }
    }
}
