using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class CommentData
{
    public string name;
    public string value;
    public int eventIdx;
}
[Serializable]

[CreateAssetMenu]
public class CommentSO : ScriptableObject
{
    CommentData data;
    public List<CommentData> texts;
    public int generalIdx;
    public int pointOutIdx= 0;
    public int destinationIdx;
}
