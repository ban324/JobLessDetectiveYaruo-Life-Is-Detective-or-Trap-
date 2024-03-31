using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

public class FlagLoader : EditorWindow
{
    private string BVariable;

    [MenuItem("Tools/FlagLoader")]
    static void Init()
    {
        Debug.Log("dd");
        var curWindow = EditorWindow.GetWindow(typeof(FlagLoader));
        var cWindow = ((FlagLoader)curWindow);
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
            FlagDownloader loader = gObj.AddComponent<FlagDownloader>();
            loader.TextLoad(int.Parse(BVariable)+1);
            //
        }

    }

}

public class FlagDownloader : MonoBehaviour
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
        string url = $"https://docs.google.com/spreadsheets/d/{documentID}/export?format=csv&gid=1940729237";

        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();
        Debug.Log(www.downloadHandler.text);
        string[] strs = www.downloadHandler.text.Split('\n');
        int cnt = 0;
        
        while (cnt < scriptCnt)
        {
            string[] info = strs[cnt].Split(',');
            FlagSO flag = ScriptableObject.CreateInstance<FlagSO>();
            flag.key = info[0];
            flag.conditions = new List<FlagCondition>();
            for(int i = 1; i< info.Length; i++)
            {
                if (info[i] == "-" || info[i][0] == '-') continue;
                FlagCondition cond = new FlagCondition();
                Debug.Log((int)(info[i][0]));
                cond.key = info[i];
                cond.flaged = false;
                flag.conditions.Add(cond);
            }
            string path = Application.dataPath;
            path += "/03.SO/Flags/";
            AssetDatabase.CreateAsset(flag, $"Assets/03.SO/Flags/Flag{flag.key}.asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            cnt++;
        }
        Debug.Log("성공적으로 성공");
    }
}