using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TalkEventType
{
    Null,
    ImageSet,
    GetItem,
    PointOut,
    Proposal,
    MapMove,
    MapUnlock,
    Question
}

[Serializable]
public class EventData 
{
    public TalkEventType evtType;
    public string target1Key;
    public string target2Key;
    public string target3Key;

    public EventData(TalkEventType evtType, string target1, string target2 = null, string target3 = null)
    {
        this.evtType = evtType;
        this.target1Key = target1;
        this.target2Key = target2;
        this.target3Key = target3;
    }
}
