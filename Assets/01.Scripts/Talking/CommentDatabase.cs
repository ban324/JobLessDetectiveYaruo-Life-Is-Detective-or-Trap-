using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommentDatabase : MonoBehaviour
{
    public static CommentDatabase instance;
    public List<CommentSO> commentList;
    public Dictionary<string, CommentSO> commentDictionary;

    private void Awake()
    {
        instance = this;
        commentDictionary = new Dictionary<string, CommentSO>();
        foreach(CommentSO comment in commentList)
        {
            commentDictionary.Add(comment.name.Split("Comment")[1], comment);
            Debug.Log(comment.name.Split("Comment")[1]);
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
}
