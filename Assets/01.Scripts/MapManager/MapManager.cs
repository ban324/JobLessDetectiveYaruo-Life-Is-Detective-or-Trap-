using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    public static MapManager instance;
    public List<MapSO> mapSOList;
    public Dictionary<string, GameObject> testamentsDictionary;
    public Dictionary<string, MapSO> maps;
    private Dictionary<string, MapSO> mapSODict;
    public MapSO currentMap;
    public GameObject mapPanel;
    public Image backGround;
    public Image previewImage;
    public TextMeshProUGUI previewText;
    public Transform mapButtonParent;
    public GameObject mapButtonPrefab;
    public Dictionary<string,Button> mapButtons;
    public Transform testamentParent;
    public PlacedCharacter placedCharacter;
    public ImpactEffect changeEffect;
    public Dictionary<string, List<string>> connectMapsDictionary;
    private void Awake()
    {
        instance = this;
        maps = new Dictionary<string, MapSO>();

        mapSODict = new Dictionary<string, MapSO>();

        mapButtons = new Dictionary<string, Button>();
        connectMapsDictionary =new Dictionary<string, List<string>>();
        testamentsDictionary = new Dictionary<string, GameObject>();
        foreach(var v in mapSOList)
        {
            mapSODict.Add(v.mapName, v);
        }
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(1) && TextManager.instance.state == TalkState.none && TextManager.instance.IsClearForMap())
        {
            if(mapPanel.activeSelf)
            {
                SetMapScreen(false);
            }else
            {
                SetMapScreen(true);
            }
        }
        
        if(Input.GetKeyDown(KeyCode.Escape) && TextManager.instance.state == TalkState.none)
        {
            SetMapScreen(false);
        }
        //if (Input.GetKeyDown(KeyCode.V))
        //{
        //    foreach(var map in mapSOList)
        //    {
        //        OnMapUnlocked(map);
        //    }
        //}
    }
    public MapSO GetMap(string key)
    {
        if(mapSODict.ContainsKey(key))
        {
            Debug.Log("∏  ¡‹");
            return mapSODict[key];
        }
        return null;
    }
    public void AddEnterEvent(string key, UnityAction action)
    {
        Debug.Log(key + " ¿Ã∫•∆Æ √ﬂ∞° Ω√µµ");
        foreach(var map in maps)
        {
            Debug.Log(map.Value.mapName);
        }
        if(mapSODict.ContainsKey(key))
        {
            Debug.Log("¿Ã∫•∆Æ √ﬂ∞° º∫∞¯");
            mapSODict[key].enterEvt.AddListener(action);
        }
    }
    public void OnMapUnlocked(MapSO map, string targetName = "-")
    {
        Debug.Log(map);
        if (!map) return;
        Debug.Log("∏  √ﬂ∞°µ  : " + map.mapName);
        Debug.Log(map.mapName);
        if (targetName =="-")
        {
            if (!maps.ContainsKey(map.mapName))
            {

                maps.Add(map.mapName, map);
            }
            if (!connectMapsDictionary.ContainsKey(currentMap.mapName))
            {
                connectMapsDictionary.Add(currentMap.mapName, new List<string>());
            }
            if (!connectMapsDictionary.ContainsKey(map.mapName))
            {
                connectMapsDictionary.Add(map.mapName, new List<string>());
            }
            if (!connectMapsDictionary[currentMap.mapName].Contains(map.mapName))
            {
                connectMapsDictionary[currentMap.mapName].Add(map.mapName);
            }
            if (!connectMapsDictionary[map.mapName].Contains(currentMap.mapName))
            {

                connectMapsDictionary[map.mapName].Add(currentMap.mapName);
            }

            if (!mapButtons.ContainsKey(map.mapName))
            { 
                GameObject btn = Instantiate(mapButtonPrefab, mapButtonParent);
                btn.GetComponent<MapButton>().Initialize();
                btn.GetComponentInChildren<TextMeshProUGUI>().text = map.mapName;
                btn.GetComponent<Button>().onClick.AddListener(() => {
                    MapManager.instance.ChangeMap(map.mapName);
                });
                btn.GetComponent<MapButton>().onPointerEnterEvent.AddListener(() =>
                {
                    MapManager.instance.previewImage.sprite = map.mapSprite;
                    MapManager.instance.previewText.text = map.mapName;
                });
                mapButtons.Add(map.mapName, btn.GetComponent<Button>());

            }
        }
        else
        {
            if (!maps.ContainsKey(map.mapName))
            {

                maps.Add(map.mapName, map);
            }
            if (!connectMapsDictionary.ContainsKey(targetName))
            {
                connectMapsDictionary.Add(targetName, new List<string>());
            }
            if (!connectMapsDictionary.ContainsKey(map.mapName))
            {
                connectMapsDictionary.Add(map.mapName, new List<string>());
            }
            if (!connectMapsDictionary[targetName].Contains(map.mapName))
            {
                connectMapsDictionary[targetName].Add(map.mapName);
            }
            if (!connectMapsDictionary[map.mapName].Contains(targetName))
            {

                connectMapsDictionary[map.mapName].Add(targetName);
            }
            if(!mapButtons.ContainsKey(map.mapName))
            {
                GameObject btn = Instantiate(mapButtonPrefab, mapButtonParent);
                btn.GetComponent<MapButton>().Initialize();
                btn.GetComponentInChildren<TextMeshProUGUI>().text = map.mapName;
                btn.GetComponent<Button>().onClick.AddListener(() => {
                    MapManager.instance.ChangeMap(map.mapName);
                });
                btn.GetComponent<MapButton>().onPointerEnterEvent.AddListener(() =>
                {
                    MapManager.instance.previewImage.sprite = map.mapSprite;
                    MapManager.instance.previewText.text = map.mapName;
                });
                mapButtons.Add(map.mapName, btn.GetComponent<Button>());

            }
        }

        
    }

    public void PlaceCharacter(string imageKey, string commentKey)
    {
        placedCharacter.Initialize(imageKey, commentKey);
    }
    public void ChangeMap(string mapName)
    {
        if (currentMap.mapName == mapName) return;
        if (mapSODict.ContainsKey(mapName))
        {
            foreach(var v in testamentsDictionary)
            {
                if(v.Value.activeSelf)
                {
                    Debug.Log(v.Value.gameObject.name);
                    v.Value.SetActive(false);
                    Debug.Log(v.Value.activeSelf);
                }
            }
            changeEffect.evt.AddListener(() =>
            {
                currentMap = mapSODict[mapName];
                Debug.Log(currentMap.mapName);
                Debug.Log(mapName);
                backGround.sprite = currentMap.mapSprite;
                Debug.Log(currentMap.enterEvt);
                currentMap.enterEvt?.Invoke();
                currentMap.enterEvt = new UnityEvent();
                foreach (GameObject obj in currentMap.testaments)
                {
                    if (testamentsDictionary.ContainsKey(obj.name))
                    {
                        testamentsDictionary[obj.name].SetActive(true);
                    }
                    else
                    {
                        GameObject testa = Instantiate(obj, testamentParent);
                        testa.name = obj.name;
                        testa.transform.localScale = obj.transform.localScale;
                        testamentsDictionary.Add(obj.name, testa);
                        testa.GetComponent<TestamentItem>().Initialize();
                    }
                }
                placedCharacter.Load();
            });
                changeEffect.OnEffect();
        }
        SetMapScreen(false);
    }
    public void SetMapScreen(bool val)
    {
        mapPanel.SetActive(val);
        foreach(var v in mapButtons)
        {
            GameObject obj = v.Value.gameObject;
            if(!connectMapsDictionary.ContainsKey(currentMap.mapName))
            {
                connectMapsDictionary.Add(currentMap.mapName, new List<string>());
            }
            if(connectMapsDictionary[currentMap.mapName].Contains(obj.GetComponentInChildren<TextMeshProUGUI>().text) )
            {
                obj.gameObject.SetActive(true);
            }else
            {
                obj.gameObject.SetActive(false);
            }
        }
        previewImage.sprite = currentMap.mapSprite;
        previewText.text = currentMap.mapName;
    }
    public void LockMap(string targetMap, string target2Map)
    {
        if(maps.ContainsKey(targetMap))
        {
            if(connectMapsDictionary.ContainsKey(targetMap))
            {
                if (connectMapsDictionary[targetMap].Contains(target2Map))
                {
                    connectMapsDictionary[targetMap].Remove(target2Map);
                }
            }
        }
    }
}
