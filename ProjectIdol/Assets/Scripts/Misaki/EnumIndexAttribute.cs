using System;
using System.Linq;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// Showing an array with Enum as keys in the property inspector. (Supported children)
/// �v���p�e�B�C���X�y�N�^�[�� Enum ���L�[�Ƃ��Ď��z���\������B
/// </summary>
public class EnumIndexAttribute : PropertyAttribute
{
    private string[] _names; // string�z��

    /// <summary>
    /// Constructor �R���X�g���N�^
    /// </summary>
    /// <param name="enumType"></param>
    public EnumIndexAttribute(Type enumType) => _names = Enum.GetNames(enumType);

#if UNITY_EDITOR
    /// <summary>
    /// Show inspector �C���X�y�N�^�[��\��
    /// </summary>
    [CustomPropertyDrawer(typeof(EnumIndexAttribute))]
    private class Drawer : PropertyDrawer
    {
        public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
        {
            var names = ((EnumIndexAttribute)attribute)._names;
            // propertyPath returns something like hogehoge.Array.data[0]
            // propertyPath �� hogehoge �̂悤�Ȃ��̂�Ԃ��܂��B�z��.�f�[�^[0]
            // so get the index from there.
            // ���������āA��������C���f�b�N�X���擾���܂��B
            var index = int.Parse(property.propertyPath.Split('[', ']').Where(c => !string.IsNullOrEmpty(c)).Last());
            if (index < names.Length) label.text = names[index];
            EditorGUI.PropertyField(rect, property, label, includeChildren: true);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, includeChildren: true);
        }
    }
#endif
}
