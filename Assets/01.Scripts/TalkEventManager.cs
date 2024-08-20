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

                        TextManager.instance.TryOpenTalk(CommentDatabase.instance.GetComment("4"));
                    
                }
                break;
            case "6":
                {
                        //TextManager.instance.TryOpenTalk(CommentDatabase.instance.GetComment("6"));
                    
                }
                break;
            case "8":
                {
                    Debug.LogError("8�� �ߵ�");
                    MapManager.instance.PlaceCharacter("�߶��̿�", "8");
                }
                break;
            case "10":
                {
                    Debug.LogError("9�� �ߵ�");
                    MapManager.instance.PlaceCharacter("�߶��̿�", "8");
                }
                break;
            case "11":
                {
                    Debug.LogError("10�� �ߵ�");
                    MapManager.instance.PlaceCharacter("�߶��̿�", "11");
                }
                break;
            case "12":
                {
                    MapManager.instance.PlaceCharacter("�߶��̿�", "11");
                }
                break;
            case "13":
                {
                    MapManager.instance.PlaceCharacter("�߶��̿�", "13");
                }
                break;
            case "14":
                {
                    MapManager.instance.PlaceCharacter("�߶��̿�", "13");
                }
                break;
            case "15":
                {
                    MapManager.instance.AddEnterEvent("�繫��", () =>
                    {
                        TextManager.instance.TryOpenTalk(CommentDatabase.instance.GetComment("16"));
                    });
                }
                break;
            case "16":
                {
                    MapManager.instance.AddEnterEvent("���ѷα׳�", () =>
                    {
                        PopupManager.instance.ClosePopup();
                        TextManager.instance.state = TalkState.none;
                        TextManager.instance.blur.Play(false);
                        TextManager.instance.SetBoxActive(false);


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
        switch (key)
        {
            case "8":
                {
                    MapManager.instance.PlaceCharacter("�߶��̿�", "8");
                }
                break;
            case "9":
                {
                    MapManager.instance.PlaceCharacter("�߶��̿�", "8");
                }
                break;
            case "10":
                {
                    MapManager.instance.PlaceCharacter("�߶��̿�", "8");
                }
                break;
            case "11":
                {
                    MapManager.instance.PlaceCharacter("�߶��̿�", "11 ");
                }
                break;
            case "12":
                {
                    MapManager.instance.PlaceCharacter("�߶��̿�", "12");
                }
                break;
            case "13":
                {
                    MapManager.instance.PlaceCharacter("�߶��̿�", "13");
                }
                break;
            case "15":
                {
                    MapManager.instance.AddEnterEvent("�繫��", () =>
                    {
                        TextManager.instance.TryOpenTalk(CommentDatabase.instance.GetComment("16"));
                    });
                }
                break;
            case "16":
                {
                    MapManager.instance.ChangeMap("���ѷα׳�");
                }
                break;
            default:
                //TextManager.instance.TryOpenTalk(CommentDatabase.instance.GetComment(key));
                break;
        }

    }
}
