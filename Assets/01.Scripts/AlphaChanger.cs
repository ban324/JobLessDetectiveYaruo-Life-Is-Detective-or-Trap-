using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlphaChanger : MonoBehaviour
{
    public void Awake()
    {
        GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;

    }
}
