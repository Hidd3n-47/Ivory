using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(MemberDropdownAttribute))]
public class MemberDropdownDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        var attribute = (MemberDropdownAttribute)this.attribute;

        // Find the parent object containing this field.
        string path    = property.propertyPath;
        int    lastDot = path.LastIndexOf('.');

        string parentPath = lastDot >= 0 ? path.Substring(0, lastDot) : "";

        string targetPath = string.IsNullOrEmpty(parentPath)
                          ? attribute.TargetFieldName
                          : parentPath + "." + attribute.TargetFieldName;

        var targetProperty =  property.serializedObject.FindProperty(targetPath);

        var target = targetProperty?.objectReferenceValue as MonoBehaviour;

        if (target == null)
        {
            EditorGUI.PropertyField(position, property, label);
            EditorGUI.EndProperty();
            return;
        }

        var members = target.GetType()
            .GetMembers(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
            .OrderBy(m => m.Name)
            .ToArray();

        var options = new[] { "(None)" }
            .Concat(members.Select(m => m.Name))
            .ToArray();

        int selected = Array.FindIndex(members, m => m.Name == property.stringValue) + 1;

        EditorGUI.BeginChangeCheck();

        int choice = EditorGUI.Popup(position, label.text, selected, options);

        if (EditorGUI.EndChangeCheck())
        {
            // Since first item in the names options is "(None)" set it to empty string, i.e. no member variable.
            property.stringValue = choice == 0 ? "" : members[choice - 1].Name;
        }

        EditorGUI.EndProperty();
    }
}