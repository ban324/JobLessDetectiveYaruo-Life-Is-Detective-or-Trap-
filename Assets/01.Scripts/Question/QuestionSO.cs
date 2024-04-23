using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class QuestionSO : ScriptableObject
{
    public int idx;
    public int rightidx;
    public List<string> selects;
    public string rightTogo;
    public string wrongTogo;
}
