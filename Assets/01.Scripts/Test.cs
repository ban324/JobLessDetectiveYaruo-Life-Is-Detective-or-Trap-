using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Sequence seq;
    public bool b;
    void Start()
    {
        seq = DOTween.Sequence();
        seq.AppendCallback(() =>
        {
            b = true;
        }).SetAutoKill(false).Append(transform.DOMove(Vector3.up, 0.4f)).Append(transform.DOMove(Vector3.zero, 0.4f)).AppendCallback(() =>
        {
            b = false;
        });

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {

            seq.Restart();

            if (!b)
            {
                Debug.Log(seq.IsActive());
            }

        }
    }
}
