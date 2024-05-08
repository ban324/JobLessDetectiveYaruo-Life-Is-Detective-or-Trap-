using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ImagePanelTween : MonoBehaviour
{
    public Sequence enableSeq;
    public Sequence disableSeq;
    public TextMeshProUGUI nameBox;
    private void Awake()
    {
        nameBox = GetComponentInChildren<TextMeshProUGUI>();
       enableSeq  = DOTween.Sequence().SetAutoKill(false).Append( transform.DOScaleY(1, 0.2f));
        disableSeq = DOTween.Sequence().SetAutoKill(false).Append(transform.DOScaleY(0, 0.1f));
    }
}
