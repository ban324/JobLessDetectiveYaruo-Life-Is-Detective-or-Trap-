using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class FlagSO : ScriptableObject
{
    public string key;
    public List<FlagCondition> conditions;
    public bool isInvoked;
    public bool IsConditionSuccessed()
    {
        foreach (var condition in conditions)
        {
            if (condition.flaged == false)
            {
                return false;
            }
        }
        return true;
    }
}