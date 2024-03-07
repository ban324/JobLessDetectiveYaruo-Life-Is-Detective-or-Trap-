using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum TalkState
{
    none,
    onTalk,
    waitTalk,
}
public class TextManager : MonoBehaviour
{

    public static TextManager instance;

    public List<SCGSO> cgs;
    public Dictionary<string, SCGSO> cgDictionary;

    public GameObject talkBox;
    public TextMeshProUGUI nameBox;
    public TextMeshProUGUI textBox;
    public Image talkCG;
    public TalkState state;
    public CommentSO currentComment;
    public int commentIdx;
    public void Awake()
    {
        instance = this;
        cgDictionary = new Dictionary<string, SCGSO>();
        foreach(SCGSO s in cgs)
        {
            cgDictionary.Add(s.cName, s);
        }
    }

    public void StartTalk(CommentData comment)
    {
        talkBox.SetActive(true);
        state = TalkState.onTalk;
        StartCoroutine(TalkCoroutine(comment));
    }
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            StopAllCoroutines();
            if(state == TalkState.onTalk)
            {
                state = TalkState.waitTalk;
                textBox.text = currentComment.texts[commentIdx].value;
            }
            else if(currentComment && state == TalkState.waitTalk && commentIdx < currentComment.texts.Count-1)
            {
                StartTalk(currentComment.texts[++commentIdx]);
            }
            else if(currentComment && state == TalkState.waitTalk && commentIdx >= currentComment.texts.Count-1)
            {
                state = TalkState.none;
                SetBoxActive(false);
            }

        }
    }

    public void TryOpenTalk(CommentSO comment)
    {
        currentComment = comment;
        
    }
    public void SetBoxActive(bool v)
    {
        talkBox.SetActive(v);
    }
    public void OpenSCG(CommentData data)
    {
        if(cgDictionary.ContainsKey(data.name))
        {
            talkCG.gameObject.SetActive(true);
            talkCG.sprite = cgDictionary[data.name].sprites[0];
        }
        else
        {
            talkCG.gameObject.SetActive(false);
        }
    }
    IEnumerator TalkCoroutine(CommentData comment)
    {
        nameBox.text = comment.name != "-" ?comment.name : string.Empty;
        textBox.text = string.Empty;

        int idx = 0;
        while (idx < comment.value.Length)
        {
            textBox.text += comment.value[idx];
            OpenSCG(comment);
            switch (comment.value[idx])
            {
                case ' ':
                    yield return new WaitForSeconds(0.05f);
                    break;
                case '.':
                    yield return new WaitForSeconds(0.05f);
                        break;
                case '"':
                    break;
                default:
                    yield return new WaitForSeconds(0.1f);
                    break;
            }
            idx++;
        }

        state = commentIdx < currentComment.texts.Count ? TalkState.waitTalk : TalkState.none;
    }
}
