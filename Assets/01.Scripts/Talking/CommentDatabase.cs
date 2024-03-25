using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.Rendering;

[Serializable]
public class FlagCondition
{
    public string key;
    public bool flaged;
}


public class CommentDatabase : MonoBehaviour
{
    public static CommentDatabase instance;
    public List<CommentSO> commentList;
    public List<FlagSO> flagList;
    private Dictionary<string, CommentSO> commentDictionary;
    private Dictionary<string, FlagSO> flagDictionary;
    public Dictionary<string, FlagCondition> conditionDictionary;
    private void Awake()
    {
        instance = this;
        commentDictionary = new Dictionary<string, CommentSO>();
        conditionDictionary = new Dictionary<string, FlagCondition>();
        flagDictionary= new Dictionary<string, FlagSO>();
        foreach(CommentSO comment in commentList)
        {
            commentDictionary.Add(comment.name.Split("Comment")[1], comment);
            //Debug.Log(comment.name.Split("Comment")[1]);
        }
        foreach(FlagSO flag in flagList)
        {
            flagDictionary.Add(flag.key, flag);
            foreach(FlagCondition con in flag.conditions)
            {
                if (!conditionDictionary.ContainsKey(con.key))
                {
                    conditionDictionary.Add(con.key, con);
                }
            }
        }
        
    }

    public CommentSO GetComment(string key)
    {
        Debug.Log(key);
        foreach(var v in commentDictionary)
        {
            Debug.Log(v.Key + ": " + v.Value);
        }
        if(commentDictionary.ContainsKey(key))
        {
            Debug.Log("πË√‚");
            return commentDictionary[key];
        }
        return null;
    }
    public void CheckFlags()
    {
        Debug.Log("asedfsd");
        foreach(var flag in flagDictionary)
        {
            if(!flag.Value.isInvoked)
            {
                FlagSO so = flag.Value;
                if(so.IsConditionSuccessed())
                {
                    
                    so.isInvoked = true;

                }
            }
        }
    }
}
