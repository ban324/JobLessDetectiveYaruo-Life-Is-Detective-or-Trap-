using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PopupManager : MonoBehaviour
{
    public GameObject imagePanel;
    public Image image;
    public List<TestamentCGSO> cgSOList;
    public Dictionary<string,TestamentCGSO> cgSOs;
    public static PopupManager instance;
    private void Awake()
    {
        instance = this;
        cgSOs=  new Dictionary<string,TestamentCGSO>();
        foreach(var c in cgSOList)
        {
            Debug.Log(c.idx.ToString());
            cgSOs.Add(c.idx.ToString(), c);
        }
        imagePanel.SetActive(false);
    }
    public void OpenItem(string key)
    {
        Debug.Log(key);
        if(cgSOs.ContainsKey(key))
        {
            imagePanel.SetActive(true);
            image.sprite = cgSOs[key].sprite;
        }
    }
    public void ClosePopup()
    {
        imagePanel.SetActive(false);
    }
}
