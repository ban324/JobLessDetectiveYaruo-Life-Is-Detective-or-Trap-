using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlacedCharacter : MonoBehaviour
{
    public Image characterImage;
    public TestamentItem clickSrc;
    public string commentKey;
    public Dictionary<string, string> mapData;
    private void Awake()
    {
        characterImage = GetComponent<Image>();
        clickSrc = GetComponent<TestamentItem>();
        mapData = new Dictionary<string, string>();
    }
    public void Initialize(string newKey, string newCommentKey)
    {
        Debug.Log("캐릭터 세팅됨");
        commentKey = newCommentKey;
        Debug.Log(newKey);
        mapData.Add(commentKey, MapManager.instance.currentMap.mapName);
        if(TextManager.instance.cgDictionary.ContainsKey(newKey))
        {
            characterImage.enabled = true;
            characterImage.sprite = TextManager.instance.cgDictionary[newKey].sprites[0];
            clickSrc.enabled = true;
            clickSrc.commentKey = commentKey;
        }else
        {
            characterImage.enabled = false;
            clickSrc.enabled = false;
        }
        clickSrc.Initialize();
    }
    
}
