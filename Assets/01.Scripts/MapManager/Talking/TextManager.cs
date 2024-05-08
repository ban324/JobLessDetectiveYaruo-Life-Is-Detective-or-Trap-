using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum TalkState
{
    none,
    onTalk,
    waitTalk,
    question
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
    public ImpactEffect impact;
    public BlurUIEffect blur;
    public Color pointTextColor;
    [SerializeField]private string beforeCGKey;
    private string radiusKey;
    private bool isTestament;
    public Material blurMat;
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
            string[] ss = textBox.text.Split('"');
            textBox.text = string.Empty;
            foreach (string s in ss)
            {
                textBox.text += s;
            }
            bool isStar = false;
            string str = textBox.text;
            textBox.text = string.Empty;
            foreach(char c in str)
            {
                if(c == '*')
                {
                    if(!isStar)
                    {
                        textBox.text += $"<color=#{UnityEngine.ColorUtility.ToHtmlStringRGB(pointTextColor)}>";
                    isStar = !isStar;
                    }else
                    {
                        textBox.text +=c + "</color>";
                    isStar = !isStar;
                        continue;
                    }
                }
                textBox.text += c;
            }
        }

        if (GetCurrentEvent().evtType == TalkEventType.GetItem && !InventoryManager.instance.IsAlreadyGetted(GetCurrentEvent().target1Key))
        {
            
            InventoryManager.instance.AddTestament(TestamentManager.instance.GetItem(GetCurrentEvent().target1Key));

        }
        if (GetCurrentEvent().evtType == TalkEventType.MapMove)
        {
            //if (!MapManager.instance.maps.ContainsKey(GetCurrentEvent().target1Key))
            //{
            //    Debug.Log(GetCurrentEvent().target1Key);
            //    MapManager.instance.OnMapUnlocked(MapManager.instance.GetMap(GetCurrentEvent().target1Key));
            //}
            MapManager.instance.ChangeMap(GetCurrentEvent().target1Key);
        }
        if (GetCurrentEvent().evtType == TalkEventType.MapUnlock)
        {
            if (!MapManager.instance.maps.ContainsKey(GetCurrentEvent().target1Key))
            {
                Debug.Log(GetCurrentEvent().target1Key);
                MapManager.instance.OnMapUnlocked(MapManager.instance.GetMap(GetCurrentEvent().target1Key));
            }
        }
        if(GetCurrentEvent().evtType == TalkEventType.Question)
        {
            QuestionManager.instance.Question(GetCurrentEvent().target1Key);
        }
        if(commentIdx +1 == currentComment.texts.Count )
        {
            Debug.Log(currentComment.keyValue);
            talkCG.gameObject.SetActive(false);
            CommentDatabase.instance.SetFlag(currentComment.keyValue);
            CommentDatabase.instance.CheckFlags();
            MapManager.instance.placedCharacter.Load();
        }
        if (commentIdx +1< currentComment.texts.Count && currentComment.texts[commentIdx+1].evt.evtType == TalkEventType.GetItem && InventoryManager.instance.IsAlreadyGetted(currentComment.texts[commentIdx + 1].evt.target1Key))
        {
            commentIdx++;
        }
    }
    private void Start()
    {
        TryOpenTalk(CommentDatabase.instance.GetComment("0"));
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
                    blur.Play(false);
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
                    PopupManager.instance.ClosePopup();
                    state = TalkState.none;
                    blur.Play(false);
                    SetBoxActive(false);
                }
            }


        }
    }

    public void TryOpenTalk(CommentSO comment)
    {
        MapManager.instance.PlaceCharacter("-", "-");
        if (comment == null)
        {
            return;
        }
        StopAllCoroutines();
        currentComment = comment;
        SetBoxActive(true);
        PopupManager.instance.ClosePopup();
        commentIdx = 0;
        blur.Play(true);
        StartTalk(comment.texts[commentIdx]);
    }
    public void SetBoxActive(bool v)
    {
        DG.Tweening.Sequence seq = DOTween.Sequence();
        if(v)
        {
            var vec = Camera.main.WorldToViewportPoint(Vector2.zero);
            seq.Append(talkBox.transform.DOMove(new Vector3(0,0,talkBox.transform.position.z), 0.4f));

        }
        else
        {
            Debug.Log("fal  ");
            PopupManager.instance.ClosePopup();

            var vec = Camera.main.WorldToViewportPoint(Vector2.zero);
            Debug.Log(vec);
            seq.Append(talkBox.transform.DOMove(new Vector3(0, -10, talkBox.transform.position.z), 0.4f));

        }
        seq.Restart();
    }
    public void OpenSCG(CommentData data)
    {
        
        if (cgDictionary.ContainsKey(data.name))
        {
            beforeCGKey = data.name;
            talkCG.gameObject.SetActive(true);
            talkCG.sprite = cgDictionary[data.name].sprites[0];
        }
        else if (data.name != "-" && data.name != "야루오")
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
                    //Debug.Log("asddfasdfs");
                    //commentIdx++;
                    PassText(true);
                    yield return null;
                }
                    break;
            default:
                break;
        }
            //impact.OnEffect();
        //Debug.Log("스위치 끝");
        nameBox.text = comment.name != "-" ?comment.name : string.Empty;
        if(nameBox.text.IndexOf("*") != -1)
        {
            nameBox.text = nameBox.text.Remove(nameBox.text.IndexOf("*"));
        }
        textBox.text = string.Empty;
        isTestament = false;
        int idx = 0;
        bool isblue = false;    
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
            if (comment.value[idx] == '*')
            {
                if(!isblue)
                {
                    textBox.text += $"<color=#{UnityEngine.ColorUtility.ToHtmlStringRGB(pointTextColor)}>";

                }else
                {
                    textBox.text += "</color>";
                }
            }
            if (comment.value[idx] == '"')
            {
                idx++;
                continue; 
            }
            textBox.text += comment.value[idx];
            OpenSCG(comment);
            if(!isblue)
            {
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

            }

            idx++;
        }
        PassText(true);
    }
    private void OnDestroy()
    {
        blurMat.SetFloat("_Radius", 1);

    }
}   
