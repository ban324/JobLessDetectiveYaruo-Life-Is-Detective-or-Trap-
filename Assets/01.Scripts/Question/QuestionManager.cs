using DG.Tweening;
using Febucci.UI.Actions;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour
{
    public List<QuestionSO> questionList;
    public Dictionary<string, QuestionSO> questionDic;
    public GameObject questionBtnPrefab;
    public static QuestionManager instance;
    public GameObject questionPanel;
    public Transform btnParent;

    private Sequence openSeq;
    private Sequence closeSeq;
    private List<GameObject> _questionBtns;

    private void Awake()
    {
        if (instance == null) instance = this;
        questionDic = new Dictionary<string, QuestionSO>();
        foreach(var v in questionList)
        {
            questionDic.Add(v.idx.ToString(), v);
        }    
        openSeq = DOTween.Sequence();
        closeSeq = DOTween.Sequence();
        openSeq.SetAutoKill(false).AppendCallback(() => { questionPanel.SetActive(true); }).Append(questionPanel.GetComponent<Image>().DOFade(1, 0.3f)).AppendCallback(() => { });
        closeSeq.SetAutoKill(false).AppendCallback(() => {  }).Append(questionPanel.GetComponent<Image>().DOFade(0, 0.05f)).AppendCallback(() => { questionPanel.SetActive(false); });
        questionPanel.SetActive(false);
    }

    private void Update()
    {
        //if(Input.GetKeyDown(KeyCode.U))
        //{
        //    Question("0");
        //}
    }
    public void Question(string questionKey)
    {
        if(!questionPanel.activeSelf)
        {
            if(!openSeq.IsPlaying())
            {
                openSeq.Restart();
                AskQuestion(questionKey);
            }    
        }else
        {
            if (!closeSeq.IsPlaying())
            {
                closeSeq.Restart();
            }
        }
    }
    public void AskQuestion(string questionKey)
    {
        Debug.Log(questionKey + " = " + questionDic.ContainsKey(questionKey));
        foreach(var c in questionDic)
        {
            Debug.Log(c.Key);
        }
        for(int i = 0; i < btnParent.childCount; i++)
        {
            Destroy(btnParent.GetChild(i).gameObject);
        }
        if(questionDic.ContainsKey(questionKey))
        {
            QuestionSO so = questionDic[questionKey];

            for(int i = 0; i < so.selects.Count; i++)
            {
                Button btn = Instantiate(questionBtnPrefab, btnParent).GetComponent<Button>();
                btn.GetComponentInChildren<TextMeshProUGUI>().text = so.selects[i];
                Debug.Log(so.selects[i]);
                if(i==so.rightidx)
                {
                    btn.onClick.AddListener(() =>
                    {
                        closeSeq.Restart();
                        TextManager.instance.TryOpenTalk(CommentDatabase.instance.GetComment(so.rightTogo));
                    });

                }else
                {

                    btn.onClick.AddListener(() =>
                    {
                        closeSeq.Restart();
                        TextManager.instance.TryOpenTalk(CommentDatabase.instance.GetComment(so.wrongTogo));
                    });
                }
            }
        }
    }
}
