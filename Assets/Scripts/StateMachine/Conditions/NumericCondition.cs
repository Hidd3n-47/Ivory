using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine;

public enum Comparison
{
    Equal,
    NotEqual,
    GreaterThan,
    GreaterThanOrEqual,
    LessThan,
    LessThanOrEqual
}

[Serializable]
public class NumericCondition : ICondition
{
    [SerializeField]            private MonoBehaviour _target;
    [SerializeField,
     MemberDropdown("_target")] private string        _memberName;
    [SerializeField]            private Comparison    _comparison;
    [SerializeField]            private float         _value;

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

        float actual;

        try
        {
            actual = Convert.ToSingle(value);
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
                return actual > _value;
            case Comparison.GreaterThanOrEqual:
                return actual >= _value;
            case Comparison.LessThan:
                return actual < _value;
            case Comparison.LessThanOrEqual:
                return actual <= _value;
            default:
                return false;
        }
    }
}
