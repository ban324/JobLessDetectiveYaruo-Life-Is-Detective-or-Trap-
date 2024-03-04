using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterSO : ScriptableObject
{
    public List<Sprite> sCG;
    public List<CommentSO> comments;
}

public class Character : MonoBehaviour
{
    public CharacterSO so;
    private void Awake()
    {
        
    }
}
