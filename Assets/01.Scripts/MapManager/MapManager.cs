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
    public Transform mapButtonParent;
    public GameObject mapButtonPrefab;
    public Transform testamentParent;
    public PlacedCharacter placedCharacter;
    private void Awake()
    {
        instance = this;
        maps = new Dictionary<string, MapSO>();

        mapSODict = new Dictionary<string, MapSO>();

        testamentsDictionary = new Dictionary<string, GameObject>();
        foreach(var v in mapSOList)
        {
            mapSODict.Add(v.mapName, v);
        }
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(1) && TextManager.instance.state == TalkState.none)
        {
            if(mapPanel.activeSelf)
            {
                SetMapScreen(false);
            }else
            {
                SetMapScreen(true);
            }
        }
        if(Input.GetKeyDown(KeyCode.V))
        {
            foreach(var map in mapSOList)
            {
                OnMapUnlocked(map);
            }
        }
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
        if(maps.ContainsKey(key))
        {
            Debug.Log("¿Ã∫•∆Æ √ﬂ∞° º∫∞¯");
            maps[key].enterEvt.AddListener(action);
        }
    }
    public void OnMapUnlocked(MapSO map)
    {
        if (!map) return;
        if(!maps.ContainsKey(map.mapName))
        {
            Debug.Log("∏  √ﬂ∞°µ  : " + map.mapName);
            Debug.Log(map.mapName);
            maps.Add(map.mapName, map);
            GameObject btn = Instantiate(mapButtonPrefab, mapButtonParent);
            btn.GetComponent<MapButton>().Initialize();
            btn.GetComponentInChildren<TextMeshProUGUI>().text = map.mapName;
            btn.GetComponent<Button>().onClick.AddListener(() => {
                MapManager.instance.ChangeMap(map.mapName);
            });
            btn.GetComponent<MapButton>().onPointerEnterEvent.AddListener(() =>
            {
                MapManager.instance.previewImage.sprite = map.mapSprite;
            });

        }
    }

    public void PlaceCharacter(string imageKey, string commentKey)
    {
        placedCharacter.Initialize(imageKey, commentKey);
    }
    public void ChangeMap(string mapName)
    {
        if (maps.ContainsKey(mapName))
        {
            foreach(var v in testamentsDictionary)
            {
                if(v.Value.activeSelf)
                {
                    Debug.Log(v.Value.gameObject.name);
                    v.Value.SetActive(false);
                }
            }
            currentMap = maps[mapName];
            backGround.sprite = currentMap.mapSprite;
            Debug.Log(currentMap.enterEvt);
            currentMap.enterEvt?.Invoke();
            currentMap.enterEvt = new UnityEvent() ;
            foreach(GameObject obj in currentMap.testaments)
            {
                if(testamentsDictionary.ContainsKey(obj.name))
                {
                    testamentsDictionary[obj.name].SetActive(true);
                }else
                {
                    GameObject testa = Instantiate(obj,testamentParent);
                    testa.name = obj.name;
                    testa.transform.localScale = obj.transform.localScale;
                    testamentsDictionary.Add(obj.name, testa);
                    //testa.GetComponent<TestamentItem>().Initialize();
                }
            }
            placedCharacter.Load();
        }
        SetMapScreen(false);
    }
    public void SetMapScreen(bool val)
    {
        mapPanel.SetActive(val);
        previewImage.sprite = currentMap.mapSprite;
    }
}
