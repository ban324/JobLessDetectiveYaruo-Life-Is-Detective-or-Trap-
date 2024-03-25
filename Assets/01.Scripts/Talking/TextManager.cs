using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
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
    public void PassText(bool isWaited)
    {
        Debug.Log(textBox.text);
        Debug.Log(commentIdx);
        StopAllCoroutines();
        //state = commentIdx + 1 < currentComment.texts.Count ? TalkState.waitTalk : TalkState.none;
        state = TalkState.waitTalk;
        if (!isWaited)
        {
            textBox.text = string.Empty;

            foreach (string s in currentComment.texts[commentIdx].value.Split('^'))
            {
                textBox.text += s;
            }
        }
        if (GetCurrentEvent().evtType == TalkEventType.GetItem && !InventoryManager.instance.IsAlreadyGetted(GetCurrentEvent().target1Key))
        {
            
            InventoryManager.instance.AddTestament(TestamentManager.instance.GetItem(GetCurrentEvent().target1Key));

        }
        if(commentIdx +1 == currentComment.texts.Count )
        {
            CommentDatabase.instance.CheckFlags();
        }
        if(commentIdx +1< currentComment.texts.Count && currentComment.texts[commentIdx+1].evt.evtType == TalkEventType.GetItem && InventoryManager.instance.IsAlreadyGetted(currentComment.texts[commentIdx + 1].evt.target1Key))
        {
            commentIdx++;
        }
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
                    PassText(false);
                }
                else if (currentComment && state == TalkState.waitTalk && commentIdx < currentComment.texts.Count - 1)
                {
                    StartTalk(currentComment.texts[++commentIdx]);
                }
                else if (currentComment && state == TalkState.waitTalk && commentIdx >= currentComment.texts.Count - 1)
                {
                    PopupManager.instance.ClosePopup();
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
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(state == TalkState.waitTalk || state == TalkState.onTalk)
            {
                Debug.Log("가능");
                if(GetCurrentEvent().evtType == TalkEventType.PointOut)
                {
                    Debug.Log("실행");
                    TryOpenTalk(CommentDatabase.instance.GetComment(GetCurrentEvent().target1Key));
                    InspectionManager.instance.isOnCapture = !InspectionManager.instance.isOnCapture;
                }
            }
        }
    }

    public void TryOpenTalk(CommentSO comment)
    {
        if(comment == null)
        {
            return;
        }
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
        switch (comment.evt.evtType)
        {
            case TalkEventType.ImageSet:
                PopupManager.instance.OpenItem(comment.evt.target1Key);
                break;
            case TalkEventType.GetItem:
                //InventoryManager.instance.AddTestament(TestamentManager.instance.GetItem(comment.evt.target1Key));
                if (InventoryManager.instance.IsAlreadyGetted(comment.evt.target1Key))
                {
                    Debug.Log("asddfasdfs");
                    //commentIdx++;
                    PassText(true);
                    yield return null;
                }
                    break;
            default:
                break;
        }
        Debug.Log("스위치 끝");
        nameBox.text = comment.name != "-" ?comment.name : string.Empty;
        textBox.text = string.Empty;
        isTestament = false;
        int idx = 0;
        while (idx < comment.value.Length)
        {
            //Debug.Log(comment.value);
            if (comment.value[idx] == '^')
            {
                if(InspectionManager.instance.isOnCapture)
                {
                    if(!isTestament)
                    {
                        textBox.text += $"<color=#{UnityEngine.ColorUtility.ToHtmlStringRGB(Color.red)}>";
                        isTestament = true;
                        

                    }else
                    {
                        textBox.text += "</color>";
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
        PassText(true);
    }

}
