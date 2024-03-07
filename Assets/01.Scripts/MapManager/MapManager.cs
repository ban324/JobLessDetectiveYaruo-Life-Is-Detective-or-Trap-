using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    public static MapManager instance;
    public List<MapSO> mapSOList;
    public Dictionary<string, MapSO> maps;
    public MapSO currentMap;
    public GameObject mapPanel;
    public Image backGround;
    public Transform mapButtonParent;
    public GameObject mapButtonPrefab;
    private void Awake()
    {
        instance = this;
        maps = new Dictionary<string, MapSO>();


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
    private void OnMapUnlocked(MapSO map)
    {
        maps.Add(map.mapName, map);
        GameObject btn = Instantiate(mapButtonPrefab, mapButtonParent);
        btn.GetComponentInChildren<TextMeshProUGUI>().text = map.mapName;
        btn.GetComponent<Button>().onClick.AddListener(()=>{
            MapManager.instance.ChangeMap(map.mapName);
        });
    }
    public void ChangeMap(string mapName)
    {
        Debug.Log(mapName);
        foreach(var map in maps)
        {
            Debug.Log(map.Key);
        }
        if (maps.ContainsKey(mapName))
        {
            currentMap = maps[mapName];
            backGround.sprite = currentMap.mapSprite;
        }
        SetMapScreen(false);
    }
    public void SetMapScreen(bool val)
    {
        mapPanel.SetActive(val);
    }
}
