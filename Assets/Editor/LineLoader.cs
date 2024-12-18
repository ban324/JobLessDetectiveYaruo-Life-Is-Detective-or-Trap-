using Codice.CM.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

public class LineLoader : EditorWindow
{
    private string AVariable;
    private string BVariable;

    [MenuItem("Tools/LineLoader")]
    static void Init()
    {
        Debug.Log("dd");
        var curWindow = EditorWindow.GetWindow(typeof(LineLoader));
        var cWindow = ((LineLoader)curWindow);
        cWindow.Show();
    }

    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        
    }
    private void OnGUI()
    {
        BVariable = EditorGUILayout.TextField("input2", BVariable);
        if (GUILayout.Button("Extract"))
        {
            GameObject gObj = new GameObject();
            TextLoader loader = gObj.AddComponent<TextLoader>();
            loader.TextLoad(int.Parse(BVariable));
        }

    }

}

public class TextLoader : MonoBehaviour
{
    public string documentID = "1KYiUvCeLeeiT5Byq0_4ZAGyY1nNtqxIEWeZYBjr2rtQ";
    public void TextLoad(int pageId)
    {
        
        StartCoroutine(TextDownload(pageId));
    }
    IEnumerator TextDownload(int scriptCnt) 
    {
        Debug.Log("asdf");
        Debug.Log("File Downloading...");
        string url = $"https://docs.google.com/spreadsheets/d/{documentID}/export?format=csv&gid=0";

        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();
        Debug.Log(www.downloadHandler.text);
        string[] strs = www.downloadHandler.text.Split('\n');
        int idx = 0, cnt = 0;
        if(FindObjectOfType<CommentDatabase>())
        {
            FindObjectOfType<CommentDatabase>().commentList = new List<CommentSO> { };
        }
        while(cnt <= scriptCnt)
        {
            string[] info = strs[idx].Split(',');
            Debug.Log(strs[idx]);
            Debug.Log(info[0]);
            Debug.Log(info[1]);
            Debug.Log(info[2]);
            CommentSO commentSO = ScriptableObject.CreateInstance<CommentSO>();
            commentSO.keyValue = info[0];
            commentSO.wrongPointoutIdx = info[1] == "-"? "-":(info[1]);
            commentSO.wrongProposalIdx = info[2] == "-" ?  "-": (info[2]);
            commentSO.texts = new List<CommentData>();
            idx++;
            while (idx < strs.Length&&strs[idx] != "---,,,,,")
            {
                
                try
                {
                    Debug.Log(strs[idx]);

                    CommentData data = new CommentData();
                    string[] spl = strs[idx].Split(",");
                    string name = spl[0];
                    //Debug.Log(name);
                    for(int i = 0; i < spl.Length; i++)
                    {
                        while (spl[i].IndexOf('\r') != -1)
                        {
                            spl[i]=  spl[i].Remove(spl[i].LastIndexOf('\r'));
                        }
                    }
                    string t = strs[idx].Remove(0, strs[idx].IndexOf(',')+1);
                    t = t.Remove(t.LastIndexOf(','), t.Length - t.LastIndexOf(',')-1);
                    t = t.Remove(t.LastIndexOf(','), t.Length - t.LastIndexOf(',')-1);
                    t = t.Remove(t.LastIndexOf(','), t.Length - t.LastIndexOf(',')-1);
                    t = t.Remove(t.LastIndexOf(','), t.Length - t.LastIndexOf(',')-1);
                    string ev1 = spl[spl.Length - 4];
                    string ev2 = spl[spl.Length - 3];
                    string ev3 = spl[spl.Length - 2];
                    string ev4 = spl[spl.Length - 1];
                    
                    while (t.IndexOf('\"') != -1)
                    {
                        t = t.Remove(t.LastIndexOf("\""), 1);
                    }

                    string[] arr = { name, t ,ev1,ev2,ev3,ev4};
                    string c = arr[0];
                    if(c=="---")
                    {
                        idx++;
                        break;
                    }
                    data.name = c;
                    data.value = arr[1];
                    EventData evt = null;
                    switch (ev1)
                    {
                        case "0":
                            evt= new EventData(TalkEventType.ImageSet, ev2);
                        break;
                        case "1":
                            evt = new EventData(TalkEventType.GetItem, ev2);
                        break;
                        case "2":
                            evt = new EventData(TalkEventType.PointOut, ev2);
                        break;
                        case "3":
                            evt = new EventData(TalkEventType.Proposal, ev2,ev3,ev4);
                        break;
                        case "4":
                            evt = new EventData(TalkEventType.MapMove, ev2);
                            break;
                        case "5":
                            evt = new EventData(TalkEventType.MapUnlock, ev2, ev3);
                            break;
                        case "6":
                            evt = new EventData(TalkEventType.Question, ev2);
                            break;
                        default:
                            Debug.Log(ev1);
                            evt = new EventData(TalkEventType.Null, null);
                            break;
                    }
                    data.evt = evt;
                    Debug.Log(data.name);
                    Debug.Log(data.value);


                    commentSO.texts.Add(data);
                    
                    idx++;
                }
                catch(Exception exp)
                {
                    Debug.Log(exp.Message);
                    break;
                }
            }
            string path = Application.dataPath;
            path += "/03.SO/Comment/";
            AssetDatabase.CreateAsset(commentSO, $"Assets/03.SO/Comment/Comment{commentSO.keyValue}.asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            if(FindObjectOfType<CommentDatabase>())
            {
                FindObjectOfType<CommentDatabase>().commentList.Add(commentSO);
            }
            Debug.Log(cnt);
            cnt++;
        }
        Debug.Log("성공적으로 성공");
    }
}