using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;


[Serializable]
public class CommentData
{
    public string name;
    public string value;
    public EventData evt;
}
[Serializable]

[CreateAssetMenu]
public class CommentSO : ScriptableObject
{
    CommentData data;
    public List<CommentData> texts;
    public string keyValue;
    public string wrongPointoutIdx;
    public string wrongProposalIdx;
}
