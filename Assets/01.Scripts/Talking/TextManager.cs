using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public enum TalkState
{
    none,
    onTalk,
    waitTalk,
}
public class TextManager : MonoBehaviour
{
    public int id = 0;
    public int idx = 0;

    public static TextManager instance;

    public TextMeshProUGUI nameBox;
    public TextMeshProUGUI textBox;
    public TalkState state;
    public CommentSO currentComment;
    public int commentIdx;
    public void Awake()
    {
        instance = this;
        state = TalkState.none;
    }

    public void StartTalk(CommentData comment)
    {
        state = TalkState.onTalk;
        StartCoroutine(TalkCoroutine(comment));
    }
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            state = TalkState.waitTalk;
            textBox.text = currentComment.texts[commentIdx].value;
            StartTalk(currentComment.texts[++commentIdx]);

        }
    }
    IEnumerator TalkCoroutine(CommentData comment)
    {
        nameBox.text = comment.name != "-" ?comment.name : string.Empty;
        textBox.text = string.Empty;
        int idx = 0;
        while (idx < comment.value.Length)
        {
            textBox.text += comment.value[idx++];
            switch (comment.value[idx])
            {
                case ' ':
                    yield return new WaitForSeconds(0.1f);
                    break;
                case '.':
                    yield return new WaitForSeconds(0.05f);
                        break;
                case '"':
                    break;
                default:
                    yield return new WaitForSeconds(0.2f);
                    break;
            }
        }
    }
}
