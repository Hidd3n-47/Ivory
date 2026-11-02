using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine;


[Serializable]
public class BoolCondition : ICondition
{
    [SerializeField]            private MonoBehaviour _target;
    [SerializeField,
     MemberDropdown("_target")] private string        _memberName;
    [SerializeField]            private Comparison    _comparison;
    [SerializeField]            private bool          _value;

    public bool Evaluate()
    {
        if (_target == null || string.IsNullOrEmpty(_memberName))
        {
            return false;
        }

        const BindingFlags flags = BindingFlags.Instance | BindingFlags.Public;

        object value = null;
        Type   type  = _target.GetType();

        MethodInfo method = type.GetMethod(_memberName, flags, null, Type.EmptyTypes, null);

        if (method != null)
        {
            value = method.Invoke(_target, null);
        }
        else
        {
            var property = type.GetProperty(_memberName, flags);

            if (property != null && property.CanRead && property.GetIndexParameters().Length == 0)
            {
                value = property.GetValue(_target);
            }
            else
            {
                var field = type.GetField(_memberName, flags);

                if (field != null)
                {
                    value = field.GetValue(_target);
                }
            }
        }

        if (value == null)
        {
            return false;
        }

        bool actual;

        try
        {
            actual = Convert.ToBoolean(value);
        }
        catch (Exception)
        {
            return false;
        }

        switch (_comparison)
        {
            case Comparison.Equal:
                return actual == _value;
            case Comparison.NotEqual:
                return actual != _value;
            case Comparison.GreaterThan:
            case Comparison.GreaterThanOrEqual:
            case Comparison.LessThan:
            case Comparison.LessThanOrEqual:
            default:
                return false;
        }
    }
}
