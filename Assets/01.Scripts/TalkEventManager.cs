using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkEventManager : MonoBehaviour
{
    public static TalkEventManager instance;

    private void Awake()
    {
        instance = this;
    }
    public void InvokeEventToKey(string key)
    {
        //Debug.Log(key);
        switch (key)
        {
            case "1":
                {
                    MapManager.instance.AddEnterEvent("사무소 앞", () =>
                    {
                        //Debug.Log("1번 이벤트 실행");
                        TextManager.instance.TryOpenTalk(CommentDatabase.instance.GetComment("1"));
                    });
                }
                break;
            case "2":
                {
                    MapManager.instance.AddEnterEvent("공원", () =>
                    {
                        //Debug.Log("1번 이벤트 실행");
                        TextManager.instance.TryOpenTalk(CommentDatabase.instance.GetComment("2"));
                    });
                }
                break;
            case "6":
                {
                        TextManager.instance.TryOpenTalk(CommentDatabase.instance.GetComment("6"));
                    
                }
                break;
            case "7":
                {
                    MapManager.instance.PlaceCharacter("지나가던 여자", "7");

                }
                break;
            case "12":
                {
                    MapManager.instance.PlaceCharacter("-" , "-");
                    MapManager.instance.AddEnterEvent("사무소", () =>
                    {
                        MapManager.instance.PlaceCharacter("사무소장", "12");
                    });
                }
                break;
            default:
                //TextManager.instance.TryOpenTalk(CommentDatabase.instance.GetComment(key));
                break;
        }
    }
}
