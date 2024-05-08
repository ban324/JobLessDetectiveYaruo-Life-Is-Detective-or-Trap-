using System;
using System.Collections.Generic;
using UnityEngine;

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
            FlagSO copy = Instantiate(flag);
            flagDictionary.Add(copy.key, copy);
            for(int i = 0; i < copy.conditions.Count; i ++)
            {
                if (conditionDictionary.ContainsKey( copy.conditions[i].key))
                {
                    copy.conditions[i] = conditionDictionary[copy.conditions[i].key];
                }else
                {
                    FlagCondition cd = new FlagCondition();
                    foreach (char c in copy.conditions[i].key)
                    {
                        if(48 <= c && c <= 57 || c =='-')
                        {
                            cd.key += c;
                        }
                        Debug.LogError(cd.key);
                    }
                    cd.flaged = copy.conditions[i].flaged;
                    conditionDictionary.Add(cd.key, cd);
                    copy.conditions[i] = cd;
                    string e = string.Empty;
                    
                }
            }
        }
        
    }

    public CommentSO GetComment(string key)
    {
        Debug.Log(key);
        if(commentDictionary.ContainsKey(key))
        {
            return commentDictionary[key];
        }
        return null;
    }
    public void SetFlag(string key)
    {
        //Debug.Log(conditionDictionary.ContainsKey(key));
        //Debug.Log("플래그 세팅 시도");
        if (conditionDictionary.ContainsKey(key))
        {
            Debug.LogError("플래그 세팅 성공" + key);
            conditionDictionary[key].flaged = true;
        }
    }
    public void CheckFlags()
    {
        //Debug.Log("asedfsd");
        foreach(var flag in flagDictionary)
        {
            if(!flag.Value.isInvoked)
            {
                FlagSO so = flag.Value;
                if (so.IsConditionSuccessed())
                {
                    Debug.LogError("체크 성공" + so.key);
                    TalkEventManager.instance.InvokeEventToKey(so.key);
                    so.isInvoked = true;

                }
            }
            else
            {
                TalkEventManager.instance.InvokeRepeatingEventToKey(flag.Key);
            }
        }
    }
}
