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
        cgSOs=  new Dictionary<string,TestamentCGSO>();
        Debug.Log(cgSOList.Count);
        foreach(var c in cgSOList)
        {
            Debug.Log(c.idx.ToString());
            cgSOs.Add(c.idx.ToString(), c);
        }
    }
    public void OpenItem(string key)
    {
        Debug.Log(key);
        TextManager.instance.textBox.text = key;
        if(cgSOs.ContainsKey(key))
        {
            imagePanel.enableSeq.Restart();
            image.sprite = cgSOs[key].sprite;
            imagePanel.nameBox.text = cgSOs[key].sname;
        }
    }
    public void ClosePopup()
    {
        imagePanel.disableSeq.Restart();
    }
}
