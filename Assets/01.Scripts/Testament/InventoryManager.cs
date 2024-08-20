using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Sequence = DG.Tweening.Sequence;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventoryPanel;

    public RectTransform iconParent;

    public TestamentIcon testamentIconPrefab;

    public static InventoryManager  instance;
    public List<TestamentSO> testaments;
    public List<TestamentIcon> testamentsIcons;

    public TextMeshProUGUI testaNameBox;
    public TextMeshProUGUI testaDescBox;
    public bool isIconMoving;
    public int displayIdx = 0;

    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if(TextManager.instance.IsClearForInventory())
            {

                if (inventoryPanel.activeSelf)
                {
                    DisableInventory();
                }
                else
                {

                    EnableInventory();
                }
            }
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (inventoryPanel.activeSelf)
            {
                DisableInventory();
            }

        }
        if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)) && !isIconMoving)
        {
            if(inventoryPanel.activeSelf)
            {
                Debug.Log(!(displayIdx + (int)(Input.GetAxisRaw("Horizontal")) == -1 && Input.GetKeyDown(KeyCode.LeftArrow)));
                Debug.Log(!(displayIdx + (int)(Input.GetAxisRaw("Horizontal")) == testaments.Count  && Input.GetKeyDown(KeyCode.RightArrow)));
                if(!(displayIdx + (int)(Input.GetAxisRaw("Horizontal")) == -1 &&Input.GetKeyDown(KeyCode.LeftArrow)) && !( displayIdx + (int)(Input.GetAxisRaw("Horizontal")) == testaments.Count && Input.GetKeyDown(KeyCode.RightArrow)))
                {
                    displayIdx = Mathf.Clamp(displayIdx + (int)(Input.GetAxisRaw("Horizontal")), 0, testaments.Count - 1);

                    SlideInventory();

                }
            }
        }
        if(inventoryPanel.activeSelf && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("판넬 : " + inventoryPanel.activeSelf + " 스테이트 : " + (TextManager.instance.state != TalkState.none).ToString());
            if ( TextManager.instance.state != TalkState.none)
            {
                SlideInventory();
                Debug.Log(" 이벤트 타입 : " + (TextManager.instance.GetCurrentEvent().evtType == TalkEventType.Proposal).ToString() + " 아이디 일치");
                if (TextManager.instance.GetCurrentEvent().evtType == TalkEventType.Proposal && testaments[displayIdx].idx == TextManager.instance.GetCurrentEvent().target1Key)
                {
                    testamentsIcons[displayIdx].evt.AddListener(() =>
                    {
                        testamentsIcons[displayIdx].ReturnOriginalScale();
                        TextManager.instance.TryOpenTalk(CommentDatabase.instance.GetComment(TextManager.instance.GetCurrentEvent().target2Key));
                        DisableInventory();
                        testamentsIcons[displayIdx].evt = new UnityEngine.Events.UnityEvent();
                    });
                    testamentsIcons[displayIdx].Proposal();
                }else
                {
                    testamentsIcons[displayIdx].evt.AddListener(() =>
                    {
                        testamentsIcons[displayIdx].ReturnOriginalScale();
                        TextManager.instance.TryOpenTalk(CommentDatabase.instance.GetComment(TextManager.instance.GetCurrentEvent().target3Key));
                        DisableInventory();
                        testamentsIcons[displayIdx].evt = new UnityEngine.Events.UnityEvent();
                    });
                    testamentsIcons[displayIdx].Proposal();

                }
            }
        }
    }
    public void DisableInventory()
    {
        inventoryPanel.SetActive(false);

    }
    public void AddTestament(TestamentSO testament)
    {
        testaments.Add(testament);
        TestamentIcon icon = Instantiate(testamentIconPrefab, iconParent);
        icon.gameObject.SetActive(false);
        testamentsIcons.Add(icon);
        icon.Initiate(testament.imageSprite);
    }
    public bool IsAlreadyGetted(string key)
    {
        foreach(var v in testaments)
        {
            if(v.idx == key)
            {
                return true;
            }
        }
        return false;
    }
    public void EnableInventory()
    {
        inventoryPanel.SetActive(true);
        SlideInventory();
    }
    public void SlideInventory()
    {
        isIconMoving = true;
        DG.Tweening.Sequence leftSeq = DOTween.Sequence();
        DG.Tweening.Sequence rightSeq = DOTween.Sequence();
        DG.Tweening.Sequence centerSeq = DOTween.Sequence();
        DG.Tweening.Sequence activeSeq = DOTween.Sequence();
        string testamentName ="";
        string testamentDesc ="";
        for(int i = 0; i < testamentsIcons.Count; i++)
        {
            RectTransform testa = testamentsIcons[i].GetComponent<RectTransform>();
            if(i == displayIdx - 1)
            {
                testa.GetComponent<TestamentIcon>().image.GetComponent<RectTransform>().localPosition = testamentIconPrefab.image.GetComponent<RectTransform>().localPosition;
                leftSeq.AppendCallback(() =>
                {
                    testa.gameObject.SetActive(true);
                }).Append(testa.DOSizeDelta(new Vector2(300, 300), 0.3f)).Join(testa.GetComponent<TestamentIcon>().image.GetComponent<RectTransform>().DOSizeDelta(new Vector2(200, 200), 0.2f)).Join(testa.GetComponent<TestamentIcon>().image.GetComponent<RectTransform>().DOAnchorPos(testa.GetComponent<TestamentIcon>().image.GetComponent<RectTransform>().anchoredPosition + Vector2.down * 20, 0.3f)).Join(testa.DOAnchorPos(new Vector2(-575, 180), 0.3f)).Join(testa.GetComponent<TestamentIcon>().image.GetComponent<RectTransform>().DOSizeDelta(new Vector2(200, 200), 0.2f)).Join(testa.GetComponent<TestamentIcon>().image.GetComponent<RectTransform>().DOAnchorPos(testa.GetComponent<TestamentIcon>().image.GetComponent<RectTransform>().anchoredPosition + Vector2.down * 20, 0.3f));

            }
            else if(i == displayIdx + 1)
            {

                testa.GetComponent<TestamentIcon>().image.GetComponent<RectTransform>().localPosition = testamentIconPrefab.image.GetComponent<RectTransform>().localPosition;
                rightSeq.AppendCallback(() =>
                {
                testa.GetComponent<TestamentIcon>().image.GetComponent<RectTransform>().localPosition = testamentIconPrefab.image.GetComponent<RectTransform>().localPosition;
                    testa.gameObject.SetActive(true);
                }).Append(testa.DOSizeDelta(new Vector2(300, 300), 0.3f)).Join(testa.GetComponent<TestamentIcon>().image.GetComponent<RectTransform>().DOSizeDelta(new Vector2(200, 200), 0.2f)).Join(testa.GetComponent<TestamentIcon>().image.GetComponent<RectTransform>().DOAnchorPos(testa.GetComponent<TestamentIcon>().image.GetComponent<RectTransform>().anchoredPosition + Vector2.down * 20, 0.3f)).Join(testa.DOAnchorPos(new Vector2(575, 180), 0.3f));

            }
            else if(i==displayIdx)
            {
                centerSeq.AppendCallback(() =>
                {
                    testa.gameObject.SetActive(true);
                }).Append(testa.DOAnchorPos(new Vector2(0, 180), 0.3f)).
                Join(testa.DOSizeDelta(testamentIconPrefab.GetComponent<RectTransform>().sizeDelta, 0.2f)).
                Join(testa.GetComponent<TestamentIcon>().image.GetComponent<RectTransform>().DOSizeDelta(testamentIconPrefab.GetComponent<TestamentIcon>().image.GetComponent<RectTransform>().sizeDelta, 0.2f)).
                Join(testa.GetComponent<TestamentIcon>().image.GetComponent<RectTransform>().DOAnchorPos(testamentIconPrefab.GetComponent<TestamentIcon>().image.GetComponent<RectTransform>().anchoredPosition + Vector2.up , 0.3f)).
                JoinCallback(() =>
                {
                    testa.GetComponent<TestamentIcon>().ReturnOriginalScale();
                    Debug.LogError("증거 설명!!!!!!!!!!!!!!!!1" + testaDescBox.text);
                    Debug.LogError("증거물 이름!!!!!!!!!!!!!!!!"+testaNameBox.text);
                    

                });
                testamentDesc = testaments[i].tDescription;
                testamentName = testaments[i].tName;
            }
            else
            {
                activeSeq.AppendCallback(() => {
                    testa.gameObject.SetActive(false);
                });
            }
        }

        Sequence seq = DOTween.Sequence();
        seq.AppendCallback(() =>
        {
            testaNameBox.text = string.Empty;
            testaDescBox.text = string.Empty;

        }).Append(leftSeq).Join(rightSeq).Join(centerSeq).Join(activeSeq).AppendCallback(() =>
        {
            isIconMoving = false;
            testaNameBox.text = testamentName;
            testaDescBox.text = testamentDesc;

        });
        seq.Restart();  
    }
    //public bool 
}
