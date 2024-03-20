using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TestamentCGSO:ScriptableObject
{
    public Sprite sprite;
    public int idx;
    public string sname;
}

public class ImageManager : MonoBehaviour
{
    public GameObject imagePanel;
    public List<TestamentCGSO> cgSOList;
    public Dictionary<string,TestamentCGSO> cgSOs;
    private void Awake()
    {
        foreach(var c in cgSOList)
        {
            cgSOs.Add(c.idx.ToString(), c);
        }
        imagePanel.SetActive(false);
    }

}
