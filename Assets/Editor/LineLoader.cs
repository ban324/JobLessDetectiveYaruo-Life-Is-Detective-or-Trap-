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
        AVariable = EditorGUILayout.TextField("input1", AVariable);
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
    public string documentID = "1L5z4yT3bdVGbAKWCEoxPfjc1jvIhkJjvtgazdVP_gts";
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
        while(cnt < scriptCnt)
        {
            string[] info = strs[idx].Split(',');
            Debug.Log(strs[idx]);
            Debug.Log(info[0]);
            Debug.Log(info[1]);
            Debug.Log(info[2]);
            CommentSO commentSO = ScriptableObject.CreateInstance<CommentSO>();
            commentSO.generalIdx = int.Parse(info[0]);
            commentSO.pointOutIdx = info[1] == "-"? int.MaxValue - 1:int.Parse(info[1]);
            commentSO.destinationIdx = info[1] == "-" ? int.MaxValue -1 : int.Parse(info[2]);
            commentSO.texts = new List<CommentData>();
            idx++;
            while (idx < strs.Length&&strs[idx] != "---,,")
            {
                
                try
                {
                    Debug.Log(strs[idx]);

                    CommentData data = new CommentData();
                    string c = strs[idx].Split(',')[0];
                    if(c=="---")
                    {
                        idx++;
                        break;
                    }
                    data.name = c;
                    string v = strs[idx].Remove(0, strs[idx].IndexOf(',')+1);
                    Debug.Log(v[v.Length - 2]);
                    if (v[v.Length-2] != '-')
                    {
                        data.eventIdx = int.Parse(v[v.Length - 2    ].ToString());
                    }else
                    {
                        data.eventIdx = 0;
                    }
                    v = v.Remove(v.LastIndexOf(','), 3);
                    data.value = v;
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
            AssetDatabase.CreateAsset(commentSO, $"Assets/03.SO/Comment/Comment{commentSO.generalIdx}.asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log(cnt);
            cnt++;
        }
    }
}