using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
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
    public TMP_Text textBox;
    public Image talkCG;
    public TalkState state;
    public CommentSO currentComment;
    public int commentIdx;

    private bool isTestament;
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

    public EventData GetCurrentEvent()
    {
        return currentComment.texts[commentIdx].evt;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (!InspectionManager.instance.isOnCapture)
            {
                StopAllCoroutines();
                if (state == TalkState.onTalk)
                {
                    state = TalkState.waitTalk;
                    textBox.text = string.Empty;        
                    foreach (string s in currentComment.texts[commentIdx].value.Split('^'))
                    {
                        textBox.text += s;
                    }
                }
                else if (currentComment && state == TalkState.waitTalk && commentIdx < currentComment.texts.Count - 1)
                {
                    StartTalk(currentComment.texts[++commentIdx]);
                }
                else if (currentComment && state == TalkState.waitTalk && commentIdx >= currentComment.texts.Count - 1)
                {
                    state = TalkState.none;
                    SetBoxActive(false);
                }

            }else
            {
                if (currentComment && state == TalkState.waitTalk && commentIdx < currentComment.texts.Count - 1)
                {
                    StopAllCoroutines();
                    StartTalk(currentComment.texts[++commentIdx]);
                }
                else if (currentComment && state == TalkState.waitTalk && commentIdx >= currentComment.texts.Count - 1)
                {
                    StopAllCoroutines();
                    state = TalkState.none;
                    SetBoxActive(false);
                }
            }


        }
    }

    public void TryOpenTalk(CommentSO comment)
    {
        StopAllCoroutines();
        currentComment = comment;
        SetBoxActive(true);
        commentIdx = 0;
        StartTalk(comment.texts[commentIdx]);
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
        isTestament = false;
        int idx = 0;
        switch (comment.evt.evtType)
        {
            case TalkEventType.ImageSet:

                break;
            case TalkEventType.GetItem:
                break;
            default:
                break;
        }
        while (idx < comment.value.Length)
        {
            if (comment.value[idx] == '^')
            {
                if(InspectionManager.instance.isOnCapture)
                {
                    if(!isTestament)
                    {
                        textBox.text += $"<link={comment.evt}><color=#{UnityEngine.ColorUtility.ToHtmlStringRGB(Color.red)}>";
                        isTestament = true;
                        

                    }else
                    {
                        textBox.text += "</color></link>";
                        isTestament = false;
                    }
                }
                idx++;
                continue;

            }
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
