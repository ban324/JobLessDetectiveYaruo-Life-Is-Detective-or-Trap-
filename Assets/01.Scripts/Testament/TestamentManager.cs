using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestamentManager : MonoBehaviour
{

    public List<TestamentSO> testamentSOList;
    public Dictionary<string, TestamentSO> testaments;
    public static TestamentManager instance;
    private void Awake()
    {
        instance = this;
        testaments = new Dictionary<string, TestamentSO>();
        foreach (var c in testamentSOList)
        {
            testaments.Add(c.idx, c);
        }
    }
    public TestamentSO GetItem(string key)
    {
        TestamentSO ans = null;
        if(testaments.ContainsKey(key))
        {
            ans = testaments[key];
        }
        return ans; 
    }
}
