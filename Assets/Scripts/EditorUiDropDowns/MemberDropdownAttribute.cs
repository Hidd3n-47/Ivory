using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field)]
public class MemberDropdownAttribute : PropertyAttribute
{
    public string TargetFieldName { get; }

    public MemberDropdownAttribute(string targetFieldName)
    {
        TargetFieldName = targetFieldName;
    }
}
