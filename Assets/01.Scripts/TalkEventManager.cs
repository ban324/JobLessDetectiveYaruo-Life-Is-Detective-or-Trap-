using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkEventManager : MonoBehaviour
{
    public static TalkEventManager instance;

    private void Awake()
    {
        instance = this;
    }
    public void InvokeEventToKey(string key)
    {
        switch (key)
        {
            
            default:
                TextManager.instance.TryOpenTalk(CommentDatabase.instance.GetComment(key));
                break;
        }
    }
}
