using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ConditionTypeSelectorAttribute))]
public class ConditionTypeSelectorDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        // Get all the classes that are from ICondition.
        // This will exclude any that cannot be instantiated (abstract/interface etc.)
        // And only include ones that are marked as serializable.
        var types = TypeCache.GetTypesDerivedFrom<ICondition>()
            .Where(t =>
                !t.IsAbstract                &&
                !t.IsInterface               &&
                !t.ContainsGenericParameters &&
                 t.IsSerializable)
            .OrderBy(t => t.Name)
            .ToArray();

        var current     = property.managedReferenceValue;
        var currentType = current?.GetType();

        var names = new[] { "(None)" }
            .Concat(types.Select(t => t.Name))
            .ToArray();

        int selected = currentType == null ? 0 : Array.FindIndex(types, t => t == currentType) + 1;

        var dropdownRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);

        EditorGUI.BeginChangeCheck();

        int choice = EditorGUI.Popup(dropdownRect, label.text, selected, names);

        if (EditorGUI.EndChangeCheck())
        {
            // Since first item in the names options is "(None)" set it to null if this option was selected.
            property.managedReferenceValue = choice == 0 ? null : Activator.CreateInstance(types[choice - 1]);

            property.serializedObject.ApplyModifiedProperties();
        }

        if (property.managedReferenceValue != null)
        {
            EditorGUI.indentLevel++;

            var   iterator      = property.Copy();
            var   end           = iterator.GetEndProperty();
            bool  enterChildren = true;
            float y             = dropdownRect.yMax + 2f;

            while (iterator.NextVisible(enterChildren) && !SerializedProperty.EqualContents(iterator, end))
            {
                enterChildren = false;

                float height = EditorGUI.GetPropertyHeight(iterator, true);

                var rect = new Rect(position.x, y, position.width, height);

                EditorGUI.PropertyField(rect, iterator, true);
                y += height + EditorGUIUtility.standardVerticalSpacing;
            }

            EditorGUI.indentLevel--;
        }

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float height = EditorGUIUtility.singleLineHeight;

        if (property.managedReferenceValue == null)
        {
            return height;
        }

        var iterator       = property.Copy();
        var end            = iterator.GetEndProperty();
        bool enterChildren = true;

        while (iterator.NextVisible(enterChildren) && !SerializedProperty.EqualContents(iterator, end))
        {
            enterChildren = false;

            height += EditorGUI.GetPropertyHeight(iterator, true) + EditorGUIUtility.standardVerticalSpacing;
        }

        return height + 2f;
    }
}