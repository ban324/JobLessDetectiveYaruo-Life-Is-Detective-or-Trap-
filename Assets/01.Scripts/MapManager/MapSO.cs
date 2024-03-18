    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MapSO : ScriptableObject
{
    public string mapName;
    public Sprite mapSprite;    
    public List<GameObject> testaments;
}
