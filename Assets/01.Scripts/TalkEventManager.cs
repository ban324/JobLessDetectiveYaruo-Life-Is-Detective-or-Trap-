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
                    MapManager.instance.AddEnterEvent("�繫�� ��", () =>
                    {
                        Debug.Log("1�� �̺�Ʈ ����");
                        TextManager.instance.TryOpenTalk(CommentDatabase.instance.GetComment("1"));
                    });
                        Debug.Log("1�� �̺�Ʈ �߰�");
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
                    MapManager.instance.OnMapUnlocked(MapManager.instance.GetMap("����â �Ǽ� 104ȣ"));

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
                    Debug.LogError("8�� �ߵ�");
                    MapManager.instance.PlaceCharacter("�߶��̿�", "8");
                }
                break;
            case "8-1":
                {
                    Debug.LogError("8-1�� �ߵ�");
                    MapManager.instance.PlaceCharacter("�߶��̿�", "8");
                }
                break;
            case "9":
                {
                    Debug.LogError("9�� �ߵ�");
                    MapManager.instance.PlaceCharacter("�߶��̿�", "8");
                }
                break;
            case "10":
                {
                    Debug.LogError("10�� �ߵ�");
                    MapManager.instance.PlaceCharacter("�߶��̿�", "10");
                }
                break;
            case "11":
                {
                    MapManager.instance.PlaceCharacter("�߶��̿�", "10");
                }
                break;
            case "12":
                {
                    MapManager.instance.PlaceCharacter("�߶��̿�", "12");
                }
                break;
            case "13":
                {
                    MapManager.instance.PlaceCharacter("�߶��̿�", "12");
                }
                break;
            case "15":
                {
                    MapManager.instance.AddEnterEvent("�繫��", () =>
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
                    Debug.LogError("8�� �ߵ�");
                    MapManager.instance.PlaceCharacter("�߶��̿�", "8");
                }
                break;
            case "9":
                {
                    Debug.LogError("9�� �ߵ�");
                    MapManager.instance.PlaceCharacter("�߶��̿�", "8");
                }
                break;
            case "10":
                {
                    Debug.LogError("10�� �ߵ�");
                    MapManager.instance.PlaceCharacter("�߶��̿�", "10");
                }
                break;
            case "11":
                {
                    MapManager.instance.PlaceCharacter("�߶��̿�", "10");
                }
                break;
            case "12":
                {
                    MapManager.instance.PlaceCharacter("�߶��̿�", "12");
                }
                break;
            case "13":
                {
                    MapManager.instance.PlaceCharacter("�߶��̿�", "12");
                }
                break;
            case "15":
                {
                    MapManager.instance.AddEnterEvent("�繫��", () =>
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
