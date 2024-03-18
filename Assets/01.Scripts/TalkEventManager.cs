using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkEventManager : MonoBehaviour
{
    public static TalkEventManager ininstance;

    private void Awake()
    {
        
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
