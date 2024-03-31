    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class MapSO : ScriptableObject
{
    public string mapName;
    public Sprite mapSprite;    
    public List<GameObject> testaments;
    //public Dictionary<GameObject> testaments
    public UnityEvent enterEvt;
}
