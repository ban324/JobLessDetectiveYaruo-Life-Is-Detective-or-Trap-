using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlacedCharacter : MonoBehaviour
{
    public Image characterImage;
    public TestamentItem clickSrc;
    public string commentKey;
    public Dictionary<string, KeyValuePair<string, string>> mapData;
    private void Awake()
    {
        characterImage = GetComponent<Image>();
        clickSrc = GetComponent<TestamentItem>();
        mapData = new Dictionary<string, KeyValuePair<string,string>>();
        gameObject.SetActive(false);
    }
    public void Initialize(string newKey, string newCommentKey)
    {
        //Debug.Log("ƒ≥∏Ø≈Õ ºº∆√µ ");
        commentKey = newCommentKey;
        if(newKey == "-" || newKey[0] == '-')
        {
            //Debug.Log("ƒ≥∏Ø≈Õ ≤®¡¸");
            characterImage.enabled = false;
            clickSrc.enabled = false;
            return;
        }
        if (mapData.ContainsKey(MapManager.instance.currentMap.mapName))
        {
            mapData[MapManager.instance.currentMap.mapName] = new KeyValuePair<string, string>(newKey, commentKey);

        }
        else
        {
            //Debug.LogError("§±§∑∑™§¿§±§§ø¿éT¿˙∑°§¡¡÷§¿∑Ó≥ª§¡§√∑£æÓéA¡÷∑¨§∑∑±æﬂêE§∏§©§∏§©");
            //Debug.LogError("ƒ≥∏Ø≈Õ πËƒ°µ ");

            mapData.Add(MapManager.instance.currentMap.mapName, new KeyValuePair<string, string>(newKey, commentKey));
        }
        if (TextManager.instance.cgDictionary.ContainsKey(newKey))
        {
            characterImage.enabled = true;
            characterImage.sprite = TextManager.instance.cgDictionary[newKey].sprites[0];
            clickSrc.enabled = true;
            clickSrc.commentKey = commentKey;
        }
        clickSrc.Initialize();
    }
    private void Update()
    {
        //if(Input.GetKeyDown(KeyCode.L))
        //{
        //    Debug.LogError("tlwkr");
        //    foreach (var v in mapData)
        //    {
        //        Debug.Log(v.Value.Key + "  " + v.Value.Value);
        //    }
        //    Debug.LogError("tlwkr");

        //}
    }
    public void Load()
    {
        Debug.LogError("∑ŒµÂµ ");
        if (mapData.ContainsKey(MapManager.instance.currentMap.mapName) )
        {
            Debug.LogWarning(MapManager.instance.currentMap.mapName);
            gameObject.SetActive(true);
            clickSrc.commentKey = mapData[MapManager.instance.currentMap.mapName].Value;
            characterImage.enabled = true;
            characterImage.sprite = TextManager.instance.cgDictionary[mapData[MapManager.instance.currentMap.mapName].Key].sprites[0];
            clickSrc.enabled = true;
            clickSrc.commentKey = commentKey;
        }
        else
        {
            characterImage.enabled = false;
            clickSrc.enabled = false;

        }
    }
    public void RemoveCharacter(string mapKey)
    {
        if (mapData.ContainsKey(mapKey))
        {
            mapData.Remove(mapKey);
            characterImage.enabled = false;
            clickSrc.enabled = false;

        }
    }
    private void OnDisable()
    {
        Debug.LogError("asdfasdfsa");
    }
}
