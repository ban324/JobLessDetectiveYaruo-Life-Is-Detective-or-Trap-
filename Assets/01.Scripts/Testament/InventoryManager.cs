using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

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

    public int displayIdx = 0;

    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            AddTestament(AssetDatabase.LoadAssetAtPath<TestamentSO>("Assets/03.SO/Testament/Crayon.asset"));
            AddTestament(AssetDatabase.LoadAssetAtPath<TestamentSO>("Assets/03.SO/Testament/Assran.asset"));
        }
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if(inventoryPanel.activeSelf)
            {
                DisableInventory();
            }
            else
            {

                DisplayInventory();
            }
        }
        if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if(inventoryPanel.activeSelf)
            {
                displayIdx = Mathf.Clamp(displayIdx + (int)(Input.GetAxisRaw("Horizontal")), 0, testaments.Count-1);
                SlideInventory();
            }
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            if (inventoryPanel.activeSelf && TextManager.instance.state != TalkState.none || TextManager.instance.state != TalkState.waitTalk)
            {
                if (TextManager.instance.GetCurrentEvent().evtType == TalkEventType.Proposal && testaments[displayIdx].id == TextManager.instance.GetCurrentEvent().target1Key)
                {
                    DisableInventory();
                    TextManager.instance.TryOpenTalk(CommentDatabase.instance.GetComment(TextManager.instance.GetCurrentEvent().target2Key));
                }else
                {
                    DisableInventory();
                    TextManager.instance.TryOpenTalk(CommentDatabase.instance.GetComment(TextManager.instance.GetCurrentEvent().target3Key));

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

    public void DisplayInventory()
    {
        inventoryPanel.SetActive(true);
        SlideInventory();
    }

    public void SlideInventory()
    {
        for(int i = 0; i < testamentsIcons.Count; i++)
        {
            RectTransform testa = testamentsIcons[i].GetComponent<RectTransform>();
            if(i == displayIdx - 1)
            {
                testa.gameObject.SetActive(true);
                testa.sizeDelta = new Vector2(300, 300);
                testa.GetComponent<TestamentIcon>().image.GetComponent<RectTransform>().sizeDelta = new Vector2(300, 300);
                testa.anchoredPosition = new Vector2(-575, 180);
            }else if(i == displayIdx + 1)
            {

                testa.gameObject.SetActive(true);
                testa.sizeDelta = new Vector2(300, 300);
                testa.GetComponent<TestamentIcon>().image.GetComponent<RectTransform>().sizeDelta = new Vector2(300, 300);
                testa.anchoredPosition = new Vector2(575, 180);
            }
            else if(i==displayIdx)
            {
                testa.gameObject.SetActive(true);
                testa.anchoredPosition = new Vector2(0, 180);
                testa.sizeDelta = new Vector2(500, 500);
                testa.GetComponent<TestamentIcon>().image.GetComponent<RectTransform>().sizeDelta = new Vector2(500, 500);
                testaNameBox.text = testaments[i].tName;
                testaDescBox.text = testaments[i].tDescription;
            }
            else
            {
                testa.gameObject.SetActive(false);
            }
        }
    }
}