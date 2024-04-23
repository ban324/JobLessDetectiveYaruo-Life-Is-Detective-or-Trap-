using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
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
        Debug.LogError(key);
        switch (key)
        {
            case "1":
                {
                    MapManager.instance.AddEnterEvent("사무소 앞", () =>
                    {
                        Debug.Log("1번 이벤트 실행");
                        TextManager.instance.TryOpenTalk(CommentDatabase.instance.GetComment("1"));
                    });
                        Debug.Log("1번 이벤트 추가");
                }
                break;
            case "2":
                {
                        TextManager.instance.TryOpenTalk(CommentDatabase.instance.GetComment("2"));
                }
                break;
            case "3":
                {
                        TextManager.instance.TryOpenTalk(CommentDatabase.instance.GetComment("3"));
                }
                break;
            case "4":
                {
                    MapManager.instance.OnMapUnlocked(MapManager.instance.GetMap("와장창 맨션 104호"));

                        TextManager.instance.TryOpenTalk(CommentDatabase.instance.GetComment("3"));
                    
                }
                break;
            case "6":
                {
                        TextManager.instance.TryOpenTalk(CommentDatabase.instance.GetComment("6"));
                    
                }
                break;
            case "8":
                {
                    Debug.LogError("8번 발동");
                    MapManager.instance.PlaceCharacter("야라나이오", "8");
                }
                break;
            case "8-1":
                {
                    Debug.LogError("8-1번 발동");
                    MapManager.instance.PlaceCharacter("야라나이오", "8");
                }
                break;
            case "9":
                {
                    Debug.LogError("9번 발동");
                    MapManager.instance.PlaceCharacter("야라나이오", "8");
                }
                break;
            case "10":
                {
                    Debug.LogError("10번 발동");
                    MapManager.instance.PlaceCharacter("야라나이오", "10");
                }
                break;
            case "11":
                {
                    MapManager.instance.PlaceCharacter("야라나이오", "10");
                }
                break;
            case "12":
                {
                    MapManager.instance.PlaceCharacter("야라나이오", "12");
                }
                break;
            case "13":
                {
                    MapManager.instance.PlaceCharacter("야라나이오", "12");
                }
                break;
            case "15":
                {
                    MapManager.instance.AddEnterEvent("사무소", () =>
                    {
                        TextManager.instance.TryOpenTalk(CommentDatabase.instance.GetComment("15"));
                    });
                }
                break;
            default:
                //TextManager.instance.TryOpenTalk(CommentDatabase.instance.GetComment(key));
                break;
        }
    }

    public void InvokeRepeatingEventToKey(string key)
    {
        Debug.LogError(key);
        switch (key)
        {
            case "8":
                {
                    Debug.LogError("8번 발동");
                    MapManager.instance.PlaceCharacter("야라나이오", "8");
                }
                break;
            case "9":
                {
                    Debug.LogError("9번 발동");
                    MapManager.instance.PlaceCharacter("야라나이오", "8");
                }
                break;
            case "10":
                {
                    Debug.LogError("10번 발동");
                    MapManager.instance.PlaceCharacter("야라나이오", "10");
                }
                break;
            case "11":
                {
                    MapManager.instance.PlaceCharacter("야라나이오", "10");
                }
                break;
            case "12":
                {
                    MapManager.instance.PlaceCharacter("야라나이오", "12");
                }
                break;
            case "13":
                {
                    MapManager.instance.PlaceCharacter("야라나이오", "12");
                }
                break;
            case "15":
                {
                    MapManager.instance.AddEnterEvent("사무소", () =>
                    {
                        TextManager.instance.TryOpenTalk(CommentDatabase.instance.GetComment("15"));
                    });
                }
                break;
            default:
                //TextManager.instance.TryOpenTalk(CommentDatabase.instance.GetComment(key));
                break;
        }

    }
}
