using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.UI;


public class PopupManager : MonoBehaviour
{
    public ImagePanelTween imagePanel;
    public Image image;
    public List<TestamentCGSO> cgSOList;
    public Dictionary<string,TestamentCGSO> cgSOs;
    public static PopupManager instance;
    private void Awake()
    {
        instance = this;
        Debug.Log("LoadStart");
        cgSOs=  new Dictionary<string,TestamentCGSO>();
        Debug.Log("Dictionary Created");
        Debug.Log(cgSOList.Count);
        foreach(var c in cgSOList)
        {
            Debug.Log(c.idx.ToString());
            cgSOs.Add(c.idx.ToString(), c);
        }
        Debug.Log("SO Added");
        Debug.Log("LoadEnd");
    }
    public void OpenItem(string key)
    {
        Debug.Log(key);
        TextManager.instance.textBox.text = key;
        if(cgSOs.ContainsKey(key))
        {
            imagePanel.enableSeq.Restart();
            image.sprite = cgSOs[key].sprite;
        }
    }
    public void ClosePopup()
    {
        imagePanel.disableSeq.Restart();
    }
}
