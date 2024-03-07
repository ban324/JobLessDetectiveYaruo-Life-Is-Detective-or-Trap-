using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestamentIcon : MonoBehaviour
{
    public Image image;
    public void Initiate(Sprite sprite)
    {
        image.sprite = sprite;
    }
}
